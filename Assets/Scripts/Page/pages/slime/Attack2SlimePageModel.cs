using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2SlimePageModel {

  public const string PAGE_KEY = "slime/attack2";
  private const string KEY_HUNT_MORE = "slime/attack0";
  private const string KEY_GO_CASTLE = EndSlimePageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_CREAM_PUFF_MATCHA;
    model.setPageTypeChoice();
    model.main_bg = "240_135/slime_encount";

    DataMgr.Increment("exp", 1);

    ChoiceModel.instance.setTitle("スライムを撃破した。1の経験値をえた！");

    int exp = DataMgr.GetInt("exp");
    if (exp >= 3) {
      ChoiceModel.instance.AddButton(KEY_HUNT_MORE, "レベルアップ！");
    } else {
      ChoiceModel.instance.AddButton(KEY_HUNT_MORE, "もっとスライムを狩るぞ！");
    }
    ChoiceModel.instance.AddButton(KEY_GO_CASTLE, "魔王城へ向かおう");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (key == KEY_GO_CASTLE) {
      PageModel.pushedTappedScreen(key);
      return;
    }

    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
