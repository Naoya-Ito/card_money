using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskTipsDice1CastlePageModel {
  public const string PAGE_KEY = "castle/ask_tips_dice1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "冒険中、さまざまな能力判定を求められます。\nダイスの目で成功か失敗か決まるんです";
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";
    model.next_page = AskTipsDice2CastlePageModel.PAGE_KEY;

    return model;
  }
}
