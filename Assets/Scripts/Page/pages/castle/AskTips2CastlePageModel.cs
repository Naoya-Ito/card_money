using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskTips2CastlePageModel {
  public const string PAGE_KEY = "castle/ask_tips2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "なので急ぎつつも、寄り道して力をつけるのです！";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";
    model.next_page = AskTipsChoiceCastlePageModel.PAGE_KEY;

    return model;
  }
}
