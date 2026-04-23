using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLovePushSuccess3CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love_push_success3";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "ふっ。\nそれでは今から3分間だけ、あなたはヒメではなくただの女子高生だ";
    model.main_bg = "bg/castle_gray";
    model.speaker = "カッパ";

    model.next_page = AskPrivateLovePushSuccess4CastlePageModel.PAGE_KEY;
    return model;
  }
}
