using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTalkA1MaouPageModel {
  public const string PAGE_KEY = "maou/dog_talk_a1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "怪しいのは、お前だワン";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "オルテガ";
    model.next_page = DogTalkA2MaouPageModel.PAGE_KEY;
    return model;
  }
}
