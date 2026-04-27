using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start1OpPageModel {

  public const string PAGE_KEY = "op/start1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "「あっ！　冒険者界隈のマドンナ、シオリーナがいるぞ！」";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";
    model.next_page = Start2OpPageModel.PAGE_KEY;

    return model;
  }
}
