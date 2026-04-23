using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLovePushSuccess6CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love_push_success6";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "私は・・\n私は一度でも良いので、大冒険がしたい";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";

    model.next_page = AskPrivateLovePushSuccess7CastlePageModel.PAGE_KEY;
    return model;
  }
}
