using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLovePushSuccess2CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love_push_success2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "でもたまには羽目を外したい日もあります";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";

    model.next_page = AskPrivateLovePushSuccess3CastlePageModel.PAGE_KEY;
    return model;
  }
}
