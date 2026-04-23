using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1ShioriPageModel {

  public const string PAGE_KEY = "shiori/game1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "よし、今日はストレートファイターに決めた！";
    model.main_bg = "other/cutin";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";

    KappaController.instance.stopAnimation();
    KappaController.instance.changeImage("kappa_joy");
    KappaController.instance.showCenter();

    model.next_page = Game2ShioriPageModel.PAGE_KEY;
    return model;
  }
}
