using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuriASuccess4MaouPageModel {
  public const string PAGE_KEY = "maou/gokuri_a_success4";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "さすがはカッパ。\nもう二度と慢心はしない。\n全力でお相手しよう！";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "魔王";

    model.next_page = BattleMaouPageModel.PAGE_KEY;
    return model;
  }
}
