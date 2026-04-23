using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPolitics1CastlePageModel {
  public const string PAGE_KEY = "castle/ask_politics1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "昨今の外交問題についてどう思いますか？";
    model.main_bg = "bg/castle_gray";
    model.speaker = "カッパ";

    KappaController.instance.stopAnimation();
    KappaController.instance.changeImage("kappa_dot");
    KappaController.instance.showCenter();

    model.next_page = AskPolitics2CastlePageModel.PAGE_KEY;
    return model;
  }
}
