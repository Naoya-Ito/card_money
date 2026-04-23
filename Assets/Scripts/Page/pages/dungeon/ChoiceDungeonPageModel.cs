using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDungeonPageModel {
  public const string PAGE_KEY = "dungeon/choice";
  private const string CHOICE_ASK = "maou/start";
  private const string CHOICE_RUSH = DogIntroMaouPageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DUNGEON;
    model.setPageTypeChoice();
    model.main_bg = "240_135/bg_plain";
    model.speaker = "カッパ";

    ChoiceModel.instance.setTitle("ひよってるやついる？");
    ChoiceModel.instance.AddButton(CHOICE_ASK, "ひよってるやついる？");
    ChoiceModel.instance.AddButton(CHOICE_RUSH, "無言で突撃する");
    ChoiceModel.instance.SetButtonEnabled(1, false, "未実装");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
