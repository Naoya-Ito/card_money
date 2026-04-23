using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLove2CastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "ちょっ。カッパさん。\n勇気と蛮勇を履き違えたら死にますよ！";
    model.main_bg = "240_135/majisuka";
    model.next_page = AskPrivateLoveChoiceCastlePageModel.PAGE_KEY;

    KappaController.instance.hideKappa();

    return model;
  }
}
