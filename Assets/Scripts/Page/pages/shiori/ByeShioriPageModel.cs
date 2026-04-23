using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByeShioriPageModel {

  public const string PAGE_KEY = "shiori/bye";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "ふーん。じゃあね";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_cool";
    model.speaker = "シオリーナ";
//    model.bgm = "game_op";

    KappaController.instance.hideKappa();

    model.next_page = EndShioriPageModel.PAGE_KEY;
    return model;
  }
}
