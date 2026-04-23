using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuriASuccess1MaouPageModel {
  public const string PAGE_KEY = "maou/gokuri_a_success1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "取り分はフィフティー・フィフティーで行こう";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "カッパ";

    model.next_page = GokuriASuccess2MaouPageModel.PAGE_KEY;
    return model;
  }
}
