using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextQuestionShioriPageModel {

  public const string PAGE_KEY = "shiori/next_question";
  private const string CHOICE_SEA = "shiori/next_warn1";
  private const string CHOICE_CASTLE = EndShioriPageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.setPageTypeChoice();
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "シオリーナ";

    KappaController.instance.hideKappa();

    ChoiceModel.instance.setTitle("シオリーナ「次はどこへ行く？」");
    ChoiceModel.instance.AddButton(CHOICE_SEA, "海へ行こう！", "リア充");
    ChoiceModel.instance.AddButton(CHOICE_CASTLE, "魔王城へ行くぞ！");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
