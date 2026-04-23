using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpCastlePageModel {

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "勇者カッパよ。\n魔王が滅びの呪文オワタを唱えました。\n3分後に世界は破滅します。";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";
//    model.bgm = "game_op";

    model.next_page = Op2CastlePageModel.PAGE_KEY;

    return model;
  }
}
