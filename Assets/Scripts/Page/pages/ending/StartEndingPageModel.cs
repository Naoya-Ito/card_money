using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndingPageModel {

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_WAKUWAKU;
    model.main_text = "残念。続きはまた今度。\n今はまだここまでしか開発できていないのである。　　完！";
    model.main_bg = "sunset_with_usagi";

    KappaController.instance.hideKappa();

    model.next_page = "ending/start";
    return model;
  }

}
