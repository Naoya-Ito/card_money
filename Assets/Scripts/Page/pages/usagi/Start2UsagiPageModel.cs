using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start2UsagiPageModel {
  public const string PAGE_KEY = "usagi/start2";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "今なら出血大サービス。\n2ゴールドで仲間になるっすよ";
    model.main_bg = "240_135/majisuka";
    model.speaker = "ウサギ";

    KappaController.instance.hideKappa();

    model.next_page = ChoiceUsagiPageModel.PAGE_KEY;
    return model;
  }
}
