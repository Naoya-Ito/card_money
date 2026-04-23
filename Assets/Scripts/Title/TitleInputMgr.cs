using UnityEngine;
using UnityEngine.InputSystem;

public class TitleInputMgr : MonoBehaviour {
  public void OnOkButton(InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Performed) {
//      Debug.Log(context.control);
//      Debug.Log("Input System Keyboard Sample ok");
      TitleMgr.instance.pushedEnterButton();
    }
  }

  public void OnMove(InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Performed) {
 //     Debug.Log(context.control);
 //     Debug.Log("Input System Keyboard Sample move");
      Vector2 input = context.ReadValue<Vector2>();
      if (TitleMgr.instance != null) {
        TitleMgr.instance.HandleDirectionalInput(input);
      }
    }
  }

  public void OnEscapeButton(InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Performed) {
//      Debug.Log(context.control);
//      Debug.Log("Input System Keyboard Sample ok");
      GearButton.instance.pressButton();
    }
  }
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

    if(GearButton.instance.isPressed) return;

    if (current.escapeKey.wasPressedThisFrame){
      GearButton.instance.pressButton();
    } else if (current.upArrowKey.wasPressedThisFrame){
//      TitleMgr.instance.upCursor();
    } else if (current.downArrowKey.wasPressedThisFrame) {
//      TitleMgr.instance.downCursor();
//    } else if (current.leftArrowKey.wasPressedThisFrame) {
//      TitleMgr.instance.pushedPVButton();
//    } else if (current.rightArrowKey.wasPressedThisFrame) {
//      TitleMgr.instance.pushedBGMButton();
    } else if (current.enterKey.wasPressedThisFrame){
      TitleMgr.instance.pushedEnterButton();
//    } else if (current.qKey.wasPressedThisFrame){
//      TitleMgr.instance.pushedEndButton();
    }
  }

  // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.Gamepad.html
  //  n
  // w e
  //  s
  private void pressedGamepad(){
    var current = Gamepad.current;
    if (current == null) return;

    if(GearButton.instance.isPressed) return;

    if (current.selectButton.wasPressedThisFrame) {
      PlayerPrefs.DeleteAll();
      CommonUtil.changeScene("TitleScene");
    } else if (current.startButton.wasPressedThisFrame) {
      TitleMgr.instance.pushedEnterButton();
    } else if (current.buttonNorth.wasPressedThisFrame){
      TitleMgr.instance.upCursor();
    } else if (current.buttonSouth.wasPressedThisFrame) {
      TitleMgr.instance.downCursor();
    } else if (current.buttonEast.wasPressedThisFrame) {
      TitleMgr.instance.pushedEnterButton();
    } else if (current.buttonWest.wasPressedThisFrame) {
      TitleMgr.instance.pushedEnterButton();
    }
  }
*/
}
