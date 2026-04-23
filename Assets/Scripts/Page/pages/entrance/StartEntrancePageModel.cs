using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEntrancePageModel {
  public const string PAGE_KEY = "entrance/start";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DUNGEON;
    model.main_text = "ここが魔王城か。\nすごく、ウィザー・ドリィ的な雰囲気だ";
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "カッパ";
    model.next_page = EndEntrancePageModel.PAGE_KEY;
    return model;
  }
}
