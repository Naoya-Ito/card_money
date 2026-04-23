using UnityEngine;

public class Shiori2Floor1PageModel {
  public const string PAGE_KEY = "floor1/shiori2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    model.main_text = "シオリーナは矢を受け止めた。";
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "";
    model.next_page = Shiori3Floor1PageModel.PAGE_KEY;
    return model;
  }
}
