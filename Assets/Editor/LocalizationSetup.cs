using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public static class LocalizationSetup {
  private const string RootFolder = "Assets/Localization";
  private const string LocaleFolder = RootFolder + "/Locales";
  private const string TableFolder = RootFolder + "/Tables";
  private const string SettingsPath = RootFolder + "/LocalizationSettings.asset";
  private const string TableCollectionName = "GameStrings";

  [MenuItem("Tools/Localization/Setup Base Tables")]
  public static void SetupBaseTables() {
    EnsureFolder(RootFolder);
    EnsureFolder(LocaleFolder);
    EnsureFolder(TableFolder);

    LocalizationSettings settings = EnsureLocalizationSettings();
    if (settings == null) {
      Debug.LogError("Localization Settings の作成に失敗しました。");
      return;
    }

    Locale english = EnsureLocale(SystemLanguage.English, LocaleFolder + "/English.asset");
    Locale japanese = EnsureLocale(SystemLanguage.Japanese, LocaleFolder + "/Japanese.asset");
    if (english == null || japanese == null) {
      Debug.LogError("Locale の作成に失敗しました。");
      return;
    }

    EnsureStringTableCollection(english, japanese);

    AssetDatabase.SaveAssets();
    AssetDatabase.Refresh();
  }

  private static LocalizationSettings EnsureLocalizationSettings() {
    LocalizationSettings settings = AssetDatabase.LoadAssetAtPath<LocalizationSettings>(SettingsPath);
    if (settings == null) {
      settings = ScriptableObject.CreateInstance<LocalizationSettings>();
      AssetDatabase.CreateAsset(settings, SettingsPath);
    }

    LocalizationEditorSettings.ActiveLocalizationSettings = settings;
    return settings;
  }

  private static Locale EnsureLocale(SystemLanguage language, string assetPath) {
    Locale locale = AssetDatabase.LoadAssetAtPath<Locale>(assetPath);
    if (locale == null) {
      locale = Locale.CreateLocale(language);
      AssetDatabase.CreateAsset(locale, assetPath);
    }

    if (LocalizationEditorSettings.GetLocale(locale.Identifier) == null) {
      LocalizationEditorSettings.AddLocale(locale);
    }
    return locale;
  }

  private static void EnsureStringTableCollection(Locale english, Locale japanese) {
    StringTableCollection collection = LocalizationEditorSettings.GetStringTableCollection(TableCollectionName);
    if (collection == null) {
      collection = LocalizationEditorSettings.CreateStringTableCollection(
        TableCollectionName,
        TableFolder,
        new List<Locale> { english, japanese }
      );
    } else {
      EnsureTable(collection, english);
      EnsureTable(collection, japanese);
    }

    foreach (StringTable table in collection.StringTables) {
      LocalizationEditorSettings.SetPreloadTableFlag(table, true);
      EditorUtility.SetDirty(table);
    }
    EditorUtility.SetDirty(collection);
    EditorUtility.SetDirty(collection.SharedData);
  }

  private static void EnsureTable(StringTableCollection collection, Locale locale) {
    if (collection.GetTable(locale.Identifier) == null) {
      collection.AddNewTable(locale.Identifier);
    }
  }

  private static void EnsureFolder(string path) {
    if (AssetDatabase.IsValidFolder(path)) return;
    string parent = Path.GetDirectoryName(path);
    string name = Path.GetFileName(path);
    if (!string.IsNullOrEmpty(parent) && !AssetDatabase.IsValidFolder(parent)) {
      EnsureFolder(parent);
    }
    AssetDatabase.CreateFolder(parent, name);
  }
}
