using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start2SlimePageModel {

  public const string PAGE_KEY = "slime/start2";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_OP;
    model.main_text = "スライムだ！\n見るからにザコそうな、モンスターだ。";
    model.main_bg = "240_135/slime_encount";
//    model.bgm = "game_op";

    KappaController.instance.hideKappa();

    model.next_page = ChoiceSlimePageModel.PAGE_KEY;
    return model;
  }
}
