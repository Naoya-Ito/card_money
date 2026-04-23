using UnityEngine;

public class UsagiFloor1PageModel {
  public const string PAGE_KEY = "floor1/usagi";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    model.main_text = "防御魔法を唱えるっす！\n\n万物の根源たるマナよ、強固なる盾となる我々の安全を・・";
    model.main_bg = "240_135/dungeon_up";
    model.main_image = "chara/usagi_attack";
    model.speaker = "ウサギ";
    model.next_page = Usagi2Floor1PageModel.PAGE_KEY;
    return model;
  }
}
