using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTalkC1MaouPageModel {
  public const string PAGE_KEY = "maou/dog_talk_c1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "す、すいませんでしたワーン";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "オルテガ";
    model.next_page = DogTalkC2MaouPageModel.PAGE_KEY;
    return model;
  }
}
