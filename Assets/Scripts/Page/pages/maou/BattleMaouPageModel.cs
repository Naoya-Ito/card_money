using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMaouPageModel {
  public const string PAGE_KEY = "maou/battle";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_PAREIDOLIA;
    model.main_text = "戦闘開始。\n";
    model.main_bg = "other/cutin";
    model.speaker = "魔王";
    model.next_page = Line3MaouPageModel.PAGE_KEY;

    return model;
  }
}
