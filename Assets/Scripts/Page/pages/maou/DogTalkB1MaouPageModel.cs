using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTalkB1MaouPageModel {
  public const string PAGE_KEY = "maou/dog_talk_b1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "心の友よ。\\n通るが良いワン";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "オルテガ";
    model.next_page = EndDungeonPageModel.PAGE_KEY;
    return model;
  }
}
