using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

public static class Texture240135PresetConfig {
  public const string TargetFolder = "Assets/Resources/Textures/240_135";
  public const string PresetPath = "Assets/Editor/Presets/Texture240135Point.preset";

  public static bool IsTarget(string assetPath) {
    return assetPath.StartsWith(TargetFolder + "/");
  }

  public static void ApplySettings(TextureImporter importer) {
    if (importer == null) return;

    importer.textureType = TextureImporterType.Sprite;
    importer.spriteImportMode = SpriteImportMode.Single;
    importer.mipmapEnabled = false;
    importer.alphaIsTransparency = true;
    importer.npotScale = TextureImporterNPOTScale.None;
    importer.filterMode = FilterMode.Point;
    importer.textureCompression = TextureImporterCompression.Uncompressed;
    importer.compressionQuality = 0;
    importer.isReadable = true;
  }

  public static bool NeedsSettings(TextureImporter importer) {
    if (importer == null) return false;

    return importer.textureType != TextureImporterType.Sprite
      || importer.spriteImportMode != SpriteImportMode.Single
      || importer.mipmapEnabled
      || !importer.alphaIsTransparency
      || importer.npotScale != TextureImporterNPOTScale.None
      || importer.filterMode != FilterMode.Point
      || importer.textureCompression != TextureImporterCompression.Uncompressed
      || importer.compressionQuality != 0
      || !importer.isReadable;
  }

  public static Preset EnsurePreset(TextureImporter importer) {
    Preset preset = AssetDatabase.LoadAssetAtPath<Preset>(PresetPath);
    if (preset != null || importer == null) return preset;

    string directory = Path.GetDirectoryName(PresetPath);
    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) {
      Directory.CreateDirectory(directory);
      AssetDatabase.Refresh();
    }

    Preset created = new Preset(importer);
    AssetDatabase.CreateAsset(created, PresetPath);
    AssetDatabase.SaveAssets();
    return created;
  }
}

public class Texture240135PresetApplier : AssetPostprocessor {
  private void OnPreprocessTexture() {
    if (!Texture240135PresetConfig.IsTarget(assetPath)) return;
    TextureImporter importer = (TextureImporter)assetImporter;
    Texture240135PresetConfig.ApplySettings(importer);
    Preset preset = Texture240135PresetConfig.EnsurePreset(importer);
    if (preset != null) {
      preset.ApplyTo(importer);
    }
  }
}

public static class Texture240135PresetInitializer {
  [InitializeOnLoadMethod]
  private static void ReimportExistingTexturesIfNeeded() {
    EditorApplication.delayCall += ApplyPresetToExistingTextures;
  }

  [MenuItem("Tools/Textures/Reimport 240x135 Textures")]
  private static void ApplyPresetToExistingTextures() {
    string[] textureGuids = AssetDatabase.FindAssets("t:Texture2D", new[] { Texture240135PresetConfig.TargetFolder });
    foreach (string textureGuid in textureGuids.OrderBy(guid => guid)) {
      string path = AssetDatabase.GUIDToAssetPath(textureGuid);
      TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
      if (!Texture240135PresetConfig.NeedsSettings(importer)) continue;
      AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }
  }
}
