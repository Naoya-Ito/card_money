using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogIntroMaouPageModel {
  public const string PAGE_KEY = "maou/dog_intro";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "やや、あれは地獄の門番、犬の三連星だぁ！";
    model.main_bg = "bg/bg_youhishi";
    model.next_page = DogChoiceMaouPageModel.PAGE_KEY;
    return model;
  }
}
