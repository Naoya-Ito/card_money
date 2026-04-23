using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogGaiaMaouPageModel {
  public const string PAGE_KEY = "maou/dog_gaia";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "ここは通さぬワン！";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "イヌの三連星　ガイア";
    model.next_page = DogBattleMaouPageModel.PAGE_KEY;
    return model;
  }
}
