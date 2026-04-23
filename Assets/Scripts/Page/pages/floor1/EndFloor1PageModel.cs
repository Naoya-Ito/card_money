using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFloor1PageModel {
  public const string PAGE_KEY = "floor1/end";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    Floor1TrapState.ClearArrowDamage();

    model.main_text = "カッパは矢の罠の通路を通り抜けた。\n卑劣な罠を用意する魔王め・・許さん！";
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "カッパ";
    model.next_page = StartMaouPageModel.PAGE_KEY;
    return model;
  }
}
