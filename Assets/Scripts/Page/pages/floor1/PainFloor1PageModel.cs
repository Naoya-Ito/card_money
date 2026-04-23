using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainFloor1PageModel {
  public const string PAGE_KEY = "floor1/pain";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    Floor1TrapState.ApplyArrowDamage();
    int damage = Floor1TrapState.GetArrowDamage();

    model.main_bg = "240_135/yoro";
    model.speaker = "カッパ";
    if (damage <= 0) {
      model.main_text = "あいったー！";
    } else {
      model.main_text = $"あいったー！\n{damage} のダメージ！";
    }

    model.next_page = Floor1TrapState.IsKappaDead()
      ? Floor1TrapState.ARROW_GAME_OVER_KEY
      : SurviveFloor1PageModel.PAGE_KEY;

    return model;
  }
}
