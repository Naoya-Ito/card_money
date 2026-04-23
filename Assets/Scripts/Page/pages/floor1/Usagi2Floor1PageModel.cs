using UnityEngine;

public class Usagi2Floor1PageModel {
  public const string PAGE_KEY = "floor1/usagi2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    model.main_text = "ぎゃー、呪文が長すぎたー！";
    model.main_bg = "240_135/dungeon_up";
    model.main_image = "chara/usagi_cry";
    model.speaker = "ウサギ";
    model.next_page = Usagi3Floor1PageModel.PAGE_KEY;
    return model;
  }
}
