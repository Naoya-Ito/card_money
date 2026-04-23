using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usagi7SlimePageModel {

  public const string PAGE_KEY = "slime/usagi7";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "てめぇはゆるさねー！";
    model.main_bg = "240_135/slime_encount";
    model.main_image = "chara/kappa_igi";
    model.speaker = "カッパ";

    KappaController.instance.hideKappa();

    model.next_page = BattleSlimePageModel.PAGE_KEY;
    return model;
  }
}
