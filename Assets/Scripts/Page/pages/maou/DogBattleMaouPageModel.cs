using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBattleMaouPageModel {
  public const string PAGE_KEY = "maou/dog_battle";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "戦闘開始。\n";
    model.main_bg = "other/cutin";
    model.speaker = "イヌの三連星";
    model.next_page = DogWinMaouPageModel.PAGE_KEY;
    return model;
  }
}
