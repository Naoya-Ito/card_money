using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDungeonCrossPageModel {
  public const string PAGE_KEY = "dungeon_cross/start";
  private const string CHOICE_STRAIGHT = StartFloor1PageModel.PAGE_KEY;
  private const string CHOICE_RIGHT = "dungeon_cross/right_pending";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.setPageTypeChoice();
    model.main_bg = "240_135/dungeon_cross";

    ChoiceModel.instance.setTitle("分かれ道だ・・");
    ChoiceModel.instance.AddButton(CHOICE_STRAIGHT, "まっすぐ進もう");
    ChoiceModel.instance.AddButton(CHOICE_RIGHT, "右が良い気がする");
    ChoiceModel.instance.SetButtonEnabled(2, false, "未実装");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (key == CHOICE_RIGHT) {
      return;
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
