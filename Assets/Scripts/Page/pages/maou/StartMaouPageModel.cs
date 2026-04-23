using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMaouPageModel {
  public const string PAGE_KEY = "maou/start";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "「よくぞきたな」";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "魔王";

    model.next_page = ChoiceMaouPageModel.PAGE_KEY;
    return model;
  }
}
