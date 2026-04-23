using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RingConst {

  public const float SCREEN_WIDTH = 960.0f;
  public const float SCREEN_HEIGHT = 540.0f;

  public const int DAY_START_HOUR = 8;
  public const int DAY_LUNCH_HOUR = 12;
  public const int DAY_END_HOUR = 23;

  // お金の価格
  public const int COST_SKILL_DAY_WORK_MIN = 1;
  public const int COST_BONE = 1;
  public const int COST_ONE = 1;
  public const int COST_WORK = 2;
  public const int COST_DICE = 5;
  public const int COST_SKILL_DAY_WORK_MAX = 3;
  public const int COST_NORMAL_LUNCH = 2;
  public const int COST_SLIME = 5;
  public const int COST_PARTY_CAT = 5;
  public const int COST_INN_NORMAL = 3;
  public const int COST_GOOD_LUNCH = 5;
  public const int COST_SELL_BLOOD = 10;
  public const int COST_DOG = 10;
  public const int COST_SKELTON = 10;
  public const int COST_CROSS = 7;
  public const int COST_KIZOKU = 15; // 貴族の初期の所持金ボーナス
  public const int COST_STEAL_TOWN = 38;
  public const int COST_STEAL_SHOP = 40;
  public const int COST_DEVIL_GOLD = 66;
  public const int COST_GOLD_SHOP = 99;
  public const int COST_SELL_BLOOD_MAX = 100;
  public const int COST_HITMAN = 300;
  public const int COST_DEVIL_PANALTY = 6666;

  // HP関連
  public const int HP_HEAL_UMA = 2;
  public const int HP_DAMAGE_HARD_WALK = 4;

  // ステータスアップ
  public const int STR_MGI_UP_LUNCH_NORMAL = 1;
  public const int HP_UP_LUNCH_NORMAL = 5;
  public const int AGI_UP_LUNCH_NORMAL = 2;

  public const int STR_DOWN_INN_NORMAL_ALL_ATTACK = -2;

  // ボーナス
  public const int BONUS_STR_MGI_UP = 1;
  public const int BONUS_HP_UP = 5;
  public const int BONUS_AGI_UP = 2;
  public const int BONUS_ALL_ATTACK_STR_MINUS = -2;

  // 罪の重さ
  public const int GUILTY_LV_LUNCH = 1;
  public const int GUILTY_LV_SHOP_STEAL = 2;
  public const int GUILTY_LV_TOWN_STEAL = 2;

  //  戦闘系の時間
  public const float DURATION_DAMAGED = 0.5f;

  // page scene
  // ゲー未画面の歩きアニメーション秒数
  public const float KAPPA_DEFAULT_X = -200;
  public const float KAPPA_DEFAULT_Y = 120;
  public const float WALK_ANIMATION_DURATION = 1.5f;
  public const float WALK_WIDTH = 590;

  // status key
  public const string STATUS_KEY_CHARM = "char";
  public const string STATUS_KEY_LUCK = "luck";

  // party status
  public const int JK_STR = 3;
  public const int JK_AGI = 7;


  // page scene
  public const float BG_MOVE_DURATION = 0.2f;

  public const float DURATION_PAGE_BUTTON_PUSHED_DELAY = 0.3f;

  public const string TALK_POS_RIGHT = "right";
  public const string TALK_POS_LEFT = "left";

  public const string RIGHT_IMAGE_NOTHING = "nothing";

  public const int STATUS_UP_STR_CHURCH_INORI = 2;





  // name
  public const string NAME_USAGI = "補佐官ウサギ";

  // animation
  public const int ANIMATION_STATE_IDOL = 0;
  public const int ANIMATION_STATE_KAPPA_RUN = 1;
  public const int ANIMATION_STATE_KAPPA_DRUM = 2;
  public const int ANIMATION_STATE_USAGI_RUN = 1;
  public const int ANIMATION_STATE_USAGI_GUITAR = 2;
  public const int ANIMATION_STATE_TANUKI_RUN = 1;
  public const int ANIMATION_STATE_TANUKI_GUITAR = 2;
/*

  public static List<string> getFirstDeck() {
    return new List<string>() {
//      "fighter", "namake",
      "income_up", "punch", "usagi3", "nezumi", "meido",
      "fire_shot", "bomb",
      "usagi_house", "wall", "houdai",
      "usagi_guitar",
//      "meido",
//      "dog_sniper",
//      "wizard", 
    //k "dark_knight",
    };
  }

  // ユニットのステータス
  public const int UNIT_HP_BASE = 30; // ユニットの基準HP
  public const int UNIT_STR_BASE = 5; // ユニットの基準HP
  public const int UNIT_DEF_BASE = 2; // ユニットの基準HP
  public const float UNIT_ATTACK_DURATION_BASE = 0.2f;
  public const float UNIT_ATTACK_WAIT_DURATION_BASE = 0.1f;

  // ユニットのサイズ
  public const float UNIT_SIZE_NORMAL = 0.8f;
  public const float UNIT_SIZE_MINI = 0.6f;
  public const float UNIT_SIZE_BIG = 1.0f;

  // ユニットの歩き
  public const int UNIT_WALK_NOIMAGE_DURATION_BASE = 6; // 歩き画像がない場合、何歩で上下運動をするか
  public const float UNIT_WALK_NOIMAGE_DURATION_HEIGHT = 7f; // 歩き画像がない場合、どれだけ上下運動するか

  // 戦闘関係の時間設定
  public const int UNIT_MOVE_SPEED = 10; // 低いほど移動速度が速い。 この値に達するとユニットは動く
  public const int UNIT_UNIQUE_SKILL = 700; // この値に達するとユニットは固有技を発動する

  public const int CUTIN_ANIMATION_TIME = 200; // nフレーム後に再始動
  public const int FOOTER_CUTIN_ANIMATION_TIME = 300; // nフレーム後に再始動

  // ボス設定
  public const int BOSS_SUMMON_ENEMY_DURATION = 2000;
  */

}
