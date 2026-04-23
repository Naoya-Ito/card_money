using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack0SlimePageModel {

  public const string PAGE_KEY = "slime/attack0";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "スライム「ぷるぷる。スライムの意地を見せてやるーっ！」";
    model.main_bg = "240_135/slime_encount";
    model.speaker = "スライム";

    model.next_page = BattleSlimePageModel.PAGE_KEY;
    return model;
  }
}
