using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceMaouPageModel {
  public const string PAGE_KEY = "maou/choice";
  private const string CHOICE_GOKURI = Gokuri1MaouPageModel.PAGE_KEY;
  private const string CHOICE_REFUSE = Line1MaouPageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.setPageTypeChoice();
    model.main_bg = "240_135/maou_jk_240_135";
    model.speaker = "魔王";

    ChoiceModel.instance.setTitle("世界の半分をやるから仲間になる気はないか？");
    ChoiceModel.instance.AddButton(CHOICE_GOKURI, "ごくり");
    ChoiceModel.instance.AddButton(CHOICE_REFUSE, "断る！　だが断る！");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
