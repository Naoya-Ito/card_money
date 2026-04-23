using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTalk1MaouPageModel {
  public const string PAGE_KEY = "maou/dog_talk1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "ハロー！\\nマイネーム・イズ・カッパー";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "カッパ";
    model.next_page = DogTalkChoiceMaouPageModel.PAGE_KEY;
    return model;
  }
}
