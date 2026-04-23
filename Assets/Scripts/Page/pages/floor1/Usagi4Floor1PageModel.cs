using UnityEngine;

public class Usagi4Floor1PageModel {
  public const string PAGE_KEY = "floor1/usagi4";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    model.main_text = "ウサギの勇姿は決して、忘れはしない。\n先を急ごう。";
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "カッパ";
    model.next_page = EndFloor1PageModel.PAGE_KEY;
    return model;
  }
}
