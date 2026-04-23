using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextChoiceShioriPageModel {

  public const string PAGE_KEY = "shiori/next_choice";
  private const string CHOICE_MOVIE = "shiori/movie1";
  private const string CHOICE_GAME = "shiori/game1";
  private const string CHOICE_CASTLE = EndShioriPageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_GREAT_SUGAR_KO;
    model.setPageTypeChoice();
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "シオリーナ";

    KappaController.instance.hideKappa();

    ChoiceModel.instance.setTitle("シオリーナ「どこに行こっか？」");
    ChoiceModel.instance.AddButton(CHOICE_MOVIE, "映画館に行こうか", "所持金-5\n攻撃力-2\nすばやさ+1\n魅力+1\nスキル習得");
    int gold = DataMgr.GetInt("gold");
    int atk = DataMgr.GetInt("atk");
    int nextGold = Mathf.Max(0, gold - 1);
    int nextAtk = Mathf.Max(1, atk + 1);
    string gameExplain = $"所持金：{gold}→{nextGold}\n攻撃力：{atk}→{nextAtk}";
    ChoiceModel.instance.AddButton(CHOICE_GAME, "ゲーセンに行こう！", gameExplain);
    ChoiceModel.instance.AddButton(CHOICE_CASTLE, "魔王城に行くぞ！", "すばやさ+1");

    if (gold < 5) {
      ChoiceModel.instance.SetButtonEnabled(1, false, "条件:所持金5以上");
    }
    if (gold < 1) {
      ChoiceModel.instance.SetButtonEnabled(2, false, "条件:所持金1以上");
    }

    return model;
  }

  static public void pushedChoiceButton(string key) {
    int gold = DataMgr.GetInt("gold");
    if (key == CHOICE_MOVIE) {
      if (gold < 5) return;
      ApplyStats(-5, -2, 1, 1);
    } else if (key == CHOICE_GAME) {
      if (gold < 1) return;
      ApplyStats(-1, 1, 0, 0);
    } else if (key == CHOICE_CASTLE) {
      DataMgr.Increment("agi", 1);
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }

  private static void ApplyStats(int goldDelta, int atkDelta, int agiDelta, int charmDelta) {
    int gold = Mathf.Max(0, DataMgr.GetInt("gold") + goldDelta);
    int atk = Mathf.Max(1, DataMgr.GetInt("atk") + atkDelta);
    int agi = Mathf.Max(1, DataMgr.GetInt("agi") + agiDelta);
    int charm = Mathf.Max(1, DataMgr.GetInt("charm") + charmDelta);
    DataMgr.SetInt("gold", gold);
    DataMgr.SetInt("atk", atk);
    DataMgr.SetInt("agi", agi);
    DataMgr.SetInt("charm", charm);
  }
}
