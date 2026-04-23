using UnityEngine;

public class GearButton : MonoBehaviour{

  [System.NonSerialized] public bool isPressed = false;
  [System.NonSerialized] public static GearButton instance = null;

  private void Awake(){
    if(instance == null) {
      instance = this;
    } else {
      Destroy(this.gameObject);
    }
  }


  public void pressButton(){
    if(isPressed) return;

    isPressed = true;
    DataMgr.SetStr("settings_return_scene", CommonUtil.getCurrentSceneName());
    CommonUtil.changeScene("SettingScene");

//  ポーズする
//    Time.timeScale = 0;
  }

}
