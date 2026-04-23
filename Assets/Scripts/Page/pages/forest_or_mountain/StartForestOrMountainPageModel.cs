using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForestOrMountainPageModel {
  public const string PAGE_KEY = "forest_or_mountain/start";
  private const string CHOICE_MOUNTAIN = "forest_or_mountain/mountain_pending";
  private const string CHOICE_FOREST = "forest_or_mountain/forest_pending";
  private const string CHOICE_CASTLE = EndForestOrMountainPageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.setPageTypeChoice();
    model.main_bg = "240_135/map_240_135";

    ChoiceModel.instance.setTitle("セレステ山とメメント森が見える");
    ChoiceModel.instance.AddButton(CHOICE_MOUNTAIN, "山に登ろう");
    ChoiceModel.instance.AddButton(CHOICE_FOREST, "森が呼んでるぅ");
    ChoiceModel.instance.AddButton(CHOICE_CASTLE, "魔王城へ直行する");
    ChoiceModel.instance.SetButtonEnabled(1, false, "未実装");
    ChoiceModel.instance.SetButtonEnabled(2, false, "未実装");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (key == CHOICE_MOUNTAIN || key == CHOICE_FOREST) {
      return;
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
