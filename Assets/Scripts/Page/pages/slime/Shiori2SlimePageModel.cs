using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiori2SlimePageModel {

  public const string PAGE_KEY = "slime/shiori2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "スライムは爆散消滅した。\nYOU  WIN!!";
    model.main_bg = "240_135/slime_encount";
    model.main_image = "";
    model.speaker = "";

    KappaController.instance.hideKappa();

    model.next_page = Attack2SlimePageModel.PAGE_KEY;
    return model;
  }
}
