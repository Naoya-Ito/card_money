using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class CharacterCreateSceneMgr : MonoBehaviour {
  private const string TITLE_SUFFIX = "勇者かっぱ";
  private const int BASE_ATK = 1;
  private const int BASE_AGI = 1;
  private const int BASE_CHARM = 1;
  private const int BASE_WIS = 1;
  private const int BASE_GOLD = 10;

  private struct TitleModifier {
    public string key;
    public string defaultTitle;
    public int maxHp;
    public int atk;
    public int agi;
    public int charm;
    public int wis;
    public int gold;

    public TitleModifier(string key, string defaultTitle, int maxHp, int atk, int agi, int charm, int wis, int gold) {
      this.key = key;
      this.defaultTitle = defaultTitle;
      this.maxHp = maxHp;
      this.atk = atk;
      this.agi = agi;
      this.charm = charm;
      this.wis = wis;
      this.gold = gold;
    }

    public string GetTitle() {
      return LocalizationUtil.GetOrDefault(key, defaultTitle);
    }
  }

  private static readonly TitleModifier[] TitlesX = new TitleModifier[] {
    new TitleModifier(LocalizationKeys.CharCreateTitleXStylish, "おしゃれな", -2, 0, 0, 2, 0, -1),
    new TitleModifier(LocalizationKeys.CharCreateTitleXMacho, "マッチョな", 1, 1, 0, 0, 0, 0),
    new TitleModifier(LocalizationKeys.CharCreateTitleXSwift, "疾風の", -1, 0, 3, 0, 0, 0),
    new TitleModifier(LocalizationKeys.CharCreateTitleXWealthy, "金持ちの", 0, 0, 0, 0, 0, 2),
    new TitleModifier(LocalizationKeys.CharCreateTitleXQuiet, "むっつり", 0, 0, 0, 0, 1, 0),
    new TitleModifier(LocalizationKeys.CharCreateTitleXLimited, "余命半年の", -9, 3, 0, 3, 3, -2),
  };

  private static readonly TitleModifier[] TitlesY = new TitleModifier[] {
    new TitleModifier(LocalizationKeys.CharCreateTitleYClumsy, "ドジっ子", -1, 0, 0, 0, 0, 1),
    new TitleModifier(LocalizationKeys.CharCreateTitleYHandsome, "イケメン", 0, 0, 0, 1, 0, -1),
    new TitleModifier(LocalizationKeys.CharCreateTitleYElite, "エリート", 0, 1, 1, 0, 0, 0),
    new TitleModifier(LocalizationKeys.CharCreateTitleYRookie, "新米", -2, 0, 2, 0, 0, 0),
    new TitleModifier(LocalizationKeys.CharCreateTitleYGenius, "天才", -1, 0, 0, 0, 2, 0),
  };

  private TextMeshProUGUI titleXText;
  private TextMeshProUGUI titleYText;
  private TextMeshProUGUI titleSuffixText;
  private RectTransform titleRoot;
  private RectTransform titleTooltipRoot;
  private TextMeshProUGUI titleTooltipHeader;
  private TextMeshProUGUI titleTooltipBody;
  private TitleModifier currentTitleX;
  private TitleModifier currentTitleY;
  private RectTransform titleTooltipHeaderRect;
  private RectTransform titleTooltipBodyRect;
  private Image titleTooltipBackground;
  private const float TITLE_TOOLTIP_PADDING = 8f;
  private const float TITLE_TOOLTIP_HEADER_HEIGHT = 32f;
  private const float TITLE_TOOLTIP_LINE_HEIGHT = 24f;
  private const float TITLE_TOOLTIP_GAP = 4f;
  private TextMeshProUGUI statLabelText;
  private TextMeshProUGUI statValueText;
  private TMP_FontAsset defaultFont;
  private Button decideButton;
  private Button rollButton;
  private readonly List<Button> selectionButtons = new List<Button>();
  private int selectedIndex = -1;
  private Image kappaImage;
  private static readonly string[] KAPPA_SPRITE_KEYS = new string[] {
    "chara/kappa_joy",
    "chara/kappa_run1",
    "chara/kappa_run2",
    "chara/kappa_run3",
    "chara/kappa_sad",
    "chara/kappa_dot",
    "chara/kappa_ase",
    "chara/kappa_macho",
    "chara/kappa_taiho",
    "chara/kappa_yase",
    "chara/kappa_dead",
    "chara/kappa_drum1",
    "chara/kappa_drum2",
    "chara/kappa_drum3",
    "chara/kappa_drum4",
    "chara/kappa_drum5",
    "chara/kappa_ecape",
    "chara/kappa_escape",
    "chara/kappa_bak_run1",
    "chara/kappa_bak_run2",
    "chara/kappa_bak_run3",
    "chara/kappa_namake",
  };
  private readonly List<Sprite> kappaSprites = new List<Sprite>();
  private string titleX = "";
  private string titleY = "";
  private int atk = BASE_ATK;
  private int agi = BASE_AGI;
  private int charm = BASE_CHARM;
  private int wis = BASE_WIS;
  private int gold = BASE_GOLD;

  private void Start() {
    if (FadeManager.instance != null) {
      FadeManager.instance.ClearFadeState();
    }
    LocalizationUtil.ApplySavedLocale();
    if (BGMMgr.instance != null) {
      BGMMgr.instance.changeBGM(BGMMgr.KEY_SENDOUSURU_OTOKO);
    }
    LoadDefaultFont();
    LoadKappaSprites();
    EnsureCamera();
    Canvas canvas = EnsureCanvas();
    if (canvas == null) return;
    EnsureEventSystem();
    BuildUi(canvas);
    RollTitles();
    ApplyLocalizedTexts();
    ClearSelection();
  }

  private void EnsureCamera() {
    if (Camera.main != null) return;
    GameObject cameraObj = new GameObject("Main Camera");
    Camera cam = cameraObj.AddComponent<Camera>();
    cam.orthographic = true;
    cam.orthographicSize = 270f;
    cam.clearFlags = CameraClearFlags.SolidColor;
    cam.backgroundColor = Color.black;
    cameraObj.tag = "MainCamera";
    cameraObj.transform.position = new Vector3(0f, 0f, 0f);
    cameraObj.transform.localScale = Vector3.one;
  }

  private Canvas EnsureCanvas() {
    Canvas existing = FindCanvasInActiveScene();
    if (existing != null) return existing;
    Debug.LogError("Canvas が Scene に見つかりません。");
    return null;
  }

  private Canvas FindCanvasInActiveScene() {
    Scene activeScene = SceneManager.GetActiveScene();
    if (!activeScene.IsValid()) return null;
    GameObject[] roots = activeScene.GetRootGameObjects();
    foreach (GameObject root in roots) {
      if (root == null) continue;
      if (root.name != "Canvas") continue;
      Canvas canvas = root.GetComponent<Canvas>();
      if (canvas != null) return canvas;
    }
    return null;
  }

  private void EnsureEventSystem() {
    Scene activeScene = SceneManager.GetActiveScene();
    EventSystem[] systems = Object.FindObjectsByType<EventSystem>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    foreach (EventSystem system in systems) {
      if (system == null) continue;
      if (system.gameObject.scene != activeScene) continue;
      return;
    }
    Debug.LogError("EventSystem が Scene に見つかりません。");
  }

  private void BuildUi(Canvas canvas) {
    RectTransform canvasRect = canvas.GetComponent<RectTransform>();

    Image bg = FindSceneImage(canvasRect, "bg");
    if (bg == null) {
      Debug.LogError("bg が Scene に見つかりません。");
    } else {
      SetupSceneImage(bg, "bg/bg_youhishi");
      StretchToParent(bg.rectTransform);
    }

    kappaImage = FindSceneImage(canvasRect, "kappa");
    if (kappaImage == null) {
      Debug.LogError("kappa が Scene に見つかりません。");
    } else {
      SetupSceneImage(kappaImage, "chara/kappa_joy");
    }

    titleRoot = FindSceneRect(canvasRect, "title_root");
    if (titleRoot == null) {
      Debug.LogError("title_root が Scene に見つかりません。");
    }

    titleXText = FindSceneText(canvasRect, "title_x");
    if (titleXText == null) {
      Debug.LogError("title_x が Scene に見つかりません。");
    }
    titleYText = FindSceneText(canvasRect, "title_y");
    if (titleYText == null) {
      Debug.LogError("title_y が Scene に見つかりません。");
    }
    titleSuffixText = FindSceneText(canvasRect, "title_suffix");
    if (titleSuffixText == null) {
      Debug.LogError("title_suffix が Scene に見つかりません。");
    } else {
      titleSuffixText.text = LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateTitleSuffix, TITLE_SUFFIX);
    }
    BindTitleTooltip(canvasRect);

    RectTransform statsRoot = FindSceneRect(canvasRect, "stats_root");
    if (statsRoot == null) {
      Debug.LogError("stats_root が Scene に見つかりません。");
    }

    statLabelText = FindSceneText(canvasRect, "stat_labels");
    if (statLabelText == null) {
      Debug.LogError("stat_labels が Scene に見つかりません。");
    } else {
      statLabelText.text = BuildStatLabelsText();
    }

    statValueText = FindSceneText(canvasRect, "stat_values");
    if (statValueText == null) {
      Debug.LogError("stat_values が Scene に見つかりません。");
    }

    Button decideButton = FindSceneButton(canvasRect, "decide_button");
    if (decideButton == null) {
      Debug.LogError("decide_button が Scene に見つかりません。");
    } else {
      SetupSceneButton(
        decideButton,
        LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateDecide, "これに決めた！"),
        "other/button_yellow"
      );
      this.decideButton = decideButton;
      decideButton.onClick.RemoveAllListeners();
      decideButton.onClick.AddListener(DecideCharacter);
    }

    Button rollButton = FindSceneButton(canvasRect, "roll_button");
    if (rollButton == null) {
      Debug.LogError("roll_button が Scene に見つかりません。");
    } else {
      SetupSceneButton(
        rollButton,
        LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateRoll, "ダイスロール"),
        "other/button_blue"
      );
      this.rollButton = rollButton;
      rollButton.onClick.RemoveAllListeners();
      rollButton.onClick.AddListener(RollTitles);
    }
    CacheSelectionButtons();
  }

  private void Update() {
    bool previous = false;
    bool next = false;

    if (Keyboard.current != null) {
      bool up = Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame;
      bool down = Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame;
      bool left = Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame;
      bool right = Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame;
      previous = previous || up || left;
      next = next || down || right;
      if (Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame) {
        PressSelectedButton();
      }
    }

    if (Gamepad.current != null) {
      bool up = Gamepad.current.dpad.up.wasPressedThisFrame || Gamepad.current.leftStick.up.wasPressedThisFrame;
      bool down = Gamepad.current.dpad.down.wasPressedThisFrame || Gamepad.current.leftStick.down.wasPressedThisFrame;
      bool left = Gamepad.current.dpad.left.wasPressedThisFrame || Gamepad.current.leftStick.left.wasPressedThisFrame;
      bool right = Gamepad.current.dpad.right.wasPressedThisFrame || Gamepad.current.leftStick.right.wasPressedThisFrame;
      previous = previous || up || left;
      next = next || down || right;
      if (Gamepad.current.buttonSouth.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame) {
        PressSelectedButton();
      }
    }

    if (previous || next) {
      HandleDirectionalInput(previous, next);
    }
  }

  private void HandleDirectionalInput(bool previous, bool next) {
    if (selectionButtons.Count == 0) return;
    if (selectedIndex < 0) {
      if (next && !previous) {
        SelectButton(selectionButtons.Count - 1);
      } else {
        SelectButton(0);
      }
      return;
    }
    if (previous) {
      MoveSelection(-1);
    } else if (next) {
      MoveSelection(1);
    }
  }

  private void MoveSelection(int delta) {
    int target = Mathf.Clamp(selectedIndex + delta, 0, selectionButtons.Count - 1);
    if (target == selectedIndex) return;
    SelectButton(target);
  }

  private void SelectButton(int index) {
    selectedIndex = index;
    Button button = selectionButtons[index];
    if (button == null) return;
    if (EventSystem.current != null) {
      EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
    button.Select();
  }

  private void PressSelectedButton() {
    if (selectedIndex < 0 || selectedIndex >= selectionButtons.Count) return;
    Button button = selectionButtons[selectedIndex];
    if (button == null || !button.interactable) return;
    button.onClick.Invoke();
  }

  private void ClearSelection() {
    selectedIndex = -1;
    if (EventSystem.current != null) {
      EventSystem.current.SetSelectedGameObject(null);
    }
  }

  private void CacheSelectionButtons() {
    selectionButtons.Clear();
    if (decideButton != null) selectionButtons.Add(decideButton);
    if (rollButton != null) selectionButtons.Add(rollButton);
  }

  private void RollTitles() {
    currentTitleX = TitlesX[Random.Range(0, TitlesX.Length)];
    currentTitleY = TitlesY[Random.Range(0, TitlesY.Length)];
    titleX = currentTitleX.GetTitle();
    titleY = currentTitleY.GetTitle();

    atk = BASE_ATK + currentTitleX.atk + currentTitleY.atk;
    agi = BASE_AGI + currentTitleX.agi + currentTitleY.agi;
    charm = BASE_CHARM + currentTitleX.charm + currentTitleY.charm;
    wis = BASE_WIS + currentTitleX.wis + currentTitleY.wis;
    gold = BASE_GOLD;

    atk = Mathf.Max(1, atk);
    agi = Mathf.Max(1, agi);
    charm = Mathf.Max(1, charm);
    wis = Mathf.Max(1, wis);
    gold = Mathf.Max(0, gold);

    if (titleXText != null) {
      titleXText.text = titleX;
    }
    if (titleYText != null) {
      titleYText.text = titleY;
    }
    if (titleSuffixText != null) {
      titleSuffixText.text = LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateTitleSuffix, TITLE_SUFFIX);
    }
    if (statValueText != null) {
      statValueText.text = $"{atk}\n{agi}\n{charm}\n{wis}\n{gold}";
    }
    UpdateKappaSprite();
  }

  private void DecideCharacter() {
    int fixedMaxHp = DataMgr.GetInt("max_hp");
    DataMgr.SetInt("hp", fixedMaxHp);
    DataMgr.SetInt("hp_kappa", fixedMaxHp);
    DataMgr.SetInt("atk", atk);
    DataMgr.SetInt("agi", agi);
    DataMgr.SetInt("charm", charm);
    DataMgr.SetInt("wis", wis);
    DataMgr.SetInt("gold", gold);
    DataMgr.SetStr("title_x", titleX);
    DataMgr.SetStr("title_y", titleY);
    CommonUtil.changeScene("GameScene");
  }

  private Image CreateImage(string name, RectTransform parent, string spriteKey) {
    GameObject obj = new GameObject(name);
    obj.transform.SetParent(parent, false);
    obj.transform.localScale = Vector3.one;
    RectTransform rect = obj.AddComponent<RectTransform>();
    rect.localPosition = Vector3.zero;
    Image image = obj.AddComponent<Image>();
    image.sprite = Resources.Load<Sprite>($"Textures/{spriteKey}");
    image.raycastTarget = false;
    return image;
  }

  private TextMeshProUGUI CreateText(string name, RectTransform parent, float fontSize, TextAlignmentOptions alignment, bool raycastTarget = false) {
    GameObject obj = new GameObject(name);
    obj.transform.SetParent(parent, false);
    obj.transform.localScale = Vector3.one;
    RectTransform rect = obj.AddComponent<RectTransform>();
    rect.localPosition = Vector3.zero;
    TextMeshProUGUI text = obj.AddComponent<TextMeshProUGUI>();
    if (defaultFont != null) {
      text.font = defaultFont;
    }
    text.fontSize = fontSize;
    text.alignment = alignment;
    text.color = Color.black;
    text.raycastTarget = raycastTarget;
    return text;
  }

  private RectTransform CreateEmptyRect(string name, RectTransform parent) {
    GameObject obj = new GameObject(name);
    obj.transform.SetParent(parent, false);
    obj.transform.localScale = Vector3.one;
    RectTransform rect = obj.AddComponent<RectTransform>();
    rect.localPosition = Vector3.zero;
    return rect;
  }

  private Button CreateButton(RectTransform parent, string name, string label, string spriteKey) {
    GameObject obj = new GameObject(name);
    obj.transform.SetParent(parent, false);
    obj.transform.localScale = Vector3.one;
    RectTransform rect = obj.AddComponent<RectTransform>();
    rect.localPosition = Vector3.zero;

    Image image = obj.AddComponent<Image>();
    image.sprite = Resources.Load<Sprite>($"Textures/{spriteKey}");
    image.type = Image.Type.Sliced;
    Button button = obj.AddComponent<Button>();

    TextMeshProUGUI text = CreateText($"{name}_text", rect, 24, TextAlignmentOptions.Center);
    text.rectTransform.anchorMin = new Vector2(0f, 0f);
    text.rectTransform.anchorMax = new Vector2(1f, 1f);
    text.rectTransform.pivot = new Vector2(0.5f, 0.5f);
    text.rectTransform.sizeDelta = Vector2.zero;
    text.rectTransform.anchoredPosition = Vector2.zero;
    text.text = label;
    UnifiedButtonTheme.ApplyTo(button);
    return button;
  }

  private Button FindSceneButton(RectTransform root, string name) {
    return FindSceneComponent<Button>(root, name);
  }

  private RectTransform FindSceneRect(RectTransform root, string name) {
    return FindSceneComponent<RectTransform>(root, name);
  }

  private TextMeshProUGUI FindSceneText(RectTransform root, string name) {
    return FindSceneComponent<TextMeshProUGUI>(root, name);
  }

  private Image FindSceneImage(RectTransform root, string name) {
    return FindSceneComponent<Image>(root, name);
  }

  private T FindSceneComponent<T>(RectTransform root, string name) where T : Component {
    if (root == null || string.IsNullOrEmpty(name)) return null;
    T[] components = root.GetComponentsInChildren<T>(true);
    foreach (T component in components) {
      if (component == null) continue;
      if (component.name != name) continue;
      return component;
    }
    return null;
  }

  private void SetupSceneImage(Image image, string spriteKey) {
    if (image == null) return;
    image.sprite = Resources.Load<Sprite>($"Textures/{spriteKey}");
    image.raycastTarget = false;
  }

  private void SetupSceneButton(Button button, string label, string spriteKey) {
    if (button == null) return;
    Image image = button.GetComponent<Image>();
    if (image == null) {
      Debug.LogError($"{button.name} に Image がありません。");
      return;
    }
    image.sprite = Resources.Load<Sprite>($"Textures/{spriteKey}");
    image.type = Image.Type.Sliced;

    TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
    if (text == null) {
      Debug.LogError($"{button.name} に TextMeshProUGUI がありません。");
      return;
    }
    if (defaultFont != null) {
      text.font = defaultFont;
    }
    text.fontSize = 24;
    text.alignment = TextAlignmentOptions.Center;
    text.color = Color.black;
    text.text = label;
    text.raycastTarget = false;

    UnifiedButtonTheme.ApplyTo(button);
  }

  private void StretchToParent(RectTransform rect) {
    rect.anchorMin = Vector2.zero;
    rect.anchorMax = Vector2.one;
    rect.pivot = new Vector2(0.5f, 0.5f);
    rect.anchoredPosition = Vector2.zero;
    rect.sizeDelta = Vector2.zero;
  }

  private void LoadDefaultFont() {
    defaultFont = Resources.Load<TMP_FontAsset>("Fonts/AozoraMinchoRegular SDF");
    if (defaultFont == null) {
      defaultFont = TMP_Settings.defaultFontAsset;
    }
    if (defaultFont == null) {
      Debug.LogError("CharacterCreate のデフォルトフォント読込に失敗しました。");
    }
  }

  private void UpdateKappaSprite() {
    if (kappaImage == null || kappaSprites.Count == 0) return;
    kappaImage.sprite = kappaSprites[Random.Range(0, kappaSprites.Count)];
  }

  private void LoadKappaSprites() {
    kappaSprites.Clear();
    foreach (string key in KAPPA_SPRITE_KEYS) {
      Sprite sprite = Resources.Load<Sprite>($"Textures/{key}");
      if (sprite == null) continue;
      kappaSprites.Add(sprite);
    }
    if (kappaSprites.Count == 0) {
      Sprite fallback = Resources.Load<Sprite>("Textures/chara/kappa_joy");
      if (fallback != null) {
        kappaSprites.Add(fallback);
      }
    }
  }

  private void BindTitleTooltip(RectTransform canvasRect) {
    titleTooltipRoot = FindSceneRect(canvasRect, "title_tooltip");
    if (titleTooltipRoot == null) {
      Debug.LogError("title_tooltip が Scene に見つかりません。");
      return;
    }
    titleTooltipBackground = titleTooltipRoot.GetComponent<Image>();
    if (titleTooltipBackground == null) {
      Debug.LogError("title_tooltip に Image がありません。");
      return;
    }
    titleTooltipBackground.color = new Color(0f, 0f, 0f, 0.65f);
    titleTooltipBackground.raycastTarget = false;

    titleTooltipHeader = FindSceneText(canvasRect, "title_tooltip_header");
    if (titleTooltipHeader == null) {
      Debug.LogError("title_tooltip_header が Scene に見つかりません。");
      return;
    }
    titleTooltipHeaderRect = titleTooltipHeader.rectTransform;
    titleTooltipHeader.color = Color.white;

    titleTooltipBody = FindSceneText(canvasRect, "title_tooltip_body");
    if (titleTooltipBody == null) {
      Debug.LogError("title_tooltip_body が Scene に見つかりません。");
      return;
    }
    titleTooltipBodyRect = titleTooltipBody.rectTransform;
    titleTooltipBody.color = Color.white;

    titleTooltipRoot.gameObject.SetActive(false);
    SetupTitleHover(titleXText, true);
    SetupTitleHover(titleYText, false);
  }

  private void SetupTitleHover(TextMeshProUGUI target, bool isTitleXValue) {
    if (target == null) return;
    TitleTooltipHover hover = target.gameObject.AddComponent<TitleTooltipHover>();
    hover.manager = this;
    hover.isTitleX = isTitleXValue;
  }

  public void ShowTitleTooltip(bool isTitleXValue) {
    if (titleTooltipRoot == null || titleTooltipHeader == null || titleTooltipBody == null) return;
    TitleModifier mod = isTitleXValue ? currentTitleX : currentTitleY;
    titleTooltipHeader.text = mod.GetTitle();
    string bodyText = BuildTitleEffectText(mod);
    titleTooltipBody.text = bodyText;
    int lineCount = Mathf.Max(1, bodyText.Split('\n').Length);
    UpdateTitleTooltipLayout(lineCount);
    titleTooltipRoot.gameObject.SetActive(true);
  }

  public void HideTitleTooltip() {
    if (titleTooltipRoot == null) return;
    titleTooltipRoot.gameObject.SetActive(false);
  }

  private string BuildTitleEffectText(TitleModifier mod) {
    List<string> lines = new List<string>();
    AddEffectLine(lines, LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateStatAtk, "攻撃力"), mod.atk);
    AddEffectLine(lines, LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateStatAgi, "すばやさ"), mod.agi);
    AddEffectLine(lines, LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateStatCharm, "魅力"), mod.charm);
    AddEffectLine(lines, LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateStatWis, "かしこさ"), mod.wis);
    if (lines.Count == 0) {
      lines.Add(LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateNoEffect, "効果なし"));
    }
    return string.Join("\n", lines);
  }

  private void AddEffectLine(List<string> lines, string label, int value) {
    if (value == 0) return;
    lines.Add($"{label}{FormatSigned(value)}");
  }

  private string FormatSigned(int value) {
    if (value > 0) return $"+{value}";
    if (value < 0) return $"{value}";
    return "+0";
  }

  private void UpdateTitleTooltipLayout(int lineCount) {
    if (titleTooltipRoot == null || titleTooltipHeaderRect == null || titleTooltipBodyRect == null) return;
    float bodyHeight = TITLE_TOOLTIP_LINE_HEIGHT * lineCount;
    float totalHeight = TITLE_TOOLTIP_PADDING + TITLE_TOOLTIP_HEADER_HEIGHT + TITLE_TOOLTIP_GAP + bodyHeight + TITLE_TOOLTIP_PADDING;
    titleTooltipRoot.sizeDelta = new Vector2(titleTooltipRoot.sizeDelta.x, totalHeight);
    titleTooltipHeaderRect.sizeDelta = new Vector2(titleTooltipHeaderRect.sizeDelta.x, TITLE_TOOLTIP_HEADER_HEIGHT);
    titleTooltipHeaderRect.anchoredPosition = new Vector2(TITLE_TOOLTIP_PADDING, -TITLE_TOOLTIP_PADDING);
    float bodyTop = TITLE_TOOLTIP_PADDING + TITLE_TOOLTIP_HEADER_HEIGHT + TITLE_TOOLTIP_GAP;
    titleTooltipBodyRect.sizeDelta = new Vector2(titleTooltipBodyRect.sizeDelta.x, bodyHeight);
    titleTooltipBodyRect.anchoredPosition = new Vector2(TITLE_TOOLTIP_PADDING, -bodyTop);
  }

  private void AddPreferredWidthFitter(TextMeshProUGUI target) {
    if (target == null) return;
    ContentSizeFitter fitter = target.gameObject.AddComponent<ContentSizeFitter>();
    fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
    fitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
  }

  private void ApplyLocalizedTexts() {
    if (titleSuffixText != null) {
      titleSuffixText.text = LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateTitleSuffix, TITLE_SUFFIX);
    }
    if (statLabelText != null) {
      statLabelText.text = BuildStatLabelsText();
    }
    if (decideButton != null) {
      SetButtonLabel(decideButton, LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateDecide, "これに決めた！"));
    }
    if (rollButton != null) {
      SetButtonLabel(rollButton, LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateRoll, "ダイスロール"));
    }
    if (currentTitleX.key != null) {
      titleX = currentTitleX.GetTitle();
    }
    if (currentTitleY.key != null) {
      titleY = currentTitleY.GetTitle();
    }
    if (titleXText != null) titleXText.text = titleX;
    if (titleYText != null) titleYText.text = titleY;
  }

  private void SetButtonLabel(Button button, string label) {
    if (button == null) return;
    TextMeshProUGUI[] texts = button.GetComponentsInChildren<TextMeshProUGUI>(true);
    foreach (TextMeshProUGUI text in texts) {
      text.text = label;
    }
  }

  private void OnEnable() {
    if (!LocalizationSettings.HasSettings) return;
    LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
  }

  private void OnDisable() {
    if (!LocalizationSettings.HasSettings) return;
    LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
  }

  private void OnLocaleChanged(Locale _) {
    ApplyLocalizedTexts();
  }

  private string BuildStatLabelsText() {
    return string.Join("\n", new string[] {
      LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateStatAtk, "攻撃力"),
      LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateStatAgi, "すばやさ"),
      LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateStatCharm, "魅力"),
      LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateStatWis, "かしこさ"),
      LocalizationUtil.GetOrDefault(LocalizationKeys.CharCreateStatGold, "所持金"),
    });
  }
}

public class TitleTooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  public CharacterCreateSceneMgr manager;
  public bool isTitleX;

  public void OnPointerEnter(PointerEventData eventData) {
    if (manager == null) return;
    manager.ShowTitleTooltip(isTitleX);
  }

  public void OnPointerExit(PointerEventData eventData) {
    if (manager == null) return;
    manager.HideTitleTooltip();
  }
}
