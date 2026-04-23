using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ng1ShioriPageModel {

  public const string PAGE_KEY = "shiori/ng1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "「一緒に帰って、\n 　友達に噂とかされると\n 　恥ずかしいし……」";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "シオリーナ";
//    model.bgm = "game_op";

    model.next_page = Ng2ShioriPageModel.PAGE_KEY;
    return model;
  }
}
