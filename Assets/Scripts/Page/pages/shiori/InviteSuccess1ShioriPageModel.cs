using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteSuccess1ShioriPageModel {

  public const string PAGE_KEY = "shiori/invite_success1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "「うーん、どうしようかな」";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "シオリーナ";

    KappaController.instance.hideKappa();

    model.next_page = InviteSuccess2ShioriPageModel.PAGE_KEY;
    return model;
  }
}
