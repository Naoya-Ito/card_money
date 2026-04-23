using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverMgr : MonoBehaviour {

  [SerializeField] public TextMeshProUGUI main_text;
  [SerializeField] public Button result_button;
  [SerializeField] public Image right_image;
  [SerializeField] public TextMeshProUGUI hint_text;

  private const string AUTO_SAVE_PAGE_KEY = "autosave_page";
  private static readonly string[] HINT_TEXTS = new string[] {
    "すばやさの確率で攻撃を避ける事ができるぞ。\nスピードは大事！",
    "戦闘ではすばやさが高いほど早く行動するぞ。\nスピードは大事！"
  };
  [System.NonSerialized] public string dead_reason = "";
  [System.NonSerialized] public static GameOverMgr instance = null;

  private void Awake(){
    if(instance == null) {
      instance = this;
    } else {
      Destroy(this.gameObject);
    }
  }

  void Start() {
    BGMMgr.instance.changeBGM("game_over");
    DataMgr.SetStr(AUTO_SAVE_PAGE_KEY, "");
    DataMgr.SetStr("page", "");
    dead_reason = DataMgr.GetStr("dead_reason");
    DataMgr.SetStr("dead_reason", "");

    updateScene();
    cursor_pos = CURSOR_POS_END;
    pre_cursor_pos = 99;
    cursorButton(cursor_pos);
  }

  private void updateScene(){
    if (right_image != null) {
      right_image.gameObject.SetActive(false);
    }
    switch(dead_reason) {
      case EndingModel.BADEND_TIME_OVER:
        main_text.text = "勇者カッパは間に合わなかった。\n3分という時間はあまりにも短すぎたのだ！\n\nああ、こんな事ならば毎日ポテチを食べてグータラしないで、もっと修行を積んでおくべきだった。";
        break;
      case EndingModel.BADEND_SLIME:
        main_text.text = "まさかスライムに負けてしまう勇者がこの世に存在するとは。\nカッパを倒したスライムは大ブレイクしモテモテとなった。";
        break;
      case EndingModel.BADEND_DOG:
        main_text.text = "勇者カッパはイヌの三連星に負けて、カッパから負け犬へとクラスチェンジした。\nこれからは語尾にワンを付けて可愛いキャラで売っていこうとカッパは誓うのであった。";
        break;
      case EndingModel.BADEND_MAOU_FAIR:
        main_text.text = "勇者カッパは爆速で魔王城にたどり着いたが、残念ながら返り討ちにあった。\nもっと寄り道をして鍛えてから魔王城に行くべきだった。";
        break;
      case EndingModel.BADEND_ARROW:
        main_text.text = "カッパは矢に当たって死んだ。";
        break;
      default:
        main_text.text = "カッパは頑張ったが、なんやかんやうまくいかなかった。\nこの体験を元に『カッパは辛いよ』という映画を作ったが全くヒットせずに多額の借金を抱える事となった。";
        Debug.LogError($"unknown dead reason. key={dead_reason}");
        break;
    }
    updateHintText();
  }

  private void updateHintText() {
    if (hint_text == null) return;
    if (HINT_TEXTS.Length == 0) {
      hint_text.text = "";
      return;
    }
    int index = Random.Range(0, HINT_TEXTS.Length);
    hint_text.text = HINT_TEXTS[index];
  }

  private bool isButtonPushed = false;
  public void pushedResultButton(){
    if(isButtonPushed) {
      return;
    }
    isButtonPushed = true;

    CommonUtil.changePushedButtonImage(result_button);
    CommonUtil.changeScene("TitleScene");
  }

  [System.NonSerialized] public int cursor_pos = 99;
  [System.NonSerialized] public int pre_cursor_pos = 99;
  const int CURSOR_POS_END = 1;

    public void upCursor(){
    cursor_pos += 1;
    if(cursor_pos >= 90) cursor_pos = CURSOR_POS_END;
    if(cursor_pos >= CURSOR_POS_END) cursor_pos = CURSOR_POS_END;
    if(pre_cursor_pos == cursor_pos) return;

    cursorButton(cursor_pos);
  }

  public void downCursor(){
    cursor_pos -= 1;
    if(cursor_pos >= 90) cursor_pos = CURSOR_POS_END;

    if(cursor_pos <= CURSOR_POS_END) cursor_pos = CURSOR_POS_END;
    if(pre_cursor_pos == cursor_pos) return;

    cursorButton(cursor_pos);
  }

  public void cursorButton(int i){
    CommonUtil.changeCursorButtonImage(result_button);

  }

  public void unCursorButton(int i){
    CommonUtil.changeUnCursorButtonImage(result_button);
  }

  public void pushedEnterButton(){
    switch(cursor_pos) {
      case CURSOR_POS_END:
        pushedResultButton();
        break;
      default:
        Debug.Log($"unknown pushed enter button. cursor_pos={cursor_pos}");
        return;
    }

    unCursorButton(cursor_pos);
    cursor_pos = 99;
  }
}
