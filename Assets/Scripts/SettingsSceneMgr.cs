using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsSceneMgr : MonoBehaviour {
  [SerializeField] private TextMeshProUGUI title_text;
  [SerializeField] private TextMeshProUGUI debug_label;
  [SerializeField] private TextMeshProUGUI language_label;
  [SerializeField] private Toggle debug_toggle;
  [SerializeField] private Toggle language_ja_toggle;
  [SerializeField] private Toggle language_en_toggle;
  [SerializeField] private TextMeshProUGUI language_ja_label;
  [SerializeField] private TextMeshProUGUI language_en_label;
  [SerializeField] private Button reset_button;
  [SerializeField] private TextMeshProUGUI reset_label;
  [SerializeField] private GameObject reset_confirm_root;
  [SerializeField] private TextMeshProUGUI reset_confirm_text;
  [SerializeField] private Button reset_yes_button;
  [SerializeField] private Button reset_no_button;
  [SerializeField] private TextMeshProUGUI reset_yes_label;
  [SerializeField] private TextMeshProUGUI reset_no_label;
  [SerializeField] private Button back_button;
  [SerializeField] private TextMeshProUGUI back_label;

  private void Start() {
    LocalizationUtil.ApplySavedLocale();
    if (reset_confirm_root != null) {
      reset_confirm_root.SetActive(false);
    }
    ApplyLocalizedTexts();
    SetupToggleStates();
    BindEvents();
    EnsureInitialSelection();
  }

  private void Update() {
    HandleGamepadSubmit();
  }

  private void ApplyLocalizedTexts() {
    if (title_text != null) {
      title_text.text = LocalizationUtil.GetOrDefault(LocalizationKeys.TitleSettings, "設定");
    }
    if (debug_label != null) {
      debug_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingDebugMode, "デバッグモード");
    }
    if (language_label != null) {
      language_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingLanguage, "言語");
    }
    if (language_ja_label != null) {
      language_ja_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.LanguageJapanese, "日本語");
    }
    if (language_en_label != null) {
      language_en_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.LanguageEnglish, "English");
    }
    if (reset_label != null) {
      reset_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingReset, "すべてのデータを初期化する");
    }
    if (reset_confirm_text != null) {
      reset_confirm_text.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingResetConfirm, "本当にいいですか？");
    }
    if (reset_yes_label != null) {
      reset_yes_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingResetYes, "はい");
    }
    if (reset_no_label != null) {
      reset_no_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingResetNo, "いいえ");
    }
    if (back_label != null) {
      back_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingBack, "戻る");
    }
  }

  private void SetupToggleStates() {
    if (debug_toggle != null) {
      debug_toggle.isOn = DataMgr.GetBool("debug_mode");
    }
    bool isEnglish = LocalizationUtil.IsEnglish();
    if (language_ja_toggle != null) {
      language_ja_toggle.isOn = !isEnglish;
    }
    if (language_en_toggle != null) {
      language_en_toggle.isOn = isEnglish;
    }
  }

  private void BindEvents() {
    if (debug_toggle != null) {
      debug_toggle.onValueChanged.RemoveAllListeners();
      debug_toggle.onValueChanged.AddListener(OnDebugToggleChanged);
    }
    if (language_ja_toggle != null) {
      language_ja_toggle.onValueChanged.RemoveAllListeners();
      language_ja_toggle.onValueChanged.AddListener(OnJapaneseToggleChanged);
    }
    if (language_en_toggle != null) {
      language_en_toggle.onValueChanged.RemoveAllListeners();
      language_en_toggle.onValueChanged.AddListener(OnEnglishToggleChanged);
    }
    if (reset_button != null) {
      reset_button.onClick.RemoveAllListeners();
      reset_button.onClick.AddListener(ShowResetConfirm);
    }
    if (reset_yes_button != null) {
      reset_yes_button.onClick.RemoveAllListeners();
      reset_yes_button.onClick.AddListener(ConfirmReset);
    }
    if (reset_no_button != null) {
      reset_no_button.onClick.RemoveAllListeners();
      reset_no_button.onClick.AddListener(HideResetConfirm);
    }
    if (back_button != null) {
      back_button.onClick.RemoveAllListeners();
      back_button.onClick.AddListener(GoBack);
    }
  }

  private void OnDebugToggleChanged(bool isOn) {
    DataMgr.SetBool("debug_mode", isOn);
  }

  private void OnJapaneseToggleChanged(bool isOn) {
    if (!isOn) return;
    LocalizationUtil.ApplyLocale("ja");
    ApplyLocalizedTexts();
  }

  private void OnEnglishToggleChanged(bool isOn) {
    if (!isOn) return;
    LocalizationUtil.ApplyLocale("en");
    ApplyLocalizedTexts();
  }

  private void EnsureInitialSelection() {
    if (EventSystem.current == null) return;
    if (EventSystem.current.currentSelectedGameObject != null) return;
    if (debug_toggle != null) {
      EventSystem.current.SetSelectedGameObject(debug_toggle.gameObject);
      return;
    }
    if (reset_button != null) {
      EventSystem.current.SetSelectedGameObject(reset_button.gameObject);
      return;
    }
    if (back_button != null) {
      EventSystem.current.SetSelectedGameObject(back_button.gameObject);
    }
  }

  private void HandleGamepadSubmit() {
    if (Gamepad.current == null) return;
    if (!Gamepad.current.startButton.wasPressedThisFrame) return;
    SubmitCurrentSelection();
  }

  private void SubmitCurrentSelection() {
    if (EventSystem.current == null) return;
    if (EventSystem.current.currentSelectedGameObject == null) {
      EnsureInitialSelection();
    }
    if (EventSystem.current.currentSelectedGameObject == null) return;
    BaseEventData eventData = new BaseEventData(EventSystem.current);
    ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, eventData, ExecuteEvents.submitHandler);
  }

  private void ShowResetConfirm() {
    if (reset_confirm_root == null) return;
    reset_confirm_root.SetActive(true);
  }

  private void HideResetConfirm() {
    if (reset_confirm_root == null) return;
    reset_confirm_root.SetActive(false);
  }

  private void ConfirmReset() {
    PlayerPrefs.DeleteAll();
    CommonUtil.changeScene("TitleScene");
  }

  private void GoBack() {
    string returnScene = DataMgr.GetStr("settings_return_scene");
    if (string.IsNullOrEmpty(returnScene)) {
      returnScene = "TitleScene";
    }
    CommonUtil.changeScene(returnScene);
  }
}
