using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTalkChoiceMaouPageModel {
  public const string PAGE_KEY = "maou/dog_talk_choice";
  private const string CHOICE_A = DogTalkA1MaouPageModel.PAGE_KEY;
  private const string CHOICE_B = DogTalkB1MaouPageModel.PAGE_KEY;
  private const string CHOICE_C = DogTalkC1MaouPageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.setPageTypeChoice();
    model.main_bg = "bg/bg_youhishi";

    ChoiceModel.instance.setTitle("どんな話題を振る？");
    ChoiceModel.instance.AddButton(CHOICE_A, "怪しい人を見ました");
    ChoiceModel.instance.AddButton(CHOICE_B, "ワイロは欲しいかね？", "所持金-3");
    ChoiceModel.instance.AddButton(CHOICE_C, "私は魔王の息子だ", "魅力判定7");
    int gold = DataMgr.GetInt("gold");
    if (gold < 3) {
      ChoiceModel.instance.SetButtonEnabled(2, false, "条件: 所持金3以上");
    }

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (key == CHOICE_B) {
      int gold = DataMgr.GetInt("gold");
      DataMgr.SetInt("gold", Mathf.Max(0, gold - 3));
    }
    if (key == CHOICE_C) {
      int charm = DataMgr.GetInt("charm");
      string next = charm >= 7 ? DogTalkC1MaouPageModel.PAGE_KEY : DogTalkCfail1MaouPageModel.PAGE_KEY;
      DataMgr.SetStr("page", next);
      GameSceneMgr.instance.updateScene(next);
      return;
    }
    if (string.IsNullOrEmpty(key)) return;
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
