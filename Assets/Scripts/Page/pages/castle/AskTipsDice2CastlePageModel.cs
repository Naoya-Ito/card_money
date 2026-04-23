using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskTipsDice2CastlePageModel {
  public const string PAGE_KEY = "castle/ask_tips_dice2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "例えば攻撃力判定10ならば、\nカッパの攻撃力＋ダイス2個の出目合計が10以上ならば成功になります";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";
    model.next_page = AskTipsDice3CastlePageModel.PAGE_KEY;

    return model;
  }
}
