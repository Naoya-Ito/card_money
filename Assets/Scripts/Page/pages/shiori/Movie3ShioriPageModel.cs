using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie3ShioriPageModel {

  public const string PAGE_KEY = "shiori/movie3";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "すごく良かったね。\nラストの溶鉱炉にカッパが落ちていくシーンは\n涙なしでは見れなかったよ";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "シオリーナ";

    KappaController.instance.hideKappa();

    model.next_page = Movie4ShioriPageModel.PAGE_KEY;
    return model;
  }
}
