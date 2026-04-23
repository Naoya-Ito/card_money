using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUsagiPageModel {
  public const string PAGE_KEY = "usagi/start";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.bgm = BGMMgr.KEY_SHIMANAGASHI;
    model.main_text = "カッパさん。\n魔王を倒すには仲間が必要不可欠っす。\n例えば俊敏なウサギとかがオススメっす";
    model.main_bg = "240_135/majisuka";
    model.speaker = "ウサギ";

    KappaController.instance.hideKappa();

    model.next_page = Start2UsagiPageModel.PAGE_KEY;
    return model;
  }
}
