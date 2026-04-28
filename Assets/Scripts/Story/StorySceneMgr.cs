using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.InputSystem;

public class StorySceneMgr : MonoBehaviour {
  [System.NonSerialized] public static StorySceneMgr instance = null;
  [SerializeField] public GameObject main_text_area;
  [SerializeField] public GameObject tap_screen_area;
  [SerializeField] public Image image;
  [SerializeField] public Image main_image_ui;
  [SerializeField] public TextMeshProUGUI main_text;
  [System.NonSerialized] public PageModel page;
  private string current_page_key = "";
  private string localizedMainText = "";
  private string localizedSpeaker = "";

  void Start() {
    string key = DataMgr.GetStr("page");
    updateScene(key);
  }

  private void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(this.gameObject);
    }
  }

  public void goToNextPage(string key) {
    DataMgr.SetStr("page", key);
    updateScene(key);
  }

  public void tappedScreen() {
    if (page == null) return;
    if (page.page_type != PageModel.PAGE_TYPE_NORMAL && page.page_type != PageModel.PAGE_TYPE_MAP_MOVE) return;
    string nextKey = page.next_page;
    if (string.IsNullOrEmpty(nextKey)) return;
    // TODO 今のままでは動かない: PageModel.pushedTappedScreen は GameSceneMgr.instance に依存している可能性がある
    PageModel.pushedTappedScreen(nextKey);
  }

  public void updateScene(string key) {
    string prevKey = current_page_key;
    current_page_key = key;
    DataMgr.SetStr("page", key);
    page = PageModel.getPageModelByKey(key);
    if (page.TryTransitScene()) {
      return;
    }
    localizedMainText = ResolveLocalizedPageText(key, "main_text", page.main_text);
    localizedSpeaker = ResolveLocalizedPageText(key, "speaker", page.speaker);
    if (page.page_type != PageModel.PAGE_TYPE_MAP_MOVE) {
      // TODO 今のままでは動かない: map_move の復帰処理は StoryScene に未実装
    }
    if (key.EndsWith("/end")) {
      if (string.IsNullOrEmpty(localizedMainText) && !string.IsNullOrEmpty(page.next_page)) {
        PageModel.pushedTappedScreen(page.next_page);
        return;
      }
    }

    switch (page.page_type) {
      case PageModel.PAGE_TYPE_NORMAL:
        if (tap_screen_area != null) tap_screen_area.SetActive(true);
        if (main_text_area != null) main_text_area.SetActive(true);
        if (string.IsNullOrEmpty(localizedMainText)) {
          if (main_text_area != null) main_text_area.SetActive(false);
        } else {
          if (main_text_area != null) main_text_area.SetActive(true);
          if (main_text != null) main_text.text = localizedMainText;
        }
        break;
      case PageModel.PAGE_TYPE_MAP_MOVE:
        if (tap_screen_area != null) tap_screen_area.SetActive(true);
        if (main_text_area != null) main_text_area.SetActive(true);
        if (string.IsNullOrEmpty(localizedMainText)) {
          if (main_text_area != null) main_text_area.SetActive(false);
        } else {
          if (main_text_area != null) main_text_area.SetActive(true);
          if (main_text != null) main_text.text = localizedMainText;
        }
        PlayMapMove(page);
        break;
      case PageModel.PAGE_TYPE_CHOICE:
        if (tap_screen_area != null) tap_screen_area.SetActive(false);
        if (main_text_area != null) main_text_area.SetActive(false);
        break;
      default:
        Debug.Log($"unknown page type. type={page.page_type}");
        break;
    }

    if (page.bgm != "") {
      if (BGMMgr.instance != null) {
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
    Sprite prev_sprite = image != null ? image.sprite : null;
    if (page.page_type == PageModel.PAGE_TYPE_MAP_MOVE) {
      if (image != null) image.sprite = prev_sprite;
    } else if (!is_slide_page) {
      if (image != null) image.sprite = Resources.Load<Sprite>($"Textures/{page.main_bg}");
    } else {
      if (image != null) image.sprite = prev_sprite;
    }
    MarkGallerySeenFromSprite(image != null ? image.sprite : null);
    UpdateMainImage(page);
  }

  private void updateNameArea() {
    // TODO 今のままでは StoryScene に対応する名前表示レイアウトを実装していない
  }

  private void updateMainTextStyle(string key) {
    if (main_text == null) return;
    if (key == "castle/op2") {
      main_text.alignment = TextAlignmentOptions.Center;
      main_text.fontSize = main_text.fontSize * 1.4f;
    } else {
      main_text.alignment = TextAlignmentOptions.Left;
    }
  }

  private string ResolveLocalizedPageText(string pageKey, string field, string fallback) {
    string localized = LocalizationUtil.Get(LocalizationUtil.GetPageKey(pageKey, field));
    if (!string.IsNullOrEmpty(localized)) return localized;
    return fallback;
  }

  private void MarkGallerySeen(string imageKey) {
    // TODO 今のままでは StoryScene にギャラリー管理を合わせる実装が必要
  }

  private void MarkGallerySeenFromSprite(Sprite sprite) {
    // TODO 今のままでは StoryScene にギャラリー管理を合わせる実装が必要
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

  private void PlayMapMove(PageModel model) {
    // TODO 今のままでは動かない: map_move のアニメーションを StoryScene に移していない
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
}
