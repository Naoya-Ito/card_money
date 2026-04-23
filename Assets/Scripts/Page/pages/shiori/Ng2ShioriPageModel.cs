using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ng2ShioriPageModel {

  public const string PAGE_KEY = "shiori/ng2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "な、なんだってぇー！";
    model.main_bg = "240_135/nandate";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";
//    model.bgm = "game_op";

    model.next_page = Ng3ShioriPageModel.PAGE_KEY;
    return model;
  }
}
