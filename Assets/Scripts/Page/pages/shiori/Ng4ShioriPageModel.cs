using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ng4ShioriPageModel {

  public const string PAGE_KEY = "shiori/ng4";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "「じゃあねカッパくん。\nほどほどに現実を見てね」";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "シオリーナ";

    model.next_page = Ng5ShioriPageModel.PAGE_KEY;
    return model;
  }
}
