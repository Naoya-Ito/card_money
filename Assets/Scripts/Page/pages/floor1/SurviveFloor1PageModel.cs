using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveFloor1PageModel {
  public const string PAGE_KEY = "floor1/survive";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    model.main_text = "なんとか耐えきった。\nカッパが長男じゃなかったら耐えきれなかっただろう";
    model.main_bg = "240_135/dungeon_up";
    model.main_image = "chara/kappa_cry";
    model.speaker = "カッパ";
    model.next_page = EndFloor1PageModel.PAGE_KEY;
    return model;
  }
}
