using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usagi3SlimePageModel {

  public const string PAGE_KEY = "slime/usagi3";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "うう、あっしはこれまでっす・・";
    model.main_bg = "240_135/slime_encount";
    model.main_image = "chara/usagi_cry";
    model.speaker = "ウサギ";

    KappaController.instance.hideKappa();

    model.next_page = Usagi4SlimePageModel.PAGE_KEY;
    return model;
  }
}
