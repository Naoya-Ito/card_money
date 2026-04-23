using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class GameViewResolutionSetter {
  private const int TargetWidth = 960;
  private const int TargetHeight = 540;
  private const string TargetLabel = "960x540";

  [MenuItem("Tools/GameView/Set 960x540 1x")]
  private static void SetGameView960x540() {
    SetGameViewSize(TargetWidth, TargetHeight, TargetLabel);
    SetGameViewScale(1f);
  }

  private static void SetGameViewSize(int width, int height, string label) {
    Type sizesType = GetUnityEditorType("UnityEditor.GameViewSizes");
    object sizesInstance = sizesType.GetProperty("instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
    if (sizesInstance == null) return;

    Type groupType = GetUnityEditorType("UnityEditor.GameViewSizeGroupType");
    MethodInfo getGroup = sizesType.GetMethod("GetGroup");
    object group = getGroup?.Invoke(sizesInstance, new object[] { (int)Enum.Parse(groupType, "Standalone") });
    if (group == null) return;

    int index = FindSizeIndex(group, width, height);
    if (index < 0) {
      AddCustomSize(group, width, height, label);
      index = FindSizeIndex(group, width, height);
    }
    if (index < 0) return;

    Type gameViewType = GetUnityEditorType("UnityEditor.GameView");
    EditorWindow gameView = EditorWindow.GetWindow(gameViewType);
    PropertyInfo selectedSizeIndex = gameViewType.GetProperty("selectedSizeIndex", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    selectedSizeIndex?.SetValue(gameView, index);
  }

  private static int FindSizeIndex(object group, int width, int height) {
    if (group == null) return -1;
    Type groupType = group.GetType();
    MethodInfo getTotalCount = groupType.GetMethod("GetTotalCount");
    MethodInfo getGameViewSize = groupType.GetMethod("GetGameViewSize");
    if (getTotalCount == null || getGameViewSize == null) return -1;

    int total = (int)getTotalCount.Invoke(group, null);
    for (int i = 0; i < total; i++) {
      object size = getGameViewSize.Invoke(group, new object[] { i });
      if (size == null) continue;
      int w = (int)size.GetType().GetProperty("width")?.GetValue(size, null);
      int h = (int)size.GetType().GetProperty("height")?.GetValue(size, null);
      if (w == width && h == height) return i;
    }
    return -1;
  }

  private static void AddCustomSize(object group, int width, int height, string label) {
    Type sizeType = GetUnityEditorType("UnityEditor.GameViewSize");
    Type sizeTypeEnum = GetUnityEditorType("UnityEditor.GameViewSizeType");
    if (sizeType == null || sizeTypeEnum == null) return;
    object fixedResolution = Enum.Parse(sizeTypeEnum, "FixedResolution");
    ConstructorInfo ctor = sizeType.GetConstructor(new[] { sizeTypeEnum, typeof(int), typeof(int), typeof(string) });
    object newSize = ctor?.Invoke(new object[] { fixedResolution, width, height, label });
    if (newSize == null) return;

    MethodInfo addCustomSize = group.GetType().GetMethod("AddCustomSize");
    addCustomSize?.Invoke(group, new object[] { newSize });
  }

  private static void SetGameViewScale(float scale) {
    Type gameViewType = GetUnityEditorType("UnityEditor.GameView");
    EditorWindow gameView = EditorWindow.GetWindow(gameViewType);
    if (gameView == null) return;

    FieldInfo zoomAreaField = gameViewType.GetField("m_ZoomArea", BindingFlags.Instance | BindingFlags.NonPublic);
    if (zoomAreaField == null) return;
    object zoomArea = zoomAreaField.GetValue(gameView);
    if (zoomArea == null) return;

    FieldInfo scaleField = zoomArea.GetType().GetField("m_Scale", BindingFlags.Instance | BindingFlags.NonPublic);
    if (scaleField != null) {
      scaleField.SetValue(zoomArea, new Vector2(scale, scale));
    }
    FieldInfo translationField = zoomArea.GetType().GetField("m_Translation", BindingFlags.Instance | BindingFlags.NonPublic);
    if (translationField != null) {
      translationField.SetValue(zoomArea, Vector2.zero);
    }
  }

  private static Type GetUnityEditorType(string name) {
    return typeof(Editor).Assembly.GetType(name);
  }
}
