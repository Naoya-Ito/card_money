using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usagi6SlimePageModel {

  public const string PAGE_KEY = "slime/usagi6";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "怒りで覚醒した！\n攻撃力+1\n防御力+1\nすばやさ+1";
    model.main_bg = "240_135/slime_encount";
    model.main_image = "chara/kappa_macho";
    model.speaker = "";

    DataMgr.Increment("atk", 1);
    DataMgr.Increment("def", 1);
    DataMgr.Increment("agi", 1);

    KappaController.instance.hideKappa();

    model.next_page = Usagi7SlimePageModel.PAGE_KEY;
    return model;
  }
}
