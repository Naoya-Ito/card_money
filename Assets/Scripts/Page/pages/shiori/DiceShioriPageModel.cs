using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceShioriPageModel {

  public const string PAGE_KEY = "shiori/dice";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "タップでダイスを止めてくれ。";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";

    KappaController.instance.hideKappa();

    model.next_page = "";
    return model;
  }
}
