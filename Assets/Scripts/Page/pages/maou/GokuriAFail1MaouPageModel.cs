using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuriAFail1MaouPageModel {
  public const string PAGE_KEY = "maou/gokuri_a_fail1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "みえすいた嘘をつくな";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "魔王";

    model.next_page = GokuriAFail2MaouPageModel.PAGE_KEY;
    return model;
  }
}
