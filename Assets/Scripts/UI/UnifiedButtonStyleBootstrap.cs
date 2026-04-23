using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnifiedButtonStyleBootstrap : MonoBehaviour {
  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
  private static void Boot() {
    if (Object.FindAnyObjectByType<UnifiedButtonStyleBootstrap>() != null) return;
    GameObject obj = new GameObject(nameof(UnifiedButtonStyleBootstrap));
    DontDestroyOnLoad(obj);
    obj.AddComponent<UnifiedButtonStyleBootstrap>();
  }

  private void OnEnable() {
    SceneManager.sceneLoaded += OnSceneLoaded;
    ApplyToAllButtons();
  }

  private void OnDisable() {
    SceneManager.sceneLoaded -= OnSceneLoaded;
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
    ApplyToAllButtons();
  }

  private void ApplyToAllButtons() {
    Button[] buttons = Object.FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    foreach (Button button in buttons) {
      UnifiedButtonTheme.ApplyTo(button);
    }
  }
}
