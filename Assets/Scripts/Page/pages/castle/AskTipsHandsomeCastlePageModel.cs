using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskTipsHandsomeCastlePageModel {
  public const string PAGE_KEY = "castle/ask_tips_handsome";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "黙秘権を行使しますね";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_shock";
    model.speaker = "ヒメ";
    model.next_page = AskEndCastlePageModel.PAGE_KEY;

    return model;
  }
}
