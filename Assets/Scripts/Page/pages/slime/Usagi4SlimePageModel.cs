using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usagi4SlimePageModel {

  public const string PAGE_KEY = "slime/usagi4";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "ウ、ウサギーー！";
    model.main_bg = "240_135/slime_encount";
    model.main_image = "chara/kappa_ase";
    model.speaker = "カッパ";

    KappaController.instance.hideKappa();

    model.next_page = Usagi5SlimePageModel.PAGE_KEY;
    return model;
  }
}
