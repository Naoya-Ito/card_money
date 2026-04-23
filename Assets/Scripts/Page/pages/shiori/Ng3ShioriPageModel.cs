using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ng3ShioriPageModel {

  public const string PAGE_KEY = "shiori/ng3";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "「そ、そうだね。それじゃあお先に」";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";
//    model.bgm = "game_op";

    model.next_page = Ng4ShioriPageModel.PAGE_KEY;
    return model;
  }
}
