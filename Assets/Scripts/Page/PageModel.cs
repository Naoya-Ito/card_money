using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.Reflection;

public class PageModel {

  public const string PAGE_TYPE_NORMAL = "normal";
  public const string PAGE_TYPE_CHOICE = "choice";
  public const string PAGE_TYPE_MAP_MOVE = "map_move";
  public string page_type = PAGE_TYPE_NORMAL;


  public string main_text = "";
  public string main_bg = "";
  public string main_image = "";
  public string bgm = "";
  public string speaker = "";

  public string next_page = "";

  public bool isChangeScene = false;
  public bool isKappaDead = false;

  static public void pushedTappedScreen(string key) {
    switch (key) {
      case "op/end":
        DataMgr.SetInt("chapter", 1);
        CommonUtil.changeScene("GameScene");
        break;
      case "maou/end":
        if (DataMgr.GetStr("page") == "maou/end") {
          CommonUtil.changeScene("EndingScene");
        } else {
          GameSceneMgr.instance.goToNextPage(key);
        }
        break;
      case "chara/usagi":
      default:
        //Debug.Log($"Error!! pushedTappedScreen. key={key}");
        GameSceneMgr.instance.goToNextPage(key);
        break;
    }
  }

  static public Type getStaticObject(string key) {
    string target_class_name = "";
    try {
      target_class_name = getClassNameByPath(key);
      //      Debug.Log($"get static class. name={target_class_name}");
      return Type.GetType(target_class_name);
    }
    catch (Exception e) {
      Debug.Log($"error!  getClassNameByPath. key={key}. name={target_class_name}");
      Debug.Log(e.Message);
      return Type.GetType("ErrorPageModel");
    }
  }

  static public string getClassNameByPath(string key)
  {
    try
    {
      string[] tmp_page = key.Split("/");
      string category = CommonUtil.UpperCamelCase(tmp_page[0]);
      string[] tmp_child = tmp_page[1].Split("_");
      List<string> tmp_list = new List<string>() { };
      foreach (string v in tmp_child)
      {
        tmp_list.Add(CommonUtil.UpperCamelCase(v));
      }
      string k = String.Join("", tmp_list);

      return $"{k}{category}PageModel";
    }
    catch (Exception e)
    {
      Debug.Log($"error!  getClassNameByPath. key={key}");
      Debug.Log(e.Message);
      return "ErrorPageModel";
    }
  }

  static public PageModel getPageModelByKey(string key) {
    try {
      Type static_obj = getStaticObject(key);
      MethodInfo getPageData = static_obj.GetMethod("getPageData");
      object obj = getPageData.Invoke(null, new object[] { });
      return (PageModel)obj;
    }
    catch (Exception e) {
      Debug.Log($"Error!! cant get class file. class_name={key}. message={e.Message}");
      Type static_obj = Type.GetType("ErrorPageModel");
      MethodInfo getPageData = static_obj.GetMethod("getPageData");
      object obj = getPageData.Invoke(null, new object[] { });
      return (PageModel)obj;
    }
  }

  static public void setButton(string key) {
    try {
      Type static_obj = getStaticObject(key);
      MethodInfo setButton = static_obj.GetMethod("setButton");
      setButton.Invoke(null, new object[] { });
    }
    catch (Exception e) {
      Debug.Log($"Error!! setButton. key={key}. message={e.Message}");
    }
  }

  static public void pushedButton(string pushed_key) {
    string now_page = DataMgr.GetStr("page");
    Type static_obj = PageModel.getStaticObject(now_page);
    MethodInfo pushedChoice = static_obj.GetMethod("pushedChoiceButton");
    pushedChoice.Invoke(null, new object[] { pushed_key });
  }


  public void setPageTypeChoice() {
    page_type = PAGE_TYPE_CHOICE;
    ChoiceModel.instance.initChoice();

  }

  public void setPageTypeMapMove() {
    page_type = PAGE_TYPE_MAP_MOVE;
  }

}
