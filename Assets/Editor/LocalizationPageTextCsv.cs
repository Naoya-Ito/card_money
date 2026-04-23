using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

public static class LocalizationPageTextCsv {
  private const string PagesRoot = "Assets/Scripts/Page/pages";
  private const string ExportPath = "Assets/Localization/Exports/page_strings.csv";
  private const string TableName = "GameStrings";

  [MenuItem("Tools/Localization/Export Page Strings")]
  public static void ExportPageStrings() {
    Dictionary<string, string> extracted = ExtractPageStrings();
    StringTableCollection collection = LocalizationEditorSettings.GetStringTableCollection(TableName);
    StringTable jaTable = collection != null ? collection.GetTable("ja") as StringTable : null;
    StringTable enTable = collection != null ? collection.GetTable("en") as StringTable : null;

    EnsureFolder(Path.GetDirectoryName(ExportPath));
    using (StreamWriter writer = new StreamWriter(ExportPath, false)) {
      writer.WriteLine("key,ja,en");
      foreach (string key in GetSortedKeys(extracted)) {
        string ja = GetTableValueOrDefault(jaTable, key, extracted[key]);
        string en = GetTableValueOrDefault(enTable, key, "");
        writer.WriteLine($"{CsvEscape(key)},{CsvEscape(ja)},{CsvEscape(en)}");
      }
    }

    AssetDatabase.Refresh();
  }

  [MenuItem("Tools/Localization/Import Page Strings")]
  public static void ImportPageStrings() {
    if (!File.Exists(ExportPath)) {
      Debug.LogError("CSV が見つかりません。");
      return;
    }

    StringTableCollection collection = LocalizationEditorSettings.GetStringTableCollection(TableName);
    if (collection == null) {
      Debug.LogError("String Table Collection が見つかりません。");
      return;
    }

    Locale ja = LocalizationEditorSettings.GetLocale("ja");
    Locale en = LocalizationEditorSettings.GetLocale("en");
    if (ja == null || en == null) {
      Debug.LogError("Locale が見つかりません。");
      return;
    }

    StringTable jaTable = GetOrCreateTable(collection, ja);
    StringTable enTable = GetOrCreateTable(collection, en);

    foreach (CsvRow row in ReadCsv(ExportPath)) {
      if (string.IsNullOrEmpty(row.key)) continue;
      if (!string.IsNullOrEmpty(row.ja)) {
        UpsertEntry(jaTable, row.key, row.ja);
      }
      if (!string.IsNullOrEmpty(row.en)) {
        UpsertEntry(enTable, row.key, row.en);
      }
    }

    EditorUtility.SetDirty(collection.SharedData);
    EditorUtility.SetDirty(jaTable);
    EditorUtility.SetDirty(enTable);
    AssetDatabase.SaveAssets();
  }

  private static Dictionary<string, string> ExtractPageStrings() {
    Dictionary<string, string> result = new Dictionary<string, string>();
    string[] files = Directory.GetFiles(PagesRoot, "*.cs", SearchOption.AllDirectories);
    foreach (string file in files) {
      string content = File.ReadAllText(file);
      string pageKey = GetPageKey(content);
      if (string.IsNullOrEmpty(pageKey)) continue;

      AddEntry(result, LocalizationUtil.GetPageKey(pageKey, "main_text"), ExtractFirstString(content, "model.main_text"));
      AddEntry(result, LocalizationUtil.GetPageKey(pageKey, "speaker"), ExtractFirstString(content, "model.speaker"));
      AddEntry(result, LocalizationUtil.GetChoiceKey(pageKey, "title"), ExtractFirstString(content, "ChoiceModel.instance.setTitle"));

      List<ChoiceCall> choices = ExtractAddButtonCalls(content);
      for (int i = 0; i < choices.Count; i++) {
        int index = i + 1;
        ChoiceCall call = choices[i];
        AddEntry(result, LocalizationUtil.GetChoiceKey(pageKey, $"option{index}.text"), call.text);
        AddEntry(result, LocalizationUtil.GetChoiceKey(pageKey, $"option{index}.explain"), call.explain);
      }

      foreach (ChoiceDisabledCall call in ExtractDisabledExplainCalls(content)) {
        string key = LocalizationUtil.GetChoiceKey(pageKey, $"option{call.index}.disabled_explain");
        AddEntry(result, key, call.text);
      }
    }
    return result;
  }

  private static string GetPageKey(string content) {
    Match match = Regex.Match(content, "PAGE_KEY\\s*=\\s*\"(?<key>[^\"]+)\"");
    return match.Success ? match.Groups["key"].Value : "";
  }

  private static string ExtractFirstString(string content, string prefix) {
    Match match = Regex.Match(content, Regex.Escape(prefix) + "\\s*=\\s*\"(?<value>(?:\\\\.|[^\"])*)\"");
    if (!match.Success) return "";
    return Regex.Unescape(match.Groups["value"].Value);
  }

  private static List<ChoiceCall> ExtractAddButtonCalls(string content) {
    List<ChoiceCall> calls = new List<ChoiceCall>();
    foreach (Match match in Regex.Matches(content, "ChoiceModel\\.instance\\.AddButton\\((?<args>[^;]+)\\);")) {
      string args = match.Groups["args"].Value;
      List<string> strings = ExtractStringArgs(args);
      if (strings.Count < 2) continue;
      string text = strings.Count >= 2 ? strings[1] : "";
      string explain = strings.Count >= 3 ? strings[2] : "";
      calls.Add(new ChoiceCall { text = text, explain = explain });
    }
    return calls;
  }

  private static List<ChoiceDisabledCall> ExtractDisabledExplainCalls(string content) {
    List<ChoiceDisabledCall> calls = new List<ChoiceDisabledCall>();
    foreach (Match match in Regex.Matches(content, "ChoiceModel\\.instance\\.SetButtonEnabled\\((?<args>[^;]+)\\);")) {
      string args = match.Groups["args"].Value;
      Match indexMatch = Regex.Match(args, "^(?<index>\\d+)");
      if (!indexMatch.Success) continue;
      int index = int.Parse(indexMatch.Groups["index"].Value);
      List<string> strings = ExtractStringArgs(args);
      if (strings.Count == 0) continue;
      calls.Add(new ChoiceDisabledCall { index = index, text = strings[strings.Count - 1] });
    }
    return calls;
  }

  private static List<string> ExtractStringArgs(string args) {
    List<string> values = new List<string>();
    foreach (Match match in Regex.Matches(args, "\"(?<value>(?:\\\\.|[^\"])*)\"")) {
      values.Add(Regex.Unescape(match.Groups["value"].Value));
    }
    return values;
  }

  private static void AddEntry(Dictionary<string, string> dict, string key, string value) {
    if (string.IsNullOrEmpty(key)) return;
    if (string.IsNullOrEmpty(value)) return;
    if (!dict.ContainsKey(key)) {
      dict.Add(key, value);
    }
  }

  private static IEnumerable<string> GetSortedKeys(Dictionary<string, string> dict) {
    List<string> keys = new List<string>(dict.Keys);
    keys.Sort();
    return keys;
  }

  private static string CsvEscape(string value) {
    if (value == null) return "\"\"";
    string escaped = value.Replace("\"", "\"\"");
    return $"\"{escaped}\"";
  }

  private static IEnumerable<CsvRow> ReadCsv(string path) {
    List<CsvRow> rows = new List<CsvRow>();
    string[] lines = File.ReadAllLines(path);
    for (int i = 1; i < lines.Length; i++) {
      string line = lines[i];
      if (string.IsNullOrEmpty(line)) continue;
      List<string> cells = ParseCsvLine(line);
      if (cells.Count < 3) continue;
      rows.Add(new CsvRow { key = cells[0], ja = cells[1], en = cells[2] });
    }
    return rows;
  }

  private static List<string> ParseCsvLine(string line) {
    List<string> cells = new List<string>();
    int index = 0;
    while (index < line.Length) {
      if (line[index] == '"') {
        index++;
        int start = index;
        while (index < line.Length) {
          if (line[index] == '"' && index + 1 < line.Length && line[index + 1] == '"') {
            index += 2;
            continue;
          }
          if (line[index] == '"') break;
          index++;
        }
        string cell = line.Substring(start, index - start).Replace("\"\"", "\"");
        cells.Add(cell);
        index = Mathf.Min(index + 1, line.Length);
        if (index < line.Length && line[index] == ',') index++;
      } else {
        int start = index;
        while (index < line.Length && line[index] != ',') index++;
        cells.Add(line.Substring(start, index - start));
        if (index < line.Length && line[index] == ',') index++;
      }
    }
    return cells;
  }

  private static StringTable GetOrCreateTable(StringTableCollection collection, Locale locale) {
    StringTable table = collection.GetTable(locale.Identifier) as StringTable;
    if (table == null) {
      table = collection.AddNewTable(locale.Identifier) as StringTable;
    }
    return table;
  }

  private static void UpsertEntry(StringTable table, string key, string value) {
    StringTableEntry entry = table.GetEntry(key);
    if (entry == null) {
      table.AddEntry(key, value);
    } else {
      entry.Value = value;
    }
  }

  private static string GetTableValueOrDefault(StringTable table, string key, string fallback) {
    if (table == null) return fallback;
    StringTableEntry entry = table.GetEntry(key);
    if (entry == null) return fallback;
    return entry.LocalizedValue;
  }

  private static void EnsureFolder(string path) {
    if (string.IsNullOrEmpty(path)) return;
    if (AssetDatabase.IsValidFolder(path)) return;
    string parent = Path.GetDirectoryName(path);
    string name = Path.GetFileName(path);
    if (!string.IsNullOrEmpty(parent) && !AssetDatabase.IsValidFolder(parent)) {
      EnsureFolder(parent);
    }
    AssetDatabase.CreateFolder(parent, name);
  }

  private struct ChoiceCall {
    public string text;
    public string explain;
  }

  private struct ChoiceDisabledCall {
    public int index;
    public string text;
  }

  private struct CsvRow {
    public string key;
    public string ja;
    public string en;
  }
}
