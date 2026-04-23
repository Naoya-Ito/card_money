using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Setting : MonoBehaviour {

  [SerializeField] Slider bgmSlider;
  [SerializeField] public Toggle changeSceneTimeToggle;
  [SerializeField] public Toggle changeDayToggle;
  [SerializeField] private Toggle debug_mode_toggle;

  [SerializeField] public Button giveup_button;
  [SerializeField] public Button title_button;
  [SerializeField] public Button delete_button;
  [SerializeField] private Button language_button;
  [SerializeField] private TextMeshProUGUI giveup_label;
  [SerializeField] private TextMeshProUGUI title_label;
  [SerializeField] private TextMeshProUGUI delete_label;
  [SerializeField] private TextMeshProUGUI change_scene_label;
  [SerializeField] private TextMeshProUGUI day_label;
  [SerializeField] private TextMeshProUGUI reset_label;
  [SerializeField] private TextMeshProUGUI volume_label;
  [SerializeField] private TextMeshProUGUI debug_label;

  [System.NonSerialized] public static Setting instance = null;
  private bool localeBound = false;

  private void Awake(){
    if(instance == null) {
      instance = this;
    } else {
      Destroy(this.gameObject);
    }
  }

  private void OnEnable() {
    if (!LocalizationSettings.HasSettings) return;
    if (!localeBound) {
      LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
      localeBound = true;
    }
  }

  private void OnDisable() {
    if (!LocalizationSettings.HasSettings) return;
    if (localeBound) {
      LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
      localeBound = false;
    }
  }

  void Start() {
    LocalizationUtil.ApplySavedLocale();
    if(PlayerPrefs.HasKey("bgm_volume")) {
      float bgm_volume = DataMgr.GetFloat("bgm_volume");
      bgmSlider.value = bgm_volume;
    } else {
      bgmSlider.value = 1.0f;
      DataMgr.SetFloat("bgm_volume", 1.0f);
    }

    changeSceneTimeToggle.isOn = DataMgr.GetBool("change_scene_time_speed_up");
    changeDayToggle.isOn = DataMgr.GetBool("change_day_speed_up");
    if (debug_mode_toggle != null) {
      debug_mode_toggle.isOn = DataMgr.GetBool("debug_mode");
      debug_mode_toggle.onValueChanged.RemoveAllListeners();
      debug_mode_toggle.onValueChanged.AddListener(_ => OnDebugModeToggleChanged());
    }
    if (language_button != null) {
      language_button.onClick.RemoveAllListeners();
      language_button.onClick.AddListener(ToggleLanguage);
    }
    ApplyLocalizedTexts();

    // Time.timeScale = 0.0f;
  }

  void OnDestroy() {
    // Time.timeScale = 1.0f;

    GearButton.instance.isPressed = false;
	}


  private bool isEndButtonPushed = false;
  public void pushedEndButton(){
    if(isEndButtonPushed) {
      return;
    }
    isEndButtonPushed = true;
    CommonUtil.changePushedButtonImage(delete_button);

    finishGame();
  }

  public void cancel(){
    GameObject panel = GameObject.Find("SettingMenu");
    if(panel != null) {
      Destroy(panel);
    } else {
      Debug.Log("SettingMenu is not found.");
    }
  }

  private void finishGame(){
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }

  public void retireGame(){
    DataMgr.SetStr("dead_reason", "retire");
    CommonUtil.changeScene("GameOverScene");
    CommonUtil.changePushedButtonImage(giveup_button);
  }


  private int pushed = 0;
  public void resetAllData(){
    if(pushed == 0) {
      if (reset_label != null) {
        reset_label.text = LocalizationUtil.GetOrDefault(
          LocalizationKeys.SettingResetConfirm,
          "本当にいいですか？"
        );
      }
      pushed += 1;
    } else {
      // Time.timeScale = 1.0f;
      PlayerPrefs.DeleteAll();
      CommonUtil.changeScene("TitleScene");
    }
  }

  public void changeBGMSlider(){
    DataMgr.SetFloat("bgm_volume", bgmSlider.value);

    AudioListener.volume = bgmSlider.value;
//    BGMMgr.instance.changeVolume();
  }

  public void goTitle(){
    CommonUtil.changePushedButtonImage(title_button);
    CommonUtil.changeScene("TitleScene");
  }

  public void OnChangeSceneTimeToggleChanged(){
    DataMgr.SetBool("change_scene_time_speed_up", changeSceneTimeToggle.isOn);
  }

  public void OnChangeDayToggleChanged(){
    DataMgr.SetBool("change_day_speed_up", changeDayToggle.isOn);
  }

  public void OnDebugModeToggleChanged() {
    if (debug_mode_toggle == null) return;
    DataMgr.SetBool("debug_mode", debug_mode_toggle.isOn);
    if (GameSceneMgr.instance != null) {
      GameSceneMgr.instance.RefreshDebugPageName();
    }
  }

  private void ToggleLanguage() {
    LocalizationUtil.ToggleLocale();
    ApplyLocalizedTexts();
  }

  private void ApplyLocalizedTexts() {
    if (giveup_label != null) {
      giveup_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingRetire, "今回の冒険は諦める");
    }
    if (title_label != null) {
      title_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingTitle, "タイトルに戻る");
    }
    if (delete_label != null) {
      delete_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingReset, "すべてのデータを初期化する");
    }
    if (change_scene_label != null) {
      change_scene_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingFastScene, "画面暗転を高速化");
    }
    if (day_label != null) {
      day_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingFastDay, "日数経過を高速化");
    }
    if (reset_label != null) {
      reset_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingReset, "すべてのデータを初期化する");
    }
    if (volume_label != null) {
      volume_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingVolume, "音量");
    }
    if (debug_label != null) {
      debug_label.text = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingDebugMode, "デバッグモード");
    }
    UpdateLanguageLabel();
  }

  private void UpdateLanguageLabel() {
    if (language_button == null) return;
    TextMeshProUGUI text = language_button.GetComponentInChildren<TextMeshProUGUI>(true);
    if (text == null) return;
    string language = LocalizationUtil.IsEnglish()
      ? LocalizationUtil.GetOrDefault(LocalizationKeys.LanguageEnglish, "英語")
      : LocalizationUtil.GetOrDefault(LocalizationKeys.LanguageJapanese, "日本語");
    string label = LocalizationUtil.GetOrDefault(LocalizationKeys.SettingLanguage, "言語");
    text.text = $"{label}: {language}";
  }

  private void OnLocaleChanged(Locale _) {
    ApplyLocalizedTexts();
  }

  [System.NonSerialized] public int cursor_pos = 99;
  [System.NonSerialized] public int pre_cursor_pos = 99;
  const int CURSOR_POS_DELETE = 0;
  const int CURSOR_POS_TITLE = 1;
  const int CURSOR_POS_GIVEUP = 2;

  public void upCursor(){
    cursor_pos += 1;
    if(cursor_pos >= 90) cursor_pos = CURSOR_POS_GIVEUP;

    if(cursor_pos >= CURSOR_POS_GIVEUP) cursor_pos = CURSOR_POS_GIVEUP;
    if(pre_cursor_pos == cursor_pos) return;

    cursorButton(cursor_pos);
  }

  public void downCursor(){
    cursor_pos -= 1;
    if(cursor_pos >= 90) cursor_pos = CURSOR_POS_GIVEUP;
    if(cursor_pos < CURSOR_POS_DELETE) cursor_pos = CURSOR_POS_DELETE;
    if(pre_cursor_pos == cursor_pos) return;

    cursorButton(cursor_pos);
  }

  public void unCursorButton(int i){
    switch(i) {
      case CURSOR_POS_GIVEUP:
        CommonUtil.changeUnCursorButtonImage(giveup_button);
        break;
      case CURSOR_POS_TITLE:
        CommonUtil.changeUnCursorButtonImage(title_button);
        break;
      case CURSOR_POS_DELETE:
        CommonUtil.changeUnCursorButtonImage(delete_button);
        break;
      default:
        break;
    }
  }

  public void cursorButton(int i){
    unCursorButton(pre_cursor_pos);
    pre_cursor_pos = cursor_pos;
    switch(i) {
      case CURSOR_POS_GIVEUP:
        CommonUtil.changeCursorButtonImage(giveup_button);
        break;
      case CURSOR_POS_TITLE:
        CommonUtil.changeCursorButtonImage(title_button);
        break;
      case CURSOR_POS_DELETE:
        CommonUtil.changeCursorButtonImage(delete_button);
        break;
      default:
        break;
    }
  }

  public void pushedEnterButton(){
    switch(cursor_pos) {
      case CURSOR_POS_GIVEUP:
        retireGame();
        break;
      case CURSOR_POS_TITLE:
        goTitle();
        break;
      case CURSOR_POS_DELETE:
        pushedEndButton();
        break;
      default:
        break;
    }
  }
}
