using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

public static class LocalizationStringTableSeeder {
  private const string TableName = "GameStrings";
  private const string TableFolder = "Assets/Localization/Tables";

  [MenuItem("Tools/Localization/Seed Base Strings")]
  public static void SeedBaseStrings() {
    StringTableCollection collection = LocalizationEditorSettings.GetStringTableCollection(TableName);
    if (collection == null) {
      collection = LocalizationEditorSettings.CreateStringTableCollection(TableName, TableFolder);
    }

    Locale english = EnsureLocale("en", SystemLanguage.English);
    Locale japanese = EnsureLocale("ja", SystemLanguage.Japanese);
    if (english == null || japanese == null) {
      Debug.LogError("Locale の準備に失敗しました。");
      return;
    }

    StringTable enTable = GetOrCreateTable(collection, english);
    StringTable jaTable = GetOrCreateTable(collection, japanese);

    Dictionary<string, string> ja = BuildJapaneseEntries();
    Dictionary<string, string> en = BuildEnglishEntries();

    UpsertEntries(jaTable, ja);
    UpsertEntries(enTable, en);

    EditorUtility.SetDirty(collection.SharedData);
    EditorUtility.SetDirty(jaTable);
    EditorUtility.SetDirty(enTable);
    AssetDatabase.SaveAssets();
  }

  private static Locale EnsureLocale(string code, SystemLanguage fallbackLanguage) {
    Locale locale = LocalizationEditorSettings.GetLocale(code);
    if (locale != null) return locale;
    locale = LocalizationEditorSettings.GetLocale(fallbackLanguage);
    if (locale != null) return locale;
    locale = Locale.CreateLocale(fallbackLanguage);
    EnsureFolder("Assets/Localization");
    EnsureFolder("Assets/Localization/Locales");
    AssetDatabase.CreateAsset(locale, $"Assets/Localization/Locales/{fallbackLanguage}.asset");
    LocalizationEditorSettings.AddLocale(locale);
    return locale;
  }

  private static StringTable GetOrCreateTable(StringTableCollection collection, Locale locale) {
    StringTable table = collection.GetTable(locale.Identifier) as StringTable;
    if (table == null) {
      table = collection.AddNewTable(locale.Identifier) as StringTable;
    }
    return table;
  }

  private static void UpsertEntries(StringTable table, Dictionary<string, string> entries) {
    foreach (KeyValuePair<string, string> entry in entries) {
      StringTableEntry existing = table.GetEntry(entry.Key);
      if (existing == null) {
        table.AddEntry(entry.Key, entry.Value);
      } else {
        existing.Value = entry.Value;
      }
    }
  }

  private static void EnsureFolder(string path) {
    if (AssetDatabase.IsValidFolder(path)) return;
    string parent = System.IO.Path.GetDirectoryName(path);
    string name = System.IO.Path.GetFileName(path);
    if (!string.IsNullOrEmpty(parent) && !AssetDatabase.IsValidFolder(parent)) {
      EnsureFolder(parent);
    }
    AssetDatabase.CreateFolder(parent, name);
  }

  private static Dictionary<string, string> BuildJapaneseEntries() {
    return new Dictionary<string, string> {
      { LocalizationKeys.TitleStartNew, "ゲームを始める" },
      { LocalizationKeys.TitleStartContinue, "つづきから始める" },
      { LocalizationKeys.TitleGallery, "図鑑" },
      { LocalizationKeys.TitleSettings, "設定" },
      { LocalizationKeys.TitleQuit, "ゲームを終了する" },
      { LocalizationKeys.TitlePv, "PV" },

      { LocalizationKeys.SettingRetire, "今回の冒険は諦める" },
      { LocalizationKeys.SettingFastScene, "画面暗転を高速化" },
      { LocalizationKeys.SettingFastDay, "日数経過を高速化" },
      { LocalizationKeys.SettingTitle, "タイトルに戻る" },
      { LocalizationKeys.SettingReset, "すべてのデータを初期化する" },
      { LocalizationKeys.SettingResetConfirm, "本当にいいですか？" },
      { LocalizationKeys.SettingResetYes, "はい" },
      { LocalizationKeys.SettingResetNo, "いいえ" },
      { LocalizationKeys.SettingVolume, "音量" },
      { LocalizationKeys.SettingLanguage, "言語" },
      { LocalizationKeys.SettingDebugMode, "デバッグモード" },
      { LocalizationKeys.SettingBack, "戻る" },
      { LocalizationKeys.LanguageJapanese, "日本語" },
      { LocalizationKeys.LanguageEnglish, "英語" },

      { LocalizationKeys.CharCreateTitleSuffix, "勇者かっぱ" },
      { LocalizationKeys.CharCreateStatLabels, "最大HP\n攻撃力\nすばやさ\n魅力\nかしこさ\n所持金" },
      { LocalizationKeys.CharCreateDecide, "これに決めた！" },
      { LocalizationKeys.CharCreateRoll, "ダイスロール" },
      { LocalizationKeys.CharCreateNoEffect, "効果なし" },
      { LocalizationKeys.CharCreateStatMaxHp, "最大HP" },
      { LocalizationKeys.CharCreateStatAtk, "攻撃力" },
      { LocalizationKeys.CharCreateStatAgi, "すばやさ" },
      { LocalizationKeys.CharCreateStatCharm, "魅力" },
      { LocalizationKeys.CharCreateStatWis, "かしこさ" },
      { LocalizationKeys.CharCreateStatGold, "所持金" },

      { LocalizationKeys.CharCreateTitleXStylish, "おしゃれな" },
      { LocalizationKeys.CharCreateTitleXMacho, "マッチョな" },
      { LocalizationKeys.CharCreateTitleXSwift, "疾風の" },
      { LocalizationKeys.CharCreateTitleXWealthy, "金持ちの" },
      { LocalizationKeys.CharCreateTitleXQuiet, "むっつり" },
      { LocalizationKeys.CharCreateTitleXLimited, "余命半年の" },

      { LocalizationKeys.CharCreateTitleYClumsy, "ドジっ子" },
      { LocalizationKeys.CharCreateTitleYHandsome, "イケメン" },
      { LocalizationKeys.CharCreateTitleYElite, "エリート" },
      { LocalizationKeys.CharCreateTitleYRookie, "新米" },
      { LocalizationKeys.CharCreateTitleYGenius, "天才" },

      { LocalizationKeys.LockedChoiceNoticeText, "ちょっ、\nそのボタンを押す条件を満たしてないっす！" },
      { LocalizationKeys.LockedChoiceNoticeSpeaker, "ウサギ" },
      { LocalizationKeys.LockedChoiceNoticeKappaText, "くっ、ボタンを押す条件を満たしていない！" },
      { LocalizationKeys.LockedChoiceNoticeKappaSpeaker, "カッパ" },
      { LocalizationKeys.LockedChoiceNoticeShioriText, "そのボタンは押す条件を\n満たしていないんじゃーないかな" },
      { LocalizationKeys.LockedChoiceNoticeShioriSpeaker, "シオリーナ" },
    };
  }

  private static Dictionary<string, string> BuildEnglishEntries() {
    return new Dictionary<string, string> {
      { LocalizationKeys.TitleStartNew, "Start Game" },
      { LocalizationKeys.TitleStartContinue, "Continue" },
      { LocalizationKeys.TitleGallery, "Gallery" },
      { LocalizationKeys.TitleSettings, "Settings" },
      { LocalizationKeys.TitleQuit, "Quit Game" },
      { LocalizationKeys.TitlePv, "PV" },

      { LocalizationKeys.SettingRetire, "Give up this adventure" },
      { LocalizationKeys.SettingFastScene, "Faster screen fade" },
      { LocalizationKeys.SettingFastDay, "Faster day progression" },
      { LocalizationKeys.SettingTitle, "Return to Title" },
      { LocalizationKeys.SettingReset, "Reset all data" },
      { LocalizationKeys.SettingResetConfirm, "Are you sure?" },
      { LocalizationKeys.SettingResetYes, "Yes" },
      { LocalizationKeys.SettingResetNo, "No" },
      { LocalizationKeys.SettingVolume, "Volume" },
      { LocalizationKeys.SettingLanguage, "Language" },
      { LocalizationKeys.SettingDebugMode, "Debug Mode" },
      { LocalizationKeys.SettingBack, "Back" },
      { LocalizationKeys.LanguageJapanese, "Japanese" },
      { LocalizationKeys.LanguageEnglish, "English" },

      { LocalizationKeys.CharCreateTitleSuffix, "Kappa Hero" },
      { LocalizationKeys.CharCreateStatLabels, "Max HP\nAttack\nAgility\nCharm\nWisdom\nGold" },
      { LocalizationKeys.CharCreateDecide, "Choose this!" },
      { LocalizationKeys.CharCreateRoll, "Roll Dice" },
      { LocalizationKeys.CharCreateNoEffect, "No effect" },
      { LocalizationKeys.CharCreateStatMaxHp, "Max HP" },
      { LocalizationKeys.CharCreateStatAtk, "Attack" },
      { LocalizationKeys.CharCreateStatAgi, "Agility" },
      { LocalizationKeys.CharCreateStatCharm, "Charm" },
      { LocalizationKeys.CharCreateStatWis, "Wisdom" },
      { LocalizationKeys.CharCreateStatGold, "Gold" },

      { LocalizationKeys.CharCreateTitleXStylish, "Stylish" },
      { LocalizationKeys.CharCreateTitleXMacho, "Muscular" },
      { LocalizationKeys.CharCreateTitleXSwift, "Swift" },
      { LocalizationKeys.CharCreateTitleXWealthy, "Wealthy" },
      { LocalizationKeys.CharCreateTitleXQuiet, "Reserved" },
      { LocalizationKeys.CharCreateTitleXLimited, "Six-months-to-live" },

      { LocalizationKeys.CharCreateTitleYClumsy, "Clumsy" },
      { LocalizationKeys.CharCreateTitleYHandsome, "Handsome" },
      { LocalizationKeys.CharCreateTitleYElite, "Elite" },
      { LocalizationKeys.CharCreateTitleYRookie, "Rookie" },
      { LocalizationKeys.CharCreateTitleYGenius, "Genius" },

      { LocalizationKeys.LockedChoiceNoticeText, "Hey,\nYou do not meet the requirements!" },
      { LocalizationKeys.LockedChoiceNoticeSpeaker, "Usagi" },
      { LocalizationKeys.LockedChoiceNoticeKappaText, "Tch, you do not meet the requirements to press that button!" },
      { LocalizationKeys.LockedChoiceNoticeKappaSpeaker, "Kappa" },
      { LocalizationKeys.LockedChoiceNoticeShioriText, "I do not think you meet the\nrequirements to press that button." },
      { LocalizationKeys.LockedChoiceNoticeShioriSpeaker, "Shiorina" },
    };
  }
}
