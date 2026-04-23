using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingModel {
  public static List<string> badend_list = new List<string>() {
    BADEND_FIRST_BATTLE,
    /*
    BADEND_CAT,
    BADEND_HARD_WALK,
    BADEND_LOVE_NG_PRIEST,
    BADEND_MAOU_FAIR,
    BADEND_RETIRE,
    BADEND_SLIME,
    BADEND_SKELTON,
    BADEND_TAIHO,
    BADEND_DOG
    */
  };

  public const string BADEND_FIRST_BATTLE = "first_battle";
  public const string BADEND_CAT = "cat";
  public const string BADEND_HARD_WALK = "hard_walk";
  public const string BADEND_LOVE_NG_PRIEST = "love_ng_priest";
  public const string BADEND_MAOU_FAIR = "maou_fair";
  public const string BADEND_RETIRE = "retire";
  public const string BADEND_SLIME = "slime";
  public const string BADEND_SKELTON = "skelton";
  public const string BADEND_TAIHO = "taiho";
  public const string BADEND_DOG = "dog";
  public const string BADEND_TIME_OVER = "time_over";
  public const string BADEND_ARROW = "arrow";
}
