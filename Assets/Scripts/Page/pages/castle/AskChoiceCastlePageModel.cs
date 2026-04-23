using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskChoiceCastlePageModel {
  public const string PAGE_KEY = "castle/ask_choice";
  private const string CHOICE_TIPS = AskTips1CastlePageModel.PAGE_KEY;
  private const string CHOICE_SUPPORT = "castle/ask_politics1";
  private const string CHOICE_PRIVATE = AskPrivateChoiceCastlePageModel.PAGE_KEY;

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.setPageTypeChoice();
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";
    model.speaker = "ヒメ";

    ChoiceModel.instance.setTitle("何を聞こう？");
    ChoiceModel.instance.AddButton(CHOICE_TIPS, "冒険のコツを聞く");
    int wis = DataMgr.GetInt("wis");
    int nextWis = Mathf.Max(1, wis + 1);
    string supportExplain = $"かしこさ {wis}→{nextWis}";
    ChoiceModel.instance.AddButton(CHOICE_SUPPORT, "政治について聞く", supportExplain);
    ChoiceModel.instance.AddButton(CHOICE_PRIVATE, "プライベートな事を聞く");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    switch (key) {
      case CHOICE_SUPPORT:
        DataMgr.Increment("wis", 1);
        DataMgr.SetStr("page", CHOICE_SUPPORT);
        GameSceneMgr.instance.updateScene(CHOICE_SUPPORT);
        return;
      case CHOICE_PRIVATE:
        DataMgr.SetStr("page", AskPrivateChoiceCastlePageModel.PAGE_KEY);
        GameSceneMgr.instance.updateScene(AskPrivateChoiceCastlePageModel.PAGE_KEY);
        return;
      default:
        break;
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
