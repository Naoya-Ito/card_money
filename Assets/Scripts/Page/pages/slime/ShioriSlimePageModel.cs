using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShioriSlimePageModel {

  public const string PAGE_KEY = "slime/shiori";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "10年早いんだよ！！！";
    model.main_bg = "240_135/slime_encount";
    model.main_image = "128_128/shiori_cool";
    model.speaker = "シオリーナ";

    KappaController.instance.hideKappa();

    model.next_page = Shiori2SlimePageModel.PAGE_KEY;
    return model;
  }
}
