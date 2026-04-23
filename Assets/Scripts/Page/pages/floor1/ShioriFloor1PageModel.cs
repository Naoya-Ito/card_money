using UnityEngine;

public class ShioriFloor1PageModel {
  public const string PAGE_KEY = "floor1/shiori";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    model.main_text = "二指真空把";
    model.main_bg = "240_135/dungeon_up";
    model.main_image = "128_128/shiori_cool";
    model.speaker = "シオリーナ";
    model.next_page = Shiori2Floor1PageModel.PAGE_KEY;
    return model;
  }
}
