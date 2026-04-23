using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game5ShioriPageModel {

  public const string PAGE_KEY = "shiori/game5";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "ゲームでは一度も勝てなかったけれど、とても楽しかった。";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "";

    KappaController.instance.hideKappa();

    model.next_page = NextQuestionShioriPageModel.PAGE_KEY;
    return model;
  }
}
