using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteSuccess6ShioriPageModel {

  public const string PAGE_KEY = "shiori/invite_success6";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "「やったー！」";
    model.main_bg = "other/cutin";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";

    KappaController.instance.stopAnimation();
    KappaController.instance.changeImage("kappa_joy");
    KappaController.instance.showCenter();

    model.next_page = InviteSuccess7ShioriPageModel.PAGE_KEY;
    return model;
  }
}
