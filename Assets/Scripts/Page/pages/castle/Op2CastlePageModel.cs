using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Op2CastlePageModel {
  public const string PAGE_KEY = "castle/op2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "「な、なんだってー！」";
    model.main_bg = "240_135/nandate";
    model.speaker = "";
//    model.bgm = "game_op";

    model.next_page = Op3CastlePageModel.PAGE_KEY;
    return model;
  }
}
