using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskCastlePageModel {
  public const string PAGE_KEY = "castle/ask";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "ヒメ「なんでも聞いてください！\n時間がないので即答しますよ！」";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.next_page = AskChoiceCastlePageModel.PAGE_KEY;

    return model;
  }
}
