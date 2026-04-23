using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTalkCfail1MaouPageModel {
  public const string PAGE_KEY = "maou/dog_talk_cfail1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "嘘だと分かったワン。\n通すわけにはいかないワン。";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "オルテガ";
    model.next_page = DogTalkCfail2MaouPageModel.PAGE_KEY;
    return model;
  }
}
