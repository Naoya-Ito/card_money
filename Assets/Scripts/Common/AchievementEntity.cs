using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementEntity", menuName = "Create AchievementEntity")]

public class AchievementEntity : ScriptableObject {
  public string key;
  public string title;
  [Multiline] public string description;
  }