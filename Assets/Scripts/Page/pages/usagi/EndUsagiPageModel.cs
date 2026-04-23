using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUsagiPageModel {
  public const string PAGE_KEY = "usagi/end";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "さて、気を取り直して魔王城へ向かうか！";
    model.main_bg = "240_135/bg_plain";
    model.speaker = "カッパ";

    bool usagiJoined = DataMgr.GetBool("ally_usagi_joined");
    DataMgr.SetBool("ally_usagi_run", usagiJoined);
    DataMgr.SetBool("ally_tanuki_run", false);
    if (usagiJoined && DataMgr.GetBool("ally_usagi_popup_pending")) {
      DataMgr.SetBool("ally_usagi_popup_pending", false);
      if (GameSceneMgr.instance != null) {
        GameSceneMgr.instance.ShowAllyStatusPopup("ウサギが仲間になった！");
      }
    }

    KappaController.instance.animateRun();
    KappaController.instance.showCenter();

    model.next_page = EndTaxiUsagiPageModel.PAGE_KEY;
    return model;
  }
}
