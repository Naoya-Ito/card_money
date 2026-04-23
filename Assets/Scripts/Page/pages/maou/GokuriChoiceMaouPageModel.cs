using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuriChoiceMaouPageModel {
  public const string PAGE_KEY = "maou/gokuri_choice";
  private const string CHOICE_A = GokuriASuccess1MaouPageModel.PAGE_KEY;
  private const string CHOICE_B = GokuriB1MaouPageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.setPageTypeChoice();
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "魔王";

    DataMgr.SetInt("maou_agi_mod", 0);

    ChoiceModel.instance.setTitle("魔王の誘いに対して");
    ChoiceModel.instance.AddButton(CHOICE_A, "油断させて隙をつく", "かしこさ判定10\n成功時：魔王のすばやさ-10\n失敗時：5ダメージ受ける");
    ChoiceModel.instance.AddButton(CHOICE_B, "丁重にお断りする");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (key == CHOICE_A) {
      string infoText = "出目の合計＋かしこさが10以上で成功";
      GameSceneMgr.instance.StartStatDice(
        DataMgr.GetInt("wis"),
        10,
        GokuriASuccess1MaouPageModel.PAGE_KEY,
        GokuriAFail1MaouPageModel.PAGE_KEY,
        infoText,
        () => DataMgr.SetInt("maou_agi_mod", -10),
        () => ApplyKappaDamage(5)
      );
      return;
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }

  private static void ApplyKappaDamage(int damage) {
    int hp = DataMgr.GetInt("hp_kappa");
    hp = Mathf.Max(0, hp - damage);
    DataMgr.SetInt("hp_kappa", hp);
  }
}
