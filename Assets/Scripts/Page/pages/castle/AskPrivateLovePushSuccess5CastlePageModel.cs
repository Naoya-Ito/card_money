using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLovePushSuccess5CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love_push_success5";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "おお・・\nカッパさんがイケメンに見える・・";
    model.main_bg = "bg/castle_gray";
    model.speaker = "ウサギ";

    model.next_page = AskPrivateLovePushSuccess6CastlePageModel.PAGE_KEY;
    return model;
  }
}
