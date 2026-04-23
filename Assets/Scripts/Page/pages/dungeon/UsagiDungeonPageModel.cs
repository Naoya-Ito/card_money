using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsagiDungeonPageModel {
  public const string PAGE_KEY = "dungeon/usagi";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DUNGEON;
    model.main_text = "ごくり。ここが魔王の住むダンジョン・・。\\n恐ろしい気配がするっす";
    model.main_bg = "240_135/bg_plain";
    model.speaker = "ウサギ";

    model.next_page = ChoiceDungeonPageModel.PAGE_KEY;
    return model;
  }
}
