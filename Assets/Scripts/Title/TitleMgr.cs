using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class TitleMgr : MonoBehaviour{

  [SerializeField] public CanvasGroup title_logo;
  [SerializeField] public CanvasGroup wizard;
  [SerializeField] public CanvasGroup dragon;
  [SerializeField] public CanvasGroup dog;

  [SerializeField] public GameObject usagi;
  [SerializeField] public Animator usagi_anim;
  [SerializeField] public GameObject tanuki;
  [SerializeField] public Animator tanuki_anim;

  private Button start_button;
  [SerializeField] public Button exit_button;
  private Button gallery_button;
  private Button setting_button;

  // pv
  [SerializeField] public GameObject pv_bg;
  [SerializeField] public GameObject pv_text;

  private const float fadein_duration = 10.0f;
  private const string AUTO_SAVE_PAGE_KEY = "autosave_page";
  private readonly List<Button> orderedButtons = new List<Button>();
  private Button selectedButton;
  private int selectedIndex = -1;
  private EventSystem cachedEventSystem;
  private bool cachedSendNavigationEvents;
  [System.NonSerialized] public static TitleMgr instance = null;
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

// タイトル画面

  void Start(){
    LocalizationUtil.ApplySavedLocale();
    BGMMgr.instance.changeBGM("title");

    fadeInImage();
    if (HeaderBar.instance != null) {
      HeaderBar.instance.hideStatus();
    }
    DisableDefaultNavigation();
    SetupStartButton();
    SetupGalleryButton();
    SetupSettingButton();
    RefreshLocalizedTexts();
    CacheSelectionButtons();
    ClearSelection();

    KappaController.instance.animateDrum();
    usagi_anim.SetInteger("moveType", RingConst.ANIMATION_STATE_USAGI_GUITAR);
    tanuki_anim.SetInteger("moveType", RingConst.ANIMATION_STATE_TANUKI_GUITAR);
  }

  private void OnDestroy() {
    RestoreDefaultNavigation();
  }

  private void fadeInImage(){
    title_logo.alpha = 0f;
    wizard.alpha = 0;
    dragon.alpha = 0;
    dog.alpha = 0;

    title_logo.DOFade(1, fadein_duration).SetAutoKill(false).SetLink(gameObject);
    wizard.DOFade(1, fadein_duration).SetAutoKill(false).SetLink(gameObject);
    dragon.DOFade(1, fadein_duration).SetAutoKill(false).SetLink(gameObject);
    dog.DOFade(1, fadein_duration).SetAutoKill(false).SetLink(gameObject);
  }

  private void updatePVScene(){
    pv_bg.SetActive(true);
    pv_text.SetActive(true);

    List<string> list = new List<string> {"StartButton", "SettingButton", "PVButton", "EndButton"};
    foreach(string key in list) {
      GameObject button = GameObject.Find(key);
      button.SetActive(false);
    }

    DataMgr.SetBool("pv_mode", false);

    Invoke("pvEnd", 20);
  }

  private void pvEnd(){
    CommonUtil.changeScene("TitleScene");
  }

  private bool isButtonPushed = false;

  public void pushedStart(){
    if(isButtonPushed) {
      return;
    }
    isButtonPushed = true;
    if (start_button != null) {
      CommonUtil.changePushedButtonImage(start_button);
    }

    // デデータがあればロードして実行
    string pageKey = DataMgr.GetStr(AUTO_SAVE_PAGE_KEY);
    if (!string.IsNullOrEmpty(pageKey)) {
      DataMgr.SetStr("page", pageKey);
      CommonUtil.changeScene("GameScene");
      return;
    }

    DataMgr.SetBool("pv_mode", false);
    InitDataMgr.initData();
    //CommonUtil.changeScene("GameScene");
    CommonUtil.changeScene("StoryScene");
  }

  private void startFirstGame(){
//    Debug.Log("set first game");
    InitDataMgr.initData();
    //CommonUtil.changeScene("StoryScene");
    CommonUtil.changeScene("BirthScene");
  }
  public void pushedEndButton(){
    if(isButtonPushed) {
      return;
    }
    isButtonPushed = true;

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }

  public void pushedPVButton(){
    if(isButtonPushed) return;

    isButtonPushed = true;

    DataMgr.SetBool("pv_mode", true);
    DataMgr.SetInt("pv_level", 1);

    BGMMgr.instance.stopMusic();
    CommonUtil.changeScene("PVScene");
  }

  public void pushedGalleryButton(){
    if (isButtonPushed) return;
    isButtonPushed = true;
    if (gallery_button != null) {
      CommonUtil.changePushedButtonImage(gallery_button);
    }
    CommonUtil.changeScene("GalleryScene");
  }

  private void SetupGalleryButton() {
    GameObject obj = GameObject.Find("GalleryButton");
    if (obj == null) return;
    gallery_button = obj.GetComponent<Button>();
    if (gallery_button != null) {
      gallery_button.onClick.RemoveAllListeners();
      gallery_button.onClick.AddListener(pushedGalleryButton);
      Image image = obj.GetComponent<Image>();
      if (image != null) {
        gallery_button.targetGraphic = image;
      }
    }
    SetButtonText(obj, LocalizationUtil.GetOrDefault(LocalizationKeys.TitleGallery, "図鑑"));
  }

  private void DisableDefaultNavigation() {
    if (EventSystem.current == null) return;
    cachedEventSystem = EventSystem.current;
    cachedSendNavigationEvents = cachedEventSystem.sendNavigationEvents;
    cachedEventSystem.sendNavigationEvents = false;
  }

  private void RestoreDefaultNavigation() {
    if (cachedEventSystem == null) return;
    cachedEventSystem.sendNavigationEvents = cachedSendNavigationEvents;
    cachedEventSystem = null;
  }

  private void CacheSelectionButtons() {
    start_button = FindButton("StartButton");
    if (gallery_button == null) {
      gallery_button = FindButton("GalleryButton");
    }
    if (setting_button == null) {
      setting_button = FindButton("SettingButton");
    }
    RefreshSelectionButtons();
  }

  private void SetupStartButton() {
    GameObject obj = GameObject.Find("StartButton");
    if (obj == null) return;
    start_button = obj.GetComponent<Button>();
    string pageKey = DataMgr.GetStr(AUTO_SAVE_PAGE_KEY);
    string label = string.IsNullOrEmpty(pageKey)
      ? LocalizationUtil.GetOrDefault(LocalizationKeys.TitleStartNew, "ゲームを始める")
      : LocalizationUtil.GetOrDefault(LocalizationKeys.TitleStartContinue, "つづきから始める");
    SetButtonText(obj, label);
  }

  private void SetupSettingButton() {
    GameObject obj = GameObject.Find("SettingButton");
    if (obj == null) return;
    setting_button = obj.GetComponent<Button>();
    if (setting_button == null) return;
    setting_button.onClick.RemoveAllListeners();
    setting_button.onClick.AddListener(OpenSettingsScene);
    Image image = obj.GetComponent<Image>();
    if (image != null) {
      setting_button.targetGraphic = image;
    }
  }

  private Button FindButton(string name) {
    GameObject obj = GameObject.Find(name);
    if (obj == null) return null;
    return obj.GetComponent<Button>();
  }

  private void OpenSettingsScene() {
    DataMgr.SetStr("settings_return_scene", "TitleScene");
    CommonUtil.changeScene("SettingScene");
  }

  private void RefreshLocalizedTexts() {
    SetupStartButton();
    SetupGalleryButton();
    UpdateButtonTextByName("SettingButton", LocalizationUtil.GetOrDefault(LocalizationKeys.TitleSettings, "設定"));
    UpdateButtonTextByName("EndButton", LocalizationUtil.GetOrDefault(LocalizationKeys.TitleQuit, "ゲームを終了する"));
    UpdateButtonTextByName("PVButton", LocalizationUtil.GetOrDefault(LocalizationKeys.TitlePv, "PV"));
  }

  private void UpdateButtonTextByName(string name, string label) {
    GameObject obj = GameObject.Find(name);
    if (obj == null) return;
    SetButtonText(obj, label);
  }

  private void SetButtonText(GameObject obj, string label) {
    if (obj == null) return;
    TextMeshProUGUI[] texts = obj.GetComponentsInChildren<TextMeshProUGUI>(true);
    foreach (TextMeshProUGUI text in texts) {
      text.text = label;
    }
    Text[] legacyTexts = obj.GetComponentsInChildren<Text>(true);
    foreach (Text legacyText in legacyTexts) {
      legacyText.text = label;
    }
  }

  private void OnLocaleChanged(Locale _) {
    RefreshLocalizedTexts();
  }

  public void HandleDirectionalInput(Vector2 input) {
    if (input == Vector2.zero) return;
    RefreshSelectionButtons();
    if (selectedIndex < 0) {
      SelectStartButton();
      return;
    }
    if (input.y > 0f) {
      MoveSelection(-1);
    } else if (input.y < 0f) {
      MoveSelection(1);
    }
  }

  public void upCursor() {
    RefreshSelectionButtons();
    if (selectedIndex < 0) {
      SelectStartButton();
      return;
    }
    MoveSelection(-1);
  }

  public void downCursor() {
    RefreshSelectionButtons();
    if (selectedIndex < 0) {
      SelectStartButton();
      return;
    }
    MoveSelection(1);
  }

  public void pushedEnterButton(){
    Button button = GetSelectedButton();
    if (button == null) return;
    if (!IsSelectable(button)) return;
    button.onClick.Invoke();
  }

  private void SelectStartButton() {
    RefreshSelectionButtons();
    if (orderedButtons.Count == 0) return;
    int index = orderedButtons.IndexOf(start_button);
    if (index < 0) {
      SelectButtonAt(0);
      return;
    }
    SelectButtonAt(index);
  }

  private void MoveSelection(int direction) {
    RefreshSelectionButtons();
    if (orderedButtons.Count == 0) return;
    int index = selectedIndex;
    while (true) {
      index += direction;
      if (index < 0 || index >= orderedButtons.Count) return;
      Button candidate = orderedButtons[index];
      if (IsSelectable(candidate)) {
        SelectButtonAt(index);
        return;
      }
    }
  }

  private bool IsSelectable(Button button) {
    if (button == null) return false;
    if (!button.interactable) return false;
    return button.gameObject.activeInHierarchy;
  }

  private Button GetSelectedButton() {
    if (!IsSelectable(selectedButton)) return null;
    return selectedButton;
  }

  private void SelectButtonAt(int index) {
    selectedIndex = index;
    Button button = orderedButtons[index];
    if (button == null) return;
    selectedButton = button;
    if (EventSystem.current != null) {
      EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
    button.Select();
  }

  private void ClearSelection() {
    selectedIndex = -1;
    selectedButton = null;
    if (EventSystem.current != null) {
      EventSystem.current.SetSelectedGameObject(null);
    }
  }

  private void RefreshSelectionButtons() {
    if (start_button == null) {
      start_button = FindButton("StartButton");
    }
    if (gallery_button == null) {
      gallery_button = FindButton("GalleryButton");
    }
    if (setting_button == null) {
      setting_button = FindButton("SettingButton");
    }
    orderedButtons.Clear();
    AddButton(start_button);
    AddButton(gallery_button);
    AddButton(setting_button);
    AddButton(exit_button);
    selectedIndex = selectedButton != null ? orderedButtons.IndexOf(selectedButton) : -1;
    if (selectedIndex < 0) {
      selectedButton = null;
    }
  }

  private void AddButton(Button button) {
    if (button == null) return;
    orderedButtons.Add(button);
  }
}
