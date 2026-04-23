using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVSceneMgr : MonoBehaviour {

  // 最後はタイトル画面が表示され、fadeout と共に発売日を表示


  [SerializeField] public Image bg;

  [System.NonSerialized] public static PVSceneMgr instance = null;

/*
  private void Awake(){
    if(instance == null) {
      instance = this;
    } else {
      Destroy(this.gameObject);
    }
  }

  void Start() {
    KappaController.instance.InitWithoutData();
    BGMMgr.instance.changeBGM("flower");

    BGModel.instance.moveBG(10.0f, -1);
  }

  private float dt = 0;
  private int second = 0;

  void Update() {
    dt += Time.deltaTime;
    if (dt > 1) {
      second += 1;
      execSecond(second);
      dt = 0.0f;
    }
  }

  // x秒ごとの処理
  public void execSecond(int time){
    switch(time){
//      case 1:
//        KappaController.instance.view.runAnimation();
//        break;
//      case 7:
//        KappaController.instance.view.run32Animation();
//        break;
      case 16:
        bg.sprite = Resources.Load<Sprite>("Textures/event/sunset_with_usagi");
 //       CommonUtil.changeImage("bg", "event/owari");
        break;
      case 17:
        bg.sprite = Resources.Load<Sprite>("Textures/event/knight_with_usagi");
        break;
      case 18:
        bg.sprite = Resources.Load<Sprite>("Textures/event/knight_hitori");
        break;
      case 86:
        goBackTitle();
        break;
      default:
        break;
    }
  }

  private bool isGoBack = false;
  public void goBackTitle(){
    if(isGoBack) return;

    isGoBack = true;
    Debug.Log("goBackTitle");
    BGMMgr.instance.stopMusic();
    CommonUtil.changeScene("TitleScene");
  }
  */

}
