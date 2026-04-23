using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakFail1MaouPageModel {
  public const string PAGE_KEY = "maou/sneak_fail1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "おい、そこで何をしているワン！";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "オルテガ";
    model.next_page = SneakFail2MaouPageModel.PAGE_KEY;
    return model;
  }
}
