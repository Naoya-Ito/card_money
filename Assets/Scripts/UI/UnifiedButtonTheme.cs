using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class UnifiedButtonTheme {
  private const string NormalSpritePath = "Textures/other/button_blue";
  private const string DisabledSpritePath = "Textures/other/button_gray";
  private const string FontPath = "Fonts/AozoraMinchoRegular SDF";
  private static readonly Color HighlightedColor = new Color(1f, 0.95f, 0.45f, 0.8f);
  public static Color HighlightColor => HighlightedColor;

  private static bool loaded;
  private static Sprite normalSprite;
  private static Sprite disabledSprite;
  private static TMP_FontAsset font;

  public static void ApplyTo(Button button) {
    if (button == null) return;
    Image image = button.GetComponent<Image>();
    if (image == null) return;
    TextMeshProUGUI tmpText = button.GetComponentInChildren<TextMeshProUGUI>(true);
    Text legacyText = button.GetComponentInChildren<Text>(true);
    bool hasTmpText = tmpText != null && !string.IsNullOrEmpty(tmpText.text);
    bool hasLegacyText = legacyText != null && !string.IsNullOrEmpty(legacyText.text);
    bool hasText = hasTmpText || hasLegacyText;
    bool hasButtonSprite = image.sprite != null && image.sprite.name.StartsWith("button_");
    if (!hasText && !hasButtonSprite) return;
    EnsureLoaded();
    if (normalSprite != null) {
      image.sprite = normalSprite;
      image.type = Image.Type.Sliced;
    }
    if (disabledSprite != null) {
      SpriteState state = button.spriteState;
      state.disabledSprite = disabledSprite;
      button.spriteState = state;
    }
    if (tmpText != null) {
      if (font != null) {
        tmpText.font = font;
      }
      tmpText.color = Color.black;
    }
    if (legacyText != null) {
      legacyText.color = Color.black;
    }
    ApplyHoverColor(button);
  }

  private static void EnsureLoaded() {
    if (loaded) return;
    loaded = true;
    normalSprite = Resources.Load<Sprite>(NormalSpritePath);
    disabledSprite = Resources.Load<Sprite>(DisabledSpritePath);
    font = Resources.Load<TMP_FontAsset>(FontPath);
  }

  private static void ApplyHoverColor(Button button) {
    ColorBlock colors = button.colors;
    colors.highlightedColor = HighlightedColor;
    colors.pressedColor = HighlightedColor;
    colors.selectedColor = HighlightedColor;
    button.colors = colors;
    button.transition = Selectable.Transition.ColorTint;
  }
}
