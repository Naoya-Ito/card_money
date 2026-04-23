using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuriASuccess3MaouPageModel {
  public const string PAGE_KEY = "maou/gokuri_a_success3";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "完全に不意をついた！\n魔王のすばやさ-10";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "";

    model.next_page = GokuriASuccess4MaouPageModel.PAGE_KEY;
    return model;
  }
}
