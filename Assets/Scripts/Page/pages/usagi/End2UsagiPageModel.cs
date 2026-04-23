using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End2UsagiPageModel {
  public const string PAGE_KEY = "usagi/end2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.setPageTypeMapMove();
    model.main_text = "移動中・・";
    model.main_bg = "240_135/map_240_135";
    model.speaker = "";
    model.next_page = StartShioriPageModel.PAGE_KEY;
    return model;
  }
}
