using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem;

// 選択肢ボタンの親コントローラー
public class ChoiceModel : MonoBehaviour {

  [SerializeField] public Image choice_area;
  [SerializeField] public TextMeshProUGUI choice_title;

  // 未使用
  [SerializeField] public GameObject button1;
  [SerializeField] public GameObject button2;
  [SerializeField] public GameObject button3;

  [SerializeField] public TextMeshProUGUI button1_text;
  [SerializeField] public TextMeshProUGUI button2_text;
  [SerializeField] public TextMeshProUGUI button3_text;

  [SerializeField] public TextMeshProUGUI button1_explain;
  [SerializeField] public TextMeshProUGUI button2_explain;
  [SerializeField] public TextMeshProUGUI button3_explain;

  [System.NonSerialized] public List<string> choice_keys = new List<string>() { };
  private int choiceVersion = 0;

  private RectTransform button_area_rect;
  private HorizontalLayoutGroup button_layout;
  private float default_button_width = 0f;
  private float default_title_width = 0f;
  private float default_explain_width = 0f;
  private float default_button_text_width = 0f;
  private readonly List<GameObject> selectionBoxes = new List<GameObject>();
  private readonly List<Image> selectionImages = new List<Image>();
  private readonly List<int> selectionChoiceIndices = new List<int>();
  private int selectedIndex = -1;
  private int selectedChoiceIndex = -1;
  private int hoveredChoiceIndex = -1;
  private Sprite choiceNormalSprite;
  private Sprite choiceDisabledSprite;
  private readonly bool[] lockedChoices = new bool[3];


  [System.NonSerialized] public static ChoiceModel instance = null;

  private void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(this.gameObject);
    }
    CacheLayoutRefs();
    DisableExplainRaycast();
    DisableButtonTextRaycast();
    LoadChoiceSprites();
    ApplyChoiceBoxTheme();
    SetupHoverEvents();
  }

  public void initChoice() {
    hideAllButtons();
    choice_keys = new List<string>() { };
    for (int i = 0; i < lockedChoices.Length; i++) {
      lockedChoices[i] = false;
    }
    UpdateButtonWidths(3);
    DisableExplainRaycast();
    DisableButtonTextRaycast();
    ResetButtonState(1);
    ResetButtonState(2);
    ResetButtonState(3);
    choiceVersion++;
    ClearSelection();
  }

  private void Update() {
    if (choice_area == null || !choice_area.gameObject.activeInHierarchy) return;
    if (GameSceneMgr.instance != null && GameSceneMgr.instance.IsLockedNoticeActive) return;
    bool left = false;
    bool right = false;
    bool submit = false;

    if (Keyboard.current != null) {
      left = left || Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame;
      right = right || Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame;
      submit = submit || Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame;
    }

    if (Gamepad.current != null) {
      left = left || Gamepad.current.dpad.left.wasPressedThisFrame || Gamepad.current.leftStick.left.wasPressedThisFrame;
      right = right || Gamepad.current.dpad.right.wasPressedThisFrame || Gamepad.current.leftStick.right.wasPressedThisFrame;
      submit = submit || Gamepad.current.buttonSouth.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame;
    }

    if (left || right) {
      HandleHorizontalInput(left, right);
    }
    if (submit) {
      PressSelectedButton();
    }
  }

  private void HandleHorizontalInput(bool left, bool right) {
    RefreshSelectionBoxes();
    if (selectionBoxes.Count == 0) return;
    if (selectedIndex < 0) {
      int startIndex = right ? Mathf.Min(1, selectionBoxes.Count - 1) : 0;
      SelectButtonAt(startIndex);
      return;
    }
    if (left) {
      MoveSelection(-1);
    } else if (right) {
      MoveSelection(1);
    }
  }

  private void MoveSelection(int delta) {
    if (selectionBoxes.Count == 0) return;
    int target = Mathf.Clamp(selectedIndex + delta, 0, selectionBoxes.Count - 1);
    if (target == selectedIndex) return;
    SelectButtonAt(target);
  }

  private void SelectButtonAt(int index) {
    selectedIndex = index;
    if (selectionBoxes[index] == null) return;
    selectedChoiceIndex = selectionChoiceIndices[index];
    UpdateChoiceColors();
    if (EventSystem.current != null) {
      EventSystem.current.SetSelectedGameObject(selectionBoxes[index]);
    }
  }

  private void PressSelectedButton() {
    RefreshSelectionBoxes();
    if (selectedIndex < 0 || selectedIndex >= selectionBoxes.Count) return;
    if (selectedChoiceIndex <= 0) return;
    if (GameSceneMgr.instance == null) return;
    if (IsChoiceLocked(selectedChoiceIndex)) {
      GameSceneMgr.instance.ShowLockedChoiceNotice();
      return;
    }
    GameSceneMgr.instance.pushedChoiceButton(selectedChoiceIndex);
  }

  private void RefreshSelectionBoxes() {
    selectionBoxes.Clear();
    selectionImages.Clear();
    selectionChoiceIndices.Clear();
    AddSelectionBox(button1, 1);
    AddSelectionBox(button2, 2);
    AddSelectionBox(button3, 3);
    selectedIndex = selectionChoiceIndices.IndexOf(selectedChoiceIndex);
    if (selectedIndex < 0) {
      selectedChoiceIndex = -1;
    }
    UpdateChoiceColors();
  }

  private void ClearSelection() {
    selectedIndex = -1;
    selectedChoiceIndex = -1;
    hoveredChoiceIndex = -1;
    if (EventSystem.current != null) {
      EventSystem.current.SetSelectedGameObject(null);
    }
    UpdateChoiceColors();
  }

  private void AddSelectionBox(GameObject boxObj, int choiceIndex) {
    if (boxObj == null) return;
    if (!boxObj.activeInHierarchy) return;
    if (!IsChoiceSelectable(boxObj)) return;
    Image image = boxObj.GetComponent<Image>();
    if (image == null) return;
    selectionBoxes.Add(boxObj);
    selectionImages.Add(image);
    selectionChoiceIndices.Add(choiceIndex);
  }

  private bool IsChoiceSelectable(GameObject boxObj) {
    return true;
  }

  private void LoadChoiceSprites() {
    if (choiceNormalSprite == null) {
      choiceNormalSprite = Resources.Load<Sprite>("Textures/other/button_blue");
    }
    if (choiceDisabledSprite == null) {
      choiceDisabledSprite = Resources.Load<Sprite>("Textures/other/button_gray");
    }
  }

  private void ApplyChoiceBoxTheme() {
    ApplyChoiceBoxThemeTo(button1);
    ApplyChoiceBoxThemeTo(button2);
    ApplyChoiceBoxThemeTo(button3);
    UpdateChoiceColors();
  }

  private void ApplyChoiceBoxThemeTo(GameObject boxObj) {
    if (boxObj == null) return;
    Image image = boxObj.GetComponent<Image>();
    if (image == null) return;
    if (choiceNormalSprite != null) {
      image.sprite = choiceNormalSprite;
      image.type = Image.Type.Sliced;
    }
    image.color = Color.white;
  }

  private void SetupHoverEvents() {
    SetupHoverEventsFor(button1, 1);
    SetupHoverEventsFor(button2, 2);
    SetupHoverEventsFor(button3, 3);
  }

  private void SetupHoverEventsFor(GameObject boxObj, int choiceIndex) {
    if (boxObj == null) return;
    EventTrigger trigger = boxObj.GetComponentInChildren<EventTrigger>(true);
    if (trigger == null) return;
    AddEventTrigger(trigger, EventTriggerType.PointerEnter, () => OnPointerEnter(choiceIndex));
    AddEventTrigger(trigger, EventTriggerType.PointerExit, () => OnPointerExit(choiceIndex));
  }

  private void AddEventTrigger(EventTrigger trigger, EventTriggerType type, System.Action action) {
    if (trigger == null || action == null) return;
    if (trigger.triggers == null) {
      trigger.triggers = new List<EventTrigger.Entry>();
    }
    EventTrigger.Entry entry = new EventTrigger.Entry {
      eventID = type
    };
    entry.callback.AddListener(_ => action());
    trigger.triggers.Add(entry);
  }

  private void OnPointerEnter(int choiceIndex) {
    hoveredChoiceIndex = choiceIndex;
    UpdateChoiceColors();
  }

  private void OnPointerExit(int choiceIndex) {
    if (hoveredChoiceIndex == choiceIndex) {
      hoveredChoiceIndex = -1;
      UpdateChoiceColors();
    }
  }

  public void hideArea()
  {
    choice_area.gameObject.SetActive(false);
  }
  public void showArea() {
    choice_area.gameObject.SetActive(true);
  }

  public void setTitle(string text) {
    string localized = GetLocalizedChoiceTitle(text);
    bool hasText = !string.IsNullOrEmpty(localized);
    choice_title.text = localized;
    choice_title.gameObject.SetActive(hasText);
    if (choice_title.transform.parent != null && choice_title.transform.parent.gameObject != choice_area.gameObject) {
      choice_title.transform.parent.gameObject.SetActive(hasText);
    }
  }

  public void AddButton(string key, string text, string explain = "") {
    //    Debug.Log($"add, key={key}, text={text}. choice_keys_count={choice_keys.Count}");

    choice_keys.Add(key);
    int index = choice_keys.Count;
    string localizedText = GetLocalizedChoiceOptionText(index, text);
    string localizedExplain = GetLocalizedChoiceOptionExplain(index, explain);
    string formattedExplain = FormatExplain(localizedExplain);

    if (choice_keys.Count == 1)
    {
      button1.SetActive(true);
      button1_text.text = localizedText;
      button1_explain.text = formattedExplain;
    }
    if (choice_keys.Count == 2)
    {
      button2.SetActive(true);
      button2_text.text = localizedText;
      button2_explain.text = formattedExplain;
    }
    if (choice_keys.Count == 3)
    {
      button3.SetActive(true);
      button3_text.text = localizedText;
      button3_explain.text = formattedExplain;
    }

    UpdateButtonWidths(choice_keys.Count);
    fadeInButton(choice_keys.Count);
    RefreshSelectionBoxes();
  }

  private string FormatExplain(string explain) {
    if (string.IsNullOrEmpty(explain)) return explain;
    string[] lines = explain.Split('\n');
    for (int i = 0; i < lines.Length; i++) {
      string line = lines[i].Trim();
      if (line == "" || line.Contains("→")) continue;
      Match match = Regex.Match(line, @"^(?<label>[^：:]+?)(?:：)?(?<delta>[+-]\d+)$");
      if (!match.Success) continue;
      string label = match.Groups["label"].Value;
      if (!TryGetStatKey(label, out string statKey)) continue;
      int current = DataMgr.GetInt(statKey);
      int delta = int.Parse(match.Groups["delta"].Value);
      int next = ClampStat(statKey, current + delta);
      lines[i] = $"{label}：{current}→{next}";
    }
    return string.Join("\n", lines);
  }

  private bool TryGetStatKey(string label, out string statKey) {
    if (IsLabelMatch(label, LocalizationKeys.CharCreateStatGold, "所持金")) {
      statKey = "gold";
      return true;
    }
    if (IsLabelMatch(label, LocalizationKeys.CharCreateStatMaxHp, "最大HP")) {
      statKey = "max_hp";
      return true;
    }
    if (IsLabelMatch(label, LocalizationKeys.CharCreateStatAtk, "攻撃力")) {
      statKey = "atk";
      return true;
    }
    if (IsLabelMatch(label, LocalizationKeys.CharCreateStatAgi, "すばやさ")) {
      statKey = "agi";
      return true;
    }
    if (IsLabelMatch(label, LocalizationKeys.CharCreateStatCharm, "魅力")) {
      statKey = "charm";
      return true;
    }
    if (IsLabelMatch(label, LocalizationKeys.CharCreateStatWis, "かしこさ")) {
      statKey = "wis";
      return true;
    }
    statKey = "";
    return false;
  }

  private int ClampStat(string statKey, int value) {
    if (statKey == "gold") return Mathf.Max(0, value);
    return Mathf.Max(1, value);
  }

  private bool IsLabelMatch(string label, string key, string fallback) {
    if (label == fallback) return true;
    string localized = LocalizationUtil.GetOrDefault(key, fallback);
    return label == localized;
  }

  public void SetButtonEnabled(int index, bool enabled, string disabledExplain = "") {
    GameObject button = GetButtonObject(index);
    if (button == null) return;
    if (index >= 1 && index <= 3) {
      lockedChoices[index - 1] = !enabled;
    }
    Button uiButton = button.GetComponent<Button>();
    if (uiButton != null) {
      uiButton.interactable = true;
    }
    CanvasGroup group = GetOrAddCanvasGroup(button);
    group.interactable = true;
    group.blocksRaycasts = true;
    EventTrigger[] triggers = button.GetComponentsInChildren<EventTrigger>(true);
    foreach (EventTrigger trigger in triggers) {
      trigger.enabled = true;
    }
    Image image = button.GetComponent<Image>();
    if (image != null) {
      if (enabled) {
        if (choiceNormalSprite != null) {
          image.sprite = choiceNormalSprite;
          image.type = Image.Type.Sliced;
        }
      } else {
        if (choiceDisabledSprite != null) {
          image.sprite = choiceDisabledSprite;
          image.type = Image.Type.Sliced;
        }
      }
    }
    GameObject lockOverlay = GetOrCreateLockOverlay(button);
    if (lockOverlay != null) {
      lockOverlay.SetActive(!enabled);
    }
    if (!enabled && !string.IsNullOrEmpty(disabledExplain)) {
      string localizedExplain = GetLocalizedChoiceOptionDisabledExplain(index, disabledExplain);
      TextMeshProUGUI lockCondition = GetLockConditionText(lockOverlay);
      if (lockCondition != null) {
        lockCondition.text = localizedExplain;
      }
      TextMeshProUGUI explain = GetExplainText(index);
      if (explain != null) {
        explain.text = "";
      }
    }
    if (!enabled) {
      if (selectedChoiceIndex == index) {
        selectedChoiceIndex = -1;
        selectedIndex = -1;
      }
      if (hoveredChoiceIndex == index) {
        hoveredChoiceIndex = -1;
      }
    }
    RefreshSelectionBoxes();
  }

  public bool IsChoiceLocked(int index) {
    if (index < 1 || index > 3) return false;
    return lockedChoices[index - 1];
  }
  public void hideOtherButton(int i){
    FadeOutOtherButtons(i, RingConst.DURATION_PAGE_BUTTON_PUSHED_DELAY);
  }

  public void hideAllButtons(){
    button1.SetActive(false);
    button2.SetActive(false);
    button3.SetActive(false);
  }

  public void fadeInAllButton() {
    fadeInButton(1);
    fadeInButton(2);
    fadeInButton(3);
  }

  public void fadeInButton(int i) {
    choice_area.gameObject.SetActive(true);
    if(i == 1) {
      button1.SetActive(true);
      ResetButtonAlpha(button1);
//      button1.GetComponent<CanvasGroup>().DOFade(1, fade_duration).SetAutoKill(false).SetLink(button1.gameObject);
    }
    if(i == 2) {
      button2.SetActive(true);
      ResetButtonAlpha(button2);
//      button2.GetComponent<CanvasGroup>().DOFade(1, fade_duration).SetAutoKill(false).SetLink(button2.gameObject);
    }
    if(i == 3) {
      button3.SetActive(true);
      ResetButtonAlpha(button3);
//      button3.GetComponent<CanvasGroup>().DOFade(1, fade_duration).SetAutoKill(false).SetLink(button3.gameObject);
    }
  }

  public void EnsureButtonsVisible() {
    int count = choice_keys != null ? choice_keys.Count : 0;
    if (count >= 1) {
      button1.SetActive(true);
      ResetButtonAlpha(button1);
    } else {
      button1.SetActive(false);
    }
    if (count >= 2) {
      button2.SetActive(true);
      ResetButtonAlpha(button2);
    } else {
      button2.SetActive(false);
    }
    if (count >= 3) {
      button3.SetActive(true);
      ResetButtonAlpha(button3);
    } else {
      button3.SetActive(false);
    }
  }

  public void fadeOutButton(int i) {
    FadeOutButtons(i, RingConst.DURATION_PAGE_BUTTON_PUSHED_DELAY);
  }


  public void cursorButton(int i){
//    Debug.Log($"cursor button. i = {i}");
//    unCursorButton(pre_cursor_pos);
//    pre_cursor_pos = cursor_pos;
    RefreshSelectionBoxes();
    int targetIndex = selectionChoiceIndices.IndexOf(i);
    if (targetIndex < 0) return;
    SelectButtonAt(targetIndex);
  }

  public void unCursorButton(int i){
//    Debug.Log($"uncursor button. i = {i}");
    ClearSelection();
  }

  private void CacheLayoutRefs() {
    if (button1 == null) return;
    if (button_area_rect == null) {
      button_area_rect = button1.GetComponent<RectTransform>().parent as RectTransform;
    }
    if (button_layout == null && button_area_rect != null) {
      button_layout = button_area_rect.GetComponent<HorizontalLayoutGroup>();
    }
    if (default_button_width <= 0f) {
      RectTransform rect = button1.GetComponent<RectTransform>();
      if (rect != null) {
        default_button_width = rect.sizeDelta.x;
      }
    }
    if (default_title_width <= 0f && choice_title != null) {
      RectTransform rect = choice_title.GetComponent<RectTransform>();
      if (rect != null) {
        default_title_width = rect.sizeDelta.x;
      }
    }
    if (default_explain_width <= 0f && button1_explain != null) {
      RectTransform rect = button1_explain.GetComponent<RectTransform>();
      if (rect != null) {
        default_explain_width = rect.sizeDelta.x;
      }
    }
    if (default_button_text_width <= 0f && button1_text != null) {
      RectTransform rect = button1_text.GetComponent<RectTransform>();
      if (rect != null) {
        default_button_text_width = rect.sizeDelta.x;
      }
    }
  }

  private void UpdateChoiceColors() {
    for (int i = 0; i < selectionImages.Count; i++) {
      Image image = selectionImages[i];
      if (image == null) continue;
      int choiceIndex = selectionChoiceIndices[i];
      bool highlighted = choiceIndex == selectedChoiceIndex || choiceIndex == hoveredChoiceIndex;
      image.color = highlighted ? UnifiedButtonTheme.HighlightColor : Color.white;
    }
  }

  private string GetCurrentPageKey() {
    if (GameSceneMgr.instance != null) {
      return GameSceneMgr.instance.CurrentPageKey;
    }
    return DataMgr.GetStr("page");
  }

  private string GetLocalizedChoiceTitle(string fallback) {
    string pageKey = GetCurrentPageKey();
    if (string.IsNullOrEmpty(pageKey)) return fallback;
    string key = LocalizationUtil.GetChoiceKey(pageKey, "title");
    return LocalizationUtil.GetOrDefault(key, fallback);
  }

  private string GetLocalizedChoiceOptionText(int index, string fallback) {
    string pageKey = GetCurrentPageKey();
    if (string.IsNullOrEmpty(pageKey)) return fallback;
    string key = LocalizationUtil.GetChoiceKey(pageKey, $"option{index}.text");
    return LocalizationUtil.GetOrDefault(key, fallback);
  }

  private string GetLocalizedChoiceOptionExplain(int index, string fallback) {
    string pageKey = GetCurrentPageKey();
    if (string.IsNullOrEmpty(pageKey)) return fallback;
    string key = LocalizationUtil.GetChoiceKey(pageKey, $"option{index}.explain");
    return LocalizationUtil.GetOrDefault(key, fallback);
  }

  private string GetLocalizedChoiceOptionDisabledExplain(int index, string fallback) {
    string pageKey = GetCurrentPageKey();
    if (string.IsNullOrEmpty(pageKey)) return fallback;
    string key = LocalizationUtil.GetChoiceKey(pageKey, $"option{index}.disabled_explain");
    return LocalizationUtil.GetOrDefault(key, fallback);
  }

  private void UpdateButtonWidths(int choiceCount) {
    CacheLayoutRefs();
    if (default_button_width <= 0f) return;
    float areaWidth = button_area_rect != null ? button_area_rect.rect.width : 0f;
    float paddingLeft = 0f;
    float paddingRight = 0f;
    float spacing = 0f;
    if (button_layout != null) {
      paddingLeft = button_layout.padding.left;
      paddingRight = button_layout.padding.right;
      spacing = button_layout.spacing;
    }
    if (choiceCount >= 3) {
      SetButtonsWidth(default_button_width);
      SetExplainWidths(default_explain_width);
      SetButtonTextWidths(default_button_text_width);
      SetTitleWidth(paddingLeft + paddingRight);
      return;
    }
    if (choiceCount <= 0 || areaWidth <= 0f) {
      SetButtonsWidth(default_button_width);
      SetExplainWidths(default_explain_width);
      SetButtonTextWidths(default_button_text_width);
      SetTitleWidth(paddingLeft + paddingRight);
      return;
    }
    float totalSpacing = choiceCount > 1 ? spacing * (choiceCount - 1) : 0f;
    float available = areaWidth - paddingLeft - paddingRight - totalSpacing;
    float targetWidth = available / choiceCount;
    targetWidth = Mathf.Floor(targetWidth / 4f) * 4f;
    if (targetWidth <= 0f) {
      targetWidth = default_button_width;
    }
    SetButtonsWidth(targetWidth);
    SetExplainWidths(targetWidth);
    SetButtonTextWidths(targetWidth);
    SetTitleWidth(paddingLeft + paddingRight);
  }

  private void SetButtonsWidth(float width) {
    SetButtonWidth(button1, width);
    SetButtonWidth(button2, width);
    SetButtonWidth(button3, width);
  }

  private void SetButtonWidth(GameObject button, float width) {
    if (button == null) return;
    RectTransform rect = button.GetComponent<RectTransform>();
    if (rect == null) return;
    rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
    SyncTapScreenRect(button);
  }

  private void SetExplainWidths(float width) {
    if (width <= 0f) return;
    SetTextWidth(button1_explain, width);
    SetTextWidth(button2_explain, width);
    SetTextWidth(button3_explain, width);
  }

  private void SetButtonTextWidths(float width) {
    if (width <= 0f) return;
    SetTextWidth(button1_text, width);
    SetTextWidth(button2_text, width);
    SetTextWidth(button3_text, width);
  }

  private void SetTitleWidth(float horizontalPadding = 0f) {
    if (choice_title == null || button_area_rect == null) return;
    float areaWidth = button_area_rect.rect.width - horizontalPadding;
    if (areaWidth <= 0f) {
      if (default_title_width > 0f) {
        SetTextWidth(choice_title, default_title_width);
      }
      return;
    }
    float targetWidth = Mathf.Floor(areaWidth / 4f) * 4f;
    if (targetWidth <= 0f && default_title_width > 0f) {
      targetWidth = default_title_width;
    }
    SetTextWidth(choice_title, targetWidth);
  }

  private void SetTextWidth(TextMeshProUGUI text, float width) {
    if (text == null) return;
    RectTransform rect = text.GetComponent<RectTransform>();
    if (rect == null) return;
    rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
  }

  private void DisableExplainRaycast() {
    if (button1_explain != null) button1_explain.raycastTarget = false;
    if (button2_explain != null) button2_explain.raycastTarget = false;
    if (button3_explain != null) button3_explain.raycastTarget = false;
  }

  private void DisableButtonTextRaycast() {
    if (button1_text != null) button1_text.raycastTarget = false;
    if (button2_text != null) button2_text.raycastTarget = false;
    if (button3_text != null) button3_text.raycastTarget = false;
  }

  private void SyncTapScreenRect(GameObject button) {
    EventTrigger[] triggers = button.GetComponentsInChildren<EventTrigger>(true);
    foreach (EventTrigger trigger in triggers) {
      RectTransform rect = trigger.GetComponent<RectTransform>();
      if (rect == null) continue;
      rect.anchorMin = Vector2.zero;
      rect.anchorMax = Vector2.one;
      rect.anchoredPosition = Vector2.zero;
      rect.sizeDelta = Vector2.zero;
    }
  }

  public void FadeOutButtons(int selectedIndex, float duration) {
    FadeOutOtherButtons(selectedIndex, duration);
    StartCoroutine(HideButtonsAfterDelay(duration, choiceVersion));
  }

  private void FadeOutOtherButtons(int selectedIndex, float duration) {
    if (selectedIndex != 1) FadeOutButtonObject(button1, duration);
    if (selectedIndex != 2) FadeOutButtonObject(button2, duration);
    if (selectedIndex != 3) FadeOutButtonObject(button3, duration);
  }

  private void FadeOutButtonObject(GameObject button, float duration) {
    if (button == null || !button.activeSelf) return;
    CanvasGroup group = GetOrAddCanvasGroup(button);
    group.DOKill();
    group.DOFade(0f, duration).SetEase(Ease.OutQuad).SetAutoKill(false).SetLink(button);
  }

  private void ResetButtonAlpha(GameObject button) {
    if (button == null) return;
    CanvasGroup group = GetOrAddCanvasGroup(button);
    group.DOKill();
    group.alpha = 1f;
  }

  private CanvasGroup GetOrAddCanvasGroup(GameObject button) {
    CanvasGroup group = button.GetComponent<CanvasGroup>();
    if (group == null) {
      group = button.AddComponent<CanvasGroup>();
    }
    return group;
  }

  private void ResetButtonState(int index) {
    GameObject button = GetButtonObject(index);
    if (button == null) return;
    Button uiButton = button.GetComponent<Button>();
    if (uiButton != null) {
      uiButton.interactable = true;
    }
    CanvasGroup group = GetOrAddCanvasGroup(button);
    group.interactable = true;
    group.blocksRaycasts = true;
    EventTrigger[] triggers = button.GetComponentsInChildren<EventTrigger>(true);
    foreach (EventTrigger trigger in triggers) {
      trigger.enabled = true;
    }
    Image image = button.GetComponent<Image>();
    if (image != null) {
      if (choiceNormalSprite != null) {
        image.sprite = choiceNormalSprite;
        image.type = Image.Type.Sliced;
      }
    }
    GameObject lockOverlay = GetLockOverlay(button);
    if (lockOverlay != null) {
      lockOverlay.SetActive(false);
    }
  }

  private GameObject GetButtonObject(int index) {
    if (index == 1) return button1;
    if (index == 2) return button2;
    if (index == 3) return button3;
    return null;
  }

  private TextMeshProUGUI GetExplainText(int index) {
    if (index == 1) return button1_explain;
    if (index == 2) return button2_explain;
    if (index == 3) return button3_explain;
    return null;
  }

  private GameObject GetLockOverlay(GameObject button) {
    if (button == null) return null;
    Transform found = button.transform.Find("lock_overlay");
    return found != null ? found.gameObject : null;
  }

  private GameObject GetOrCreateLockOverlay(GameObject button) {
    if (button == null) return null;
    GameObject existing = GetLockOverlay(button);
    if (existing != null) return existing;
    GameObject overlay = new GameObject("lock_overlay");
    overlay.transform.SetParent(button.transform, false);
    RectTransform rect = overlay.AddComponent<RectTransform>();
    rect.anchorMin = Vector2.zero;
    rect.anchorMax = Vector2.one;
    rect.pivot = new Vector2(0.5f, 0.5f);
    rect.anchoredPosition = Vector2.zero;
    rect.sizeDelta = Vector2.zero;
    Image bg = overlay.AddComponent<Image>();
    bg.color = new Color(0f, 0f, 0f, 0.35f);
    bg.raycastTarget = false;

    GameObject label = new GameObject("lock_label");
    label.transform.SetParent(overlay.transform, false);
    RectTransform labelRect = label.AddComponent<RectTransform>();
    labelRect.anchorMin = new Vector2(0.5f, 0.5f);
    labelRect.anchorMax = new Vector2(0.5f, 0.5f);
    labelRect.pivot = new Vector2(0.5f, 0.5f);
    labelRect.anchoredPosition = Vector2.zero;
    labelRect.sizeDelta = new Vector2(160f, 40f);
    TextMeshProUGUI text = label.AddComponent<TextMeshProUGUI>();
    text.text = "LOCKED";
    text.alignment = TextAlignmentOptions.Center;
    text.fontSize = 22f;
    text.raycastTarget = false;
    if (button1_text != null && button1_text.font != null) {
      text.font = button1_text.font;
    }
    text.color = Color.white;

    GameObject condition = new GameObject("lock_condition");
    condition.transform.SetParent(overlay.transform, false);
    RectTransform conditionRect = condition.AddComponent<RectTransform>();
    conditionRect.anchorMin = new Vector2(0.5f, 0.5f);
    conditionRect.anchorMax = new Vector2(0.5f, 0.5f);
    conditionRect.pivot = new Vector2(0.5f, 0.5f);
    conditionRect.anchoredPosition = new Vector2(0f, -22f);
    conditionRect.sizeDelta = new Vector2(220f, 32f);
    TextMeshProUGUI conditionText = condition.AddComponent<TextMeshProUGUI>();
    conditionText.text = "";
    conditionText.alignment = TextAlignmentOptions.Center;
    conditionText.fontSize = 16f;
    conditionText.raycastTarget = false;
    conditionText.color = Color.white;
    if (button1_text != null && button1_text.font != null) {
      conditionText.font = button1_text.font;
    }
    return overlay;
  }

  private TextMeshProUGUI GetLockConditionText(GameObject lockOverlay) {
    if (lockOverlay == null) return null;
    Transform found = lockOverlay.transform.Find("lock_condition");
    if (found == null) return null;
    return found.GetComponent<TextMeshProUGUI>();
  }


  private IEnumerator HideButtonsAfterDelay(float duration, int version) {
    yield return new WaitForSeconds(duration);
    if (version != choiceVersion) {
      yield break;
    }
    button1.SetActive(false);
    button2.SetActive(false);
    button3.SetActive(false);
  }
}
