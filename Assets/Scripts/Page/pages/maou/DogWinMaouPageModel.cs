using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogWinMaouPageModel {
  public const string PAGE_KEY = "maou/dog_win";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "負けたワン。\n許して欲しいワン";
    model.main_bg = "bg/bg_youhishi";
    model.speaker = "イヌの三連星　マッシュ";
    model.next_page = EndDungeonPageModel.PAGE_KEY;
    return model;
  }
}
