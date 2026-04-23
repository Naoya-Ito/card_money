using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line1MaouPageModel {
  public const string PAGE_KEY = "maou/line1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "どうしてもやるというのか。\nこれもカッパのさがか。";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "魔王";

    model.next_page = Line2MaouPageModel.PAGE_KEY;
    return model;
  }
}
