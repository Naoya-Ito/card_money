using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class CommonUtil : MonoBehaviour{

  public static int rnd(int max_num){
    return UnityEngine.Random.Range(0, max_num);
  }

  public static bool isHitPer(int per){
    return CommonUtil.rnd(100) < per;
  }

  public static void changeScene(string scene_name, float delay = 0.4f){
    if(FadeManager.instance) {
      if(DataMgr.GetBool("change_scene_time_speed_up")) {
        FadeManager.Instance.LoadScene(scene_name, 0.1f);
      } else {
        FadeManager.Instance.LoadScene(scene_name, delay);
      }
    } else {
      SceneManager.LoadScene(scene_name);
    }
  }

  public static string getCurrentSceneName(){
    return SceneManager.GetActiveScene().name;
  }

  public static string getRndStrByArray(string[] rows){
    int array_num = rows.Length;
    int rand_key = UnityEngine.Random.Range(0, array_num);
    return rows[rand_key];
  }

  public static string getRndStrByList(List<string> list){
    return list[UnityEngine.Random.Range(0, list.Count)];
  }

  public static void changeText(string key, string text) {
    if(GameObject.Find(key) == null) {
      Debug.Log($"error!! {key} is not exist. val={text}");
      return;
    }

    TextMeshProUGUI target_text = GameObject.Find(key).GetComponent<TextMeshProUGUI>();
    if(target_text != null) {
      target_text.text = text;
    } else {
      Debug.Log($"Warning!!! {key} is text. not tmp");
      Text target_text2 = GameObject.Find(key).GetComponent<Text>();
      target_text2.text = text;

    }
  }

  public static Vector3 getRandomPosBy(Vector3 pos, float rand_size){
    float new_x = pos.x + rand_size*UnityEngine.Random.Range(-1.0f, 1.0f);
    float new_y = pos.y + rand_size*UnityEngine.Random.Range(-1.0f, 1.0f);
    Vector3 new_pos = new Vector3(new_x, new_y, 1);
    return new_pos;
  }

  public static void showImage(string key){
//    Debug.Log($"show image. key={key}");
    Image image = GameObject.Find(key).GetComponent<Image>();
		var color = image.color;
		color.a = 1.0f;
		image.color = color;
  }

  public static void unvisibleImage(string key){
    Image image = GameObject.Find(key).GetComponent<Image>();
		var color = image.color;
		color.a = 0.0f;
		image.color = color;
  }

  public static void hideImage(string key) {
    SpriteRenderer sr = GameObject.Find(key).GetComponent<SpriteRenderer>();
    if(sr != null) {
      sr.gameObject.SetActive(false);
      return;
    }

    Image image = GameObject.Find(key).GetComponent<Image>();
    image.enabled = false;
  }

  // Find は重いので非推奨
  /*
  public static void hideButton(string key) {
    Image image = GameObject.Find(key).GetComponent<Image>();
		var color = image.color;
		color.a = 0.0f;
		image.color = color;
  }
  */

  public static void showButton(string key) {
    Image image = GameObject.Find(key).GetComponent<Image>();
		var color = image.color;
		color.a = 1.0f;
		image.color = color;
  }
  /*
  public static void hideButton(string key) {
    Button button = GameObject.Find(key).GetComponent<Button>();
    Vector3 pos = button.transform.localPosition;
//    Debug.Log($"key={key}. x={pos.x} y={pos.y}");
    Vector3 new_pos = new Vector3(3000, pos.y, pos.z);
    button.transform.localPosition = new_pos;
  }
  public static void showButton(string key, float pos_x) {
    Button button = GameObject.Find(key).GetComponent<Button>();
    Vector3 pos = button.transform.localPosition;
    Vector3 new_pos = new Vector3(pos_x, pos.y, pos.z);
    button.transform.localPosition = new_pos;
  }
  */

  public static void setRectWidth(string key, int width){
    RectTransform rct = GameObject.Find(key).GetComponent<RectTransform>();
    rct.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
  }

  public static bool isPV(){
    return DataMgr.GetBool("pv_mode");
  }

  public static Sprite getSpriteFromMultiple(string fileName, string spriteName){
    Sprite[] sprites = Resources.LoadAll<Sprite>($"Textures/{fileName}");
    return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(spriteName));
  }


  public static void shuffleList(List<string> list){
    for (int i = list.Count - 1; i > 0; i--){
      var j = UnityEngine.Random.Range(0, i+1);
      var temp = list[i];
      list[i] = list[j];
      list[j] = temp;
    }
  }

  public static void debugList(List<string> list){
//    Debug.Log("output debug list");
    Debug.Log(string.Join(",", list));
  }

  // 先頭を大文字にして返す
  public static string UpperCamelCase(string key) {
    try {
      if (string.IsNullOrEmpty(key)) return "Error";
      string[] parts = key.Split(new char[] { '_', '-' });
      string result = "";
      foreach (string part in parts) {
        if (string.IsNullOrEmpty(part)) continue;
        result += char.ToUpper(part[0]) + part.Substring(1);
      }
      return result == "" ? "Error" : result;
    } catch (Exception e) {
      Debug.Log($"error. upper camel case. val={key}");
      Debug.Log(e.Message);
      return "Error";
    }
  }


  // ボタンが押された時のイメージ変更
  public static void changeCursorButtonImage(Button button){
    Sprite sprite = Resources.Load<Sprite>("Textures/other/button_yellow");
    button.image.sprite = sprite;
  }

  public static void changeUnCursorButtonImage(Button button){
    Sprite sprite = Resources.Load<Sprite>("Textures/other/button_blue");
    button.image.sprite = sprite;
  }

  // ボタンが押された時のイメージ変更
  public static void changePushedButtonImage(Button button){
    GameObject.Destroy(button.GetComponent<Shadow>());
    GameObject.Destroy(button.GetComponent<Outline>());
  }
}
