using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTownPageModel {
  public const string PAGE_KEY = "town/end";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.setPageTypeMapMove();
    model.main_text = "魔王城まであと少しだ・・";
    model.main_bg = "240_135/map_240_135";
    model.speaker = "";
    model.next_page = StartForestOrMountainPageModel.PAGE_KEY;
    return model;
  }
}
