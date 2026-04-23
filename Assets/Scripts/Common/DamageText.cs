using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// prefab の  TMP にアタッチする用
// ダメージを表示するのは DamageDisplayで行う
public class DamageText : MonoBehaviour {
  [SerializeField]
  private float DeleteTime = 2.5f;
  [SerializeField]
  private float MoveRange = 50.0f;
  [SerializeField]
  private float EndAlpha = 0.2f;

  private float TimeCnt;
  private TextMeshProUGUI NowText;

  void Start() {
    TimeCnt = 0.0f;
    Destroy(this.gameObject, DeleteTime);
    NowText = this.gameObject.GetComponent<TextMeshProUGUI>();
  }

  void Update() {
    TimeCnt += Time.deltaTime;
    this.gameObject.transform.localPosition += new Vector3(0,MoveRange / DeleteTime * Time.deltaTime,0);
    float _alpha = 1.0f - (1.0f - EndAlpha) * (TimeCnt / DeleteTime);
    if (_alpha <= 0.0f) _alpha = 0.0f;
    NowText.color = new Color(NowText.color.r, NowText.color.g, NowText.color.b, _alpha);
  }
}