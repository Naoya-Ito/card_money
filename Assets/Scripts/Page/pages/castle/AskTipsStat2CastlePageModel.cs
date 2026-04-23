using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskTipsStat2CastlePageModel {
  public const string PAGE_KEY = "castle/ask_tips_stat2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "色々な選択肢を試す事で強くなれるはずです。\n多分、きっと恐らく";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";
    model.next_page = AskEndCastlePageModel.PAGE_KEY;

    return model;
  }
}
