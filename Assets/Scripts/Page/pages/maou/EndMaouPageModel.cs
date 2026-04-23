using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMaouPageModel {
  public const string PAGE_KEY = "maou/end";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHUMATSU_NO_SUE;
    model.main_text = "では見せてやろう。\\n真の姿を！！";
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "魔王";

    model.next_page = PAGE_KEY;
    return model;
  }
}
