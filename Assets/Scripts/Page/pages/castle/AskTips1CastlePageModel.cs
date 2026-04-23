using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskTips1CastlePageModel {
  public const string PAGE_KEY = "castle/ask_tips1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "今のまま魔王に挑んでも、\n恐らく瞬殺されます。\nだってカッパだもの";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";
    model.next_page = AskTips2CastlePageModel.PAGE_KEY;

    return model;
  }
}
