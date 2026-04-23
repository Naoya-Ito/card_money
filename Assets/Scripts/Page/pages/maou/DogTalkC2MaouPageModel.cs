using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTalkC2MaouPageModel {
  public const string PAGE_KEY = "maou/dog_talk_c2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "どうぞお通りくださいだワン";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "オルテガ";
    model.next_page = EndDungeonPageModel.PAGE_KEY;
    return model;
  }
}
