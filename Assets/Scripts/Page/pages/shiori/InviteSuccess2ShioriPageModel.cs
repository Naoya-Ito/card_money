using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteSuccess2ShioriPageModel {

  public const string PAGE_KEY = "shiori/invite_success2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "「たった3分でも良い・・\n俺にチャンスをくれないか」";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";

    KappaController.instance.stopAnimation();
    KappaController.instance.changeImage("kappa_dot");
    KappaController.instance.showLeft();

    model.next_page = InviteSuccess3ShioriPageModel.PAGE_KEY;
    return model;
  }
}
