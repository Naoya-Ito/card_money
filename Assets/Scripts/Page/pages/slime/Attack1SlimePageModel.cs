using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1SlimePageModel {

  public const string PAGE_KEY = "slime/attack1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "カッパの一撃をくらえーっ";
    model.main_bg = "240_135/slime_encount";
    model.speaker = "カッパ";

    model.next_page = BattleSlimePageModel.PAGE_KEY;
    return model;
  }
}
