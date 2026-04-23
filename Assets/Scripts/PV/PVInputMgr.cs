using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PVInputMgr : MonoBehaviour {
  /*
  void Update() {
    pressedKeyboard();
    pressedGamepad();
  }

  // キーボードを押した時の処理
  // キー名は https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.Keyboard.html
  private void pressedKeyboard(){
    var current = Keyboard.current;
    if (current == null) return;

    if (current.upArrowKey.wasPressedThisFrame){
      PVSceneMgr.instance.goBackTitle();
    }
  }

  // キー名は https://docs.unity3d.com/ja/Packages/com.unity.inputsystem@1.4/manual/Gamepad.html
  private void pressedGamepad(){
    var current = Gamepad.current;
    if (current == null) return;


    if (current.startButton.wasPressedThisFrame) {
      PVSceneMgr.instance.goBackTitle();
    } else if (current.buttonNorth.wasPressedThisFrame){
      PVSceneMgr.instance.goBackTitle();
    }
  }
  */
}
