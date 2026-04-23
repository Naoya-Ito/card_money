using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start1SlimePageModel {

  public const string PAGE_KEY = "slime/start1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
//    model.bgm = BGMMgr.KEY_OP;
    KappaController.instance.animateRun();
    KappaController.instance.showCenter();

    model.main_text = "魔王城へ向けて走れ、走るんだカッパ！\n\nむむ、前方に何かが見える。";
    model.main_bg = "240_135/bg_plain";
//    model.bgm = "game_op";

    model.next_page = Start2SlimePageModel.PAGE_KEY;
    return model;
  }
}
