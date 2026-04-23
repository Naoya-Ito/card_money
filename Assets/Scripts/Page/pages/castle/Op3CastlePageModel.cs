using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Op3CastlePageModel {
  public const string PAGE_KEY = "castle/op3";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "世界を救う方法はただ一つ、\n3分以内に魔王を倒すのです！";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";

    model.next_page = Op4CastlePageModel.PAGE_KEY;
    return model;
  }
}
