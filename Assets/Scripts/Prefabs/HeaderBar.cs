using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeaderBar : MonoBehaviour{
  [SerializeField] public Slider mp_bar;
  [SerializeField] public TextMeshProUGUI mp_text;
  [SerializeField] public TextMeshProUGUI mp_description;
  [SerializeField] public Image mp_box;
  [SerializeField] public TextMeshProUGUI gold_text;
  [SerializeField] public Image gold;

  [System.NonSerialized] public static HeaderBar instance = null;

  private void Awake(){
    if(instance == null) {
      instance = this;
    } else {
      Destroy(this.gameObject);
    }
  }

  public void updateCoin(){
    int gold = DataMgr.GetInt("gold");
    gold_text.text = $"{gold}";
  }
  public void updateHP(){
    int hp = DataMgr.GetInt("hp");
    int max_hp = DataMgr.GetInt("max_hp");
    mp_text.text = $"{hp}";
    mp_bar.value = (float)hp/(float)max_hp;
  }

  public void hideCoin(){
    gold_text.gameObject.SetActive(false);
    gold.gameObject.SetActive(false);
  }
  public void hideMP(){
//    mp_text.gameObject.SetActive(false);
    mp_description.gameObject.SetActive(false);
    mp_box.gameObject.SetActive(false);
    mp_text.gameObject.SetActive(false);
  }

  // TOP画面など、ヘッダー画面からステータス要素を非表示にする
  public void hideStatus(){
    hideCoin();
    hideMP();

  }
}
