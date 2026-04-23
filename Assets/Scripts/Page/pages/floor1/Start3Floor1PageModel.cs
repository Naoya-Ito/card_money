using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start3Floor1PageModel {
  public const string PAGE_KEY = "floor1/start3";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }
    model.main_text = "ガコン！";
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "";
    model.next_page = AarrowFloor1PageModel.PAGE_KEY;
    return model;
  }
}
