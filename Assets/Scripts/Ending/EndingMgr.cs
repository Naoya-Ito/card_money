using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMgr : MonoBehaviour {
  private const string AUTO_SAVE_PAGE_KEY = "autosave_page";

  void Start() {
    if(BGMMgr.instance) {
      BGMMgr.instance.changeBGM("ending");
    }
    DataMgr.SetStr(AUTO_SAVE_PAGE_KEY, "");
    DataMgr.SetStr("page", "");
  }

  public void pushedGoTitleButton(){
    CommonUtil.changeScene("TitleScene");
  }
}
