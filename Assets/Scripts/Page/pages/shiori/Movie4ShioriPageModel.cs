using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie4ShioriPageModel {

  public const string PAGE_KEY = "shiori/movie4";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "俺も・・この感動は絶対に忘れないよ";
    model.main_bg = "other/cutin";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";

    KappaController.instance.stopAnimation();
    KappaController.instance.changeImage("kappa_joy");
    KappaController.instance.showCenter();

    model.next_page = Movie5ShioriPageModel.PAGE_KEY;
    return model;
  }
}
