using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLovePushFail2CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love_push_fail2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "城内で騒がしい奴め。\n貴様を不敬罪で逮捕する";
    model.main_bg = "240_135/taiho";
    model.speaker = "イヌ";

    model.next_page = AskPrivateLovePushFail3CastlePageModel.PAGE_KEY;
    return model;
  }
}
