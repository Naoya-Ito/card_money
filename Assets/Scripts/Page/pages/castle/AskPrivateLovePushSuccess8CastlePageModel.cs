using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLovePushSuccess8CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love_push_success8";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "ヒメが仲間になった！";
    model.main_bg = "bg/castle_gray";
    model.speaker = "";

    DataMgr.SetBool("ally_hime_joined", true);
    if (GameSceneMgr.instance != null) {
      GameSceneMgr.instance.ShowAllyStatusPopup("ヒメが仲間になった！");
    }

    model.next_page = EndHimePageModel.PAGE_KEY;
    return model;
  }
}
