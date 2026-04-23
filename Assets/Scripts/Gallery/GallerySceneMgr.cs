using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class GallerySceneMgr : MonoBehaviour {
  private const string GALLERY_SEEN_PREFIX = "gallery_seen_";
  private const string GALLERY_CHAR_SEEN_PREFIX = "gallery_char_seen_";
  private const string GALLERY_RESOURCE_PATH = "Textures/240_135";
  private const float SIDE_WIDTH = 200f;
  private const float CONTENT_WIDTH = 760f;
  private const float CONTENT_HEIGHT = 540f;
  private const int THUMB_COLUMNS = 3;
  private const int THUMB_ROWS = 3;
  private const float THUMB_WIDTH = 200f;
  private const float THUMB_HEIGHT = 112f;
  private const float THUMB_SPACING = 24f;
  private const int CHAR_COLUMNS = 3;
  private const int CHAR_ROWS = 3;
  private const float CHAR_SIZE = 144f;
  private const float CHAR_SPACING = 24f;
  private const float MENU_BUTTON_HEIGHT = 48f;
  private const float MENU_BUTTON_WIDTH = 160f;
  private const float MENU_TOP_Y = 120f;
  private const float MENU_GAP_Y = 64f;
  private const float OVERLAY_TITLE_Y = 216f;
  private const float OVERLAY_STATS_Y = -200f;
  private const float OVERLAY_HIDE_X = 2000f;
  private const float CHARACTER_IMAGE_SIZE = 256f;
  private const float CHARACTER_IMAGE_X = -224f;
  private const float CHARACTER_STATS_WIDTH = 400f;
  private const float CHARACTER_STATS_HEIGHT = 240f;
  private const float CHARACTER_STATS_X = 200f;
  private const float CHARACTER_STATS_Y = -16f;
  private const float CHARACTER_TITLE_WIDTH = 400f;
  private const float CHARACTER_TITLE_HEIGHT = 40f;
  private const float CHARACTER_TITLE_X = 200f;
  private const float CHARACTER_TITLE_Y = 160f;
  private const string CHARACTER_BG_RESOURCE = "Textures/bg/bg_youhishi";

  private enum MenuMode {
    Gallery,
    Character
  }

  private TMP_FontAsset defaultFont;
  private readonly List<Sprite> gallerySprites = new List<Sprite>();
  private readonly List<GalleryThumbnail> thumbnails = new List<GalleryThumbnail>();
  private readonly List<CharacterEntry> characterEntries = new List<CharacterEntry>();
  private readonly List<CharacterThumbnail> characterThumbnails = new List<CharacterThumbnail>();
  private int currentPage = 0;
  private int pageSize = THUMB_COLUMNS * THUMB_ROWS;
  private MenuMode currentMode = MenuMode.Gallery;

  private RectTransform contentRoot;
  private RectTransform galleryRoot;
  private RectTransform characterRoot;
  private TextMeshProUGUI titleText;
  private TextMeshProUGUI pageText;
  private Button prevButton;
  private Button nextButton;
  private Button galleryTabButton;
  private Button characterTabButton;
  private TextMeshProUGUI galleryTabText;
  private TextMeshProUGUI characterTabText;
  private RectTransform overlayRoot;
  private Image overlayImage;
  private TextMeshProUGUI overlayTitleText;
  private TextMeshProUGUI overlayStatsText;
  private Image overlayBackgroundImage;
  private RectTransform overlayImageRect;
  private RectTransform overlayTitleRect;
  private RectTransform overlayStatsRect;
  private Sprite characterOverlayBg;
  private RectTransform sceneCanvasRect;

  private enum OverlayLayout {
    GalleryImage,
    CharacterDetail
  }

  private void Start() {
    if (FadeManager.instance != null) {
      FadeManager.instance.ClearFadeState();
    }
    LoadDefaultFont();
    EnsureCamera();
    Canvas canvas = EnsureCanvas();
    if (canvas == null) {
      return;
    }
    sceneCanvasRect = canvas.GetComponent<RectTransform>();
    ForceHideOverlayRoot();
    EnsureEventSystem();
    LoadGallerySprites();
    BuildCharacterEntries();
    if (!SetupUiReferences()) {
      return;
    }
    SetMode(MenuMode.Gallery);
    EnsureInitialSelection();
  }

  private void Update() {
    HandleGamepadSubmit();
  }

  private void LoadGallerySprites() {
    gallerySprites.Clear();
    Sprite[] sprites = Resources.LoadAll<Sprite>(GALLERY_RESOURCE_PATH);
    gallerySprites.AddRange(sprites.OrderBy(sprite => sprite.name));
  }

  private void BuildCharacterEntries() {
    characterEntries.Clear();
    characterEntries.Add(new CharacterEntry("kappa", "カッパ", "chara/kappa_joy", true));
    characterEntries.Add(new CharacterEntry("usagi", "うさぎ", "chara/usagi_run1", true));
    characterEntries.Add(new CharacterEntry("tanuki", "たぬき", "chara/tanuki_run1", true));
    characterEntries.Add(new CharacterEntry("hime", "ヒメ", "240_135/queen", true));
    characterEntries.Add(new CharacterEntry("shiorina", "シオリーナ", "240_135/shiori_240_135", false));
    characterEntries.Add(new CharacterEntry("slime", "スライム", "chara/slime", false));
    characterEntries.Add(new CharacterEntry("dogs", "イヌの三連星", "chara/dog_king", false));
    characterEntries.Add(new CharacterEntry("maou", "魔王", "chara/maou_jk", false));
  }

  private bool SetupUiReferences() {
    titleText = GetTextByName("TitleText");
    galleryRoot = GetRectByName("GalleryRoot");
    characterRoot = GetRectByName("CharacterRoot");
    contentRoot = GetRectByName("ContentRoot");
    RectTransform galleryGrid = GetRectByName("GalleryGrid");
    RectTransform characterGrid = GetRectByName("CharacterGrid");
    pageText = GetTextByName("PageText");
    prevButton = GetButtonByName("PrevButton");
    nextButton = GetButtonByName("NextButton");
    Button backButton = GetButtonByName("BackButton");
    galleryTabButton = GetButtonByName("MenuGalleryButton");
    characterTabButton = GetButtonByName("MenuCharacterButton");
    galleryTabText = GetTextByName("MenuGalleryText");
    characterTabText = GetTextByName("MenuCharacterText");
    overlayRoot = GetRectByName("OverlayRoot");
    overlayImage = GetImageByName("FullImage");
    overlayTitleText = GetTextByName("OverlayTitle");
    overlayStatsText = GetTextByName("OverlayStats");
    overlayBackgroundImage = overlayRoot != null ? overlayRoot.GetComponent<Image>() : null;
    overlayImageRect = overlayImage != null ? overlayImage.rectTransform : null;
    overlayTitleRect = overlayTitleText != null ? overlayTitleText.rectTransform : null;
    overlayStatsRect = overlayStatsText != null ? overlayStatsText.rectTransform : null;

    if (titleText == null || galleryRoot == null || characterRoot == null || galleryGrid == null || characterGrid == null) {
      return false;
    }

    if (prevButton != null) {
      prevButton.onClick.RemoveAllListeners();
      prevButton.onClick.AddListener(() => ChangePage(-1));
    }
    if (nextButton != null) {
      nextButton.onClick.RemoveAllListeners();
      nextButton.onClick.AddListener(() => ChangePage(1));
    }
    if (backButton != null) {
      backButton.onClick.RemoveAllListeners();
      backButton.onClick.AddListener(() => CommonUtil.changeScene("TitleScene"));
    }
    if (galleryTabButton != null) {
      galleryTabButton.onClick.RemoveAllListeners();
      galleryTabButton.onClick.AddListener(() => SetMode(MenuMode.Gallery));
    }
    if (characterTabButton != null) {
      characterTabButton.onClick.RemoveAllListeners();
      characterTabButton.onClick.AddListener(() => SetMode(MenuMode.Character));
    }
    if (overlayRoot != null) {
      Button overlayButton = overlayRoot.GetComponent<Button>();
      if (overlayButton != null) {
        overlayButton.onClick.RemoveAllListeners();
        overlayButton.onClick.AddListener(HideOverlay);
      } else {
        overlayButton = overlayRoot.gameObject.AddComponent<Button>();
        Image overlayGraphic = overlayRoot.GetComponent<Image>();
        if (overlayGraphic != null) {
          overlayButton.targetGraphic = overlayGraphic;
        }
        overlayButton.onClick.AddListener(HideOverlay);
      }
      overlayRoot.gameObject.SetActive(false);
    }

    CreateThumbnails(galleryGrid);
    CreateCharacterThumbnails(characterGrid);
    if (overlayRoot != null) {
      SetOverlayPositionX(OVERLAY_HIDE_X);
    }
    return true;
  }

  private void SetMode(MenuMode mode) {
    currentMode = mode;
    bool isGallery = mode == MenuMode.Gallery;
    if (galleryRoot != null) {
      galleryRoot.gameObject.SetActive(isGallery);
    }
    if (characterRoot != null) {
      characterRoot.gameObject.SetActive(!isGallery);
    }
    if (titleText != null) {
      titleText.text = isGallery ? "図鑑" : "キャラ";
    }
    UpdateTabVisuals();
    if (isGallery) {
      UpdatePage();
    } else {
      UpdateCharacterGrid();
    }
  }

  private void EnsureInitialSelection() {
    if (EventSystem.current == null) return;
    if (EventSystem.current.currentSelectedGameObject != null) return;
    if (galleryTabButton != null) {
      EventSystem.current.SetSelectedGameObject(galleryTabButton.gameObject);
      return;
    }
    if (prevButton != null) {
      EventSystem.current.SetSelectedGameObject(prevButton.gameObject);
      return;
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

  private void UpdateTabVisuals() {
    if (galleryTabText != null) {
      galleryTabText.color = currentMode == MenuMode.Gallery ? Color.black : new Color(0.2f, 0.2f, 0.2f, 1f);
    }
    if (characterTabText != null) {
      characterTabText.color = currentMode == MenuMode.Character ? Color.black : new Color(0.2f, 0.2f, 0.2f, 1f);
    }
  }

  private void CreateThumbnails(RectTransform parent) {
    thumbnails.Clear();
    float startX = -((THUMB_COLUMNS - 1) * (THUMB_WIDTH + THUMB_SPACING) * 0.5f);
    float startY = ((THUMB_ROWS - 1) * (THUMB_HEIGHT + THUMB_SPACING) * 0.5f);
    int index = 0;
    for (int row = 0; row < THUMB_ROWS; row++) {
      for (int col = 0; col < THUMB_COLUMNS; col++) {
        Vector2 pos = new Vector2(startX + col * (THUMB_WIDTH + THUMB_SPACING), startY - row * (THUMB_HEIGHT + THUMB_SPACING));
        GalleryThumbnail thumb = CreateThumbnail(parent, pos, index);
        thumbnails.Add(thumb);
        index++;
      }
    }
  }

  private GalleryThumbnail CreateThumbnail(RectTransform parent, Vector2 position, int index) {
    GameObject obj = new GameObject($"thumb_{index}");
    obj.transform.SetParent(parent, false);
    obj.transform.localScale = Vector3.one;
    RectTransform rect = obj.AddComponent<RectTransform>();
    rect.anchorMin = new Vector2(0.5f, 0.5f);
    rect.anchorMax = new Vector2(0.5f, 0.5f);
    rect.pivot = new Vector2(0.5f, 0.5f);
    rect.sizeDelta = new Vector2(THUMB_WIDTH, THUMB_HEIGHT);
    rect.anchoredPosition = position;

    Image image = obj.AddComponent<Image>();
    image.color = new Color(0f, 0f, 0f, 0.6f);
    image.preserveAspect = true;
    Button button = obj.AddComponent<Button>();
    int capturedIndex = index;
    button.onClick.AddListener(() => OnThumbnailClicked(capturedIndex));

    TextMeshProUGUI unknownText = CreateText("unknown_text", rect, 48, TextAlignmentOptions.Center);
    unknownText.rectTransform.anchorMin = Vector2.zero;
    unknownText.rectTransform.anchorMax = Vector2.one;
    unknownText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
    unknownText.rectTransform.sizeDelta = Vector2.zero;
    unknownText.rectTransform.anchoredPosition = Vector2.zero;
    unknownText.text = "?";
    unknownText.color = Color.white;

    return new GalleryThumbnail {
      root = rect,
      image = image,
      button = button,
      unknownText = unknownText
    };
  }


  private void CreateCharacterThumbnails(RectTransform parent) {
    characterThumbnails.Clear();
    float startX = -((CHAR_COLUMNS - 1) * (CHAR_SIZE + CHAR_SPACING) * 0.5f);
    float startY = ((CHAR_ROWS - 1) * (CHAR_SIZE + CHAR_SPACING) * 0.5f);
    int index = 0;
    for (int row = 0; row < CHAR_ROWS; row++) {
      for (int col = 0; col < CHAR_COLUMNS; col++) {
        Vector2 pos = new Vector2(startX + col * (CHAR_SIZE + CHAR_SPACING), startY - row * (CHAR_SIZE + CHAR_SPACING));
        CharacterThumbnail thumb = CreateCharacterThumbnail(parent, pos, index);
        characterThumbnails.Add(thumb);
        index++;
      }
    }
  }

  private CharacterThumbnail CreateCharacterThumbnail(RectTransform parent, Vector2 position, int index) {
    GameObject obj = new GameObject($"char_thumb_{index}");
    obj.transform.SetParent(parent, false);
    obj.transform.localScale = Vector3.one;
    RectTransform rect = obj.AddComponent<RectTransform>();
    rect.anchorMin = new Vector2(0.5f, 0.5f);
    rect.anchorMax = new Vector2(0.5f, 0.5f);
    rect.pivot = new Vector2(0.5f, 0.5f);
    rect.sizeDelta = new Vector2(CHAR_SIZE, CHAR_SIZE);
    rect.anchoredPosition = position;

    Image image = obj.AddComponent<Image>();
    image.color = new Color(0f, 0f, 0f, 0.6f);
    image.preserveAspect = true;
    Button button = obj.AddComponent<Button>();
    int capturedIndex = index;
    button.onClick.AddListener(() => OnCharacterClicked(capturedIndex));

    TextMeshProUGUI nameText = CreateText("char_name", rect, 20, TextAlignmentOptions.Center);
    nameText.rectTransform.anchorMin = new Vector2(0f, 0f);
    nameText.rectTransform.anchorMax = new Vector2(1f, 0f);
    nameText.rectTransform.pivot = new Vector2(0.5f, 0f);
    nameText.rectTransform.sizeDelta = new Vector2(0f, 24f);
    nameText.rectTransform.anchoredPosition = new Vector2(0f, 4f);
    nameText.color = Color.black;

    TextMeshProUGUI unknownText = CreateText("unknown_text", rect, 48, TextAlignmentOptions.Center);
    unknownText.rectTransform.anchorMin = Vector2.zero;
    unknownText.rectTransform.anchorMax = Vector2.one;
    unknownText.rectTransform.pivot = new Vector2(0.5f, 0.5f);
    unknownText.rectTransform.sizeDelta = Vector2.zero;
    unknownText.rectTransform.anchoredPosition = Vector2.zero;
    unknownText.text = "?";
    unknownText.color = Color.black;

    return new CharacterThumbnail {
      root = rect,
      image = image,
      button = button,
      nameText = nameText,
      unknownText = unknownText
    };
  }


  private void UpdatePage() {
    int totalPages = Mathf.Max(1, Mathf.CeilToInt(gallerySprites.Count / (float)pageSize));
    currentPage = Mathf.Clamp(currentPage, 0, totalPages - 1);
    if (pageText != null) {
      pageText.text = $"{currentPage + 1}/{totalPages}";
    }
    if (prevButton != null) {
      prevButton.interactable = currentPage > 0;
    }
    if (nextButton != null) {
      nextButton.interactable = currentPage < totalPages - 1;
    }

    for (int i = 0; i < thumbnails.Count; i++) {
      int spriteIndex = currentPage * pageSize + i;
      GalleryThumbnail thumb = thumbnails[i];
      if (spriteIndex >= 0 && spriteIndex < gallerySprites.Count) {
        Sprite sprite = gallerySprites[spriteIndex];
        bool seen = IsSeen(sprite.name);
        thumb.sprite = seen ? sprite : null;
        thumb.spriteName = sprite.name;
        thumb.image.sprite = thumb.sprite;
        thumb.image.color = seen ? Color.white : new Color(0f, 0f, 0f, 0.6f);
        thumb.button.interactable = seen;
        thumb.unknownText.gameObject.SetActive(!seen);
      } else {
        thumb.sprite = null;
        thumb.spriteName = "";
        thumb.image.sprite = null;
        thumb.image.color = new Color(0f, 0f, 0f, 0.3f);
        thumb.button.interactable = false;
        thumb.unknownText.gameObject.SetActive(false);
      }
    }
  }

  private void UpdateCharacterGrid() {
    for (int i = 0; i < characterThumbnails.Count; i++) {
      CharacterThumbnail thumb = characterThumbnails[i];
      if (i >= characterEntries.Count) {
        thumb.image.sprite = null;
        thumb.image.color = new Color(0f, 0f, 0f, 0.3f);
        thumb.button.interactable = false;
        thumb.nameText.text = "";
        thumb.unknownText.gameObject.SetActive(false);
        continue;
      }
      CharacterEntry entry = characterEntries[i];
      bool unlocked = IsCharacterUnlocked(entry);
      thumb.entry = entry;
      thumb.image.sprite = unlocked ? Resources.Load<Sprite>($"Textures/{entry.spriteKey}") : null;
      thumb.image.color = unlocked ? Color.white : new Color(0f, 0f, 0f, 0.6f);
      thumb.button.interactable = unlocked;
      thumb.nameText.text = unlocked ? entry.displayName : "？？？";
      thumb.unknownText.gameObject.SetActive(!unlocked);
    }
  }

  private void ChangePage(int delta) {
    currentPage += delta;
    UpdatePage();
  }

  private void OnThumbnailClicked(int index) {
    if (index < 0 || index >= thumbnails.Count) return;
    GalleryThumbnail thumb = thumbnails[index];
    if (thumb.sprite == null) return;
    ShowOverlay(thumb.sprite, "", "");
  }

  private void OnCharacterClicked(int index) {
    if (index < 0 || index >= characterThumbnails.Count) return;
    CharacterThumbnail thumb = characterThumbnails[index];
    if (thumb.entry == null) return;
    if (!IsCharacterUnlocked(thumb.entry)) return;
    Sprite sprite = Resources.Load<Sprite>($"Textures/{thumb.entry.spriteKey}");
    string stats = BuildCharacterStats(thumb.entry.id);
    ShowOverlay(sprite, thumb.entry.displayName, stats);
  }

  private void ShowOverlay(Sprite sprite, string title, string stats) {
    if (overlayRoot == null || overlayImage == null) return;
    overlayRoot.gameObject.SetActive(true);
    ApplyOverlayLayout(string.IsNullOrEmpty(stats) ? OverlayLayout.GalleryImage : OverlayLayout.CharacterDetail);
    overlayImage.sprite = sprite;
    if (overlayTitleText != null) {
      overlayTitleText.text = title;
      overlayTitleText.gameObject.SetActive(!string.IsNullOrEmpty(title));
    }
    if (overlayStatsText != null) {
      overlayStatsText.text = stats;
      overlayStatsText.gameObject.SetActive(!string.IsNullOrEmpty(stats));
    }
    overlayRoot.transform.SetAsLastSibling();
    SetOverlayPositionX(0f);
    overlayRoot.gameObject.SetActive(true);
  }

  private void HideOverlay() {
    if (overlayRoot == null) return;
    SetOverlayPositionX(OVERLAY_HIDE_X);
    overlayRoot.gameObject.SetActive(false);
  }

  private void ApplyOverlayLayout(OverlayLayout layout) {
    if (overlayImageRect == null) return;
    bool isCharacter = layout == OverlayLayout.CharacterDetail;
    if (isCharacter) {
      if (characterOverlayBg == null) {
        characterOverlayBg = Resources.Load<Sprite>(CHARACTER_BG_RESOURCE);
      }
      if (overlayBackgroundImage != null) {
        overlayBackgroundImage.sprite = characterOverlayBg;
        overlayBackgroundImage.color = Color.white;
        overlayBackgroundImage.preserveAspect = true;
      }
      overlayImageRect.sizeDelta = new Vector2(CHARACTER_IMAGE_SIZE, CHARACTER_IMAGE_SIZE);
      overlayImageRect.anchoredPosition = new Vector2(CHARACTER_IMAGE_X, 0f);
      overlayImage.preserveAspect = true;
      if (overlayTitleRect != null) {
        overlayTitleRect.sizeDelta = new Vector2(CHARACTER_TITLE_WIDTH, CHARACTER_TITLE_HEIGHT);
        overlayTitleRect.anchoredPosition = new Vector2(CHARACTER_TITLE_X, CHARACTER_TITLE_Y);
      }
      if (overlayTitleText != null) {
        overlayTitleText.alignment = TextAlignmentOptions.Left;
        overlayTitleText.color = Color.black;
      }
      if (overlayStatsRect != null) {
        overlayStatsRect.sizeDelta = new Vector2(CHARACTER_STATS_WIDTH, CHARACTER_STATS_HEIGHT);
        overlayStatsRect.anchoredPosition = new Vector2(CHARACTER_STATS_X, CHARACTER_STATS_Y);
      }
      if (overlayStatsText != null) {
        overlayStatsText.alignment = TextAlignmentOptions.TopLeft;
        overlayStatsText.color = Color.black;
      }
      return;
    }

    if (overlayBackgroundImage != null) {
      overlayBackgroundImage.sprite = null;
      overlayBackgroundImage.color = new Color(0f, 0f, 0f, 0.85f);
      overlayBackgroundImage.preserveAspect = false;
    }
    overlayImageRect.sizeDelta = new Vector2(960f, 540f);
    overlayImageRect.anchoredPosition = Vector2.zero;
    overlayImage.preserveAspect = true;
    if (overlayTitleRect != null) {
      overlayTitleRect.sizeDelta = new Vector2(640f, 40f);
      overlayTitleRect.anchoredPosition = new Vector2(0f, OVERLAY_TITLE_Y);
    }
    if (overlayTitleText != null) {
      overlayTitleText.alignment = TextAlignmentOptions.Center;
      overlayTitleText.color = Color.white;
    }
    if (overlayStatsRect != null) {
      overlayStatsRect.sizeDelta = new Vector2(640f, 120f);
      overlayStatsRect.anchoredPosition = new Vector2(0f, OVERLAY_STATS_Y);
    }
    if (overlayStatsText != null) {
      overlayStatsText.alignment = TextAlignmentOptions.Center;
      overlayStatsText.color = Color.white;
    }
  }

  private string BuildCharacterStats(string id) {
    if (id == "kappa") {
      int maxHp = Mathf.Max(1, DataMgr.GetInt("max_hp"));
      int atk = Mathf.Max(1, DataMgr.GetInt("atk"));
      int agi = Mathf.Max(1, DataMgr.GetInt("agi"));
      return $"最大HP {maxHp}\n攻撃力 {atk}\nすばやさ {agi}";
    }
    if (id == "usagi") {
      return "最大HP 5\n攻撃力 3\nすばやさ 4";
    }
    if (id == "tanuki") {
      return "最大HP 7\n攻撃力 2\nすばやさ 2";
    }
    if (id == "slime") {
      return "最大HP 5\n攻撃力 1\nすばやさ 5";
    }
    if (id == "dogs") {
      return "ガイア 最大HP 4 攻撃力 1 すばやさ 30\nマッシュ 最大HP 6 攻撃力 1 すばやさ 40\nオルテガ 最大HP 1 攻撃力 1 すばやさ 60";
    }
    if (id == "maou") {
      return "最大HP 40\n攻撃力 4\nすばやさ 10";
    }
    return "";
  }

  private bool IsSeen(string spriteName) {
    if (string.IsNullOrEmpty(spriteName)) return false;
    return DataMgr.GetBool($"{GALLERY_SEEN_PREFIX}{spriteName}");
  }

  private bool IsCharacterUnlocked(CharacterEntry entry) {
    if (entry == null) return false;
    if (entry.alwaysVisible) return true;
    return DataMgr.GetBool($"{GALLERY_CHAR_SEEN_PREFIX}{entry.id}");
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

  private TextMeshProUGUI CreateText(string name, RectTransform parent, float fontSize, TextAlignmentOptions alignment) {
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
    text.raycastTarget = false;
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

  private RectTransform GetRectByName(string name) {
    return FindSceneComponent<RectTransform>(name);
  }

  private Button GetButtonByName(string name) {
    return FindSceneComponent<Button>(name);
  }

  private TextMeshProUGUI GetTextByName(string name) {
    return FindSceneComponent<TextMeshProUGUI>(name);
  }

  private Image GetImageByName(string name) {
    return FindSceneComponent<Image>(name);
  }

  private T FindSceneComponent<T>(string name) where T : Component {
    if (sceneCanvasRect == null || string.IsNullOrEmpty(name)) return null;
    T[] components = sceneCanvasRect.GetComponentsInChildren<T>(true);
    foreach (T component in components) {
      if (component == null) continue;
      if (component.name != name) continue;
      return component;
    }
    return null;
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
  }

  private void SetOverlayPositionX(float x) {
    if (overlayRoot == null) return;
    Vector2 pos = overlayRoot.anchoredPosition;
    pos.x = x;
    overlayRoot.anchoredPosition = pos;
  }

  private void ForceHideOverlayRoot() {
    RectTransform root = FindSceneComponent<RectTransform>("OverlayRoot");
    if (root == null) return;
    Vector2 pos = root.anchoredPosition;
    pos.x = OVERLAY_HIDE_X;
    root.anchoredPosition = pos;
    root.gameObject.SetActive(false);
  }

  private class GalleryThumbnail {
    public RectTransform root;
    public Image image;
    public Button button;
    public TextMeshProUGUI unknownText;
    public Sprite sprite;
    public string spriteName;
  }

  private class CharacterEntry {
    public string id;
    public string displayName;
    public string spriteKey;
    public bool alwaysVisible;

    public CharacterEntry(string id, string displayName, string spriteKey, bool alwaysVisible) {
      this.id = id;
      this.displayName = displayName;
      this.spriteKey = spriteKey;
      this.alwaysVisible = alwaysVisible;
    }
  }

  private class CharacterThumbnail {
    public RectTransform root;
    public Image image;
    public Button button;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI unknownText;
    public CharacterEntry entry;
  }
}
