using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasAabbGuard : MonoBehaviour {
  [SerializeField] private int maxChecks = 3;
  [SerializeField] private bool autoRepair = true;
  private int checksRemaining;
  private Canvas ownerCanvas;
  private static readonly HashSet<int> reported = new HashSet<int>();
  private const float ExtremeThreshold = 100000f;

  private void OnEnable() {
    ownerCanvas = GetComponent<Canvas>();
    checksRemaining = Mathf.Max(1, maxChecks);
    Canvas.willRenderCanvases += OnWillRenderCanvases;
  }

  private void OnDisable() {
    Canvas.willRenderCanvases -= OnWillRenderCanvases;
  }

  private void OnWillRenderCanvases() {
    if (checksRemaining <= 0) return;
    checksRemaining--;
    ValidateAllRects();
  }

  private void Start() {
    ValidateAllRects();
    StartCoroutine(ValidateNextFrame());
  }

  private System.Collections.IEnumerator ValidateNextFrame() {
    yield return new WaitForEndOfFrame();
    ValidateAllRects();
  }

  private void ValidateAllRects() {
    if (ownerCanvas == null) return;
    RectTransform[] rects = ownerCanvas.GetComponentsInChildren<RectTransform>(true);
    foreach (RectTransform rect in rects) {
      if (rect == null) continue;
      if (!IsInvalid(rect)) continue;
      int id = rect.GetInstanceID();
      if (!reported.Contains(id)) {
        reported.Add(id);
        Debug.LogError($"Invalid AABB detected. name={rect.name} pos={rect.anchoredPosition} size={rect.sizeDelta} scale={rect.localScale}");
      }
      if (autoRepair) {
        RepairRect(rect);
      }
    }
    ValidateGraphics();
  }

  private void ValidateGraphics() {
    if (ownerCanvas == null) return;
    Graphic[] graphics = ownerCanvas.GetComponentsInChildren<Graphic>(true);
    foreach (Graphic graphic in graphics) {
      if (graphic == null) continue;
      Rect rect = graphic.rectTransform.rect;
      Rect pixelRect = graphic.GetPixelAdjustedRect();
      if (IsRectInvalid(rect) || IsRectInvalid(pixelRect)) {
        int id = graphic.GetInstanceID();
        if (!reported.Contains(id)) {
          reported.Add(id);
          Debug.LogError($"Invalid Graphic AABB detected. name={graphic.name} rect={rect} pixelRect={pixelRect}");
        }
        if (autoRepair) {
          RepairRect(graphic.rectTransform);
        }
      }
    }
  }

  private bool IsInvalid(RectTransform rect) {
    Vector2 pos = rect.anchoredPosition;
    Vector2 size = rect.sizeDelta;
    Vector3 scale = rect.localScale;
    if (!IsFinite(pos.x) || !IsFinite(pos.y)
      || !IsFinite(size.x) || !IsFinite(size.y)
      || !IsFinite(scale.x) || !IsFinite(scale.y) || !IsFinite(scale.z)) {
      return true;
    }
    if (IsExtreme(pos.x) || IsExtreme(pos.y) || IsExtreme(size.x) || IsExtreme(size.y)
      || IsExtreme(scale.x) || IsExtreme(scale.y) || IsExtreme(scale.z)) {
      return true;
    }
    // Avoid degenerate dimensions that can produce unstable UI bounds at render time.
    Rect localRect = rect.rect;
    if (!IsFinite(localRect.width) || !IsFinite(localRect.height)) return true;
    if (localRect.width <= 0f || localRect.height <= 0f) return true;
    Vector3[] corners = new Vector3[4];
    try {
      rect.GetWorldCorners(corners);
    } catch {
      return true;
    }
    foreach (Vector3 corner in corners) {
      if (!IsFinite(corner.x) || !IsFinite(corner.y) || !IsFinite(corner.z)) return true;
      if (IsExtreme(corner.x) || IsExtreme(corner.y) || IsExtreme(corner.z)) return true;
    }
    return false;
  }

  private void RepairRect(RectTransform rect) {
    Vector2 pos = rect.anchoredPosition;
    if (!IsFinite(pos.x)) pos.x = 0f;
    if (!IsFinite(pos.y)) pos.y = 0f;
    rect.anchoredPosition = pos;

    Vector2 size = rect.sizeDelta;
    if (!IsFinite(size.x)) size.x = 0f;
    if (!IsFinite(size.y)) size.y = 0f;
    if (size.x <= 0f) size.x = 4f;
    if (size.y <= 0f) size.y = 4f;
    rect.sizeDelta = size;

    Vector3 scale = rect.localScale;
    if (!IsFinite(scale.x)) scale.x = 1f;
    if (!IsFinite(scale.y)) scale.y = 1f;
    if (!IsFinite(scale.z)) scale.z = 1f;
    rect.localScale = scale;
  }

  private bool IsFinite(float value) {
    return !float.IsNaN(value) && !float.IsInfinity(value);
  }

  private bool IsExtreme(float value) {
    return Mathf.Abs(value) > ExtremeThreshold;
  }

  private bool IsRectInvalid(Rect rect) {
    return !IsFinite(rect.x) || !IsFinite(rect.y) || !IsFinite(rect.width) || !IsFinite(rect.height)
      || IsExtreme(rect.x) || IsExtreme(rect.y) || IsExtreme(rect.width) || IsExtreme(rect.height);
  }
}

public static class CanvasAabbGuardBootstrap {
  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
  private static void Boot() {
    SceneManager.sceneLoaded += OnSceneLoaded;
    TryAttach(SceneManager.GetActiveScene());
  }

  private static void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
    TryAttach(scene);
  }

  private static void TryAttach(Scene scene) {
    if (scene.name != "SettingScene") return;
    Canvas[] canvases = Object.FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    foreach (Canvas canvas in canvases) {
      if (canvas == null) continue;
      if (canvas.gameObject.scene != scene) continue;
      if (canvas.GetComponent<CanvasAabbGuard>() != null) continue;
      canvas.gameObject.AddComponent<CanvasAabbGuard>();
    }
  }
}
