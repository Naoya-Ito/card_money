using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Op4CastlePageModel {
  public const string PAGE_KEY = "castle/op4";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "そのためには毒殺や不意打ち、狙撃など方法は問いません。\nとにかく3分以内に結果を出すのです！";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_angry";
    model.speaker = "ヒメ";

    model.next_page = ChoiceCastlePageModel.PAGE_KEY;
    return model;
  }
}
