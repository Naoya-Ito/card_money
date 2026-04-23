using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameSceneMgr : MonoBehaviour {

  [SerializeField] public GameObject main_text_area;
  [SerializeField] public GameObject tap_screen_area;
  [SerializeField] public Image image;
  [SerializeField] private Image main_image_ui;
  [SerializeField] public TextMeshProUGUI main_text;
  [SerializeField] public TextMeshProUGUI timer_text;
  [SerializeField] private TextMeshProUGUI debug_page_text;
  [SerializeField] private RectTransform status_popup_root;
  [SerializeField] private TextMeshProUGUI status_popup_text;
  [SerializeField] private Image status_popup_icon;
  [SerializeField] private TextMeshProUGUI hp_text_ui;
  [SerializeField] private TextMeshProUGUI gold_text_ui;
  [SerializeField] private TextMeshProUGUI gold_change_text;
  [SerializeField] private TextMeshProUGUI hp_change_text;
  [SerializeField] public ImageBinarizerByScript faderScript;
  [System.NonSerialized] public PageModel page;
  private string current_page_key = "";
  private float default_main_text_size = 0f;
  private TextAlignmentOptions default_main_text_alignment = TextAlignmentOptions.Left;
  private float default_timer_text_size = 0f;
  private Color default_timer_text_color = Color.white;
  private bool is_timer_pulse_active = false;
  private Vector2 status_popup_shown_pos = Vector2.zero;
  private Vector2 status_popup_hidden_pos = Vector2.zero;
  private bool status_popup_initialized = false;
  private bool is_status_popup_pinned = false;
  private Sprite status_popup_default_sprite;
  private int prev_hp = 0;
  private int prev_max_hp = 0;
  private int prev_hp_ui = int.MinValue;
  private int prev_max_hp_ui = int.MinValue;
  private int prev_gold_ui = int.MinValue;
  private int prev_atk = 0;
  private int prev_agi = 0;
  private int prev_charm = 0;
  private int prev_wis = 0;
  [SerializeField] private RectTransform name_area_rect;
  [SerializeField] private TextMeshProUGUI name_text;
  [SerializeField] private RectTransform main_bg_rect;
  [SerializeField] private RectTransform left_bg_rect;
  [SerializeField] private Image left_bg_image;
  [SerializeField] private Image left_main_image;
  [SerializeField] private RectTransform right_bg_rect;
  [SerializeField] private Image right_bg_image;
  [SerializeField] private RectTransform castle_pos;
  [SerializeField] private RectTransform shiori_pos;
  [SerializeField] private RectTransform slime_pos;
  [SerializeField] private RectTransform town_pos;
  [SerializeField] private RectTransform forest_or_mountain_pos;
  [SerializeField] private RectTransform maou_pos;
  private bool is_map_move_active = false;
  private Transform map_move_prev_parent;
  private bool map_move_reparented = false;
  private GameObject lockedNoticeRoot;
  private Image lockedNoticeUsagi;
  private TextMeshProUGUI lockedNoticeText;
  private bool isLockedNoticeActive = false;
  private Vector2 lockedPrevMainPos;
  private Vector2 lockedPrevLeftPos;
  private Sprite lockedPrevLeftSprite;
  private bool lockedPrevChoiceAreaActive;
  private bool lockedPrevChoiceTitleActive;
  private bool lockedPrevChoiceTitleParentActive;
  private GameObject lockedChoiceTitleParent;
  private bool lockedPrevMainTextAreaActive;
  private string lockedPrevMainText = "";
  private string lockedPrevSpeaker = "";
  private bool lockedPrevNameAreaActive;
  private Sprite lockedPrevOverlaySprite;
  private bool lockedPrevOverlayActive;
  private Sprite lockedPrevMainImageSprite;
  private bool lockedPrevMainImageActive;
  private string localizedMainText = "";
  private string localizedSpeaker = "";
  public bool IsLockedNoticeActive => isLockedNoticeActive;
  [SerializeField] private float bg_scroll_speed = 0.3f;
  private bool is_bg_scrolling = false;
  private Material bg_scroll_material;
  private Vector2 bg_scroll_offset = Vector2.zero;
  private bool is_nandate_animating = false;
  private Coroutine battleCoroutine;
  private List<BattleFighter> battleFighters = new List<BattleFighter>();
  private List<BattleFighter> battleOrder = new List<BattleFighter>();
  private int battleOrderIndex = 0;
  private System.Random battleRandom = new System.Random();
  private const float BATTLE_TURN_DURATION = 0.5f;
  private const string BATTLE_PAGE_MAOU = "maou/battle";
  private const string BATTLE_PAGE_SLIME = "slime/battle";
  private const string BATTLE_PAGE_DOGS = "maou/dog_battle";
  private const string BATTLE_EVADE_POPUP_TEXT = "ミス";
  private const string BATTLE_EVADE_BUBBLE_TEXT = "残像だ";
  private const string GALLERY_IMAGE_PREFIX = "240_135/";
  private const string GALLERY_SEEN_PREFIX = "gallery_seen_";
  private const string GALLERY_CHAR_SEEN_PREFIX = "gallery_char_seen_";
  private const int SHIORI_CHARM_THRESHOLD = 7;
  private const string AUTO_SAVE_PAGE_KEY = "autosave_page";
  private BattleConfig currentBattleConfig;
  private GameObject battleUiRoot;
  private RectTransform battleUiRect;
  private string currentBattleKey = "";
  [SerializeField] private RectTransform battle_bg_rect;
  [SerializeField] private GameObject vs_text;
  [SerializeField] private GameObject life_gage_box;
  [SerializeField] private Slider life_gage;
  [SerializeField] private TextMeshProUGUI life_text;
  [SerializeField] private GameObject enemy_life_gage_box;
  [SerializeField] private Slider enemy_life_gage;
  [SerializeField] private TextMeshProUGUI enemy_life_text;
  [SerializeField] private Image battle_kappa_image;
  [SerializeField] private Image enemy_image;
  private GameObject damageTextPrefab;
  private GameObject sparkParticlePrefab;
  private GameObject swordEffectPrefab;
  private GameObject crowEffectPrefab;
  private GameObject impactEffectPrefab;
  private GameObject fireEffectPrefab;
  private bool isBattleWinPending = false;
  private bool isAllyDeathPending = false;
  private bool isDiceActive = false;
  private bool isDiceRolling = false;
  private bool isDiceResultReady = false;
  private Coroutine diceCoroutine;
  private GameObject diceRoot;
  private Image diceLeftImage;
  private Image diceRightImage;
  private Image diceBackdrop;
  private int diceLeftValue = 1;
  private int diceRightValue = 1;
  private string diceSuccessKey = "";
  private string diceFailKey = "";
  private int diceStatValue = 0;
  private int diceThreshold = 0;
  private bool diceShowResultImages = false;
  private string diceResultSuccessFirstKey = "";
  private string diceResultSuccessSecondKey = "";
  private string diceResultFailFirstKey = "";
  private string diceResultFailSecondKey = "";
  private System.Action diceSuccessAction;
  private System.Action diceFailAction;
  private readonly List<Image> diceResultImages = new List<Image>();
  private RectTransform dogIntroRoot;
  private Image dogIntroGaiaImage;
  private Image dogIntroMashImage;
  private Image dogIntroOrtegaImage;
  private Vector2 hp_change_text_base_pos = Vector2.zero;
  private Vector2 gold_change_text_base_pos = Vector2.zero;
  private Color hp_change_damage_color = new Color(1f, 0.23137255f, 0.1882353f, 1f);
  private Color gold_change_loss_color = new Color(1f, 0.23137255f, 0.1882353f, 1f);
  private readonly Color HP_CHANGE_HEAL_COLOR = new Color(0.2f, 0.8f, 0.35f, 1f);
  private readonly Color GOLD_CHANGE_GAIN_COLOR = new Color(0.2f, 0.8f, 0.35f, 1f);
  private static readonly Vector2 BATTLE_BG_START_POS = new Vector2(0f, 540f);
  private static readonly Vector2 BATTLE_BG_ACTIVE_POS = Vector2.zero;
  private const float BATTLE_BG_ENTER_DURATION = 0.35f;
  private const float BATTLE_VS_VISIBLE_DURATION = 0.5f;
  private const int BATTLE_ALLY_MAX_HP = 100;
  private Coroutine battleIntroCoroutine;
  private bool isBattleIntroPlaying = false;
  private Vector2 battle_kappa_initial_pos = Vector2.zero;
  private Vector2 battle_enemy_initial_pos = Vector2.zero;
  private bool battle_kappa_initial_cached = false;
  private bool battle_enemy_initial_cached = false;

  private sealed class LockedChoiceReaction {
    public string backgroundSpriteKey;
    public string spriteKey;
    public string textKey;
    public string textFallback;
    public string speakerKey;
    public string speakerFallback;
  }

  private const float MAX_LIMIT_TIME = 180f;
  private const string LOCKED_CHOICE_BG_SPRITE_KEY = "other/cutin";
  [System.NonSerialized] public float limit_time = MAX_LIMIT_TIME;
  /*

  // TODO コントローラー操作に対応
  // TODO 実績システムを追加
  // TODO ゲーム内実績システムを追加
  // TODO シーン追加: そんな装備で大丈夫っすか？　大丈夫だ、問題ない
  // TODO シーン追加: でもただのリトライじゃねえぞ。どリトライだ！
  // TODO シーン追加: 余命3分の少女
  // TODO シーン追加: 傭兵　俺を雇うかい？
  // TODO シーン追加: 筋トレする(10%の確率で攻撃力+1)
  // TODO シーン追加: パシリのクエスト
  // TODO シーン追加: Hey Yo! そこのカッパ〜。 俺らはラッパ〜
  // TODO エンディングのストーリーを実装
  // TODO キャラのステータスを見るスキル(メガネをかけさせる？)
  // TODO ゲーム公開時はデバッグモードをデフォルトOFFにする

  // story

    // 選択肢1

    // 選択肢2
    // game over くにへ帰るんだな。お前にも家族が待っているだろう。


  // ヒメとの会話
    // 急いで出発せねば！
    // ヒメと話す
      // 
      // ステータス？　カード？
      // 彼氏とかいる？　　攻撃力+1
        // 好きなタイプは？
        // プロポーズする
          // 魅力 > 10
            // 正気度-30
  //  おや、道端にスライムが！
    // 雑魚なんて無視だ
    // ぶったおす！
      // ぷるぷる、ぼく悪いスライムじゃないよ
        // それでもぶったおす！
          // うへー。　　経験値+1
            // レベルアップまで繰り返す
        // 仲間に誘う
          // カード入手　ぷるぷる震える
            // なんの効果もないカードだ。つまり、ラスボス戦でこのカードを引いたら1ターン損するという事だ。


  */

  [System.NonSerialized] public static GameSceneMgr instance = null;
  public string CurrentPageKey => current_page_key;

  private void Awake(){
    if(instance == null) {
      instance = this;
    } else {
      Destroy(this.gameObject);
    }
  }

  void Start() {
    if (main_bg_rect == null && image != null) {
      main_bg_rect = image.GetComponent<RectTransform>();
    }
    if (left_bg_rect == null) {
      GameObject left_bg_obj = GameObject.Find("left_bg");
      if (left_bg_obj != null) {
        left_bg_rect = left_bg_obj.GetComponent<RectTransform>();
        left_bg_image = left_bg_obj.GetComponent<Image>();
      }
    }
    if (right_bg_rect == null) {
      GameObject right_bg_obj = GameObject.Find("right_bg");
      if (right_bg_obj != null) {
        right_bg_rect = right_bg_obj.GetComponent<RectTransform>();
        right_bg_image = right_bg_obj.GetComponent<Image>();
      }
    }
    if (castle_pos == null) {
      GameObject posObj = GameObject.Find("castle_pos");
      if (posObj != null) {
        castle_pos = posObj.GetComponent<RectTransform>();
      }
    }
    if (shiori_pos == null) {
      GameObject posObj = GameObject.Find("shiori_pos");
      if (posObj != null) {
        shiori_pos = posObj.GetComponent<RectTransform>();
      }
    }
    if (slime_pos == null) {
      GameObject posObj = GameObject.Find("slime_pos");
      if (posObj != null) {
        slime_pos = posObj.GetComponent<RectTransform>();
      }
    }
    if (town_pos == null) {
      GameObject posObj = GameObject.Find("town_pos");
      if (posObj != null) {
        town_pos = posObj.GetComponent<RectTransform>();
      }
    }
    if (forest_or_mountain_pos == null) {
      GameObject posObj = GameObject.Find("forest_or_mountain_pos");
      if (posObj != null) {
        forest_or_mountain_pos = posObj.GetComponent<RectTransform>();
      }
    }
    if (maou_pos == null) {
      GameObject posObj = GameObject.Find("maou_pos");
      if (posObj != null) {
        maou_pos = posObj.GetComponent<RectTransform>();
      }
    }
    initNameArea();
    applyTextOutline(main_text);
    applyTextOutline(name_text);
    if (main_text != null) {
      default_main_text_size = main_text.fontSize;
      default_main_text_alignment = main_text.alignment;
    }
    if (timer_text != null) {
      default_timer_text_size = timer_text.fontSize;
      default_timer_text_color = timer_text.color;
      timer_text.textWrappingMode = TMPro.TextWrappingModes.NoWrap;
      timer_text.overflowMode = TextOverflowModes.Overflow;
    }
    initStatusPopup();
    initHpBox();
    InitBattleSceneUiRefs();
    float saved_limit_time = DataMgr.GetFloat("limit_time");
    if (saved_limit_time > 0f) {
      limit_time = saved_limit_time;
    }
    cacheCurrentStats();
    InitBgScrollMaterial();
    string key = DataMgr.GetStr("page");
    updateScene(key);
  }

  // Update is called once per frame
  private float span = 1.0f;
  private float time_second = 0.0f;
  void Update() {
    limit_time -= Time.deltaTime;
    DataMgr.SetFloat("limit_time", limit_time);
    updateFadeProgress();
    updateBgScroll();
    time_second += Time.deltaTime;
    if(time_second > span) {
      time_second = 0.0f;
      execPerSecond();
    }
    HandlePageAdvanceInput();
    HandleLockedNoticePointerInput();
    updateHpBox();
    updateGoldBox();
    updateStatusPopup();
  }

  private void HandlePageAdvanceInput() {
    bool pressed = false;
    if (Keyboard.current != null) {
      pressed = Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame;
    }
    if (Gamepad.current != null) {
      pressed = pressed || Gamepad.current.buttonSouth.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame;
    }
    if (!pressed) return;
    if (isLockedNoticeActive) {
      HideLockedChoiceNotice();
      return;
    }
    tappedScreen();
  }

  private void HandleLockedNoticePointerInput() {
    if (!isLockedNoticeActive) return;
    if (Mouse.current == null) return;
    if (!Mouse.current.leftButton.wasPressedThisFrame) return;
    HideLockedChoiceNotice();
  }

  // 1秒毎の実行処理
  private void execPerSecond(){
    int remainingSeconds = Mathf.Max(0, Mathf.FloorToInt(limit_time));
    timer_text.text = getTimeText(remainingSeconds);
    updateTimerTextEffect(remainingSeconds);
    if (limit_time < 0) {
      timeOver();
    }
  }

  private string getTimeText(int totalSeconds) {
    int min = totalSeconds / 60;
    int sec = totalSeconds % 60;
    return $"{min:D2}:{sec:D2}";
  }

  private void updateTimerTextEffect(int remainingSeconds) {
    if (timer_text == null) return;
    bool isLastMinute = remainingSeconds < 60;
    Color baseColor = isLastMinute ? Color.red : default_timer_text_color;
    if (!is_timer_pulse_active) {
      timer_text.color = baseColor;
      timer_text.fontSize = default_timer_text_size;
    }
    if (remainingSeconds == 120 || remainingSeconds == 60) {
      playTimerPulse(baseColor);
      return;
    }
    if (isLastMinute && remainingSeconds > 0 && remainingSeconds % 10 == 0) {
      playTimerPulse(baseColor);
    }
  }

  private void playTimerPulse(Color baseColor) {
    if (timer_text == null) return;
    if (is_timer_pulse_active) return;
    is_timer_pulse_active = true;
    DOTween.Kill(timer_text);
    float targetSize = default_timer_text_size * 1.4f;
    Sequence seq = DOTween.Sequence();
    seq.SetTarget(timer_text);
    seq.Append(DOTween.To(() => timer_text.fontSize, x => timer_text.fontSize = x, targetSize, 0.15f));
    seq.Join(DOTween.To(() => timer_text.color, x => timer_text.color = x, Color.red, 0.15f));
    seq.Append(DOTween.To(() => timer_text.fontSize, x => timer_text.fontSize = x, default_timer_text_size, 0.2f));
    seq.Join(DOTween.To(() => timer_text.color, x => timer_text.color = x, baseColor, 0.2f));
    seq.OnComplete(() => {
      timer_text.fontSize = default_timer_text_size;
      timer_text.color = baseColor;
      is_timer_pulse_active = false;
    });
  }

  private void updateFadeProgress() {
    if (faderScript == null) return;
    float progress = 1f - (limit_time / MAX_LIMIT_TIME);
    faderScript.SetProgress(progress);
  }

  private void initStatusPopup() {
    if (status_popup_root == null || status_popup_text == null || status_popup_icon == null) {
      GameObject rootObj = GameObject.Find("status_popup_root");
      if (rootObj != null) {
        status_popup_root = rootObj.GetComponent<RectTransform>();
        status_popup_text = rootObj.GetComponentInChildren<TextMeshProUGUI>(true);
        Transform iconTransform = rootObj.transform.Find("status_popup_icon");
        if (iconTransform != null) {
          status_popup_icon = iconTransform.GetComponent<Image>();
        }
      }
    }
    if (status_popup_icon != null && status_popup_default_sprite == null) {
      status_popup_default_sprite = status_popup_icon.sprite;
    }
    if (status_popup_root == null || status_popup_text == null) return;
    status_popup_shown_pos = status_popup_root.anchoredPosition;
    float offset = status_popup_root.sizeDelta.x + 16f;
    status_popup_hidden_pos = status_popup_shown_pos + new Vector2(-offset, 0f);
    status_popup_root.anchoredPosition = status_popup_hidden_pos;
    status_popup_initialized = true;
  }

  private void initHpBox() {
    if (gold_text_ui == null) {
      GameObject goldTextObj = GameObject.Find("GoldText");
      if (goldTextObj == null) {
        goldTextObj = GameObject.Find("gold_text");
      }
      if (goldTextObj != null) {
        gold_text_ui = goldTextObj.GetComponent<TextMeshProUGUI>();
      }
    }
    if (gold_change_text == null) {
      GameObject goldChangeTextObj = GameObject.Find("GoldChangeText");
      if (goldChangeTextObj == null) {
        goldChangeTextObj = GameObject.Find("gold_change_text");
      }
      if (goldChangeTextObj != null) {
        gold_change_text = goldChangeTextObj.GetComponent<TextMeshProUGUI>();
      }
    }

    if (hp_change_text != null) {
      hp_change_damage_color = hp_change_text.color;
      hp_change_text_base_pos = hp_change_text.rectTransform.anchoredPosition;
      hp_change_text.text = "";
      Color hiddenColor = hp_change_text.color;
      hiddenColor.a = 0f;
      hp_change_text.color = hiddenColor;
    }
    if (gold_change_text != null) {
      gold_change_loss_color = gold_change_text.color;
      gold_change_text_base_pos = gold_change_text.rectTransform.anchoredPosition;
      gold_change_text.text = "";
      Color hiddenColor = gold_change_text.color;
      hiddenColor.a = 0f;
      gold_change_text.color = hiddenColor;
    }

    updateHpBox(true);
    updateGoldBox(true);
  }

  private void InitBattleSceneUiRefs() {
    ValidateBattleSceneUiRefs();
    CacheBattleCharacterInitialStates();

    if (battle_bg_rect != null) {
      battle_bg_rect.anchoredPosition = BATTLE_BG_START_POS;
      battle_bg_rect.gameObject.SetActive(false);
    }
    if (vs_text != null) {
      vs_text.SetActive(false);
    }
    if (life_gage_box != null) {
      life_gage_box.SetActive(false);
    }
    if (enemy_life_gage_box != null) {
      enemy_life_gage_box.SetActive(false);
    }
    if (enemy_image != null) {
      enemy_image.gameObject.SetActive(false);
    }
    if (battle_kappa_image != null) {
      battle_kappa_image.gameObject.SetActive(false);
    }
  }

  private void CacheBattleCharacterInitialStates() {
    if (!battle_kappa_initial_cached && battle_kappa_image != null) {
      battle_kappa_initial_pos = battle_kappa_image.rectTransform.anchoredPosition;
      battle_kappa_initial_cached = true;
    }
    if (!battle_enemy_initial_cached && enemy_image != null) {
      battle_enemy_initial_pos = enemy_image.rectTransform.anchoredPosition;
      battle_enemy_initial_cached = true;
    }
  }

  private void ResetBattleCharacterTransform(Image imageComp, Vector2 initialPos) {
    if (imageComp == null) return;
    RectTransform rect = imageComp.rectTransform;
    rect.DOKill();
    rect.anchoredPosition = initialPos;
    rect.localEulerAngles = Vector3.zero;
    rect.localScale = Vector3.one;
    CanvasGroup group = rect.GetComponent<CanvasGroup>();
    if (group != null) {
      group.DOKill();
      group.alpha = 1f;
    }
    imageComp.gameObject.SetActive(true);
  }

  private void ValidateBattleSceneUiRefs() {
    if (battle_bg_rect == null) {
      Debug.LogError("battle_bg_rect is not assigned in Inspector.");
    }
    if (vs_text == null) {
      Debug.LogError("vs_text is not assigned in Inspector.");
    }
    if (life_gage_box == null) {
      Debug.LogError("life_gage_box is not assigned in Inspector.");
    }
    if (life_gage == null) {
      Debug.LogError("life_gage is not assigned in Inspector.");
    }
    if (life_text == null) {
      Debug.LogError("life_text is not assigned in Inspector.");
    }
    if (enemy_life_gage_box == null) {
      Debug.LogError("enemy_life_gage_box is not assigned in Inspector.");
    }
    if (enemy_life_gage == null) {
      Debug.LogError("enemy_life_gage is not assigned in Inspector.");
    }
    if (enemy_life_text == null) {
      Debug.LogError("enemy_life_text is not assigned in Inspector.");
    }
    if (enemy_image == null) {
      Debug.LogError("enemy_image is not assigned in Inspector.");
    }
    if (battle_kappa_image == null) {
      Debug.LogError("battle_kappa_image is not assigned in Inspector.");
    }
  }

  public void ShowAllyStatusPopup(string text) {
    ShowAllyStatusPopup(text, "");
  }

  public void ShowAllyStatusPopup(string text, string iconKey) {
    if (!status_popup_initialized) return;
    if (!string.IsNullOrEmpty(iconKey)) {
      SetStatusPopupIcon(iconKey);
    } else {
      ResetStatusPopupIcon();
    }
    playStatusPopupPersistent(text);
  }

  public void HideAllyStatusPopup() {
    if (!status_popup_initialized) return;
    if (!is_status_popup_pinned) return;
    if (status_popup_root == null) return;
    status_popup_root.DOKill();
    Sequence seq = DOTween.Sequence();
    seq.SetLink(status_popup_root.gameObject);
    seq.Append(status_popup_root.DOAnchorPos(status_popup_hidden_pos, 0.3f).SetEase(Ease.InQuad));
    seq.OnComplete(() => {
      is_status_popup_pinned = false;
    });
  }

  private void cacheCurrentStats() {
    prev_hp = DataMgr.GetInt("hp_kappa");
    prev_max_hp = DataMgr.GetInt("max_hp");
    prev_hp_ui = prev_hp;
    prev_max_hp_ui = prev_max_hp;
    prev_atk = DataMgr.GetInt("atk");
    prev_agi = DataMgr.GetInt("agi");
    prev_charm = DataMgr.GetInt("charm");
    prev_wis = DataMgr.GetInt("wis");
  }

  private void updateStatusPopup() {
    if (!status_popup_initialized) return;
    if (status_popup_root == null || status_popup_text == null) return;
    if (is_status_popup_pinned) return;
    if (IsBattlePage(current_page_key)) return;
    int hp = DataMgr.GetInt("hp_kappa");
    int maxHp = DataMgr.GetInt("max_hp");
    int atk = DataMgr.GetInt("atk");
    int agi = DataMgr.GetInt("agi");
    int charm = DataMgr.GetInt("charm");
    int wis = DataMgr.GetInt("wis");

    bool maxHpChanged = maxHp != prev_max_hp;
    int deltaAtk = atk - prev_atk;
    int deltaAgi = agi - prev_agi;
    int deltaCharm = charm - prev_charm;
    int deltaWis = wis - prev_wis;
    int deltaMaxHp = maxHp - prev_max_hp;
    bool atkChanged = deltaAtk != 0;
    bool agiChanged = deltaAgi != 0;
    bool charmChanged = deltaCharm != 0;
    bool wisChanged = deltaWis != 0;
    bool anyChanged = maxHpChanged || atkChanged || agiChanged || charmChanged || wisChanged;

    if (anyChanged) {
      List<string> lines = new List<string>();
      if (maxHpChanged) lines.Add($"最大HP {maxHp} ({FormatDelta(deltaMaxHp)})");
      if (atkChanged) lines.Add($"攻撃力 {atk} ({FormatDelta(deltaAtk)})");
      if (agiChanged) lines.Add($"すばやさ {agi} ({FormatDelta(deltaAgi)})");
      if (charmChanged) lines.Add($"魅力 {charm} ({FormatDelta(deltaCharm)})");
      if (wisChanged) lines.Add($"かしこさ {wis} ({FormatDelta(deltaWis)})");
      playStatusPopup(string.Join("\n", lines));
    }

    prev_hp = hp;
    prev_max_hp = maxHp;
    prev_atk = atk;
    prev_agi = agi;
    prev_charm = charm;
    prev_wis = wis;
  }

  private void updateHpBox() {
    updateHpBox(false);
  }

  private void updateHpBox(bool force) {
    if (hp_text_ui == null) return;

    int hp = Mathf.Max(0, DataMgr.GetInt("hp_kappa"));
    int maxHp = Mathf.Max(1, DataMgr.GetInt("max_hp"));
    if (hp > maxHp) hp = maxHp;

    bool hpChanged = hp != prev_hp_ui;
    bool maxHpChanged = maxHp != prev_max_hp_ui;
    if (!force && !hpChanged && !maxHpChanged) return;

    hp_text_ui.text = $"{hp}";

    if (!force && hpChanged) {
      int deltaHp = hp - prev_hp_ui;
      if (deltaHp != 0) {
        playHpChangeText(deltaHp);
      }
    }

    prev_hp_ui = hp;
    prev_max_hp_ui = maxHp;
  }

  private void updateGoldBox() {
    updateGoldBox(false);
  }

  private void updateGoldBox(bool force) {
    int gold = Mathf.Max(0, DataMgr.GetInt("gold"));
    bool goldChanged = gold != prev_gold_ui;
    if (!force && !goldChanged) return;

    if (!force && goldChanged) {
      int deltaGold = gold - prev_gold_ui;
      if (deltaGold != 0) {
        playGoldChangeText(deltaGold);
      }
    }

    if (gold_text_ui != null) {
      gold_text_ui.text = $"{gold}";
    }
    prev_gold_ui = gold;
  }

  private void playHpChangeText(int deltaHp) {
    if (hp_change_text == null) return;

    DOTween.Kill(hp_change_text);
    RectTransform changeRect = hp_change_text.rectTransform;
    if (changeRect != null) {
      changeRect.DOKill();
      changeRect.anchoredPosition = hp_change_text_base_pos;
    }

    hp_change_text.text = deltaHp > 0 ? $"+{deltaHp}" : deltaHp.ToString();
    Color baseColor = deltaHp > 0 ? HP_CHANGE_HEAL_COLOR : hp_change_damage_color;
    baseColor.a = 1f;
    hp_change_text.color = baseColor;

    Sequence seq = DOTween.Sequence();
    seq.SetLink(hp_change_text.gameObject);
    seq.AppendInterval(0.4f);
    if (changeRect != null) {
      seq.Join(changeRect.DOAnchorPosY(hp_change_text_base_pos.y + 12f, 1.4f).SetEase(Ease.OutQuad));
    }
    seq.Join(DOTween.To(
      () => hp_change_text.color,
      x => hp_change_text.color = x,
      new Color(baseColor.r, baseColor.g, baseColor.b, 0f),
      1.4f
    ));
    seq.OnComplete(() => {
      hp_change_text.text = "";
      if (changeRect != null) {
        changeRect.anchoredPosition = hp_change_text_base_pos;
      }
    });
  }

  private void playGoldChangeText(int deltaGold) {
    if (gold_change_text == null) return;

    DOTween.Kill(gold_change_text);
    RectTransform changeRect = gold_change_text.rectTransform;
    if (changeRect != null) {
      changeRect.DOKill();
      changeRect.anchoredPosition = gold_change_text_base_pos;
    }

    gold_change_text.text = deltaGold > 0 ? $"+{deltaGold}" : deltaGold.ToString();
    Color baseColor = deltaGold > 0 ? GOLD_CHANGE_GAIN_COLOR : gold_change_loss_color;
    baseColor.a = 1f;
    gold_change_text.color = baseColor;

    Sequence seq = DOTween.Sequence();
    seq.SetLink(gold_change_text.gameObject);
    seq.AppendInterval(0.4f);
    if (changeRect != null) {
      seq.Join(changeRect.DOAnchorPosY(gold_change_text_base_pos.y + 12f, 1.4f).SetEase(Ease.OutQuad));
    }
    seq.Join(DOTween.To(
      () => gold_change_text.color,
      x => gold_change_text.color = x,
      new Color(baseColor.r, baseColor.g, baseColor.b, 0f),
      1.4f
    ));
    seq.OnComplete(() => {
      gold_change_text.text = "";
      if (changeRect != null) {
        changeRect.anchoredPosition = gold_change_text_base_pos;
      }
    });
  }

  private void SetStatusPopupIcon(string spriteKey) {
    if (status_popup_icon == null) return;
    Sprite sprite = Resources.Load<Sprite>($"Textures/{spriteKey}");
    if (sprite == null) return;
    status_popup_icon.sprite = sprite;
    status_popup_icon.enabled = true;
  }

  private void ResetStatusPopupIcon() {
    if (status_popup_icon == null || status_popup_default_sprite == null) return;
    status_popup_icon.sprite = status_popup_default_sprite;
    status_popup_icon.enabled = true;
  }

  private void playStatusPopup(string text) {
    PlayStatusPopupInternal(text, true);
  }

  private void playStatusPopupPersistent(string text) {
    PlayStatusPopupInternal(text, false);
  }

  private void PlayStatusPopupInternal(string text, bool autoHide) {
    if (status_popup_root == null || status_popup_text == null) return;
    status_popup_text.text = text;
    status_popup_root.DOKill();
    status_popup_root.anchoredPosition = status_popup_hidden_pos;
    Sequence seq = DOTween.Sequence();
    seq.SetLink(status_popup_root.gameObject);
    seq.Append(status_popup_root.DOAnchorPos(status_popup_shown_pos, 0.3f).SetEase(Ease.OutQuad));
    if (autoHide) {
      seq.AppendInterval(0.7f);
      seq.Append(status_popup_root.DOAnchorPos(status_popup_hidden_pos, 0.3f).SetEase(Ease.InQuad));
    }
    is_status_popup_pinned = !autoHide;
  }

  private string FormatDelta(int delta) {
    if (delta > 0) return $"+{delta}";
    if (delta < 0) return $"<color=#FF3B30>{delta}</color>";
    return "0";
  }

  private void initNameArea() {
    if (name_text == null || name_area_rect == null) {
      GameObject name_area_obj = GameObject.Find("nameArea");
      if (name_area_obj != null) {
        name_area_rect = name_area_obj.GetComponent<RectTransform>();
        name_text = name_area_obj.GetComponentInChildren<TextMeshProUGUI>(true);
      }
    }
    setupNameAreaLayout();
  }

  private void setupNameAreaLayout() {
    if (name_area_rect == null) return;
    RectTransform main_text_rect = null;
    if (main_text_area != null) {
      main_text_rect = main_text_area.GetComponent<RectTransform>();
    }
    if (main_text_rect != null && name_area_rect.transform.parent != main_text_rect.transform.parent) {
      name_area_rect.SetParent(main_text_rect.transform.parent, false);
    }
    name_area_rect.anchorMin = new Vector2(0.5f, 0.5f);
    name_area_rect.anchorMax = new Vector2(0.5f, 0.5f);
    name_area_rect.pivot = new Vector2(0f, 0f);
    if (main_text_rect != null) {
      float x = main_text_rect.anchoredPosition.x - (main_text_rect.sizeDelta.x * 0.5f) + 8f;
      float y = main_text_rect.anchoredPosition.y + (main_text_rect.sizeDelta.y * 0.5f) + 8f;
      name_area_rect.anchoredPosition = new Vector2(x, y);
    } else {
      name_area_rect.anchoredPosition = new Vector2(-440f, -100f);
    }
    name_area_rect.sizeDelta = new Vector2(200f, 36f);
  }

  private void updateNameArea() {
    if (name_area_rect == null || name_text == null) return;
    if (IsBattlePage(current_page_key)) {
      name_area_rect.gameObject.SetActive(false);
      return;
    }
    if (page.page_type == PageModel.PAGE_TYPE_CHOICE) {
      name_area_rect.gameObject.SetActive(false);
      return;
    }
    if (string.IsNullOrEmpty(localizedSpeaker)) {
      name_area_rect.gameObject.SetActive(false);
      return;
    }
    name_text.text = localizedSpeaker;
    name_area_rect.gameObject.SetActive(true);
  }

  private void updateMainTextStyle(string key) {
    if (main_text == null) return;
    if (key == "castle/op2") {
      main_text.alignment = TextAlignmentOptions.Center;
      main_text.fontSize = default_main_text_size * 1.4f;
    } else {
      main_text.alignment = default_main_text_alignment;
      main_text.fontSize = default_main_text_size;
    }
  }

  private void applyTextOutline(TextMeshProUGUI target) {
    if (target == null) return;
    Outline outline = target.GetComponent<Outline>();
    if (outline == null) {
      outline = target.gameObject.AddComponent<Outline>();
    }
    outline.effectColor = new Color(0f, 0f, 0f, 0.35f);
    outline.effectDistance = new Vector2(1f, -1f);
  }

  private string ResolveLocalizedPageText(string pageKey, string field, string fallback) {
    string localized = LocalizationUtil.Get(LocalizationUtil.GetPageKey(pageKey, field));
    if (!string.IsNullOrEmpty(localized)) return localized;
    return fallback;
  }

  private void InitBgScrollMaterial() {
    if (image == null) return;
    if (bg_scroll_material != null) return;
    Shader shader = Shader.Find("UI/Default");
    if (shader == null) return;
    bg_scroll_material = new Material(shader);
    bg_scroll_material.mainTextureScale = Vector2.one;
    image.material = bg_scroll_material;
  }

  private void updateBgScroll() {
    if (!is_bg_scrolling || bg_scroll_material == null) return;
    bg_scroll_offset.x += bg_scroll_speed * Time.deltaTime;
    if (bg_scroll_offset.x > 1f) {
      bg_scroll_offset.x -= 1f;
    }
    bg_scroll_material.mainTextureOffset = bg_scroll_offset;
  }

  private void setBgScrollEnabled(bool enabled) {
    is_bg_scrolling = enabled;
    if (image == null) return;
    if (enabled) {
      image.type = Image.Type.Simple;
      bg_scroll_offset = Vector2.zero;
      if (bg_scroll_material != null) {
        bg_scroll_material.mainTextureOffset = bg_scroll_offset;
        bg_scroll_material.mainTextureScale = Vector2.one;
        if (image.sprite != null && image.sprite.texture != null) {
          image.sprite.texture.wrapMode = TextureWrapMode.Repeat;
          bg_scroll_material.mainTexture = image.sprite.texture;
        }
      }
    } else {
      image.type = Image.Type.Simple;
      if (bg_scroll_material != null) {
        bg_scroll_material.mainTextureOffset = Vector2.zero;
      }
    }
  }

  private void resetSlideBg() {
    if (main_bg_rect != null) {
      main_bg_rect.anchoredPosition = Vector2.zero;
    }
    if (left_bg_rect != null) {
      left_bg_rect.anchoredPosition = new Vector2(-960f, 0f);
    }
    is_nandate_animating = false;
  }

  private void playSlideIn(string spriteKey) {
    if (main_bg_rect == null || left_bg_rect == null || left_bg_image == null) return;
    left_bg_image.sprite = Resources.Load<Sprite>($"Textures/{spriteKey}");
    MarkGallerySeenFromSprite(left_bg_image.sprite);
    main_bg_rect.DOKill();
    left_bg_rect.DOKill();
    main_bg_rect.anchoredPosition = Vector2.zero;
    left_bg_rect.anchoredPosition = new Vector2(-960f, 0f);
    float duration = 0.35f;
    main_bg_rect.DOAnchorPos(new Vector2(960f, 0f), duration).SetEase(Ease.OutQuad);
    left_bg_rect.DOAnchorPos(Vector2.zero, duration).SetEase(Ease.OutQuad);
  }

  private void playSlideOut(System.Action onComplete) {
    if (main_bg_rect == null || left_bg_rect == null) {
      onComplete?.Invoke();
      return;
    }
    is_nandate_animating = true;
    main_bg_rect.DOKill();
    left_bg_rect.DOKill();
    float duration = 0.25f;
    Sequence seq = DOTween.Sequence();
    seq.Join(main_bg_rect.DOAnchorPos(Vector2.zero, duration).SetEase(Ease.InQuad));
    seq.Join(left_bg_rect.DOAnchorPos(new Vector2(-960f, 0f), duration).SetEase(Ease.InQuad));
    seq.OnComplete(() => {
      is_nandate_animating = false;
      onComplete?.Invoke();
    });
  }

  private void timeOver() {
    // TODO 時間切れのストーリー追加
    DataMgr.SetStr("dead_reason", EndingModel.BADEND_TIME_OVER);
    DataMgr.SetStr(AUTO_SAVE_PAGE_KEY, "");
    DataMgr.SetStr("page", "");
    CommonUtil.changeScene("GameOverScene");
  }

  private void MarkGallerySeen(string imageKey) {
    if (string.IsNullOrEmpty(imageKey)) return;
    if (imageKey.StartsWith(GALLERY_IMAGE_PREFIX)) {
      string name = imageKey.Substring(GALLERY_IMAGE_PREFIX.Length);
      if (string.IsNullOrEmpty(name)) return;
      DataMgr.SetBool($"{GALLERY_SEEN_PREFIX}{name}", true);
      return;
    }
    if (imageKey.StartsWith("bg/")) {
      string name = imageKey.Substring("bg/".Length);
      if (string.IsNullOrEmpty(name)) return;
      if (Resources.Load<Sprite>($"Textures/{GALLERY_IMAGE_PREFIX}{name}") == null) return;
      DataMgr.SetBool($"{GALLERY_SEEN_PREFIX}{name}", true);
    }
  }

  private void MarkGallerySeenFromSprite(Sprite sprite) {
    if (sprite == null) return;
    string name = sprite.name;
    if (string.IsNullOrEmpty(name)) return;
    if (Resources.Load<Sprite>($"Textures/{GALLERY_IMAGE_PREFIX}{name}") == null) return;
    DataMgr.SetBool($"{GALLERY_SEEN_PREFIX}{name}", true);
  }

  private void MarkCharacterSeen(string characterId) {
    if (string.IsNullOrEmpty(characterId)) return;
    DataMgr.SetBool($"{GALLERY_CHAR_SEEN_PREFIX}{characterId}", true);
  }

  private void MarkCharacterSeenFromPage(PageModel model) {
    if (model == null) return;
    string imageKey = model.main_bg;
    if (string.IsNullOrEmpty(imageKey)) return;
    if (imageKey.Contains("shiori_240_135")) {
      MarkCharacterSeen("shiorina");
    } else if (imageKey.Contains("slime_encount")) {
      MarkCharacterSeen("slime");
    } else if (imageKey.Contains("maou_jk_240_135")) {
      MarkCharacterSeen("maou");
    } else if (imageKey.Contains("queen")) {
      MarkCharacterSeen("hime");
    }
  }

  private void MarkCharacterSeenFromEnemyName(string enemyName) {
    if (string.IsNullOrEmpty(enemyName)) return;
    if (enemyName == "スライム") {
      MarkCharacterSeen("slime");
    } else if (enemyName == "魔王") {
      MarkCharacterSeen("maou");
    } else if (enemyName == "ガイア" || enemyName == "マッシュ" || enemyName == "オルテガ") {
      MarkCharacterSeen("dogs");
    }
  }

  private void fadeImage(){

  }

  // ページを key によって更新する
  public void updateScene(string key) {
    //    Debug.Log($"update scene. key={key}");
    string prevKey = current_page_key;
    current_page_key = key;
    if (is_status_popup_pinned && !string.IsNullOrEmpty(prevKey) && key != prevKey) {
      HideAllyStatusPopup();
    }
    DataMgr.SetStr(AUTO_SAVE_PAGE_KEY, key);
    ClearDiceState();
    page = PageModel.getPageModelByKey(key);
    localizedMainText = ResolveLocalizedPageText(key, "main_text", page.main_text);
    localizedSpeaker = ResolveLocalizedPageText(key, "speaker", page.speaker);
    MarkCharacterSeenFromPage(page);
    if (page.page_type != PageModel.PAGE_TYPE_MAP_MOVE) {
      if (is_map_move_active) {
        if (main_bg_rect != null) {
          main_bg_rect.DOKill();
          main_bg_rect.anchoredPosition = Vector2.zero;
        }
        if (right_bg_rect != null) {
          right_bg_rect.DOKill();
          right_bg_rect.anchoredPosition = new Vector2(960f, 0f);
        }
      }
      RestoreKappaAfterMapMove();
      is_map_move_active = false;
    }
    if (key.EndsWith("/end")) {
      if (string.IsNullOrEmpty(localizedMainText) && !string.IsNullOrEmpty(page.next_page)) {
        PageModel.pushedTappedScreen(page.next_page);
        return;
      }
    }

    switch (page.page_type) {
      case PageModel.PAGE_TYPE_NORMAL:
        tap_screen_area.SetActive(true);
        ChoiceModel.instance.hideArea();
        main_text_area.SetActive(true);
        if (string.IsNullOrEmpty(localizedMainText)) {
          main_text_area.gameObject.SetActive(false);
        } else {
          main_text_area.gameObject.SetActive(true);
          main_text.text = localizedMainText;
          //      main_text_area.GetComponent<CanvasGroup>().DOFade(1, fadeDuration).SetAutoKill(false).SetLink(main_text_area);
        }
        break;
      case PageModel.PAGE_TYPE_MAP_MOVE:
        tap_screen_area.SetActive(true);
        ChoiceModel.instance.hideArea();
        main_text_area.SetActive(true);
        if (string.IsNullOrEmpty(localizedMainText)) {
          main_text_area.gameObject.SetActive(false);
        } else {
          main_text_area.gameObject.SetActive(true);
          main_text.text = localizedMainText;
        }
        PlayMapMove(page);
        break;
      case PageModel.PAGE_TYPE_CHOICE:
        tap_screen_area.SetActive(false);
        ChoiceModel.instance.showArea();
        ChoiceModel.instance.EnsureButtonsVisible();
        main_text_area.SetActive(false);
        if (ChoiceModel.instance.choice_keys == null || ChoiceModel.instance.choice_keys.Count == 0) {
          Debug.LogError($"choice page has no options. key={key}");
        }
        if (!HasActiveChoiceButtons()) {
          Debug.LogError($"choice page has no active buttons. key={key}");
        }
        //        ChoiceModel.instance.fadeInAllButton();
        break;
      default:
        Debug.Log($"unknown page type. type={page.page_type}");
        break;
    }

    if (page.bgm != "") {
      if(BGMMgr.instance) {
        BGMMgr.instance.changeBGM(page.bgm);
      }
    }
    updateNameArea();
    updateMainTextStyle(key);
    bool is_slide_page = isSlidePage(key);
    bool keep_slide_state = isKeepSlideState(key);
    MarkGallerySeen(page.main_bg);
    if (is_slide_page) {
      MarkGallerySeen(getSlideSpriteKey(key));
    }
    Sprite prev_sprite = image.sprite;
    if (page.page_type == PageModel.PAGE_TYPE_MAP_MOVE) {
      image.sprite = prev_sprite;
    } else if (!is_slide_page) {
      image.sprite = Resources.Load<Sprite>($"Textures/{page.main_bg}");
    } else {
      image.sprite = prev_sprite;
    }
    MarkGallerySeenFromSprite(image.sprite);
    UpdateMainImage(page);
    if (faderScript != null) {
      float progress = 1f - (limit_time / MAX_LIMIT_TIME);
      faderScript.Init(image.sprite, progress);
    }
    if (bg_scroll_material != null && image.sprite != null) {
      bg_scroll_material.mainTexture = image.sprite.texture;
    }
    if (key == StartEntrancePageModel.PAGE_KEY && KappaController.instance != null) {
      KappaController.instance.stopAnimation();
    }
    setBgScrollEnabled(key == "castle/run");
    if (is_slide_page) {
      playSlideIn(getSlideSpriteKey(key));
    } else if (!keep_slide_state) {
      resetSlideBg();
    }

    if (IsBattlePage(key)) {
      StartBattle(key);
    } else {
      StopBattle();
    }
    UpdateOverlayImage(key);
    UpdateDebugPageName();
  }

  private void UpdateMainImage(PageModel model) {
    if (main_image_ui == null) return;
    if (model == null || string.IsNullOrEmpty(model.main_image)) {
      main_image_ui.sprite = null;
      main_image_ui.gameObject.SetActive(false);
      return;
    }

    Sprite sprite = Resources.Load<Sprite>($"Textures/{model.main_image}");
    if (sprite == null) {
      main_image_ui.sprite = null;
      main_image_ui.gameObject.SetActive(false);
      return;
    }

    main_image_ui.sprite = sprite;
    main_image_ui.gameObject.SetActive(true);
  }

  private void UpdateDebugPageName() {
    EnsureDebugPageText();
    if (debug_page_text == null) return;
    bool enabled = DataMgr.GetBool("debug_mode");
    debug_page_text.gameObject.SetActive(enabled);
    if (enabled) {
      debug_page_text.text = $"page: {current_page_key}";
    }
  }

  private void PlayMapMove(PageModel model) {
    if (is_map_move_active) return;
    is_map_move_active = true;
    if (right_bg_image != null && right_bg_image.sprite == null && !string.IsNullOrEmpty(model.main_bg)) {
      right_bg_image.sprite = Resources.Load<Sprite>($"Textures/{model.main_bg}");
    }
    if (main_bg_rect != null) main_bg_rect.DOKill();
    if (right_bg_rect != null) right_bg_rect.DOKill();
    if (main_bg_rect != null) main_bg_rect.anchoredPosition = Vector2.zero;
    if (right_bg_rect != null) right_bg_rect.anchoredPosition = new Vector2(960f, 0f);
    float slideDuration = 0.35f;
    if (main_bg_rect != null) {
      main_bg_rect.DOAnchorPos(new Vector2(-960f, 0f), slideDuration).SetEase(Ease.OutQuad);
    }
    if (right_bg_rect != null) {
      right_bg_rect.DOAnchorPos(Vector2.zero, slideDuration).SetEase(Ease.OutQuad);
    }
    MoveKappaWithSlide(slideDuration, () => PlayMapMoveKappa(2.5f));
  }

  private void MoveKappaWithSlide(float duration, System.Action onComplete) {
    if (KappaController.instance == null) {
      onComplete?.Invoke();
      return;
    }
    RectTransform kappaRect = KappaController.instance.GetComponent<RectTransform>();
    if (kappaRect == null) {
      onComplete?.Invoke();
      return;
    }
    kappaRect.DOKill();
    Vector2 target = kappaRect.anchoredPosition + new Vector2(-960f, 0f);
    kappaRect.DOAnchorPos(target, duration).SetEase(Ease.OutQuad).OnComplete(() => {
      KappaController.instance.stopAnimation();
      onComplete?.Invoke();
    });
  }

  private void PlayMapMoveKappa(float duration) {
    if (!TryGetMapMovePositions(current_page_key, out Vector2 startPos, out Vector2 endPos)) return;
    if (KappaController.instance == null) return;
    RectTransform kappaRect = KappaController.instance.GetComponent<RectTransform>();
    if (kappaRect == null) return;
    if (!map_move_reparented && right_bg_rect != null) {
      map_move_prev_parent = kappaRect.parent;
      kappaRect.SetParent(right_bg_rect, false);
      kappaRect.localScale = Vector3.one;
      Vector3 localPos = kappaRect.localPosition;
      localPos.z = 0f;
      kappaRect.localPosition = localPos;
      map_move_reparented = true;
    }
    KappaController.instance.animateRunInPlace();
    kappaRect.anchoredPosition = startPos;
    kappaRect.DOKill();
    kappaRect.DOAnchorPos(endPos, duration).SetEase(Ease.Linear);
  }

  private bool TryGetMapMovePositions(string key, out Vector2 startPos, out Vector2 endPos) {
    startPos = Vector2.zero;
    endPos = Vector2.zero;
    if (key == EndShioriPageModel.PAGE_KEY) {
      if (shiori_pos == null || slime_pos == null) return false;
      startPos = shiori_pos.anchoredPosition;
      endPos = slime_pos.anchoredPosition;
      return true;
    }
    if (key == EndSlimePageModel.PAGE_KEY) {
      if (slime_pos == null || town_pos == null) return false;
      startPos = slime_pos.anchoredPosition;
      endPos = town_pos.anchoredPosition;
      return true;
    }
    if (key == EndTownPageModel.PAGE_KEY) {
      if (town_pos == null || forest_or_mountain_pos == null) return false;
      startPos = town_pos.anchoredPosition;
      endPos = forest_or_mountain_pos.anchoredPosition;
      return true;
    }
    if (key == EndForestOrMountainPageModel.PAGE_KEY) {
      if (forest_or_mountain_pos == null || maou_pos == null) return false;
      startPos = forest_or_mountain_pos.anchoredPosition;
      endPos = maou_pos.anchoredPosition;
      return true;
    }
    if (castle_pos == null || shiori_pos == null) return false;
    startPos = castle_pos.anchoredPosition;
    endPos = shiori_pos.anchoredPosition;
    return true;
  }

  private void RestoreKappaAfterMapMove() {
    if (!map_move_reparented) return;
    if (KappaController.instance == null) {
      map_move_reparented = false;
      map_move_prev_parent = null;
      return;
    }
    RectTransform kappaRect = KappaController.instance.GetComponent<RectTransform>();
    if (kappaRect == null || map_move_prev_parent == null) {
      map_move_reparented = false;
      map_move_prev_parent = null;
      return;
    }
    kappaRect.SetParent(map_move_prev_parent, false);
    kappaRect.localScale = Vector3.one;
    Vector3 localPos = kappaRect.localPosition;
    localPos.z = 0f;
    kappaRect.localPosition = localPos;
    KappaController.instance.hideKappa();
    map_move_reparented = false;
    map_move_prev_parent = null;
  }

  public void RefreshDebugPageName() {
    UpdateDebugPageName();
  }

  private void EnsureDebugPageText() {
    if (debug_page_text != null) return;
    GameObject obj = GameObject.Find("debug_page_name");
    if (obj == null) return;
    debug_page_text = obj.GetComponent<TextMeshProUGUI>();
  }

  public void goToNextPage(string key){
    DataMgr.SetStr("page", key);

    updateScene(key);
  }

  public void tappedScreen(){
    if (isLockedNoticeActive) return;
    if (isDiceActive) {
      if (isDiceRolling) {
        return;
      }
      if (isDiceResultReady) {
        bool success = IsDiceSuccess(diceLeftValue, diceRightValue, diceStatValue, diceThreshold);
        string nextKey = success ? diceSuccessKey : diceFailKey;
        if (success) {
          diceSuccessAction?.Invoke();
        } else {
          diceFailAction?.Invoke();
        }
        ClearDiceState();
        DataMgr.SetStr("page", nextKey);
        updateScene(nextKey);
      }
      return;
    }
    if(page.page_type != PageModel.PAGE_TYPE_NORMAL && page.page_type != PageModel.PAGE_TYPE_MAP_MOVE) {
      return;
    }

    if (page.page_type == PageModel.PAGE_TYPE_MAP_MOVE) {
      StopMapMoveKappaImmediately();
    }

    string key = page.next_page;
    string now_key = DataMgr.GetStr("page");
    if (isSlideOutPage(now_key) && !is_nandate_animating) {
      playSlideOut(() => PageModel.pushedTappedScreen(key));
      return;
    }

    // ゲームオーバーの場合は次の処理
    if(key.StartsWith("game_over")){
      goToGameOver(key);
      return;
    }

    PageModel.pushedTappedScreen(key);
  }

  private void StopMapMoveKappaImmediately() {
    if (KappaController.instance == null) return;
    RectTransform kappaRect = KappaController.instance.GetComponent<RectTransform>();
    if (kappaRect != null) {
      kappaRect.DOKill();
    }
    KappaController.instance.stopAnimation();
    KappaController.instance.hideKappa();
  }

  private string pushed_key = "";
  private bool is_disable_pushed_button = false;
  public void pushedChoiceButton(int i) {
//    Debug.Log($"pushed choice button {i}");
    if(is_disable_pushed_button) return;
    if (ChoiceModel.instance != null && ChoiceModel.instance.IsChoiceLocked(i)) {
      ShowLockedChoiceNotice();
      return;
    }

    is_disable_pushed_button = true;
    pushed_key = ChoiceModel.instance.choice_keys[i-1];
//    Debug.Log($"pushed choice button, key = {pushed_key}");

    // 特定キーワードでは処理をキャンセル
    switch(pushed_key) {
      /*
      case "no_money":
        PageScreenModel.instance.onLeftScreen("くっ、<sprite name=\"coin\">が足りない！", "ちょっ、お金が足りないっすよ！", false);
        is_disable_pushed_button = false;
        return;
        */
      default:
        break;
    }

    // 色々fadeout
    float fadeDuration = RingConst.DURATION_PAGE_BUTTON_PUSHED_DELAY;
    //main_text_area.GetComponent<CanvasGroup>().DOFade(0, fadeDuration).SetAutoKill(false).SetLink(main_text_area);
//    talk_image.gameObject.GetComponent<CanvasGroup>().DOFade(0, fadeDuration).SetAutoKill(false).SetLink(talk_image.gameObject);

    ChoiceModel.instance.FadeOutButtons(i, fadeDuration);

    Invoke("pushedChoiceButtonDelayed", fadeDuration);
  }

  // ボタンを押した後の delay 処理
  private void pushedChoiceButtonDelayed(){
    PageModel.pushedButton(pushed_key);
    is_disable_pushed_button = false;
  }

  private void StartBattle(string battleKey) {
    if (battleCoroutine != null || isBattleIntroPlaying) return;
    currentBattleConfig = GetBattleConfig(battleKey);
    if (currentBattleConfig == null) return;
    currentBattleKey = battleKey;
    tap_screen_area.SetActive(false);
    main_text_area.SetActive(false);
    LoadBattlePrefabs();
    InitializeBattleState();
    BuildBattleUi();
    UpdateBattleUi();
    isAllyDeathPending = false;
    if (battleIntroCoroutine != null) {
      StopCoroutine(battleIntroCoroutine);
    }
    battleIntroCoroutine = StartCoroutine(BeginBattleAfterVs());
  }

  private IEnumerator BeginBattleAfterVs() {
    isBattleIntroPlaying = true;
    if (battle_bg_rect != null) {
      battle_bg_rect.gameObject.SetActive(true);
      battle_bg_rect.DOKill();
      battle_bg_rect.anchoredPosition = BATTLE_BG_START_POS;
      Tween tween = battle_bg_rect.DOAnchorPos(BATTLE_BG_ACTIVE_POS, BATTLE_BG_ENTER_DURATION).SetEase(Ease.OutQuad);
      yield return tween.WaitForCompletion();
    }
    if (vs_text != null) {
      vs_text.SetActive(true);
    }
    yield return new WaitForSeconds(BATTLE_VS_VISIBLE_DURATION);
    if (vs_text != null) {
      vs_text.SetActive(false);
    }
    isBattleIntroPlaying = false;
    battleIntroCoroutine = null;
    if (DataMgr.GetStr("page") != currentBattleKey) {
      yield break;
    }
    battleCoroutine = StartCoroutine(BattleLoop());
  }

  private void LoadBattlePrefabs() {
    if (damageTextPrefab == null) {
      damageTextPrefab = Resources.Load<GameObject>("Prefabs/damageText");
    }
    if (sparkParticlePrefab == null) {
      sparkParticlePrefab = Resources.Load<GameObject>("Prefabs/particle/spark_particle");
    }
    if (swordEffectPrefab == null) {
      swordEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/sword_effect");
    }
    if (crowEffectPrefab == null) {
      crowEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/crow_effect");
    }
    if (impactEffectPrefab == null) {
      impactEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/impact_effect");
    }
    if (fireEffectPrefab == null) {
      fireEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/fire_effect");
    }
  }

  private void StopBattle() {
    if (battleCoroutine != null) {
      StopCoroutine(battleCoroutine);
      battleCoroutine = null;
    }
    if (battleIntroCoroutine != null) {
      StopCoroutine(battleIntroCoroutine);
      battleIntroCoroutine = null;
    }
    isBattleIntroPlaying = false;
    battleFighters.Clear();
    battleOrder.Clear();
    battleOrderIndex = 0;
    if (battleUiRoot != null) {
      DOTween.Kill(battleUiRoot);
    }
    if (battle_bg_rect != null) {
      battle_bg_rect.DOKill();
      battle_bg_rect.anchoredPosition = BATTLE_BG_START_POS;
      battle_bg_rect.gameObject.SetActive(false);
    }
    if (vs_text != null) {
      vs_text.SetActive(false);
    }
    DestroyBattleUi();
    currentBattleConfig = null;
    currentBattleKey = "";
    isBattleWinPending = false;
    isAllyDeathPending = false;
  }

  private void InitializeBattleState() {
    battleFighters.Clear();
    battleOrder.Clear();
    battleOrderIndex = 0;

    int kappaMaxHp = BATTLE_ALLY_MAX_HP;
    int kappaHp = GetOrInitBattleHp("hp_kappa", kappaMaxHp);
    kappaHp = Mathf.Clamp(kappaHp, 0, kappaMaxHp);
    int kappaAtk = Mathf.Max(1, DataMgr.GetInt("atk"));
    if (DataMgr.GetBool("skill_kimi_no_nawa")) {
      kappaAtk = Mathf.Max(1, DataMgr.GetInt("charm"));
    }
    battleFighters.Add(new BattleFighter("カッパ", kappaHp, kappaMaxHp, kappaAtk, Mathf.Max(1, DataMgr.GetInt("agi")), false, "chara/kappa_run1", "hp_kappa"));
    if (currentBattleConfig != null) {
      int enemyCount = Mathf.Min(1, currentBattleConfig.enemies.Count);
      for (int i = 0; i < enemyCount; i++) {
        BattleEnemyConfig enemy = currentBattleConfig.enemies[i];
        battleFighters.Add(new BattleFighter(enemy.name, enemy.hp, enemy.hp, enemy.atk, enemy.agi, true, enemy.spriteKey, ""));
        MarkCharacterSeenFromEnemyName(enemy.name);
      }
    }

    isBattleWinPending = false;
  }

  private IEnumerator BattleLoop() {
    while (true) {
      if (DataMgr.GetStr("page") != currentBattleKey) {
        yield break;
      }
      yield return ExecuteBattleStep();
    }
  }

  private IEnumerator ExecuteBattleStep() {
    if (isAllyDeathPending) {
      yield return new WaitForSeconds(BATTLE_TURN_DURATION);
      yield break;
    }
    if (IsBattleEnded()) {
      yield break;
    }
    if (battleOrder.Count == 0 || battleOrderIndex == 0) {
      BuildBattleOrder();
    }
    BattleFighter actor = GetNextActor();
    if (actor == null) {
      yield return new WaitForSeconds(BATTLE_TURN_DURATION);
      yield break;
    }
    BattleFighter target = actor.isEnemy ? GetRandomAliveAlly() : GetRandomAliveEnemy();
    if (target == null) {
      yield return new WaitForSeconds(BATTLE_TURN_DURATION);
      yield break;
    }
    yield return AnimateAttack(actor, target);
  }

  private bool IsBattleEnded() {
    BattleConfig config = currentBattleConfig ?? GetBattleConfig(currentBattleKey);
    if (config == null) return false;
    if (GetAliveEnemies().Count == 0) {
      StopBattle();
      DataMgr.SetStr("page", config.nextPageKey);
      updateScene(config.nextPageKey);
      return true;
    }
    if (GetAliveAllies().Count == 0) {
      if (isAllyDeathPending) {
        return true;
      }
      string battleKey = currentBattleKey;
      if (string.IsNullOrEmpty(battleKey) && config != null) {
        battleKey = config.nextPageKey;
      }
      SetBattleGameOverReason(battleKey);
      StopBattle();
      CommonUtil.changeScene("GameOverScene");
      return true;
    }
    return false;
  }

  private BattleFighter GetNextActor() {
    int total = battleOrder.Count;
    for (int i = 0; i < total; i++) {
      BattleFighter actor = battleOrder[battleOrderIndex];
      battleOrderIndex = (battleOrderIndex + 1) % battleOrder.Count;
      if (actor != null && actor.hp > 0) {
        return actor;
      }
    }
    return null;
  }

  private void BuildBattleOrder() {
    battleOrder.Clear();
    List<BattleFighter> alive = GetAliveFighters();
    alive.Sort((a, b) => b.agi.CompareTo(a.agi));
    int index = 0;
    while (index < alive.Count) {
      int start = index;
      int agi = alive[index].agi;
      while (index < alive.Count && alive[index].agi == agi) {
        index++;
      }
      ShuffleRange(alive, start, index);
      for (int i = start; i < index; i++) {
        battleOrder.Add(alive[i]);
      }
    }
  }

  private void ShuffleRange(List<BattleFighter> list, int start, int end) {
    for (int i = end - 1; i > start; i--) {
      int j = battleRandom.Next(start, i + 1);
      BattleFighter tmp = list[i];
      list[i] = list[j];
      list[j] = tmp;
    }
  }

  private List<BattleFighter> GetAliveFighters() {
    List<BattleFighter> alive = new List<BattleFighter>();
    foreach (BattleFighter fighter in battleFighters) {
      if (fighter.hp > 0) {
        alive.Add(fighter);
      }
    }
    return alive;
  }

  private List<BattleFighter> GetAliveAllies() {
    List<BattleFighter> alive = new List<BattleFighter>();
    foreach (BattleFighter fighter in battleFighters) {
      if (!fighter.isEnemy && fighter.hp > 0) {
        alive.Add(fighter);
      }
    }
    return alive;
  }

  private List<BattleFighter> GetAliveEnemies() {
    List<BattleFighter> alive = new List<BattleFighter>();
    foreach (BattleFighter fighter in battleFighters) {
      if (fighter.isEnemy && fighter.hp > 0) {
        alive.Add(fighter);
      }
    }
    return alive;
  }

  private BattleFighter GetRandomAliveEnemy() {
    List<BattleFighter> enemies = GetAliveEnemies();
    if (enemies.Count == 0) return null;
    int index = battleRandom.Next(0, enemies.Count);
    return enemies[index];
  }

  private BattleFighter GetEnemy() {
    foreach (BattleFighter fighter in battleFighters) {
      if (fighter.isEnemy && fighter.hp > 0) return fighter;
    }
    return null;
  }

  private BattleFighter GetRandomAliveAlly() {
    List<BattleFighter> allies = GetAliveAllies();
    if (allies.Count == 0) return null;
    int index = battleRandom.Next(0, allies.Count);
    return allies[index];
  }

  private void BuildBattleUi() {
    InitBattleSceneUiRefs();
    if (battle_bg_rect == null) {
      Debug.LogError("battle_bg_rect is not assigned in Inspector.");
      battleUiRoot = null;
      battleUiRect = null;
      return;
    }
    battleUiRoot = battle_bg_rect.gameObject;
    battleUiRect = battle_bg_rect;

    BattleFighter ally = null;
    BattleFighter enemy = null;
    foreach (BattleFighter fighter in battleFighters) {
      if (fighter.isEnemy) {
        if (enemy == null) enemy = fighter;
      } else {
        if (ally == null) ally = fighter;
      }
    }

    if (ally != null) {
      RectTransform allyRect = battle_kappa_image != null ? battle_kappa_image.rectTransform : null;
      ally.uiRootRect = allyRect;
      Vector2 allyBasePos = battle_kappa_initial_cached ? battle_kappa_initial_pos : (allyRect != null ? allyRect.anchoredPosition : Vector2.zero);
      ally.basePos = allyBasePos;
      ally.hpSlider = life_gage;
      ally.hpText = life_text;
      ally.hpRoot = life_gage_box;
      if (battle_kappa_image != null) {
        ResetBattleCharacterTransform(battle_kappa_image, allyBasePos);
        Sprite allySprite = Resources.Load<Sprite>($"Textures/{ally.spriteKey}");
        battle_kappa_image.sprite = allySprite;
        battle_kappa_image.gameObject.SetActive(allySprite != null);
      }
      if (life_gage_box != null) {
        life_gage_box.SetActive(true);
      }
    }

    if (enemy != null) {
      RectTransform enemyRect = enemy_image != null ? enemy_image.rectTransform : null;
      enemy.uiRootRect = enemyRect;
      Vector2 enemyBasePos = battle_enemy_initial_cached ? battle_enemy_initial_pos : (enemyRect != null ? enemyRect.anchoredPosition : Vector2.zero);
      enemy.basePos = enemyBasePos;
      enemy.hpSlider = enemy_life_gage;
      enemy.hpText = enemy_life_text;
      enemy.hpRoot = enemy_life_gage_box;

      if (enemy_image != null) {
        ResetBattleCharacterTransform(enemy_image, enemyBasePos);
        Sprite enemySprite = Resources.Load<Sprite>($"Textures/{enemy.spriteKey}");
        enemy_image.sprite = enemySprite;
        enemy_image.gameObject.SetActive(enemySprite != null);
      }
      if (enemy_life_gage_box != null) {
        enemy_life_gage_box.SetActive(true);
      }
    } else {
      if (enemy_image != null) {
        enemy_image.sprite = null;
        enemy_image.gameObject.SetActive(false);
      }
      if (enemy_life_gage_box != null) {
        enemy_life_gage_box.SetActive(false);
      }
    }
  }

  private void DestroyBattleUi() {
    battleUiRoot = null;
    battleUiRect = null;
    if (life_gage_box != null) {
      life_gage_box.SetActive(false);
    }
    if (enemy_life_gage_box != null) {
      enemy_life_gage_box.SetActive(false);
    }
    if (enemy_image != null) {
      enemy_image.sprite = null;
      enemy_image.gameObject.SetActive(false);
    }
    if (battle_kappa_image != null) {
      battle_kappa_image.sprite = null;
      battle_kappa_image.gameObject.SetActive(false);
    }
  }

  private void UpdateBattleUi() {
    foreach (BattleFighter fighter in battleFighters) {
      if (fighter.hpSlider != null) {
        int maxHp = Mathf.Max(1, fighter.maxHp);
        int hp = Mathf.Clamp(fighter.hp, 0, maxHp);
        fighter.hpSlider.minValue = 0f;
        fighter.hpSlider.maxValue = 1f;
        fighter.hpSlider.value = (float)hp / maxHp;
      }
      if (fighter.hpText != null) {
        int maxHp = Mathf.Max(1, fighter.maxHp);
        int hp = Mathf.Clamp(fighter.hp, 0, maxHp);
        fighter.hpText.text = $"{hp}";
      }
    }
  }

  private void SaveBattleHp() {
    foreach (BattleFighter fighter in battleFighters) {
      if (fighter.isEnemy) continue;
      if (string.IsNullOrEmpty(fighter.hpKey)) continue;
      DataMgr.SetInt(fighter.hpKey, fighter.hp);
    }
  }

  private int GetOrInitBattleHp(string key, int defaultHp) {
    int val = DataMgr.GetInt(key);
    if (val <= 0) {
      DataMgr.SetInt(key, defaultHp);
      return defaultHp;
    }
    return val;
  }

  private bool IsBattlePage(string key) {
    return key == BATTLE_PAGE_MAOU || key == BATTLE_PAGE_SLIME || key == BATTLE_PAGE_DOGS;
  }

  private BattleConfig GetBattleConfig(string key) {
    if (key == BATTLE_PAGE_MAOU) {
      int agiMod = DataMgr.GetInt("maou_agi_mod");
      int agi = Mathf.Max(1, 10 + agiMod);
      return new BattleConfig(new BattleEnemyConfig("魔王", 40, 4, agi, "chara/maou_jk"), Line3MaouPageModel.PAGE_KEY);
    }
    if (key == BATTLE_PAGE_SLIME) {
      return new BattleConfig(new BattleEnemyConfig("スライム", 5, 1, 5, "chara/slime"), Attack1bSlimePageModel.PAGE_KEY);
    }
    if (key == BATTLE_PAGE_DOGS) {
      List<BattleEnemyConfig> enemies = new List<BattleEnemyConfig>() {
        new BattleEnemyConfig("ガイア", 4, 1, 30, "chara/dog_king"),
        new BattleEnemyConfig("マッシュ", 6, 1, 40, "chara/dog"),
        new BattleEnemyConfig("オルテガ", 1, 1, 60, "chara/dog_guard"),
      };
      return new BattleConfig(enemies, DogWinMaouPageModel.PAGE_KEY);
    }
    return null;
  }

  private IEnumerator AnimateAttack(BattleFighter actor, BattleFighter target) {
    RectTransform rect = actor.uiRootRect;
    if (rect == null) {
      ApplyDamageAndUpdate(actor, target);
      yield return new WaitForSeconds(BATTLE_TURN_DURATION);
      yield break;
    }
    Vector2 forwardOffset = actor.isEnemy ? new Vector2(-24f, 0f) : new Vector2(24f, 0f);
    Sequence seq = DOTween.Sequence();
    seq.SetLink(rect.gameObject);
    seq.Append(rect.DOAnchorPos(actor.basePos + forwardOffset, 0.4f).SetEase(Ease.OutQuad));
    seq.AppendCallback(() => ApplyDamageAndUpdate(actor, target));
    seq.Append(rect.DOAnchorPos(actor.basePos, 0.1f).SetEase(Ease.InQuad));
    yield return seq.WaitForCompletion();
  }

  private void ApplyDamageAndUpdate(BattleFighter actor, BattleFighter target) {
    if (currentBattleConfig == null) return;
    if (actor == null || target == null) return;
    if (IsEvaded(target)) {
      SpawnMissPopup(target);
      SpawnEvadeBubble(target);
      SpawnAttackEffect(actor, target);
      UpdateBattleUi();
      return;
    }
    target.hp -= actor.atk;
    if (target.hp < 0) target.hp = 0;
    SpawnDamagePopup(target, actor.atk);
    SpawnSparkEffect(target);
    SpawnAttackEffect(actor, target);
    if (target.hp == 0 && !target.isDead) {
      target.isDead = true;
      if (target.hpRoot != null) {
        target.hpRoot.SetActive(false);
      }
      PlayDeathAnimation(target, () => {
        if (target.isEnemy) {
          if (GetAliveEnemies().Count == 0) {
            FinalizeBattleWin();
          }
        } else {
          HandleAllyDeathComplete();
        }
      });
      if (!target.isEnemy && GetAliveAllies().Count == 0) {
        isAllyDeathPending = true;
      }
    }
    SaveBattleHp();
    UpdateBattleUi();
  }

  private bool IsEvaded(BattleFighter target) {
    if (target == null) return false;
    int chance = Mathf.Clamp(target.agi, 0, 95);
    int roll = battleRandom.Next(0, 100);
    return roll < chance;
  }

  private void HandleAllyDeathComplete() {
    if (!isAllyDeathPending) return;
    if (GetAliveAllies().Count > 0) {
      isAllyDeathPending = false;
      return;
    }
    SetBattleGameOverReason(currentBattleKey);
    StopBattle();
    CommonUtil.changeScene("GameOverScene");
  }

  private void SetBattleGameOverReason(string battleKey) {
    if (battleKey == BATTLE_PAGE_MAOU) {
      DataMgr.SetStr("dead_reason", EndingModel.BADEND_MAOU_FAIR);
    } else if (battleKey == BATTLE_PAGE_SLIME) {
      DataMgr.SetStr("dead_reason", EndingModel.BADEND_SLIME);
    } else if (battleKey == BATTLE_PAGE_DOGS) {
      DataMgr.SetStr("dead_reason", EndingModel.BADEND_DOG);
    }
  }

  public void StartShioriInviteDice() {
    StartStatDiceWithImages(
      DataMgr.GetInt("charm"),
      SHIORI_CHARM_THRESHOLD,
      InviteSuccess1ShioriPageModel.PAGE_KEY,
      Ng0ShioriPageModel.PAGE_KEY,
      $"出目の合計が{SHIORI_CHARM_THRESHOLD}以上で成功",
      "chara/kappa_joy",
      "chara/usagi_attack",
      "chara/kappa_sad",
      "chara/tanuki"
    );
  }

  public void StartStatDice(int statValue, int threshold, string successKey, string failKey, string infoText, System.Action successAction = null, System.Action failAction = null) {
    BeginDice(
      statValue,
      threshold,
      successKey,
      failKey,
      infoText,
      true,
      "chara/kappa_joy",
      "chara/usagi_attack",
      "chara/kappa_sad",
      "chara/tanuki",
      successAction,
      failAction
    );
  }

  private void StartStatDiceWithImages(int statValue, int threshold, string successKey, string failKey, string infoText, string successFirstKey, string successSecondKey, string failFirstKey, string failSecondKey) {
    BeginDice(statValue, threshold, successKey, failKey, infoText, true, successFirstKey, successSecondKey, failFirstKey, failSecondKey, null, null);
  }

  private void BeginDice(int statValue, int threshold, string successKey, string failKey, string infoText, bool showResultImages, string successFirstKey, string successSecondKey, string failFirstKey, string failSecondKey, System.Action successAction, System.Action failAction) {
    if (isDiceActive) return;
    isDiceActive = true;
    isDiceResultReady = false;
    diceSuccessKey = successKey;
    diceFailKey = failKey;
    diceStatValue = statValue;
    diceThreshold = threshold;
    diceShowResultImages = showResultImages;
    diceResultSuccessFirstKey = successFirstKey;
    diceResultSuccessSecondKey = successSecondKey;
    diceResultFailFirstKey = failFirstKey;
    diceResultFailSecondKey = failSecondKey;
    diceSuccessAction = successAction;
    diceFailAction = failAction;
    tap_screen_area.SetActive(true);
    if (ChoiceModel.instance != null) {
      ChoiceModel.instance.hideArea();
    }
    if (main_text_area != null) {
      main_text_area.SetActive(true);
    }
    if (main_text != null) {
      main_text.text = infoText;
    }
    CreateDiceUi();
    isDiceRolling = true;
    diceCoroutine = StartCoroutine(DiceRollRoutine());
  }

  private void ClearDiceState() {
    if (diceCoroutine != null) {
      StopCoroutine(diceCoroutine);
      diceCoroutine = null;
    }
    isDiceRolling = false;
    isDiceActive = false;
    isDiceResultReady = false;
    if (diceRoot != null) {
      Destroy(diceRoot);
      diceRoot = null;
    }
    diceLeftImage = null;
    diceRightImage = null;
    diceBackdrop = null;
    diceResultImages.Clear();
    diceSuccessAction = null;
    diceFailAction = null;
    diceShowResultImages = false;
    diceResultSuccessFirstKey = "";
    diceResultSuccessSecondKey = "";
    diceResultFailFirstKey = "";
    diceResultFailSecondKey = "";
    if (page != null && page.page_type == PageModel.PAGE_TYPE_CHOICE) {
      tap_screen_area.SetActive(false);
      if (main_text_area != null) {
        main_text_area.SetActive(false);
      }
    }
  }

  private IEnumerator DiceRollRoutine() {
    const float INTERVAL = 0.05f;
    float duration = 1f;
    float elapsed = 0f;
    while (isDiceRolling && elapsed < duration) {
      diceLeftValue = Random.Range(1, 7);
      diceRightValue = Random.Range(1, 7);
      UpdateDiceSprites();
      elapsed += INTERVAL;
      yield return new WaitForSeconds(INTERVAL);
    }
    isDiceRolling = false;
    ShowDiceResult();
  }

  private void ShowDiceResult() {
    if (!isDiceActive) return;
    bool success = IsDiceSuccess(diceLeftValue, diceRightValue, diceStatValue, diceThreshold);
    if (diceBackdrop != null) {
      diceBackdrop.DOFade(0f, 0.3f).SetEase(Ease.OutQuad).SetLink(diceBackdrop.gameObject);
    }
    if (diceShowResultImages) {
      if (success) {
        SpawnResultImages(diceResultSuccessFirstKey, diceResultSuccessSecondKey);
      } else {
        SpawnResultImages(diceResultFailFirstKey, diceResultFailSecondKey);
      }
    }
    isDiceResultReady = true;
  }

  private bool IsDiceSuccess(int diceA, int diceB, int statValue, int threshold) {
    if (diceA == 1 && diceB == 1) return false;
    if (diceA == 6 && diceB == 6) return true;
    return diceA + diceB + statValue >= threshold;
  }

  private void CreateDiceUi() {
    if (diceRoot != null) return;
    GameObject canvas = GameObject.Find("Canvas");
    if (canvas == null) return;
    diceRoot = new GameObject("dice_ui");
    diceRoot.layer = canvas.layer;
    diceRoot.transform.SetParent(canvas.transform, false);
    RectTransform rootRect = diceRoot.AddComponent<RectTransform>();
    rootRect.anchorMin = new Vector2(0.5f, 0.5f);
    rootRect.anchorMax = new Vector2(0.5f, 0.5f);
    rootRect.pivot = new Vector2(0.5f, 0.5f);
    rootRect.anchoredPosition = Vector2.zero;
    rootRect.sizeDelta = new Vector2(960f, 540f);
    diceRoot.transform.SetAsLastSibling();

    diceBackdrop = CreateDiceBackdrop();
    diceLeftImage = CreateDiceImage("dice_left", new Vector2(-80f, 0f));
    diceRightImage = CreateDiceImage("dice_right", new Vector2(80f, 0f));
    UpdateDiceSprites();
  }

  private Image CreateDiceBackdrop() {
    GameObject obj = new GameObject("dice_backdrop");
    obj.layer = diceRoot.layer;
    obj.transform.SetParent(diceRoot.transform, false);
    RectTransform rect = obj.AddComponent<RectTransform>();
    rect.anchorMin = Vector2.zero;
    rect.anchorMax = Vector2.one;
    rect.pivot = new Vector2(0.5f, 0.5f);
    rect.anchoredPosition = Vector2.zero;
    rect.sizeDelta = Vector2.zero;
    Image img = obj.AddComponent<Image>();
    img.color = new Color(0f, 0f, 0f, 0.6f);
    img.raycastTarget = false;
    return img;
  }

  private Image CreateDiceImage(string name, Vector2 pos) {
    if (diceRoot == null) return null;
    GameObject obj = new GameObject(name);
    obj.layer = diceRoot.layer;
    obj.transform.SetParent(diceRoot.transform, false);
    RectTransform rect = obj.AddComponent<RectTransform>();
    rect.anchorMin = new Vector2(0.5f, 0.5f);
    rect.anchorMax = new Vector2(0.5f, 0.5f);
    rect.pivot = new Vector2(0.5f, 0.5f);
    rect.anchoredPosition = pos;
    rect.sizeDelta = new Vector2(128f, 128f);
    Image img = obj.AddComponent<Image>();
    img.raycastTarget = false;
    return img;
  }

  private void UpdateDiceSprites() {
    if (diceLeftImage != null) {
      diceLeftImage.sprite = GetDiceSprite(diceLeftValue);
    }
    if (diceRightImage != null) {
      diceRightImage.sprite = GetDiceSprite(diceRightValue);
    }
  }

  private Sprite GetDiceSprite(int value) {
    int clamped = Mathf.Clamp(value, 1, 6);
    return Resources.Load<Sprite>($"Textures/other/dice{clamped}");
  }

  private void SpawnResultImages(string firstKey, string secondKey) {
    diceResultImages.Add(CreateResultImage("result_first", firstKey, new Vector2(-320f, 48f)));
    diceResultImages.Add(CreateResultImage("result_second", secondKey, new Vector2(-160f, -48f)));
    foreach (Image img in diceResultImages) {
      if (img == null) continue;
      RectTransform rect = img.GetComponent<RectTransform>();
      Vector2 target = rect.anchoredPosition;
      rect.anchoredPosition = new Vector2(-640f, target.y);
      rect.DOAnchorPos(target, 0.4f).SetEase(Ease.OutBack).SetLink(rect.gameObject);
    }
  }

  private Image CreateResultImage(string name, string spriteKey, Vector2 targetPos) {
    if (diceRoot == null) return null;
    GameObject obj = new GameObject(name);
    obj.layer = diceRoot.layer;
    obj.transform.SetParent(diceRoot.transform, false);
    RectTransform rect = obj.AddComponent<RectTransform>();
    rect.anchorMin = new Vector2(0.5f, 0.5f);
    rect.anchorMax = new Vector2(0.5f, 0.5f);
    rect.pivot = new Vector2(0.5f, 0.5f);
    rect.anchoredPosition = targetPos;
    rect.sizeDelta = new Vector2(160f, 160f);
    Image img = obj.AddComponent<Image>();
    img.sprite = Resources.Load<Sprite>($"Textures/{spriteKey}");
    img.raycastTarget = false;
    return img;
  }

  private void SpawnDamagePopup(BattleFighter target, int damage) {
    if (damageTextPrefab == null || battleUiRoot == null || target == null) return;
    GameObject popup = Instantiate(damageTextPrefab, battleUiRoot.transform, false);
    popup.transform.SetAsLastSibling();
    RectTransform popupRect = popup.GetComponent<RectTransform>();
    if (popupRect != null) {
      popupRect.anchoredPosition = GetDamagePopupPosition(target);
      popupRect.localScale = Vector3.one;
    } else {
      popup.transform.localPosition = new Vector3(0f, 0f, 0f);
    }
    TextMeshProUGUI text = popup.GetComponent<TextMeshProUGUI>();
    if (text == null) {
      text = popup.GetComponentInChildren<TextMeshProUGUI>();
    }
    if (text != null) {
      text.text = damage.ToString();
      text.raycastTarget = false;
      AddDamageTextShadow(text);
    }
  }

  private void SpawnMissPopup(BattleFighter target) {
    if (damageTextPrefab == null || battleUiRoot == null || target == null) return;
    GameObject popup = Instantiate(damageTextPrefab, battleUiRoot.transform, false);
    popup.transform.SetAsLastSibling();
    RectTransform popupRect = popup.GetComponent<RectTransform>();
    if (popupRect != null) {
      popupRect.anchoredPosition = GetDamagePopupPosition(target);
      popupRect.localScale = Vector3.one;
    } else {
      popup.transform.localPosition = new Vector3(0f, 0f, 0f);
    }
    TextMeshProUGUI text = popup.GetComponent<TextMeshProUGUI>();
    if (text == null) {
      text = popup.GetComponentInChildren<TextMeshProUGUI>();
    }
    if (text != null) {
      text.text = BATTLE_EVADE_POPUP_TEXT;
      text.color = new Color(0.9f, 0.9f, 0.9f, 1f);
      text.raycastTarget = false;
      AddDamageTextShadow(text);
    }
  }

  private void SpawnEvadeBubble(BattleFighter target) {
    if (battleUiRoot == null || target == null) return;
    GameObject bubble = new GameObject("evade_bubble");
    bubble.layer = battleUiRoot.layer;
    bubble.transform.SetParent(battleUiRoot.transform, false);
    RectTransform rect = bubble.AddComponent<RectTransform>();
    rect.anchorMin = new Vector2(0.5f, 0.5f);
    rect.anchorMax = new Vector2(0.5f, 0.5f);
    rect.pivot = new Vector2(0.5f, 0.5f);
    rect.sizeDelta = new Vector2(160f, 48f);
    rect.anchoredPosition = GetDamagePopupPosition(target) + new Vector2(0f, 64f);
    rect.localScale = Vector3.one;

    Image bg = bubble.AddComponent<Image>();
    bg.color = new Color(1f, 1f, 1f, 0.85f);
    bg.raycastTarget = false;
    Outline outline = bubble.AddComponent<Outline>();
    outline.effectColor = new Color(0f, 0f, 0f, 0.8f);
    outline.effectDistance = new Vector2(2f, -2f);

    GameObject textObj = new GameObject("bubble_text");
    textObj.layer = bubble.layer;
    textObj.transform.SetParent(bubble.transform, false);
    RectTransform textRect = textObj.AddComponent<RectTransform>();
    textRect.anchorMin = Vector2.zero;
    textRect.anchorMax = Vector2.one;
    textRect.pivot = new Vector2(0.5f, 0.5f);
    textRect.anchoredPosition = Vector2.zero;
    textRect.sizeDelta = Vector2.zero;
    TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
    if (main_text != null && main_text.font != null) {
      text.font = main_text.font;
    }
    text.fontSize = 24f;
    text.alignment = TextAlignmentOptions.Center;
    text.color = Color.black;
    text.text = BATTLE_EVADE_BUBBLE_TEXT;
    text.raycastTarget = false;
    text.margin = new Vector4(8f, 4f, 8f, 4f);

    CanvasGroup group = bubble.AddComponent<CanvasGroup>();
    group.alpha = 1f;
    Sequence seq = DOTween.Sequence();
    seq.SetLink(bubble);
    seq.Join(rect.DOAnchorPos(rect.anchoredPosition + new Vector2(0f, 24f), 0.6f).SetEase(Ease.OutQuad));
    seq.Join(group.DOFade(0f, 0.6f).SetEase(Ease.InQuad));
    seq.OnComplete(() => Destroy(bubble));
  }

  private void SpawnSparkEffect(BattleFighter target) {
    if (sparkParticlePrefab == null || battleUiRoot == null || target == null) return;
    GameObject spark = Instantiate(sparkParticlePrefab, battleUiRoot.transform, false);
    spark.transform.SetAsLastSibling();
    RectTransform sparkRect = spark.GetComponent<RectTransform>();
    if (sparkRect != null) {
      sparkRect.anchoredPosition = GetDamagePopupPosition(target) + new Vector2(0f, -30f);
      sparkRect.localScale = Vector3.one;
    } else {
      spark.transform.localPosition = new Vector3(0f, 0f, 0f);
    }
    ParticleSystem ps = spark.GetComponent<ParticleSystem>();
    float lifetime = 0.2f;
    if (ps != null) {
      ParticleSystem.MainModule main = ps.main;
      main.duration = 0.15f;
      main.startLifetime = 0.15f;
      main.startSize = 0.35f;
      main.startSpeed = 4f;
      main.startColor = new ParticleSystem.MinMaxGradient(new Color(1f, 0.85f, 0.2f, 1f));
      ps.Play();
    }
    Destroy(spark, lifetime);
  }

  private void SpawnAttackEffect(BattleFighter actor, BattleFighter target) {
    GameObject prefab = GetAttackEffectPrefab(actor);
    if (prefab == null || target == null) return;
    GameObject effect = Instantiate(prefab);
    Vector3 worldPos = GetEffectWorldPosition(target);
    effect.transform.position = worldPos;
    effect.transform.SetAsLastSibling();
    Destroy(effect, 0.6f);
  }

  private GameObject GetAttackEffectPrefab(BattleFighter actor) {
    if (actor == null) return null;
    if (actor.name == "カッパ") return swordEffectPrefab;
    if (actor.name == "タヌキ") return crowEffectPrefab;
    if (actor.name == "ガイア") return crowEffectPrefab;
    if (actor.name == "マッシュ") return crowEffectPrefab;
    if (actor.name == "オルテガ") return crowEffectPrefab;
    if (actor.name == "スライム") return impactEffectPrefab;
    if (actor.name == "魔王") return fireEffectPrefab;
    return null;
  }

  private void AddDamageTextShadow(TextMeshProUGUI text) {
    Shadow shadow = text.GetComponent<Shadow>();
    if (shadow == null) {
      shadow = text.gameObject.AddComponent<Shadow>();
    }
    shadow.effectColor = new Color(0f, 0f, 0f, 0.85f);
    shadow.effectDistance = new Vector2(2f, -2f);
  }

  private Vector3 GetEffectWorldPosition(BattleFighter target) {
    if (target != null && target.uiRootRect != null) {
      Vector3 worldPos = target.uiRootRect.position;
      worldPos.z = 0f;
      return worldPos;
    }
    return Vector3.zero;
  }

  private Vector2 GetDamagePopupPosition(BattleFighter target) {
    if (target != null && target.uiRootRect != null && battleUiRect != null) {
      Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, target.uiRootRect.position);
      if (RectTransformUtility.ScreenPointToLocalPointInRectangle(battleUiRect, screenPos, null, out Vector2 localPos)) {
        return localPos + new Vector2(0f, 96f);
      }
    }
    if (target != null) {
      return target.basePos + new Vector2(0f, 96f);
    }
    return Vector2.zero;
  }

  private class BattleFighter {
    public string name;
    public int hp;
    public int maxHp;
    public int atk;
    public int agi;
    public bool isEnemy;
    public string spriteKey;
    public Slider hpSlider;
    public TextMeshProUGUI hpText;
    public RectTransform uiRootRect;
    public Vector2 basePos;
    public string hpKey;
    public GameObject hpRoot;
    public bool isDead;

    public BattleFighter(string name, int hp, int maxHp, int atk, int agi, bool isEnemy, string spriteKey, string hpKey) {
      this.name = name;
      this.hp = hp;
      this.maxHp = maxHp;
      this.atk = atk;
      this.agi = agi;
      this.isEnemy = isEnemy;
      this.spriteKey = spriteKey;
      this.hpKey = hpKey;
      this.isDead = false;
    }
  }

  private void PlayDeathAnimation(BattleFighter fighter, System.Action onComplete) {
    if (fighter == null || fighter.uiRootRect == null) return;
    RectTransform rect = fighter.uiRootRect;
    Vector2 direction = fighter.isEnemy ? new Vector2(220f, 220f) : new Vector2(-220f, 220f);
    Sequence seq = DOTween.Sequence();
    seq.SetLink(rect.gameObject);
    seq.Join(rect.DOAnchorPos(fighter.basePos + direction, 0.6f).SetEase(Ease.OutQuad));
    seq.Join(rect.DORotate(new Vector3(0f, 0f, fighter.isEnemy ? -360f : 360f), 0.6f, RotateMode.FastBeyond360));
    seq.Append(rect.DOScale(0.6f, 0f));
    CanvasGroup group = rect.GetComponent<CanvasGroup>();
    if (group == null) {
      group = rect.gameObject.AddComponent<CanvasGroup>();
    }
    group.alpha = 1f;
    seq.Append(group.DOFade(0f, 0.5f).SetEase(Ease.OutQuad));
    seq.OnComplete(() => {
      onComplete?.Invoke();
      rect.gameObject.SetActive(false);
    });
  }

  private void FinalizeBattleWin() {
    if (isBattleWinPending) return;
    isBattleWinPending = true;
    IsBattleEnded();
  }

  private class BattleEnemyConfig {
    public string name;
    public int hp;
    public int atk;
    public int agi;
    public string spriteKey;

    public BattleEnemyConfig(string name, int hp, int atk, int agi, string spriteKey) {
      this.name = name;
      this.hp = hp;
      this.atk = atk;
      this.agi = agi;
      this.spriteKey = spriteKey;
    }
  }

  private class BattleConfig {
    public List<BattleEnemyConfig> enemies = new List<BattleEnemyConfig>();
    public string nextPageKey;

    public BattleConfig(BattleEnemyConfig enemy, string nextPageKey) {
      enemies.Add(enemy);
      this.nextPageKey = nextPageKey;
    }

    public BattleConfig(List<BattleEnemyConfig> enemies, string nextPageKey) {
      this.enemies = enemies ?? new List<BattleEnemyConfig>();
      this.nextPageKey = nextPageKey;
    }
  }

  public void slideOutAndUpdate(string key) {
    if (is_nandate_animating) return;
    playSlideOut(() => {
      DataMgr.SetStr("page", key);
      updateScene(key);
    });
  }

  private bool isSlidePage(string key) {
    return key == "castle/op2"
      || key == "shiori/ng2"
      || key == "castle/ask_private_love1"
      || key == "castle/ask_politics1"
      || key == "shiori/invite_success6"
      || key == "shiori/movie4"
      || key == "shiori/game1"
      || key == "shiori/game3"
      || key == "shiori/next_warn1"
      || key == "maou/sneak_success1"
      || key == "maou/gokuri1"
      || key == "maou/gokuri2"
      || key == "maou/dog_talk1";
  }

  private bool isKeepSlideState(string key) {
    return key == "shiori/ng3";
  }

  private bool isSlideOutPage(string key) {
    return key == "castle/op2"
      || key == "shiori/ng3"
      || key == "castle/ask_private_love1"
      || key == "castle/ask_politics1"
      || key == "shiori/invite_success6"
      || key == "shiori/movie4"
      || key == "shiori/game1"
      || key == "shiori/game3"
      || key == "shiori/next_warn1"
      || key == "maou/sneak_success1"
      || key == "maou/gokuri2"
      || key == "maou/dog_talk1";
  }

  private string getSlideSpriteKey(string key) {
    if (key == "castle/ask_private_love1") return "other/cutin";
    if (key == "castle/ask_politics1") return "bg/bg_youhishi";
    if (key == "shiori/invite_success6") return "other/cutin";
    if (key == "shiori/movie4") return "other/cutin";
    if (key == "shiori/game1") return "other/cutin";
    if (key == "shiori/game3") return "other/cutin";
    if (key == "shiori/next_warn1") return "240_135/majisuka";
    if (key == "maou/gokuri1") return "240_135/majisuka";
    if (key == "maou/gokuri2") return "other/cutin";
    if (key == "maou/dog_talk1") return "240_135/kappa_hello";
    return "240_135/nandate";
  }

  private void goToGameOver(string key) {
    string[] res = key.Split("/");
    string game_over_key = res[1];
    DataMgr.SetStr("dead_reason", game_over_key);
    DataMgr.SetStr(AUTO_SAVE_PAGE_KEY, "");
    DataMgr.SetStr("page", "");

    CommonUtil.changeScene("GameOverScene");
  }

  private bool HasActiveChoiceButtons() {
    GameObject area = ChoiceModel.instance != null ? ChoiceModel.instance.choice_area.gameObject : null;
    if (area == null || !area.activeSelf) return false;
    GameObject b1 = ChoiceModel.instance.button1;
    GameObject b2 = ChoiceModel.instance.button2;
    GameObject b3 = ChoiceModel.instance.button3;
    return (b1 != null && b1.activeInHierarchy)
      || (b2 != null && b2.activeInHierarchy)
      || (b3 != null && b3.activeInHierarchy);
  }

  private void UpdateOverlayImage(string key) {
    if (key == DogIntroMaouPageModel.PAGE_KEY) {
      EnsureDogIntroImages();
      if (dogIntroRoot != null) {
        dogIntroRoot.gameObject.SetActive(true);
      }
      HideCutinCharacterImage();
      MarkCharacterSeen("dogs");
      return;
    }
    if (dogIntroRoot != null) {
      dogIntroRoot.gameObject.SetActive(false);
    }
    if (IsMajisukaCutinKey(key)) {
      HideCutinCharacterImage();
      return;
    }
    if (key == "maou/gokuri2") {
      ShowCutinCharacterImage("chara/kappa_dot");
      return;
    }
    HideCutinCharacterImage();
  }

  private bool IsMajisukaCutinKey(string key) {
    return key == "shiori/next_warn1"
      || key == "castle/ask_private_love2"
      || key == "castle/ask_private_love_choice"
      || key == "usagi/start"
      || key == "usagi/start2"
      || key == "usagi/choice"
      || key == "usagi/refuse"
      || key == "maou/gokuri1";
  }

  public void ShowLockedChoiceNotice() {
    if (isLockedNoticeActive) return;
    isLockedNoticeActive = true;
    LockedChoiceReaction reaction = BuildLockedChoiceReaction();
    initNameArea();
    if (main_bg_rect != null) {
      lockedPrevMainPos = main_bg_rect.anchoredPosition;
    }
    if (left_bg_rect != null) {
      lockedPrevLeftPos = left_bg_rect.anchoredPosition;
    }
    if (left_bg_image != null) {
      lockedPrevLeftSprite = left_bg_image.sprite;
    }
    lockedPrevMainTextAreaActive = main_text_area != null && main_text_area.activeSelf;
    if (main_text != null) {
      lockedPrevMainText = main_text.text;
    }
    if (name_text != null) {
      lockedPrevSpeaker = name_text.text;
    }
    if (name_area_rect != null) {
      lockedPrevNameAreaActive = name_area_rect.gameObject.activeSelf;
    }
    Image cutinImage = GetCutinCharacterImage();
    if (cutinImage != null) {
      lockedPrevOverlaySprite = cutinImage.sprite;
      lockedPrevOverlayActive = cutinImage.gameObject.activeSelf;
    }
    if (main_image_ui != null) {
      lockedPrevMainImageSprite = main_image_ui.sprite;
      lockedPrevMainImageActive = main_image_ui.gameObject.activeSelf;
    }
    HideChoiceUiForLockedNotice();
    ApplyLockedChoiceReactionVisual(reaction);
    if (main_text_area != null) {
      main_text_area.SetActive(true);
    }
    if (main_text != null) {
      main_text.text = LocalizationUtil.GetOrDefault(
        reaction.textKey,
        reaction.textFallback
      );
    }
    if (name_text != null) {
      name_text.text = LocalizationUtil.GetOrDefault(
        reaction.speakerKey,
        reaction.speakerFallback
      );
    }
    if (name_area_rect != null) {
      name_area_rect.gameObject.SetActive(true);
    }
    if (tap_screen_area != null) {
      tap_screen_area.SetActive(false);
    }
  }

  private void HideLockedChoiceNotice() {
    if (!isLockedNoticeActive) return;
    isLockedNoticeActive = false;
    RestoreLockedNoticeBackground();
    RestoreChoiceUiForLockedNotice();
    if (main_text_area != null) {
      main_text_area.SetActive(lockedPrevMainTextAreaActive);
    }
    if (main_text != null) {
      main_text.text = lockedPrevMainText;
    }
    if (name_text != null) {
      name_text.text = lockedPrevSpeaker;
    }
    if (name_area_rect != null) {
      name_area_rect.gameObject.SetActive(lockedPrevNameAreaActive);
    }
    if (main_image_ui != null) {
      main_image_ui.sprite = lockedPrevMainImageSprite;
      main_image_ui.gameObject.SetActive(lockedPrevMainImageActive);
    }
    if (page != null && tap_screen_area != null) {
      bool canTap = page.page_type == PageModel.PAGE_TYPE_NORMAL || page.page_type == PageModel.PAGE_TYPE_MAP_MOVE;
      tap_screen_area.SetActive(canTap);
    }
  }

  private LockedChoiceReaction BuildLockedChoiceReaction() {
    List<LockedChoiceReaction> reactions = new List<LockedChoiceReaction>();
    if (DataMgr.GetBool("ally_usagi_joined")) {
      reactions.Add(new LockedChoiceReaction {
        backgroundSpriteKey = LOCKED_CHOICE_BG_SPRITE_KEY,
        spriteKey = "chara/usagi_maji",
        textKey = LocalizationKeys.LockedChoiceNoticeText,
        textFallback = "ちょっ、\nそのボタンを押す条件を満たしてないっす！",
        speakerKey = LocalizationKeys.LockedChoiceNoticeSpeaker,
        speakerFallback = "ウサギ",
      });
    }
    if (DataMgr.GetBool("ally_shiori_joined")) {
      reactions.Add(new LockedChoiceReaction {
        backgroundSpriteKey = LOCKED_CHOICE_BG_SPRITE_KEY,
        spriteKey = "128_128/shiori_cool",
        textKey = LocalizationKeys.LockedChoiceNoticeShioriText,
        textFallback = "そのボタンは押す条件を\n満たしていないんじゃーないかな",
        speakerKey = LocalizationKeys.LockedChoiceNoticeShioriSpeaker,
        speakerFallback = "シオリーナ",
      });
    }
    if (reactions.Count == 0) {
      return new LockedChoiceReaction {
        backgroundSpriteKey = LOCKED_CHOICE_BG_SPRITE_KEY,
        spriteKey = "chara/kappa_sad",
        textKey = LocalizationKeys.LockedChoiceNoticeKappaText,
        textFallback = "くっ、ボタンを押す条件を満たしていない！",
        speakerKey = LocalizationKeys.LockedChoiceNoticeKappaSpeaker,
        speakerFallback = "カッパ",
      };
    }
    return reactions[Random.Range(0, reactions.Count)];
  }

  private void ApplyLockedChoiceReactionVisual(LockedChoiceReaction reaction) {
    if (reaction == null) return;
    playSlideIn(string.IsNullOrEmpty(reaction.backgroundSpriteKey) ? LOCKED_CHOICE_BG_SPRITE_KEY : reaction.backgroundSpriteKey);
    if (main_image_ui != null) {
      main_image_ui.gameObject.SetActive(false);
    }
    ShowCutinCharacterImage(reaction.spriteKey);
  }

  private Image GetCutinCharacterImage() {
    if (left_main_image == null) {
      Debug.LogError("left_main_image is not assigned.");
    }
    return left_main_image;
  }

  private void ShowCutinCharacterImage(string spriteKey) {
    Image cutinImage = GetCutinCharacterImage();
    if (cutinImage == null) return;
    Sprite sprite = Resources.Load<Sprite>($"Textures/{spriteKey}");
    cutinImage.sprite = sprite;
    cutinImage.gameObject.SetActive(sprite != null);
  }

  private void HideCutinCharacterImage() {
    Image cutinImage = GetCutinCharacterImage();
    if (cutinImage == null) return;
    cutinImage.gameObject.SetActive(false);
  }

  private void RestoreLockedNoticeBackground() {
    if (main_bg_rect == null || left_bg_rect == null) {
      if (left_bg_image != null) {
        left_bg_image.sprite = lockedPrevLeftSprite;
      }
      return;
    }
    main_bg_rect.DOKill();
    left_bg_rect.DOKill();
    float duration = 0.25f;
    Sequence seq = DOTween.Sequence();
    seq.Join(main_bg_rect.DOAnchorPos(lockedPrevMainPos, duration).SetEase(Ease.InQuad));
    seq.Join(left_bg_rect.DOAnchorPos(lockedPrevLeftPos, duration).SetEase(Ease.InQuad));
    seq.OnComplete(() => {
      if (left_bg_image != null) {
        left_bg_image.sprite = lockedPrevLeftSprite;
      }
    });
  }

  private void HideChoiceUiForLockedNotice() {
    if (ChoiceModel.instance == null) return;
    if (ChoiceModel.instance.choice_area != null) {
      lockedPrevChoiceAreaActive = ChoiceModel.instance.choice_area.gameObject.activeSelf;
      ChoiceModel.instance.choice_area.gameObject.SetActive(false);
    }
    if (ChoiceModel.instance.choice_title != null) {
      lockedPrevChoiceTitleActive = ChoiceModel.instance.choice_title.gameObject.activeSelf;
      ChoiceModel.instance.choice_title.gameObject.SetActive(false);
      if (ChoiceModel.instance.choice_title.transform.parent != null) {
        lockedChoiceTitleParent = ChoiceModel.instance.choice_title.transform.parent.gameObject;
        lockedPrevChoiceTitleParentActive = lockedChoiceTitleParent.activeSelf;
        lockedChoiceTitleParent.SetActive(false);
      }
    }
  }

  private void RestoreChoiceUiForLockedNotice() {
    if (ChoiceModel.instance == null) return;
    if (ChoiceModel.instance.choice_area != null) {
      ChoiceModel.instance.choice_area.gameObject.SetActive(lockedPrevChoiceAreaActive);
    }
    if (ChoiceModel.instance.choice_title != null) {
      ChoiceModel.instance.choice_title.gameObject.SetActive(lockedPrevChoiceTitleActive);
    }
    if (lockedChoiceTitleParent != null) {
      lockedChoiceTitleParent.SetActive(lockedPrevChoiceTitleParentActive);
    }
    lockedChoiceTitleParent = null;
  }

  private void EnsureLockedNoticeObjects() {
    if (lockedNoticeRoot != null) return;
    GameObject canvas = GameObject.Find("Canvas");
    if (canvas == null) return;

    GameObject root = new GameObject("locked_notice_root");
    root.transform.SetParent(canvas.transform, false);
    root.transform.localScale = Vector3.one;
    RectTransform rootRect = root.AddComponent<RectTransform>();
    rootRect.anchorMin = Vector2.zero;
    rootRect.anchorMax = Vector2.one;
    rootRect.pivot = new Vector2(0.5f, 0.5f);
    rootRect.anchoredPosition = Vector2.zero;
    rootRect.sizeDelta = Vector2.zero;
    Image rootImage = root.AddComponent<Image>();
    rootImage.color = new Color(0f, 0f, 0f, 0f);
    rootImage.raycastTarget = true;

    EventTrigger trigger = root.AddComponent<EventTrigger>();
    EventTrigger.Entry entry = new EventTrigger.Entry();
    entry.eventID = EventTriggerType.PointerClick;
    entry.callback.AddListener(_ => HideLockedChoiceNotice());
    trigger.triggers = new List<EventTrigger.Entry> { entry };

    GameObject usagiObj = new GameObject("locked_notice_usagi");
    usagiObj.transform.SetParent(root.transform, false);
    usagiObj.transform.localScale = Vector3.one;
    RectTransform usagiRect = usagiObj.AddComponent<RectTransform>();
    usagiRect.anchorMin = new Vector2(0.5f, 0.5f);
    usagiRect.anchorMax = new Vector2(0.5f, 0.5f);
    usagiRect.pivot = new Vector2(0.5f, 0.5f);
    usagiRect.sizeDelta = new Vector2(320f, 320f);
    usagiRect.anchoredPosition = new Vector2(0f, 40f);
    lockedNoticeUsagi = usagiObj.AddComponent<Image>();
    lockedNoticeUsagi.raycastTarget = false;

    GameObject textObj = new GameObject("locked_notice_text");
    textObj.transform.SetParent(root.transform, false);
    textObj.transform.localScale = Vector3.one;
    RectTransform textRect = textObj.AddComponent<RectTransform>();
    textRect.anchorMin = new Vector2(0.5f, 0.5f);
    textRect.anchorMax = new Vector2(0.5f, 0.5f);
    textRect.pivot = new Vector2(0.5f, 0.5f);
    textRect.sizeDelta = new Vector2(640f, 120f);
    textRect.anchoredPosition = new Vector2(0f, -140f);
    lockedNoticeText = textObj.AddComponent<TextMeshProUGUI>();
    if (main_text != null && main_text.font != null) {
      lockedNoticeText.font = main_text.font;
    }
    lockedNoticeText.fontSize = 28f;
    lockedNoticeText.alignment = TextAlignmentOptions.Center;
    lockedNoticeText.color = Color.black;
    lockedNoticeText.raycastTarget = false;

    lockedNoticeRoot = root;
    lockedNoticeRoot.SetActive(false);
  }

  private void EnsureDogIntroImages() {
    if (dogIntroRoot != null) return;
    GameObject canvas = GameObject.Find("Canvas");
    if (canvas == null) return;
    GameObject obj = new GameObject("dog_intro_images");
    obj.transform.SetParent(canvas.transform, false);
    RectTransform rect = obj.AddComponent<RectTransform>();
    rect.anchorMin = new Vector2(0.5f, 0.5f);
    rect.anchorMax = new Vector2(0.5f, 0.5f);
    rect.pivot = new Vector2(0.5f, 0.5f);
    rect.sizeDelta = new Vector2(960f, 320f);
    rect.anchoredPosition = new Vector2(0f, 32f);
    rect.localScale = Vector3.one;
    dogIntroRoot = rect;

    dogIntroGaiaImage = CreateDogIntroImage("dog_gaia", dogIntroRoot, "chara/dog_king", new Vector2(-240f, 0f));
    dogIntroMashImage = CreateDogIntroImage("dog_mash", dogIntroRoot, "chara/dog", new Vector2(0f, 0f));
    dogIntroOrtegaImage = CreateDogIntroImage("dog_ortega", dogIntroRoot, "chara/dog_guard", new Vector2(240f, 0f));

    if (main_text_area != null) {
      int index = main_text_area.transform.GetSiblingIndex();
      obj.transform.SetSiblingIndex(Mathf.Max(0, index - 1));
    } else {
      obj.transform.SetAsLastSibling();
    }
  }

  private Image CreateDogIntroImage(string name, RectTransform parent, string spriteKey, Vector2 position) {
    GameObject obj = new GameObject(name);
    obj.transform.SetParent(parent, false);
    obj.transform.localScale = Vector3.one;
    RectTransform rect = obj.AddComponent<RectTransform>();
    rect.anchorMin = new Vector2(0.5f, 0.5f);
    rect.anchorMax = new Vector2(0.5f, 0.5f);
    rect.pivot = new Vector2(0.5f, 0.5f);
    rect.sizeDelta = new Vector2(200f, 200f);
    rect.anchoredPosition = position;
    Image image = obj.AddComponent<Image>();
    image.sprite = Resources.Load<Sprite>($"Textures/{spriteKey}");
    image.raycastTarget = false;
    return image;
  }
}
