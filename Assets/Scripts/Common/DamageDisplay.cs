using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// DamageText(prefabとscript)を表示するクラス
// UnitPrefab などにアタッチして使う
public class DamageDisplay : MonoBehaviour {
  [SerializeField]
  private GameObject ParentObj; // 表示する親オブジェクト 
  [SerializeField]
  private GameObject DamageObj; // prefab にある DamageText
  [SerializeField]
  private GameObject PosObj; // どこに表示するか

  public void ViewDamage(int _damage, bool isRandomPos = true) {
    GameObject _damageObj = Instantiate(DamageObj, ParentObj.transform);
    _damageObj.GetComponent<TextMeshProUGUI>().text = _damage.ToString();

    float rnd_x;
    if(isRandomPos) {
      rnd_x = PosObj.transform.localPosition.x + (float)(CommonUtil.rnd(60) -30);
    } else {
      rnd_x = PosObj.transform.localPosition.x;
    }
    Vector3 new_pos = new Vector3(rnd_x, PosObj.transform.localPosition.y, PosObj.transform.localPosition.z);
    _damageObj.transform.localPosition = new_pos;
  }

  public void ViewString(string text, bool isRandomPos = true) {
    GameObject _damageObj = Instantiate(DamageObj, ParentObj.transform);
    _damageObj.GetComponent<TextMeshProUGUI>().text = text;
    float rnd_x;
    if(isRandomPos) {
      rnd_x = PosObj.transform.localPosition.x + (float)(CommonUtil.rnd(60) -30);
    } else {
      rnd_x = PosObj.transform.localPosition.x;
    }
    Vector3 new_pos = new Vector3(rnd_x, PosObj.transform.localPosition.y, PosObj.transform.localPosition.z);

    if(text == "ミス!") {
      _damageObj.GetComponent<TextMeshProUGUI>().color = Color.blue;
    }
    _damageObj.transform.localPosition = new_pos;
  }
}