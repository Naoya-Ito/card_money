using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLovePushFail3CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love_push_fail3";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "カッパは反省文を書いた。\nとほほ。";
    model.main_bg = "240_135/yoro";
    model.speaker = "";

    model.next_page = EndHimePageModel.PAGE_KEY;
    return model;
  }
}
