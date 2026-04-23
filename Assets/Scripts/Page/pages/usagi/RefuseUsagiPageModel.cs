using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuseUsagiPageModel {
  public const string PAGE_KEY = "usagi/refuse";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "しょんぼり・・";
    model.main_bg = "240_135/majisuka";
    model.speaker = "ウサギ";

    DataMgr.SetBool("ally_usagi_joined", false);
    DataMgr.SetBool("ally_usagi_popup_pending", false);
    DataMgr.SetBool("ally_usagi_run", false);
    DataMgr.SetBool("ally_tanuki_run", false);

    KappaController.instance.hideKappa();

    model.next_page = EndUsagiPageModel.PAGE_KEY;
    return model;
  }
}
