using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteSuccess5ShioriPageModel {

  public const string PAGE_KEY = "shiori/invite_success5";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "「たったの3分でも、\n忘れられない思い出になったら\nすてきだよね！」";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "シオリーナ";

    KappaController.instance.hideKappa();

    model.next_page = InviteSuccess6ShioriPageModel.PAGE_KEY;
    return model;
  }
}
