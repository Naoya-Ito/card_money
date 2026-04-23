using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndShioriPageModel {

  public const string PAGE_KEY = "shiori/end";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "";
    model.main_bg = "";
    model.main_image = "";
    model.speaker = "";
    model.next_page = Start1SlimePageModel.PAGE_KEY;
    return model;
  }
}
