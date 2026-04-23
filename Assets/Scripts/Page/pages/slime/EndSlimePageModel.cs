using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSlimePageModel {

  public const string PAGE_KEY = "slime/end";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.setPageTypeMapMove();
    model.main_text = "魔王城はまだまだ遠いな";
    model.main_bg = "240_135/map_240_135";
    model.speaker = "";
    model.next_page = StartTownPageModel.PAGE_KEY;
    return model;
  }
}
