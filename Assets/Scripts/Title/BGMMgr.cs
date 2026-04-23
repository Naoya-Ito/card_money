using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMMgr : MonoBehaviour{

  [SerializeField] public AudioClip[] clips;
  [SerializeField] public AudioSource audios;
  public static BGMMgr instance = null;

  private bool isFadeOut = false;
  private double FadeOutSeconds = 2.0;
  private double FadeDeltaTime = 0;

  public const string KEY_GAME_OVER = "game_over";
  public const string KEY_DOKIDOKI = "dokidoki";
  public const string KEY_BATTLE_WIN = "battle_win";
  public const string KEY_BATTLE = "battle";
  public const string KEY_OP = "game_op";
  public const string KEY_WAKUWAKU = "wakuwaku";
  public const string KEY_HOT = "hot";
  public const string KEY_MARUGOSHI = "marugoshi";
  public const string KEY_CREAM_PUFF_MATCHA = "cream_puff_matcha";
  public const string KEY_SHIMANAGASHI = "shimanagashi";
  public const string KEY_DUNGEON = "gesuido_song";
  public const string KEY_PAREIDOLIA = "pareidolia";
  public const string KEY_SHUMATSU_NO_SUE = "shumatsu_no_sue";
  public const string KEY_GREAT_SUGAR_KO = "great_sugar_ko";
  public const string KEY_SENDOUSURU_OTOKO = "sendousuru_otoko";
  public const string KEY_EMOTIONAL = "emotional";

  static int BGM_TITLE = 0;
  static int BGM_AMEIRO = 1;
  static int BGM_FIELD = 2;
  static int BGM_GAMEOVER = 3;
  static int BGM_ENDING = 4;
  static int BGM_LAST_BOSS = 6;
  static int BGM_DOKIDOKI = 7;
  static int BGM_BATTLE = 8;
  static int BGM_BATTLE_BOSS = 9;
  static int BGM_TOWN = 10;
  static int BGM_BATTLE_WIN = 11;
  static int BGM_GAME_OP = 12;
  static int BGM_WAKUWAKU = 13;
  static int BGM_HOT = 14;
  static int BGM_MARUGOSHI = 8;
  static int BGM_CREAM_PUFF_MATCHA = 11;
  static int BGM_SHIMANAGASHI = 14;
  static int BGM_DUNGEON = 15;
  static int BGM_PAREIDOLIA = 9;
  static int BGM_SHUMATSU_NO_SUE = 6;
  static int BGM_GREAT_SUGAR_KO = 16;
  static int BGM_SENDOUSURU_OTOKO = 17;
  static int BGM_EMOTIONAL = 18;

  private void Awake(){
    if(instance == null) {
      instance = this;
    } else {
      Destroy(this.gameObject);
    }
  }

  void Start(){
    DontDestroyOnLoad(this);
//    audios = GetComponent<AudioSource>();
    EnsureClip(BGM_MARUGOSHI, KEY_MARUGOSHI);
    EnsureClip(BGM_CREAM_PUFF_MATCHA, KEY_CREAM_PUFF_MATCHA);
    EnsureClip(BGM_DUNGEON, KEY_DUNGEON);
    EnsureClip(BGM_GREAT_SUGAR_KO, KEY_GREAT_SUGAR_KO);
    EnsureClip(BGM_SENDOUSURU_OTOKO, KEY_SENDOUSURU_OTOKO);
    EnsureClip(BGM_EMOTIONAL, KEY_EMOTIONAL);

    audios.clip = clips[BGM_TITLE];
    audios.Play();

    if(PlayerPrefs.HasKey("bgm_volume")) {
      AudioListener.volume = DataMgr.GetFloat("bgm_volume");
    } else {
      DataMgr.SetFloat("bgm_volume", 1.0f);
    }
  }

  void Update(){
    if (isFadeOut) {
      FadeDeltaTime += Time.deltaTime;
      if (FadeDeltaTime >= FadeOutSeconds) {
        FadeDeltaTime = 0;
        isFadeOut = false;
        audios.Stop();
        audios.volume = 1.0f;
        return;
      }
      audios.volume = (1.0f - (float)(FadeDeltaTime / FadeOutSeconds));
    }
  }

  public void stopMusic(){
//    Debug.Log("stop music");
//    audios.Stop();
//    Debug.Log($"stop music. now day = {DataMgr.GetInt("day")}");
    isFadeOut = true;

    DataMgr.SetStr("now_bgm", "");
  }

  public void changeBGM(string key){
    if(key == "" || key == null) return;

    string now_bgm = DataMgr.GetStr("now_bgm");
//    Debug.Log($"now={now_bgm}, next bgm={key}");
    if(key == now_bgm) return;

    isFadeOut = false;
    audios.volume = 1.0f;
    DataMgr.SetStr("now_bgm", key);


    int BGM_NO;
    switch(key) {
    case "title":
      BGM_NO = BGM_TITLE;
      break;
    case "ameiro":
      BGM_NO = BGM_AMEIRO;
      break;
    case "field":
      BGM_NO = BGM_FIELD;
      break;
    case "ending":
      BGM_NO = BGM_ENDING;
      break;
    case KEY_GAME_OVER:
      BGM_NO = BGM_GAMEOVER;
      break;
    case "last_boss":
      BGM_NO = BGM_LAST_BOSS;
      break;
    case KEY_DOKIDOKI:
      BGM_NO = BGM_DOKIDOKI;
      break;
    case KEY_BATTLE_WIN:
      BGM_NO = BGM_BATTLE_WIN;
      break;
    case KEY_BATTLE:
      BGM_NO = BGM_BATTLE;
      break;
    case "battle_boss":
      BGM_NO = BGM_BATTLE_BOSS;
      break;
    case "town":
      BGM_NO = BGM_TOWN;
      break;
    case KEY_OP:
      BGM_NO = BGM_GAME_OP;
      break;
    case KEY_WAKUWAKU:
      BGM_NO = BGM_WAKUWAKU;
      break;
    case KEY_HOT:
      BGM_NO = BGM_HOT;
      break;
    case KEY_MARUGOSHI:
      BGM_NO = BGM_MARUGOSHI;
      break;
    case KEY_CREAM_PUFF_MATCHA:
      BGM_NO = BGM_CREAM_PUFF_MATCHA;
      break;
    case KEY_SHIMANAGASHI:
      BGM_NO = BGM_SHIMANAGASHI;
      break;
    case KEY_DUNGEON:
      BGM_NO = BGM_DUNGEON;
      break;
    case KEY_PAREIDOLIA:
      BGM_NO = BGM_PAREIDOLIA;
      break;
    case KEY_SHUMATSU_NO_SUE:
      BGM_NO = BGM_SHUMATSU_NO_SUE;
      break;
    case KEY_GREAT_SUGAR_KO:
      BGM_NO = BGM_GREAT_SUGAR_KO;
      break;
    case KEY_SENDOUSURU_OTOKO:
      BGM_NO = BGM_SENDOUSURU_OTOKO;
      break;
    case KEY_EMOTIONAL:
      BGM_NO = BGM_EMOTIONAL;
      break;
    default:
      Debug.Log($"unknown bgm name. key={key}");
      return;
    }
    if (clips == null || BGM_NO < 0 || BGM_NO >= clips.Length || clips[BGM_NO] == null) {
      Debug.LogError($"bgm clip missing. key={key}, index={BGM_NO}");
      return;
    }
    audios.clip = clips[BGM_NO];
    audios.Play();
  }

  private void EnsureClip(int index, string key){
    if (index < 0) return;
    if (clips == null) {
      clips = new AudioClip[index + 1];
    }
    if (clips.Length <= index) {
      AudioClip[] next = new AudioClip[index + 1];
      for (int i = 0; i < clips.Length; i++) {
        next[i] = clips[i];
      }
      clips = next;
    }
    if (clips[index] == null) {
      clips[index] = Resources.Load<AudioClip>($"Musics/{key}");
    }
  }
}
