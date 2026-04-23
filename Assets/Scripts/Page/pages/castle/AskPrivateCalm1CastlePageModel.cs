using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateCalm1CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_calm1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "正気に戻りましたか？";
    model.main_bg = "bg/castle_gray";
    model.next_page = AskPrivateCalm2CastlePageModel.PAGE_KEY;

    KappaController.instance.hideKappa();

    return model;
  }
}
