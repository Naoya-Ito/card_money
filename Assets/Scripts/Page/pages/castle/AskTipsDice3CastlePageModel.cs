using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskTipsDice3CastlePageModel {
  public const string PAGE_KEY = "castle/ask_tips_dice3";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "ただし、ダイス目が6のゾロ目なら無条件成功。\nダイス目が1のゾロ目なら無条件失敗です";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";
    model.next_page = AskEndCastlePageModel.PAGE_KEY;

    return model;
  }
}
