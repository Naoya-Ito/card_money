using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie5ShioriPageModel {

  public const string PAGE_KEY = "shiori/movie5";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.main_text = "スキル『君の縄』を習得した！\n効果：通常攻撃時、攻撃力ではなく魅力の値を参照して敵を攻撃する。";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "";

    DataMgr.SetBool("skill_kimi_no_nawa", true);
    KappaController.instance.hideKappa();

    model.next_page = NextQuestionShioriPageModel.PAGE_KEY;
    return model;
  }
}
