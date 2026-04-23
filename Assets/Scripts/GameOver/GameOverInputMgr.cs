using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverInputMgr : MonoBehaviour {
  public void OnOkButton(InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Performed) {
      GameOverMgr.instance.pushedEnterButton();
    }
  }

  public void OnMove(InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Performed) {
      Vector2 input = context.ReadValue<Vector2>();
      if(input.y > 0) {
        GameOverMgr.instance.upCursor();
      } else if (input.y < 0) {
        GameOverMgr.instance.downCursor();
      }
    }
  }

  public void OnEscapeButton(InputAction.CallbackContext context) {
    if (context.phase == InputActionPhase.Performed) {
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
//      GameOverMgr.instance.PressContinue();
    } else if (current.downArrowKey.wasPressedThisFrame) {
//      giveUp();
    } else if (current.leftArrowKey.wasPressedThisFrame) {
    } else if (current.rightArrowKey.wasPressedThisFrame) {
    } else {
    }
  }

  // キー名は https://docs.unity3d.com/ja/Packages/com.unity.inputsystem@1.4/manual/Gamepad.html
  private void pressedGamepad(){
    var current = Gamepad.current;
    if (current == null) return;

    if(GearButton.instance.isPressed) return;

    if (current.selectButton.wasPressedThisFrame) {
      GearButton.instance.pressButton();
    } else if (current.buttonNorth.wasPressedThisFrame){
//      GameOverMgr.instance.PressContinue();
    } else if (current.buttonSouth.wasPressedThisFrame) {
//      giveUp();
    } else if (current.buttonWest.wasPressedThisFrame) {
    } else if (current.buttonEast.wasPressedThisFrame) {
    }
  }
*/
}
