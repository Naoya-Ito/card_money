using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTaxiUsagiPageModel {
  public const string PAGE_KEY = "usagi/end_taxi";
  private const string CHOICE_A = End2UsagiPageModel.PAGE_KEY;
  private const string CHOICE_B = StartDungeonPageModel.PAGE_KEY;
  private const int TAXI_GOLD_COST = 5;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.setPageTypeChoice();
    model.main_bg = "240_135/bg_plain";
    model.speaker = "カッパ";

    ChoiceModel.instance.setTitle("どうやって魔王城に向かう？");
    ChoiceModel.instance.AddButton(CHOICE_A, "歩いて行く");
    ChoiceModel.instance.AddButton(CHOICE_B, "タクシーを使う(5ゴールド)");

    int gold = DataMgr.GetInt("gold");
    if (gold < TAXI_GOLD_COST) {
      ChoiceModel.instance.SetButtonEnabled(2, false, "所持金5ゴールド以上");
    }

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (string.IsNullOrEmpty(key)) return;

    if (key == CHOICE_B) {
      int gold = DataMgr.GetInt("gold");
      if (gold < TAXI_GOLD_COST) return;
      DataMgr.SetInt("gold", Mathf.Max(0, gold - TAXI_GOLD_COST));
    }

    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
