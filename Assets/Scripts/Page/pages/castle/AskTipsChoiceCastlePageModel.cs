using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskTipsChoiceCastlePageModel {
  public const string PAGE_KEY = "castle/ask_tips_choice";
  private const string CHOICE_STAT = AskTipsStat1CastlePageModel.PAGE_KEY;
  private const string CHOICE_DICE = AskTipsDice1CastlePageModel.PAGE_KEY;
  private const string CHOICE_HANDSOME = AskTipsHandsomeCastlePageModel.PAGE_KEY;

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.setPageTypeChoice();
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";

    ChoiceModel.instance.setTitle("何を聞こう？");
    ChoiceModel.instance.AddButton(CHOICE_STAT, "能力値はどうやってあげるの？");
    ChoiceModel.instance.AddButton(CHOICE_DICE, "ダイス判定とは？");
    ChoiceModel.instance.AddButton(CHOICE_HANDSOME, "僕ってイケメンですか？");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
