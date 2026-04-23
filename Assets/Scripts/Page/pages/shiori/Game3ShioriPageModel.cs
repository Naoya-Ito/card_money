using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3ShioriPageModel {

  public const string PAGE_KEY = "shiori/game3";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "なにぃ、待ちガイールだとぉ！";
    model.main_bg = "other/cutin";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";

    KappaController.instance.stopAnimation();
    KappaController.instance.changeImage("kappa_sad");
    KappaController.instance.showCenter();

    model.next_page = Game4ShioriPageModel.PAGE_KEY;
    return model;
  }
}
