using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakSuccess2MaouPageModel {
  public const string PAGE_KEY = "maou/sneak_success2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "カッパは見事に門番の目を振り切った！";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "";

    DataMgr.Increment("agi", 1);
    DataMgr.Increment("wis", 1);

    model.next_page = EndDungeonPageModel.PAGE_KEY;
    return model;
  }
}
