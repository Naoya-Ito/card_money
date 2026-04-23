using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardFloor1PageModel {
  public const string PAGE_KEY = "floor1/guard";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    model.main_text = "防御を固めるぜ！";
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "カッパ";
    model.next_page = PainFloor1PageModel.PAGE_KEY;
    return model;
  }
}
