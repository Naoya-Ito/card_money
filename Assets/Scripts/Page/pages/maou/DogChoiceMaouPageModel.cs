using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogChoiceMaouPageModel {
  public const string PAGE_KEY = "maou/dog_choice";
  private const string CHOICE_RUSH = DogGaiaMaouPageModel.PAGE_KEY;
  private const string CHOICE_TALK = DogTalk1MaouPageModel.PAGE_KEY;
  private const string CHOICE_SNEAK = SneakSuccess1MaouPageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.setPageTypeChoice();
    model.main_bg = "bg/bg_youhishi";

    ChoiceModel.instance.setTitle("門番がいるが、どうしよう");
    int atk = DataMgr.GetInt("atk");
    int nextAtk = Mathf.Max(1, atk + 1);
    ChoiceModel.instance.AddButton(CHOICE_RUSH, "突撃だぁ！", $"攻撃力：{atk}→{nextAtk}");
    ChoiceModel.instance.AddButton(CHOICE_TALK, "話しかける");
    ChoiceModel.instance.AddButton(CHOICE_SNEAK, "侵入する", "敏捷判定5");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (string.IsNullOrEmpty(key)) return;
    if (key == CHOICE_RUSH) {
      DataMgr.Increment("atk", 1);
    }
    if (key == CHOICE_SNEAK) {
      string infoText = "出目の合計＋すばやさが5以上で成功";
      GameSceneMgr.instance.StartStatDice(
        DataMgr.GetInt("agi"),
        5,
        SneakSuccess1MaouPageModel.PAGE_KEY,
        SneakFail1MaouPageModel.PAGE_KEY,
        infoText
      );
      return;
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
