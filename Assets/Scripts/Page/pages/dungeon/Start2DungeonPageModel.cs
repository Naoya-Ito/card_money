using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start2DungeonPageModel {
  public const string PAGE_KEY = "dungeon/start2";
  private const string CHOICE_FRONT = UsagiDungeonPageModel.PAGE_KEY;
  private const string CHOICE_BACK = StartEntrancePageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DUNGEON;
    model.setPageTypeChoice();
    model.main_bg = "240_135/maou_castle";
    model.speaker = "カッパ";

    ChoiceModel.instance.setTitle("どうやって入る？");
    ChoiceModel.instance.AddButton(CHOICE_FRONT, "正面から近づく");
    ChoiceModel.instance.AddButton(CHOICE_BACK, "裏口から入ろう");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (string.IsNullOrEmpty(key)) return;
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
