using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gokuri2MaouPageModel {
  public const string PAGE_KEY = "maou/gokuri2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "あ、安心しろウサギ。\nこれは敵を油断させる作戦だ";
    model.main_bg = "other/cutin";
    model.speaker = "カッパ";

    KappaController.instance.hideKappa();

    model.next_page = GokuriChoiceMaouPageModel.PAGE_KEY;
    return model;
  }
}
