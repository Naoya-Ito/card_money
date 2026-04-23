using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorPageModel : MonoBehaviour {

  static public PageModel getPageData(){
    PageModel model = new PageModel();

    string key = DataMgr.GetStr("page");
    model.main_text = $"エラーが発生しました。key={key}";

//    ChoiceModel.instance.AddButton("start/bar_in", "入ろう。きっと頼れる仲間が待っている。");

    return model;
  }

  static public void pushedChoiceButton(string key) {
    switch(key) {
      default:
        break;
    }
  }

}
