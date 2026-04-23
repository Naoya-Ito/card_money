using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class KappaController : MonoBehaviour {
  [SerializeField] public Animator anim;
  [SerializeField] public Image image; // 見た目の処理
  [SerializeField] private RuntimeAnimatorController usagi_run_controller;
  [SerializeField] private RuntimeAnimatorController tanuki_run_controller;
  [SerializeField] private Sprite usagi_run_sprite;
  [SerializeField] private Sprite tanuki_run_sprite;
  [SerializeField] private Vector2 usagi_run_offset = new Vector2(-140f, 0f);
  [SerializeField] private Vector2 tanuki_run_offset = new Vector2(140f, 0f);

  [System.NonSerialized] public static KappaController instance = null;
  private GameObject usagi_obj;
  private GameObject tanuki_obj;
  private Animator usagi_anim;
  private Animator tanuki_anim;
  private RectTransform usagi_rect;
  private RectTransform tanuki_rect;
  private void Awake(){
    if(instance == null) {
      instance = this;
    } else {
      Destroy(this.gameObject);
    }
    HideRunCompanions();
  }

  //private Sequence startSequence;
  private void setSequence(){
/*
    float move_front_time = 0.1f;
    float move_back_time = 0.2f;
    startSequence = DOTween.Sequence()
      .Append(view.image.gameObject.transform.DOLocalMoveX(RingConst.WALK_WIDTH, RingConst.WALK_ANIMATION_DURATION).SetRelative())
      .Pause()
      .SetAutoKill(false)
      .SetLink(view.image.gameObject)
      .OnComplete(() => startAnimationEnd());

      */
  }

  public void changeImage(string key) { 
    Sprite sprite = Resources.Load<Sprite>($"Textures/chara/{key}");
    image.sprite = sprite;
  }

  public void showCenter() { 
    setLocalPosition(new Vector2(0, 0));
    bringToFront();
  }
  public void showLeft() {
    setLocalPosition(new Vector2(-320f, 0f));
    bringToFront();
  }
  public void hideKappa() { 
    setLocalPosition(new Vector2(-18000, 0));
    HideRunCompanions();
  }

  // 登場時アニメーション
  public void startAnimation()
  {
    animateRun();
    //view.fullImage();
    //view.image.transform.localPosition = new Vector3(- RingConst.WALK_WIDTH , 0, 0);
    //startSequence.Restart();
  }

  private void startAnimationEnd(){
    stopAnimation();
  }

  public void stopAnimation(){
    anim.enabled = false;
    anim.SetInteger("moveType", RingConst.ANIMATION_STATE_IDOL);
    HideRunCompanions();
  }

  public void animateRun(){
    anim.enabled = true;
    anim.SetInteger("moveType", RingConst.ANIMATION_STATE_KAPPA_RUN);
    bringToFront();
    ShowRunCompanions();
  }

  public void animateRunInPlace() {
    anim.enabled = true;
    anim.SetInteger("moveType", RingConst.ANIMATION_STATE_KAPPA_RUN);
    ShowRunCompanions();
  }

  public void animateDrum(){
    anim.enabled = true;
    anim.SetInteger("moveType", RingConst.ANIMATION_STATE_KAPPA_DRUM);
    HideRunCompanions();
  }

  private void bringToFront() {
    GameObject canvas = GameObject.Find("Canvas");
    if (canvas != null && transform.parent != canvas.transform) {
      transform.SetParent(canvas.transform, false);
    }
    if (transform.parent != null) {
      transform.SetAsLastSibling();
    }
    setLocalPosition(new Vector2(transform.localPosition.x, transform.localPosition.y));
  }

  private void setLocalPosition(Vector2 pos) {
    RectTransform rect = GetComponent<RectTransform>();
    if (rect != null) {
      rect.anchoredPosition = pos;
      Vector3 local_pos = rect.localPosition;
      local_pos.z = 0f;
      rect.localPosition = local_pos;
      UpdateRunCompanionPositions();
      return;
    }
    transform.localPosition = new Vector3(pos.x, pos.y, 0f);
    UpdateRunCompanionPositions();
  }

  private void ShowRunCompanions() {
    if (!IsGameScene()) return;
    EnsureRunCompanions();
    bool usagiJoined = DataMgr.GetBool("ally_usagi_joined");
    bool showUsagi = usagiJoined;
    bool showTanuki = false;
    if (usagi_obj != null) usagi_obj.SetActive(showUsagi);
    if (tanuki_obj != null) tanuki_obj.SetActive(showTanuki);
    if (showUsagi) {
      StartRunAnimation(usagi_anim, "usagi_run", RingConst.ANIMATION_STATE_USAGI_RUN);
    }
    if (showTanuki) {
      StartRunAnimation(tanuki_anim, "tanuki_run", RingConst.ANIMATION_STATE_TANUKI_RUN);
    }
    UpdateRunCompanionPositions();
  }

  private void HideRunCompanions() {
    if (usagi_obj != null) usagi_obj.SetActive(false);
    if (tanuki_obj != null) tanuki_obj.SetActive(false);
  }

  private void EnsureRunCompanions() {
    if (usagi_obj == null && usagi_run_controller != null) {
      usagi_obj = CreateRunCompanion("usagi_run", usagi_run_controller, usagi_run_sprite, out usagi_anim, out usagi_rect);
    }
    if (tanuki_obj == null && tanuki_run_controller != null) {
      tanuki_obj = CreateRunCompanion("tanuki_run", tanuki_run_controller, tanuki_run_sprite, out tanuki_anim, out tanuki_rect);
    }
    if (usagi_obj != null && usagi_obj.transform.parent != transform.parent) {
      usagi_obj.transform.SetParent(transform.parent, false);
    }
    if (tanuki_obj != null && tanuki_obj.transform.parent != transform.parent) {
      tanuki_obj.transform.SetParent(transform.parent, false);
    }
  }

  private void StartRunAnimation(Animator animator, string stateName, int moveType) {
    if (animator == null) return;
    animator.enabled = true;
    animator.speed = 1f;
    animator.SetInteger("moveType", moveType);
    animator.Play(stateName, 0, 0f);
    if (animator.gameObject.activeInHierarchy) {
      animator.Update(0f);
    }
  }

  private GameObject CreateRunCompanion(string name, RuntimeAnimatorController controller, Sprite sprite, out Animator animator, out RectTransform rect) {
    GameObject obj = new GameObject(name);
    obj.layer = gameObject.layer;
    obj.transform.SetParent(transform.parent, false);
    rect = obj.AddComponent<RectTransform>();
    rect.anchorMin = new Vector2(0.5f, 0.5f);
    rect.anchorMax = new Vector2(0.5f, 0.5f);
    rect.pivot = new Vector2(0.5f, 0.5f);
    rect.sizeDelta = new Vector2(128f, 128f);
    obj.AddComponent<CanvasRenderer>();
    Image img = obj.AddComponent<Image>();
    img.sprite = sprite;
    img.raycastTarget = false;
    animator = obj.AddComponent<Animator>();
    animator.runtimeAnimatorController = controller;
    animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
    obj.SetActive(false);
    return obj;
  }

  private void UpdateRunCompanionPositions() {
    RectTransform rect = GetComponent<RectTransform>();
    if (rect == null) return;
    Vector2 basePos = rect.anchoredPosition;
    if (usagi_rect != null) {
      usagi_rect.anchoredPosition = basePos + usagi_run_offset;
      KeepCompanionBehind(usagi_rect);
    }
    if (tanuki_rect != null) {
      tanuki_rect.anchoredPosition = basePos + tanuki_run_offset;
      KeepCompanionBehind(tanuki_rect);
    }
  }

  private void KeepCompanionBehind(Transform companion) {
    if (companion == null || transform.parent == null) return;
    int kappaIndex = transform.GetSiblingIndex();
    companion.SetSiblingIndex(Mathf.Max(kappaIndex - 1, 0));
  }

  private bool IsGameScene() {
    return SceneManager.GetActiveScene().name == "GameScene";
  }
}
