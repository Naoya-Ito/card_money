using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceCastlePageModel {
  public const string PAGE_KEY = "castle/choice";
  private const string CHOICE_ASK = AskCastlePageModel.PAGE_KEY;
  private const string CHOICE_GO = EndHimePageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.setPageTypeChoice();
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";

    ChoiceModel.instance.setTitle("3分以内に魔王を倒さねば");
    ChoiceModel.instance.AddButton(CHOICE_ASK, "ヒメに質問する");
    int agi = DataMgr.GetInt("agi");
    string agi_explain = $"すばやさ {agi}→{agi + 1}";
    ChoiceModel.instance.AddButton(CHOICE_GO, "急いで魔王城へ向かうぞ！", agi_explain);

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (key == CHOICE_ASK) {
      DataMgr.SetStr("page", key);
      GameSceneMgr.instance.updateScene(key);
      return;
    }
    if (key == CHOICE_GO) {
      DataMgr.Increment("agi", 1);
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
