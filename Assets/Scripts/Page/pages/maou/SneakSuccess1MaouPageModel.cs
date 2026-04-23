using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakSuccess1MaouPageModel {
  public const string PAGE_KEY = "maou/sneak_success1";

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.main_text = "カッパ3世とは\\n俺の事だぁー！";
    model.main_bg = "other/cutin";
    model.speaker = "カッパ";

    KappaController.instance.changeImage("kappa_joy");

    model.next_page = SneakSuccess2MaouPageModel.PAGE_KEY;
    return model;
  }
}
