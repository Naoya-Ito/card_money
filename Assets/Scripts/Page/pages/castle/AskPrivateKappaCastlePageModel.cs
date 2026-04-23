using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateKappaCastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_kappa";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "はい。\nどこからどう見てもカッパです";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_shock";
    model.speaker = "ヒメ";
    model.next_page = AskEndCastlePageModel.PAGE_KEY;

    return model;
  }
}
