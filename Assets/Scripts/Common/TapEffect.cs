using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffect : MonoBehaviour{
    public static TapEffect instance = null;

    public float deleteTime = 1.0f;
//    public BloodObject blood;

    private void Awake(){
      if(instance == null) {
        instance = this;
      } else {
        Destroy(this.gameObject);
      }
    }

    void Start(){
      DontDestroyOnLoad(this);
    }

    void Update(){
      /*
      if (Input.GetMouseButtonDown(0)){
        string now_scene = CommonUtil.getCurrentSceneName();
        if(now_scene == "BattleShootingScene") return;
        if(DataMgr.GetBool("is_setting")) return;

        //マウスカーソルの位置を取得。
        var mousePosition = Input.mousePosition;
        mousePosition.z = 3f;

        Vector3 world_pos = Camera.main.ScreenToWorldPoint(mousePosition);
//        Instantiate(blood, world_pos, Quaternion.identity);
      }
      */
    }
}