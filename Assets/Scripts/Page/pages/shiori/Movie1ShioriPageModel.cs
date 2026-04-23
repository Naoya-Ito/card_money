using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie1ShioriPageModel {

  public const string PAGE_KEY = "shiori/movie1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "いま上映してるのは『君の縄』か。\n名作の予感しかしないね";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";

    KappaController.instance.hideKappa();

    model.next_page = Movie2ShioriPageModel.PAGE_KEY;
    return model;
  }
}
