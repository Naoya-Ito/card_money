using UnityEngine;

public static class Floor1TrapState {
  public const string ARROW_DAMAGE_KEY = "floor1_arrow_damage";
  public const string ARROW_DAMAGE_APPLIED_KEY = "floor1_arrow_damage_applied";
  public const string ARROW_GAME_OVER_KEY = "game_over/arrow";
  public const int ARROW_AVOID_THRESHOLD = 10;
  public const int ARROW_DAMAGE_AVOID_FAIL = 12;
  public const int ARROW_DAMAGE_ENDURE = 6;

  public static void SetArrowDamage(int damage) {
    DataMgr.SetInt(ARROW_DAMAGE_KEY, Mathf.Max(0, damage));
    DataMgr.SetBool(ARROW_DAMAGE_APPLIED_KEY, false);
  }

  public static int GetArrowDamage() {
    return Mathf.Max(0, DataMgr.GetInt(ARROW_DAMAGE_KEY));
  }

  public static void ApplyArrowDamage() {
    if (DataMgr.GetBool(ARROW_DAMAGE_APPLIED_KEY)) {
      return;
    }
    int damage = GetArrowDamage();
    int hp = Mathf.Max(0, DataMgr.GetInt("hp_kappa") - damage);
    DataMgr.SetInt("hp_kappa", hp);
    DataMgr.SetBool(ARROW_DAMAGE_APPLIED_KEY, true);
  }

  public static bool IsKappaDead() {
    return DataMgr.GetInt("hp_kappa") <= 0;
  }

  public static void ClearArrowDamage() {
    DataMgr.SetInt(ARROW_DAMAGE_KEY, 0);
    DataMgr.SetBool(ARROW_DAMAGE_APPLIED_KEY, false);
  }
}
