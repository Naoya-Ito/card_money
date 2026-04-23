using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usagi5SlimePageModel {

  public const string PAGE_KEY = "slime/usagi5";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_MARUGOSHI;
    model.main_text = "カッパは深い悲しみと共に、\nスーパーカッパ人に目覚めた。";
    model.main_bg = "240_135/slime_encount";
    model.main_image = "";
    model.speaker = "";

    DataMgr.SetBool("ally_usagi_joined", false);

    KappaController.instance.hideKappa();

    model.next_page = Usagi6SlimePageModel.PAGE_KEY;
    return model;
  }
}
