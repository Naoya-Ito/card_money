using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1bSlimePageModel {

  public const string PAGE_KEY = "slime/attack1b";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "ぎゃー、やーらーれーたー";
    model.main_bg = "240_135/slime_encount";
    model.speaker = "スライム";

    model.next_page = Attack2SlimePageModel.PAGE_KEY;
    return model;
  }
}
