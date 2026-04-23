using UnityEngine;

public class Usagi3Floor1PageModel {
  public const string PAGE_KEY = "floor1/usagi3";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    DataMgr.SetBool("ally_usagi_joined", false);
    DataMgr.SetBool("ally_usagi_popup_pending", false);
    DataMgr.SetBool("ally_usagi_run", false);
    DataMgr.SetBool("ally_tanuki_run", false);

    model.main_text = "ウサギは死んだ。\nもし彼が早口だったら\n防御魔法が間に合っていたのに・・";
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "";
    model.next_page = Usagi4Floor1PageModel.PAGE_KEY;
    return model;
  }
}
