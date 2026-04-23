using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWarn1ShioriPageModel {

  public const string PAGE_KEY = "shiori/next_warn1";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "カッパさん。\n任務を忘れすぎっす！";
    model.main_bg = "240_135/majisuka";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "ウサギ";

    KappaController.instance.hideKappa();

    model.next_page = NextQuestionShioriPageModel.PAGE_KEY;
    return model;
  }
}
