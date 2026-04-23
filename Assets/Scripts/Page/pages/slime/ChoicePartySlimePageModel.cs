using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicePartySlimePageModel {

  public const string PAGE_KEY = "slime/choice_party";
  private const string CHOICE_CANCEL = ChoiceSlimePageModel.PAGE_KEY;

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.setPageTypeChoice();
    model.main_bg = "240_135/slime_encount";

    KappaController.instance.hideKappa();

    ChoiceModel.instance.setTitle("誰に任せる？");

    bool usagiJoined = DataMgr.GetBool("ally_usagi_joined");
    bool shioriJoined = DataMgr.GetBool("ally_shiori_joined");

    if (usagiJoined) {
      ChoiceModel.instance.AddButton(UsagiSlimePageModel.PAGE_KEY, "ウサギに任せる");
    }

    if (shioriJoined) {
      ChoiceModel.instance.AddButton(ShioriSlimePageModel.PAGE_KEY, "シオリーナに任せる");
    }

    ChoiceModel.instance.AddButton(CHOICE_CANCEL, "やっぱりやめる");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (string.IsNullOrEmpty(key)) return;

    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
