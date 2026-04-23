using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLovePushSuccess4CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love_push_success4";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "やるべき事ではなく、やりたい事をやれば良い";
    model.main_bg = "bg/castle_gray";
    model.speaker = "カッパ";

    model.next_page = AskPrivateLovePushSuccess5CastlePageModel.PAGE_KEY;
    return model;
  }
}
