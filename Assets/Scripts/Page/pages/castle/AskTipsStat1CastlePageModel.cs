using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskTipsStat1CastlePageModel {
  public const string PAGE_KEY = "castle/ask_tips_stat1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "選択肢によってカッパさんの能力が変わります。\nご飯を食べればHPが上がったり、\n走ればすばやさが上がります";
    model.main_bg = "240_135/kappa_before_after";
    model.next_page = AskTipsStat2CastlePageModel.PAGE_KEY;

    return model;
  }
}
