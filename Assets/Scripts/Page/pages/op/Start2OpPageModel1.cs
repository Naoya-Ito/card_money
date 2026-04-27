using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start2OpPageModel {

  public const string PAGE_KEY = "op/start2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "「おーい」";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";
    model.next_page = EndOpPageModel.PAGE_KEY;

    return model;
  }
}
