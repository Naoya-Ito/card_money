using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
  [SerializeField] Image tooltipWindow;
  public float pos_x;
  public float pos_y;
  public string title;
  [Multiline] public string body;

  public bool isPause = false;
  public bool isShow = false;

  public void show(){
    if(isPause || isShow) return;

    isShow = true;  
//    CommonUtil.changeText("tooltip_title", title);
//    CommonUtil.changeText("tooltip_body", body);

    Image panel = Instantiate(tooltipWindow, gameObject.transform);
    panel.name = "tooltip";

    panel.transform.localPosition = new Vector3(pos_x, pos_y, 0.0f);

/*
    if(panel.transform.Find("Image") != null){
      Debug.Log("Image is exits");
      if(panel.transform.Find("Image/tooltip_title") != null) {
        Debug.Log("Image/tooltip_title is exits");
      } else {
        Debug.Log("Image/tooltip_title is not exits");
      }
    } else {
      Debug.Log("Image is not exits");
    }
    */

    panel.transform.Find("Image/tooltip_title").GetComponent<TextMeshProUGUI>().text = title;
    panel.transform.Find("Image/tooltip_body").GetComponent<TextMeshProUGUI>().text = body;
  }

  public void hide(){
    if(!isShow) return;
    if(isPause) return;

    Destroy(GameObject.Find("tooltip"));
    isShow = false;
    isPause = true;
    Invoke("pauseOff", 0.1f);
  }

  public void pauseOff(){
    isPause = false;
  }

  void Start(){
        
  }

  // Update is called once per frame
  void Update(){
        
  }
}
