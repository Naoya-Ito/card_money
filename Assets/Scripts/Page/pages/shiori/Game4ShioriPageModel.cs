using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game4ShioriPageModel {

  public const string PAGE_KEY = "shiori/game4";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "くにへかえるんだな。\nおまえにもかぞくがいるだろう。";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_cool";
    model.speaker = "シオリーナ";

    KappaController.instance.hideKappa();

    model.next_page = Game5ShioriPageModel.PAGE_KEY;
    return model;
  }
}
