using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public static class LocalizationSettingMenuInjector {
  private const string PrefabPath = "Assets/Prefabs/SettingMenu.prefab";

  [MenuItem("Tools/Localization/Setup Setting Menu Localization")]
  public static void SetupSettingMenuLocalization() {
    GameObject root = PrefabUtility.LoadPrefabContents(PrefabPath);
    if (root == null) {
      Debug.LogError("SettingMenu.prefab を読み込めませんでした。");
      return;
    }

    try {
      Setting setting = root.GetComponent<Setting>();
      if (setting == null) {
        Debug.LogError("Setting コンポーネントが見つかりません。");
        return;
      }

      Button languageButton = EnsureLanguageButton(root.transform);
      Toggle debugToggle = EnsureDebugToggle(root.transform);
      AssignLocalizedTextRefs(root.transform, setting, languageButton, debugToggle);

      PrefabUtility.SaveAsPrefabAsset(root, PrefabPath);
    } finally {
      PrefabUtility.UnloadPrefabContents(root);
    }
  }

  private static Button EnsureLanguageButton(Transform root) {
    Transform existing = FindByName(root, "LanguageButton");
    if (existing != null) {
      return existing.GetComponent<Button>();
    }

    Transform titleButton = FindByName(root, "TitleButton");
    if (titleButton == null) {
      Debug.LogError("TitleButton が見つかりません。");
      return null;
    }

    GameObject clone = Object.Instantiate(titleButton.gameObject, titleButton.parent);
    clone.name = "LanguageButton";
    RectTransform rect = clone.GetComponent<RectTransform>();
    RectTransform titleRect = titleButton.GetComponent<RectTransform>();
    if (rect != null && titleRect != null) {
      rect.anchoredPosition = new Vector2(titleRect.anchoredPosition.x, -152f);
    }

    EventTrigger trigger = clone.GetComponent<EventTrigger>();
    if (trigger != null) {
      Object.DestroyImmediate(trigger, true);
    }

    return clone.GetComponent<Button>();
  }

  private static void AssignLocalizedTextRefs(
    Transform root,
    Setting setting,
    Button languageButton,
    Toggle debugToggle
  ) {
    SerializedObject serializedSetting = new SerializedObject(setting);
    SetObjectField(serializedSetting, "giveup_label", GetLabelFromButton(root, "RetireButton"));
    SetObjectField(serializedSetting, "title_label", GetLabelFromButton(root, "TitleButton"));
    SetObjectField(serializedSetting, "delete_label", GetLabelFromButton(root, "ResetButton"));
    SetObjectField(serializedSetting, "change_scene_label", GetLabelFromToggle(root, "changeSceneToggle"));
    SetObjectField(serializedSetting, "day_label", GetLabelFromToggle(root, "dayToggle2"));
    SetObjectField(serializedSetting, "reset_label", FindByName(root, "resetText")?.GetComponent<TextMeshProUGUI>());
    SetObjectField(serializedSetting, "volume_label", GetLabelFromRoot(root, "bgm_slider"));
    SetObjectField(serializedSetting, "language_button", languageButton);
    SetObjectField(serializedSetting, "debug_mode_toggle", debugToggle);
    SetObjectField(serializedSetting, "debug_label", GetLabelFromToggle(root, "debugModeToggle"));
    serializedSetting.ApplyModifiedPropertiesWithoutUndo();
  }

  private static TextMeshProUGUI GetLabelFromButton(Transform root, string name) {
    Transform button = FindByName(root, name);
    if (button == null) return null;
    return button.GetComponentInChildren<TextMeshProUGUI>(true);
  }

  private static TextMeshProUGUI GetLabelFromToggle(Transform root, string name) {
    Transform toggle = FindByName(root, name);
    if (toggle == null) return null;
    return toggle.GetComponentInChildren<TextMeshProUGUI>(true);
  }

  private static TextMeshProUGUI GetLabelFromRoot(Transform root, string name) {
    Transform target = FindByName(root, name);
    if (target == null) return null;
    return target.GetComponentInChildren<TextMeshProUGUI>(true);
  }

  private static void SetObjectField(SerializedObject serializedObject, string name, Object value) {
    SerializedProperty property = serializedObject.FindProperty(name);
    if (property == null) return;
    property.objectReferenceValue = value;
  }

  private static Transform FindByName(Transform root, string name) {
    foreach (Transform child in root.GetComponentsInChildren<Transform>(true)) {
      if (child.name == name) return child;
    }
    return null;
  }

  private static Toggle EnsureDebugToggle(Transform root) {
    Transform existing = FindByName(root, "debugModeToggle");
    if (existing != null) {
      return existing.GetComponent<Toggle>();
    }

    Transform source = FindByName(root, "dayToggle2");
    if (source == null) {
      Debug.LogError("dayToggle2 が見つかりません。");
      return null;
    }

    GameObject clone = Object.Instantiate(source.gameObject, source.parent);
    clone.name = "debugModeToggle";
    RectTransform rect = clone.GetComponent<RectTransform>();
    if (rect != null) {
      rect.anchoredPosition = new Vector2(-100f, 20f);
    }
    clone.transform.SetSiblingIndex(source.GetSiblingIndex() + 1);

    TextMeshProUGUI label = clone.GetComponentInChildren<TextMeshProUGUI>(true);
    if (label != null) {
      label.text = "デバッグモード";
    }

    return clone.GetComponent<Toggle>();
  }
}
