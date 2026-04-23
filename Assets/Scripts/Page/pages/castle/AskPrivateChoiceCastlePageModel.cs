using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskPrivateChoiceCastlePageModel {
  public const string PAGE_KEY = "castle/ask_private_choice";
  private const string CHOICE_HOBBY = AskPrivateHobbyCastlePageModel.PAGE_KEY;
  private const string CHOICE_LOVE = AskPrivateLove1CastlePageModel.PAGE_KEY;
  private const string CHOICE_KAPPA = AskPrivateKappaCastlePageModel.PAGE_KEY;

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.setPageTypeChoice();
    model.main_bg = "bg/castle_gray";

    ChoiceModel.instance.setTitle("何を聞く？");
    ChoiceModel.instance.AddButton(CHOICE_HOBBY, "趣味はなんですか？");
    ChoiceModel.instance.AddButton(CHOICE_LOVE, "付き合ってる人はいる？");
    ChoiceModel.instance.AddButton(CHOICE_KAPPA, "僕ってカッパですか？");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
