using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSlimePageModel {

  public const string PAGE_KEY = "slime/choice";
  private const string CHOICE_A = Attack0SlimePageModel.PAGE_KEY;
  private const string CHOICE_B = ChoicePartySlimePageModel.PAGE_KEY;
  private const string CHOICE_C = EndSlimePageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.setPageTypeChoice();
    model.main_bg = "240_135/slime_encount";

    KappaController.instance.hideKappa();

    ChoiceModel.instance.setTitle("");
    int atk = DataMgr.GetInt("atk");
    string atk_explain = $"攻撃力 {atk}→{atk + 1}";
    ChoiceModel.instance.AddButton(CHOICE_A, "ぶったおす！　ヒャッハー！", atk_explain);

    bool usagiJoined = DataMgr.GetBool("ally_usagi_joined");
    bool shioriJoined = DataMgr.GetBool("ally_shiori_joined");
    if (usagiJoined || shioriJoined) {
      ChoiceModel.instance.AddButton(CHOICE_B, "仲間に任せる");
    } else {
      ChoiceModel.instance.AddButton("", "仲間に任せる");
      ChoiceModel.instance.SetButtonEnabled(2, false, "条件:仲間が必要");
    }

    ChoiceModel.instance.AddButton(CHOICE_C, "ざこモンスターなんて無視だ", "すばやさ+1");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (string.IsNullOrEmpty(key)) return;

    switch (key) {
      case "day1teiji/go_home":
        Debug.Log("pushed go home");
        break;
      case "day1teiji/zangyou":
        Debug.Log("pushed zangyou");
        break;
      case CHOICE_A:
        DataMgr.Increment("atk", 1);
        break;
      case CHOICE_C:
        DataMgr.Increment("agi", 1);
        break;
      default:
        break;
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
