using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTalkA2MaouPageModel {
  public const string PAGE_KEY = "maou/dog_talk_a2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "イヌの三連星がおそいかかってきた！";
    model.main_bg = "bg/bg_youhishi";
    model.next_page = DogBattleMaouPageModel.PAGE_KEY;
    return model;
  }
}
