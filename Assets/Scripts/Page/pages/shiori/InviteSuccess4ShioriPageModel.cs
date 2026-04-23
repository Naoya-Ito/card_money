using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteSuccess4ShioriPageModel {

  public const string PAGE_KEY = "shiori/invite_success4";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "「いいよ」";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_smile";
    model.speaker = "シオリーナ";

    KappaController.instance.hideKappa();

    model.next_page = InviteSuccess5ShioriPageModel.PAGE_KEY;
    return model;
  }
}
