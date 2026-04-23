using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFloor1PageModel {
  public const string PAGE_KEY = "floor1/start";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }
    model.main_text = "まっすぐな長い通路だ。\n周囲に敵の気配はない。";
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "カッパ";
    model.next_page = Start2Floor1PageModel.PAGE_KEY;
    return model;
  }
}
