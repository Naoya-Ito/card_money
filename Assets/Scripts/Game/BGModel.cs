using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BGModel : MonoBehaviour {

  [SerializeField] public Image now_bg;
  [SerializeField] public Image next_bg;

  private Sequence moveSequence;

  [System.NonSerialized] public static BGModel instance = null;

  private void Awake(){
    if(instance == null) {
      instance = this;
    } else {
      Destroy(this.gameObject);
    }
  }

  private void Start() {
  }

  // loop = -1 なら無限ループ
  public void moveBG(float interval, int loop = 0){
    moveSequence = DOTween.Sequence()
      .Append(now_bg.transform.DOLocalMoveX(-RingConst.SCREEN_WIDTH, interval).SetRelative().SetEase(Ease.Linear))
      .Join(next_bg.transform.DOLocalMoveX(-RingConst.SCREEN_WIDTH, interval).SetRelative().SetEase(Ease.Linear))
      .Append(now_bg.transform.DOLocalMoveX(RingConst.SCREEN_WIDTH, 0).SetRelative().SetEase(Ease.Linear))
      .Join(next_bg.transform.DOLocalMoveX(-RingConst.SCREEN_WIDTH, 0).SetRelative().SetEase(Ease.Linear))
//      .SetLoops(loop, LoopType.Restart)
      .Pause()
      .SetAutoKill(false)
      .SetLink(now_bg.gameObject);
    moveSequence.Restart();
  }

  public void crossBG(){
  }
}
