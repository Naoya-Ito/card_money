using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTownPageModel {
  public const string PAGE_KEY = "town/start";
  private const string CHOICE_VISIT = "town/visit_pending";
  private const string CHOICE_SKIP = EndTownPageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_EMOTIONAL;
    model.setPageTypeChoice();
    model.main_bg = "bg/bg_town";

    ChoiceModel.instance.setTitle("街だ！\n世界滅亡までまだ時間あるし、寄ってこうか？");
    ChoiceModel.instance.AddButton(CHOICE_VISIT, "寄ってくか");
    ChoiceModel.instance.AddButton(CHOICE_SKIP, "勇者は寄り道などしない！", "すばやさ+1");
    ChoiceModel.instance.SetButtonEnabled(1, false, "未実装");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (key == CHOICE_SKIP) {
      DataMgr.Increment("agi", 1);
    }
    if (key == CHOICE_VISIT) {
      return;
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
