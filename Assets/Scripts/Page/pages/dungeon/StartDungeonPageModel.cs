using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDungeonPageModel {
  public const string PAGE_KEY = "dungeon/start";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DUNGEON;
    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }
    model.main_text = "ここが魔王の住むダンジョンか。\nウィザードリィな空気がすごくするぜ";
    model.main_bg = "240_135/maou_castle";
    model.speaker = "カッパ";

    model.next_page = Start2DungeonPageModel.PAGE_KEY;
    return model;
  }
}
