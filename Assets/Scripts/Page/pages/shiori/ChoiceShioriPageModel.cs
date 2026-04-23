using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceShioriPageModel {

  public const string PAGE_KEY = "shiori/choice";
  public const string CHOICE1_KEY = "shiori/kaero";
  public const string CHOICE2_KEY = "shiori/bye";
  private const int CHARM_CHECK_THRESHOLD = 7;
  private const int DICE_MIN = 1;
  private const int DICE_MAX = 6;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.setPageTypeChoice();
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";

    KappaController.instance.hideKappa();

    ChoiceModel.instance.setTitle("カッパ「ややっ。ヲタクに優しそうなギャルがいるぞ！」");
    int charm = DataMgr.GetInt("charm");
    int successRate = GetCharmSuccessRate(charm, CHARM_CHECK_THRESHOLD);
    string charmExplain = $"魅力判定5 (成功率 {successRate}%)";
    ChoiceModel.instance.AddButton(CHOICE1_KEY, "一緒に冒険しないか？", charmExplain);
    int agi = DataMgr.GetInt("agi");
    string agi_explain = $"すばやさ {agi}→{agi + 1}";
    ChoiceModel.instance.AddButton(CHOICE2_KEY, "そうだよ。じゃあお先", agi_explain);

    return model;
  }

  static public void pushedChoiceButton(string key) {
    string next_key = key;
    switch (key) {
      case CHOICE1_KEY:
        // 魅力によって行き先を分岐
        GameSceneMgr.instance.StartShioriInviteDice();
        return;
      case CHOICE2_KEY:
        DataMgr.Increment("agi", 1);
        break;
      default:
        break;
    }
    DataMgr.SetStr("page", next_key);
    GameSceneMgr.instance.updateScene(next_key);
  }

  private static int GetCharmSuccessRate(int charm, int threshold) {
    int success = 0;
    int total = 0;
    for (int i = DICE_MIN; i <= DICE_MAX; i++) {
      for (int j = DICE_MIN; j <= DICE_MAX; j++) {
        total++;
        if (IsAutoFail(i, j)) {
          continue;
        }
        if (IsAutoSuccess(i, j)) {
          success++;
          continue;
        }
        if (i + j + charm >= threshold) {
          success++;
        }
      }
    }
    float rate = (float)success / total * 100f;
    return Mathf.RoundToInt(rate);
  }

  private static bool IsAutoFail(int a, int b) {
    return a == DICE_MIN && b == DICE_MIN;
  }

  private static bool IsAutoSuccess(int a, int b) {
    return a == DICE_MAX && b == DICE_MAX;
  }
}
