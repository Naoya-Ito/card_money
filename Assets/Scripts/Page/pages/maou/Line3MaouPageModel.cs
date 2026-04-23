using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line3MaouPageModel {
  public const string PAGE_KEY = "maou/line3";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "女子高生の姿のままでは勝てぬか";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "魔王";

    model.next_page = EndMaouPageModel.PAGE_KEY;
    return model;
  }
}
