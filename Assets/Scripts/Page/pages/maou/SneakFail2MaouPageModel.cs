using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakFail2MaouPageModel {
  public const string PAGE_KEY = "maou/sneak_fail2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "ええい、こうなったら戦うしかない";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "カッパ";
    model.next_page = DogBattleMaouPageModel.PAGE_KEY;
    return model;
  }
}
