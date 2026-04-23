using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AarrowFloor1PageModel {
  public const string PAGE_KEY = "floor1/aarrow";
  private const string CHOICE_AVOID = AvoidFloor1PageModel.PAGE_KEY;
  private const string CHOICE_ENDURE = GuardFloor1PageModel.PAGE_KEY;
  private const string CHOICE_ALLY = PartyFloor1PageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.setPageTypeChoice();
    model.main_bg = "240_135/dungeon_up";

    if (KappaController.instance != null) {
      KappaController.instance.stopAnimation();
      KappaController.instance.hideKappa();
    }

    ChoiceModel.instance.setTitle("矢の罠だ！！！");
    ChoiceModel.instance.AddButton(CHOICE_AVOID, "避けてみる", "敏捷判定10");
    ChoiceModel.instance.AddButton(CHOICE_ENDURE, "耐えてみせる");
    ChoiceModel.instance.AddButton(CHOICE_ALLY, "仲間に頼る");
    if (!HasAnyAvailableAlly()) {
      ChoiceModel.instance.SetButtonEnabled(3, false, "仲間が1人以上いる");
    }

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (string.IsNullOrEmpty(key)) return;

    if (key == CHOICE_AVOID) {
      int agi = DataMgr.GetInt("agi");
      int requiredDiceTotal = Mathf.Max(0, Floor1TrapState.ARROW_AVOID_THRESHOLD - agi);
      GameSceneMgr.instance.StartStatDice(
        agi,
        Floor1TrapState.ARROW_AVOID_THRESHOLD,
        AvoidFloor1PageModel.PAGE_KEY,
        PainFloor1PageModel.PAGE_KEY,
        $"出目の合計が{requiredDiceTotal}以上で成功",
        () => Floor1TrapState.SetArrowDamage(0),
        () => Floor1TrapState.SetArrowDamage(Floor1TrapState.ARROW_DAMAGE_AVOID_FAIL)
      );
      return;
    }

    if (key == CHOICE_ENDURE) {
      Floor1TrapState.SetArrowDamage(Floor1TrapState.ARROW_DAMAGE_ENDURE);
      DataMgr.SetStr("page", key);
      GameSceneMgr.instance.updateScene(key);
      return;
    }

    if (key == CHOICE_ALLY && HasAnyAvailableAlly()) {
      DataMgr.SetStr("page", key);
      GameSceneMgr.instance.updateScene(key);
    }
  }

  private static bool HasAnyAvailableAlly() {
    return DataMgr.GetBool("ally_usagi_joined") || DataMgr.GetBool("ally_shiori_joined");
  }
}
