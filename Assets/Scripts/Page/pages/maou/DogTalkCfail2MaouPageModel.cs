using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTalkCfail2MaouPageModel {
  public const string PAGE_KEY = "maou/dog_talk_cfail2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "覚悟してもらうワン。";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "オルテガ";
    model.next_page = DogBattleMaouPageModel.PAGE_KEY;
    return model;
  }
}
