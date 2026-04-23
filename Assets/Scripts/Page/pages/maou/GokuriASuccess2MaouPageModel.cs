using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuriASuccess2MaouPageModel {
  public const string PAGE_KEY = "maou/gokuri_a_success2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "なんて言うと思ったかパーンチ！";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "カッパ";

    model.next_page = GokuriASuccess3MaouPageModel.PAGE_KEY;
    return model;
  }
}
