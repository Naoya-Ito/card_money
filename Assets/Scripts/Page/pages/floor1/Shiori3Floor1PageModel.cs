using UnityEngine;

public class Shiori3Floor1PageModel {
  public const string PAGE_KEY = "floor1/shiori3";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    model.main_text = "女子高生がギャルピースをするのは\n二指真空把を極めるためだという事は\nあまりにも有名な話である。";
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "";
    model.next_page = EndFloor1PageModel.PAGE_KEY;
    return model;
  }
}
