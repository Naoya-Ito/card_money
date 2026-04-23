using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskEndCastlePageModel {
  public const string PAGE_KEY = "castle/ask_end";
  private const string CHOICE_MORE = AskChoiceCastlePageModel.PAGE_KEY;
  private const string CHOICE_NONE = EndHimePageModel.PAGE_KEY;

  static public PageModel getPageData(){
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.setPageTypeChoice();
    model.main_bg = "bg/castle_gray";
    model.main_image = "128_128/queen_normal";

    ChoiceModel.instance.setTitle("他に聞きたい事はありますか？");
    ChoiceModel.instance.AddButton(CHOICE_MORE, "あります！");
    ChoiceModel.instance.AddButton(CHOICE_NONE, "ないです");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }
}
