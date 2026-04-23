using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ng5ShioriPageModel {

  public const string PAGE_KEY = "shiori/ng5";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "涙が流れないように、\n上を向いて歩こう・・。";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";

    model.next_page = EndShioriPageModel.PAGE_KEY;
    return model;
  }
}
