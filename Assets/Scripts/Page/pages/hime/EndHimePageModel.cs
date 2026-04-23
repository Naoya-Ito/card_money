using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHimePageModel {

  public const string PAGE_KEY = "hime/end";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "さて、冒険に出るか。\n魔王を倒してモテモテに俺はなる！！！";
    model.main_bg = "240_135/yaruzo";
    model.speaker = "";
    model.next_page = StartUsagiPageModel.PAGE_KEY;
    return model;
  }
}
