using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceUsagiPageModel {
  public const string PAGE_KEY = "usagi/choice";
  private const string CHOICE_A = EndUsagiPageModel.PAGE_KEY;
  private const string CHOICE_B = RefuseUsagiPageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.setPageTypeChoice();
    model.main_bg = "240_135/majisuka";
    model.speaker = "ウサギ";

    KappaController.instance.hideKappa();

    ChoiceModel.instance.setTitle("");
    ChoiceModel.instance.AddButton(CHOICE_A, "よろしく頼む！", "所持金-2\nウサギ加入");
    ChoiceModel.instance.AddButton(CHOICE_B, "１ゴールドもださんぞ");

    int gold = DataMgr.GetInt("gold");
    if (gold < 2) {
      ChoiceModel.instance.SetButtonEnabled(1, false, "条件:所持金2以上");
    }

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (key == CHOICE_A) {
      int gold = DataMgr.GetInt("gold");
      if (gold < 2) return;
      DataMgr.SetInt("gold", Mathf.Max(0, gold - 2));
      JoinUsagi();
    } else if (key == CHOICE_B) {
      DataMgr.SetBool("ally_usagi_joined", false);
      DataMgr.SetBool("ally_usagi_popup_pending", false);
    }
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }

  private static void JoinUsagi() {
    DataMgr.SetBool("ally_usagi_joined", true);
    DataMgr.SetBool("ally_usagi_popup_pending", true);
    DataMgr.SetBool("ally_usagi_run", true);
    DataMgr.SetBool("ally_tanuki_run", false);
  }
}
