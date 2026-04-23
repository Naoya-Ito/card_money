using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidFloor1PageModel {
  public const string PAGE_KEY = "floor1/avoid";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }
    model.main_text = "避けたぁ！";
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "カッパ";
    model.next_page = EndFloor1PageModel.PAGE_KEY;
    return model;
  }
}
