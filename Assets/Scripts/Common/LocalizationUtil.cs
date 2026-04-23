using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public static class LocalizationUtil {
  private const string LocaleKey = "locale";
  private const string DefaultLocaleCode = "ja";

  public static string Get(string key) {
    if (!CanUseLocalization()) return "";
    if (!EnsureSelectedLocale()) return "";
    try {
      return LocalizationSettings.StringDatabase.GetLocalizedString(LocalizationKeys.TableName, key);
    } catch {
      return "";
    }
  }

  public static string GetOrDefault(string key, string fallback) {
    string text = Get(key);
    return string.IsNullOrEmpty(text) ? fallback : text;
  }

  public static void ApplySavedLocale() {
    ApplyLocale(GetSavedLocaleCode());
  }

  public static void ToggleLocale() {
    string current = GetSavedLocaleCode();
    string next = current.StartsWith("ja") ? "en" : "ja";
    ApplyLocale(next);
  }

  public static string GetSavedLocaleCode() {
    string code = DataMgr.GetStr(LocaleKey);
    if (string.IsNullOrEmpty(code)) {
      code = DefaultLocaleCode;
    }
    return code;
  }

  public static void ApplyLocale(string code) {
    if (!CanUseLocalization()) return;
    try {
      Locale locale = LocalizationSettings.AvailableLocales.GetLocale(code);
      if (locale == null) return;
      LocalizationSettings.SelectedLocale = locale;
      DataMgr.SetStr(LocaleKey, locale.Identifier.Code);
    } catch {
      return;
    }
  }

  public static bool IsEnglish() {
    return GetSavedLocaleCode().StartsWith("en");
  }

  private static bool EnsureSelectedLocale() {
    if (!CanUseLocalization()) return false;
    try {
      var localesProvider = LocalizationSettings.AvailableLocales;
      if (localesProvider == null) return false;
      var locales = localesProvider.Locales;
      if (locales == null || locales.Count == 0) return false;
      Locale locale = localesProvider.GetLocale(GetSavedLocaleCode());
      if (locale == null) {
        locale = localesProvider.GetLocale("ja");
      }
      if (locale == null) {
        locale = locales[0];
      }
      if (locale == null) return false;
      LocalizationSettings.SelectedLocale = locale;
      DataMgr.SetStr(LocaleKey, locale.Identifier.Code);
      return true;
    } catch {
      return false;
    }
  }

  private static bool CanUseLocalization() {
    if (!LocalizationSettings.HasSettings) return false;
    try {
      var init = LocalizationSettings.InitializationOperation;
      if (!init.IsValid()) return false;
      if (!init.IsDone) return false;
      if (init.OperationException != null) return false;
      return true;
    } catch {
      return false;
    }
  }

  public static string GetPageKey(string pageKey, string field) {
    return $"page.{pageKey}.{field}";
  }

  public static string GetChoiceKey(string pageKey, string field) {
    return $"choice.{pageKey}.{field}";
  }
}
