using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageBinarizerByScript : MonoBehaviour
{
    public float fadeDuration = 180f; 
    private Image targetImage;
    private Color[] originalPixels;
    private Texture2D textureCopy;
    
    // 進捗度合いを保持する変数 (0.0f: 開始, 1.0f: 完了)
    private float currentProgress = 0f; 

    void Start()
    {
        if (targetImage == null) {
            targetImage = GetComponent<Image>();
        }
        if (targetImage != null && targetImage.sprite != null) {
            // 開始時点では進捗度0で初期化
            Init(targetImage.sprite, 0f); 
        }
    }

    void Update() { }

    /// <summary>
    /// 新しいスプライトを設定し、指定された進捗度から2値化処理を開始します。
    /// </summary>
    /// <param name="newSprite">設定する新しいスプライト</param>
    /// <param name="startProgress">開始時の進捗度 (0.0fから1.0f)</param>
    public void Init(Sprite newSprite, float startProgress)
    {
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }
        
        targetImage.sprite = newSprite;

        Texture2D originalTexture = newSprite.texture;

        if (!originalTexture.isReadable)
        {
            string spriteName = newSprite != null ? newSprite.name : "unknown_sprite";
            string textureName = originalTexture != null ? originalTexture.name : "unknown_texture";
            Debug.LogError($"画像の Read/Write Enabled が有効になっていません。インポート設定を確認してください。対象: {spriteName} / {textureName}");
            this.enabled = false;
            return;
        }

        originalPixels = originalTexture.GetPixels();
        textureCopy = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false);
        textureCopy.filterMode = originalTexture.filterMode;
        textureCopy.wrapMode = originalTexture.wrapMode;
        textureCopy.wrapModeU = originalTexture.wrapModeU;
        textureCopy.wrapModeV = originalTexture.wrapModeV;
        textureCopy.anisoLevel = originalTexture.anisoLevel;

        // Spriteを再作成してImageにセット
        Sprite createdSprite = Sprite.Create(textureCopy, new Rect(0, 0, textureCopy.width, textureCopy.height), new Vector2(0.5f, 0.5f));
        targetImage.sprite = createdSprite; 
        
        // 進捗度を新しい開始値に設定
        currentProgress = Mathf.Clamp01(startProgress);

        // 初期描画
        BinarizeImage(currentProgress);

//        Debug.Log("新しい画像を設定し、進捗 " + currentProgress * 100f + "% から開始します。");
//
//        Debug.Log("操作対象のテクスチャ名: " + textureCopy.name);
//        Debug.Log("表示中のテクスチャ名: " + targetImage.sprite.texture.name);

    }

    public void SetProgress(float progress)
    {
        if (textureCopy == null || originalPixels == null) return;
        currentProgress = Mathf.Clamp01(progress);
        BinarizeImage(currentProgress);
    }

    private void BinarizeImage(float progress)
    {
//        Debug.Log("progaress:" + progress + "%");

        // 色の定義
        Color black = Color.black;
        Color gray = new Color(0.5f, 0.5f, 0.5f, 1f); // 50%灰色
        Color white = Color.white;

        Color[] newPixels = new Color[originalPixels.Length];

        for (int i = 0; i < originalPixels.Length; i++)
        {
            Color originalColor = originalPixels[i];

            // ピクセルの明るさを計算
            float luminance = 0.299f * originalColor.r + 0.587f * originalColor.g + 0.114f * originalColor.b;

            // 最終的な3階調の色を決定
            Color finalTonedColor;
            if (luminance < 0.33f) // 明るさの1/3以下なら黒
            {
                finalTonedColor = black;
            }
            else if (luminance < 0.66f) // 明るさの2/3以下なら灰色
            {
                finalTonedColor = gray;
            }
            else // それ以上なら白
            {
                finalTonedColor = white;
            }

            finalTonedColor.a = originalColor.a; // 透明度は維持

            // 元の色(originalColor)から最終的な3階調の色(finalTonedColor)へ、
            Color blendedColor = Color.Lerp(originalColor, finalTonedColor, progress);

            //newPixels[i] = Color.red;
            newPixels[i] = blendedColor;
        }

        textureCopy.SetPixels(newPixels);
        textureCopy.Apply();

        
    }

   // 現在の進捗度を取得するためのGetter
    public float GetCurrentProgress()
    {
        return currentProgress;
    }
}
