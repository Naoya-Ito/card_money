using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSlimePageModel {
  public const string PAGE_KEY = "slime/battle";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "戦闘開始。\n";
    model.main_bg = "other/cutin";
    model.speaker = "スライム";
    model.next_page = Attack1bSlimePageModel.PAGE_KEY;
    return model;
  }
}
