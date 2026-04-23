using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line2MaouPageModel {
  public const string PAGE_KEY = "maou/line2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_PAREIDOLIA;
    model.main_text = "いいだろう。\n死ぬ前に魔王の力を思いしれ！";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "魔王";

    model.next_page = BattleMaouPageModel.PAGE_KEY;
    return model;
  }
}
