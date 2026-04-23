using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatariCastlePageModel {

  public const string PAGE_KEY = "castle/katari";

  static public PageModel getPageData()
  {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.setPageTypeChoice();
    model.main_bg = "chapter1/jk_meet";

    KappaController.instance.hideKappa();

    ChoiceModel.instance.setTitle("カッパ「ややっ。ヲタクに優しそうなギャルがいるぞ！」");
    ChoiceModel.instance.AddButton("chapter1/op_jk_yes", "話しかけよう！　　[勇気+1][遅刻+1]");
    ChoiceModel.instance.AddButton("chapter1/op_jk_no", "華麗にスルーする。  [冷静+1]");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    switch (key) {
      case "day1teiji/go_home":
        Debug.Log("pushed go home");
        break;
      case "day1teiji/zangyou":
        Debug.Log("pushed zangyou");
        break;
      default:
        break;
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
