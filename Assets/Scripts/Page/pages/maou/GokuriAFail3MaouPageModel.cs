using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuriAFail3MaouPageModel {
  public const string PAGE_KEY = "maou/gokuri_a_fail3";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "茶番は終わりだ。\nロシアの大地を貴様の血で赤く染めてやろう！";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "魔王";

    model.next_page = BattleMaouPageModel.PAGE_KEY;
    return model;
  }
}
