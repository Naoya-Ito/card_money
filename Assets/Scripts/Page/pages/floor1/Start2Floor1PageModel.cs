using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start2Floor1PageModel {
  public const string PAGE_KEY = "floor1/start2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.animateRun();
      KappaController.instance.showCenter();
    }
    model.main_text = "ダッシュで駆け抜けるぜぇ！";
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "カッパ";
    model.next_page = Start3Floor1PageModel.PAGE_KEY;
    return model;
  }
}
