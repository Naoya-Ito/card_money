using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTalkCfail3MaouPageModel {
  public const string PAGE_KEY = "maou/dog_talk_cfail3";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "仕方ないワン。\n力ずくで止めるワン。";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "オルテガ";
    model.next_page = DogBattleMaouPageModel.PAGE_KEY;
    return model;
  }
}
