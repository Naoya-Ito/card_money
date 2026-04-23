using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPolitics2CastlePageModel {
  public const string PAGE_KEY = "castle/ask_politics2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "ノーコメントで";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_shock";
    model.speaker = "ヒメ";

    KappaController.instance.hideKappa();

    model.next_page = AskPrivateCalm2CastlePageModel.PAGE_KEY;
    return model;
  }
}
