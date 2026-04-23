using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndForestOrMountainPageModel {
  public const string PAGE_KEY = "forest_or_mountain/end";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.setPageTypeMapMove();
    model.main_text = "ついに魔王城に到着！";
    model.main_bg = "240_135/map_240_135";
    model.speaker = "";
    model.next_page = StartDungeonPageModel.PAGE_KEY;
    return model;
  }
}
