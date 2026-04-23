using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2ShioriPageModel {

  public const string PAGE_KEY = "shiori/game2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "私もこのゲーム、得意なんだ";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "シオリーナ";

    KappaController.instance.hideKappa();

    model.next_page = Game3ShioriPageModel.PAGE_KEY;
    return model;
  }
}
