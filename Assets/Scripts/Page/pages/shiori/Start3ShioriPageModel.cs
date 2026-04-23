using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start3ShioriPageModel {

  public const string PAGE_KEY = "shiori/start3";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "「あ、カッパくん。\n 今から冒険に出るの？」";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "シオリーナ";
//    model.bgm = "game_op";

    model.next_page = ChoiceShioriPageModel.PAGE_KEY;
    return model;
  }

}
