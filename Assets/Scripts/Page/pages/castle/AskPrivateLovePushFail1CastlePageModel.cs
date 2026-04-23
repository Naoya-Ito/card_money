using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLovePushFail1CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love_push_fail1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "つきあってくれーーー！";
    model.main_bg = "240_135/yoro";
    model.speaker = "カッパ";

    model.next_page = AskPrivateLovePushFail2CastlePageModel.PAGE_KEY;
    return model;
  }
}
