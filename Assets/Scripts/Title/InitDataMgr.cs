using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 初期データを管理
// 完全なゲーム新規状態、ゲームリスタート時の両方を管理
public class InitDataMgr {
  private const int START_HP = 100;
  private const int START_ATK = 3;
  private const int START_DEF = 3;
  private const int START_CHARM = 3;
  private const int START_AGI = 3;
  private const int START_GOLD = 10;


  public static List<string> bool_reset_list = new List<string>() {
    "is_bar_cat_out",
    "skill_kimi_no_nawa",
    "ally_usagi_joined",
    "ally_tanuki_joined",
    "ally_hime_joined",
    "ally_shiori_joined",
    "ally_usagi_popup_pending",
    "ally_usagi_run",
    "ally_tanuki_run",
  };
  public static List<string> num_reset_list = new List<string>() {
    "story_page",
    "exp",
    "def",
    "agi",
    "atk",
    "charm",
    "hp",
    "gold",
    "ally_shiori_max_hp",
    "ally_shiori_hp",
    "ally_shiori_atk",
    "ally_shiori_agi",
    "hp_shiori",
  };

  public static List<string> null_list = new List<string>() {
    "add_event",
    "field",
  };

  public static List<string> null_string_list = new List<string>() {
    "pre_card"
  };

  // ゲーム開始時のデータ
  public static void initData(){
//    DataMgr.SetStr("page", "op/start");
    DataMgr.SetStr("page", "castle/op");
    DataMgr.SetFloat("limit_time", 180f);


    foreach(string key in num_reset_list) {
      DataMgr.SetInt(key, 0);
    }
    DataMgr.SetInt("hp_usagi", 5);
    DataMgr.SetInt("hp_tanuki", 7);
    ApplyGameStartInitialStats();
    foreach(string key in bool_reset_list) {
      DataMgr.SetBool(key, false);
    }
    foreach(string key in null_string_list) {
      DataMgr.SetStr(key, "");
    }
    foreach(string key in null_list) {
      DataMgr.SetList(key, new List<string>());
    }
    DataMgr.SetBool("debug_mode", true);
  }

  public static void ApplyGameStartInitialStats() {
    DataMgr.SetInt("hp", START_HP);
    DataMgr.SetInt("hp_kappa", START_HP);
    DataMgr.SetInt("atk", START_ATK);
    DataMgr.SetInt("def", START_DEF);
    DataMgr.SetInt("charm", START_CHARM);
    DataMgr.SetInt("agi", START_AGI);
    DataMgr.SetInt("gold", START_GOLD);
  }
}
