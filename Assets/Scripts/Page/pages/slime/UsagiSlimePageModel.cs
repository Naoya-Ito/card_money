using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsagiSlimePageModel {

  public const string PAGE_KEY = "slime/usagi";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "ここは俺に任せるっす！\nスライムなんて楽勝っすよ！";
    model.main_bg = "240_135/slime_encount";
    model.main_image = "chara/usagi";
    model.speaker = "ウサギ";

    KappaController.instance.hideKappa();

    model.next_page = Usag2SlimePageModel.PAGE_KEY;
    return model;
  }
}
