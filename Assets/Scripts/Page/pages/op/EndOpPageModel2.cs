using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOpPageModel : PageModel {

  public const string PAGE_KEY = "op/end";

  static public PageModel getPageData() {
    EndOpPageModel model = new EndOpPageModel();
    model.bgm = BGMMgr.KEY_DOKIDOKI;
    model.main_text = "「おーい」";
    model.main_bg = "bg/bg_town";
    model.main_image = "128_128/shiori_wink";
    model.speaker = "カッパ";
    model.next_page = StartUsagiPageModel.PAGE_KEY;

    return model;
  }

  public override bool TryTransitScene() {
    CommonUtil.changeScene("GameScene");
    return true;
  }
}
