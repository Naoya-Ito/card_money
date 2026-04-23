using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GearInputMgr : MonoBehaviour {

  void Update() {
    pressedKeyboard();
    pressedGamepad();
  }

  // キーボードを押した時の処理
  // キー名は https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.Keyboard.html
  private void pressedKeyboard(){
    var current = Keyboard.current;
    if (current == null) return;

    if(GearButton.instance.isPressed) {
      if (Setting.instance == null) return;
      if (current.escapeKey.wasPressedThisFrame){
        Setting.instance.cancel();
      } else if (current.upArrowKey.wasPressedThisFrame){
        Setting.instance.upCursor();
      } else if (current.downArrowKey.wasPressedThisFrame) {
        Setting.instance.downCursor();
      } else if (current.leftArrowKey.wasPressedThisFrame) {
      } else if (current.rightArrowKey.wasPressedThisFrame) {
      } else if (current.escapeKey.wasPressedThisFrame){
      } else if (current.enterKey.wasPressedThisFrame){
        Setting.instance.pushedEnterButton();
      } else if (current.qKey.wasPressedThisFrame){
      }
    }
  }

  // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.Gamepad.html
  private void pressedGamepad(){
    var current = Gamepad.current;
    if (current == null) return;

    if(GearButton.instance.isPressed) {
      if (Setting.instance == null) return;
      if (current.selectButton.wasPressedThisFrame) {
        Setting.instance.cancel();
      } else if (current.startButton.wasPressedThisFrame) {
      } else if (current.selectButton.wasPressedThisFrame) {
      } else if (current.buttonNorth.wasPressedThisFrame){
        Setting.instance.upCursor();
      } else if (current.buttonSouth.wasPressedThisFrame) {
        Setting.instance.downCursor();
      } else if (current.buttonWest.wasPressedThisFrame) {
      } else if (current.buttonEast.wasPressedThisFrame) {
      } else if (current.aButton.wasPressedThisFrame) {
        Setting.instance.cancel();
      } else if (current.bButton.wasPressedThisFrame) {
        Setting.instance.pushedEnterButton();
      }
    } else {
    }
  }
}
