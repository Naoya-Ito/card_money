using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuriAFail2MaouPageModel {
  public const string PAGE_KEY = "maou/gokuri_a_fail2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "カッパはツッコミで5ダメージを受けた";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "";

    model.next_page = GokuriAFail3MaouPageModel.PAGE_KEY;
    return model;
  }
}
