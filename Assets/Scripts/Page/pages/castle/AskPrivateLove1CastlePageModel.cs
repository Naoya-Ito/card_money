using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLove1CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "付き合ってる人はいる？\nてかライソ交換しようよ〜。";
    model.main_bg = "other/cutin";
    model.next_page = AskPrivateLove2CastlePageModel.PAGE_KEY;

    KappaController.instance.stopAnimation();
    KappaController.instance.changeImage("kappa_joy");
    KappaController.instance.showCenter();

    return model;
  }
}
