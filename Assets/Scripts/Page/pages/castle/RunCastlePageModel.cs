using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCastlePageModel {
  public const string PAGE_KEY = "castle/run";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "走れ、走れカッパ！";
    model.main_bg = "240_135/bg_plain";

    KappaController.instance.animateRun();
    KappaController.instance.showCenter();

    model.next_page = StartShioriPageModel.PAGE_KEY;
    return model;
  }
}
