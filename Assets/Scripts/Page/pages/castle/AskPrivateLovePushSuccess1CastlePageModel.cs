using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLovePushSuccess1CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love_push_success1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "私の恋人は・・この国です！";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";

    model.next_page = AskPrivateLovePushSuccess2CastlePageModel.PAGE_KEY;
    return model;
  }
}
