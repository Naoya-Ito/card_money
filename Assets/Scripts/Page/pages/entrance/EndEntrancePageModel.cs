using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEntrancePageModel {
  public const string PAGE_KEY = "entrance/end";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "";
    model.main_bg = "";
    model.speaker = "";
    model.next_page = StartDungeonCrossPageModel.PAGE_KEY;
    return model;
  }
}
