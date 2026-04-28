using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOpPageModel : PageModel {

  public const string PAGE_KEY = "op/end";

  static public PageModel getPageData() {
    EndOpPageModel model = new EndOpPageModel();

    return model;
  }

  public override bool TryTransitScene() {
    CommonUtil.changeScene("GameScene");
    return true;
  }
}
