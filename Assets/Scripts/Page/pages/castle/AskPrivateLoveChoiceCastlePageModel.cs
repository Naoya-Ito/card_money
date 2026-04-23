using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateLoveChoiceCastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_love_choice";
  private const string CHOICE_CALM = AskPrivateCalm1CastlePageModel.PAGE_KEY;
  private const string CHOICE_PUSH = "castle/ask_private_love_push";

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.setPageTypeChoice();
    model.main_bg = "240_135/majisuka";

    ChoiceModel.instance.setTitle("");
    ChoiceModel.instance.AddButton(CHOICE_CALM, "そうだな。一旦落ち着こう");
    ChoiceModel.instance.AddButton(CHOICE_PUSH, "カッパには引けない時がある", "魅力判定10");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    switch (key) {
      case CHOICE_CALM:
        GameSceneMgr.instance.slideOutAndUpdate(CHOICE_CALM);
        return;
      case CHOICE_PUSH:
        GameSceneMgr.instance.StartStatDice(
          DataMgr.GetInt("charm"),
          10,
          AskPrivateLovePushSuccess1CastlePageModel.PAGE_KEY,
          AskPrivateLovePushFail1CastlePageModel.PAGE_KEY,
          "出目の合計＋魅力が10以上で成功"
        );
        return;
      default:
        break;
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
