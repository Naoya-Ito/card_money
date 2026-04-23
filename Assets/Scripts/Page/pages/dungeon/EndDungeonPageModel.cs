using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDungeonPageModel {
  public const string PAGE_KEY = "dungeon/end";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "";
    model.main_bg = "240_135/bg_plain";
    model.speaker = "";
    model.next_page = StartEntrancePageModel.PAGE_KEY;
    return model;
  }
}
