using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteSuccess7ShioriPageModel {

  public const string PAGE_KEY = "shiori/invite_success7";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "シオリーナが仲間になった！";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "";

    DataMgr.SetBool("ally_shiori_joined", true);
    GameSceneMgr.instance.ShowAllyStatusPopup("シオリーナが仲間になった！", "chara/shiori_mini");

    KappaController.instance.hideKappa();

    model.next_page = NextChoiceShioriPageModel.PAGE_KEY;
    return model;
  }
}
