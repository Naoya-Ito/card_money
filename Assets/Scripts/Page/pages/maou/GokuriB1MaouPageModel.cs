using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuriB1MaouPageModel {
  public const string PAGE_KEY = "maou/gokuri_b1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "これ以上は譲歩せぬ。\n地獄で私に詫び続けろ、カッパァーーー！";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "魔王";

    model.next_page = BattleMaouPageModel.PAGE_KEY;
    return model;
  }
}
