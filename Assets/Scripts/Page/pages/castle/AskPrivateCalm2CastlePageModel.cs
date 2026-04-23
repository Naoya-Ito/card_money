using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateCalm2CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_calm2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "ムダ話をしてる暇はありません。\nさぁ、魔王を倒しに行くのです！";
    model.main_bg = "bg/castle_gray";
    model.next_page = EndHimePageModel.PAGE_KEY;

    return model;
  }
}
