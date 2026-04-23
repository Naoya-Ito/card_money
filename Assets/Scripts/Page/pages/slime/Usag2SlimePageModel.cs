using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usag2SlimePageModel {

  public const string PAGE_KEY = "slime/usag2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "おおっと、油断したぁ！";
    model.main_bg = "240_135/slime_encount";
    model.main_image = "chara/usagi";
    model.speaker = "ウサギ";

    KappaController.instance.hideKappa();

    model.next_page = Usagi3SlimePageModel.PAGE_KEY;
    return model;
  }
}
