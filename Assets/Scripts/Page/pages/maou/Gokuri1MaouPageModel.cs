using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gokuri1MaouPageModel {
  public const string PAGE_KEY = "maou/gokuri1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "ちょっ、カッパさん。\nなに心揺れてるんすか！";
    model.main_bg = "240_135/majisuka";
    model.speaker = "ウサギ";

    KappaController.instance.hideKappa();

    model.next_page = Gokuri2MaouPageModel.PAGE_KEY;
    return model;
  }
}
