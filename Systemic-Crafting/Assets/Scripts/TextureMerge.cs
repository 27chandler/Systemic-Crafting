using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class TextureMerge : MonoBehaviour
{
    [SerializeField] private Sprite baseTexture;
    [SerializeField] private Sprite secondTexture;
    [Space]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Space]
    [SerializeField] private Tile targetTile;
    private Sprite originalSprite;
    private Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        originalSprite = targetTile.sprite;

        sprite = Sprite.Create(MergeTextures(baseTexture.texture, baseTexture.rect, secondTexture.texture, secondTexture.rect), new Rect(0, 0, 32, 32), new Vector2(0.5f,0.5f),32.0f);
        spriteRenderer.sprite = sprite;

        //targetTile.sprite = sprite;

        byte[] bytes = sprite.texture.EncodeToPNG();
        // For testing purposes, also write to a file in the project folder
        //File.WriteAllBytes(Application.dataPath + "/../SavedTexture.png", bytes);
    }

    private void OnDestroy()
    {
        targetTile.sprite = originalSprite;
    }

    static public Sprite MergeSprites(Sprite primary, Sprite secondary)
    {
        return Sprite.Create(TextureMerge.MergeTextures(primary.texture, primary.rect, secondary.texture, secondary.rect, 0.5f), new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 32.0f);
    }

    // Credit to http://girlscancode.eu/unity3d-merge-textures-tutorial/ for this...
    // (Slightly modified by me to support textures within a spritesheet)
    static public Texture2D MergeTextures(Texture2D texture_1, Rect rect_1, Texture2D texture_2, Rect rect_2, float alpha = 1.0f)
    {
        Texture2D merge_result = new Texture2D((int)rect_1.width, (int)rect_1.height);
        merge_result.filterMode = FilterMode.Point;

        for (int i = 0; i < merge_result.width; i++)
        {
            for (int j = 0; j < merge_result.height; j++)
            {
                Color tex1_color = texture_1.GetPixel((int)rect_1.x + i, (int)rect_1.y + j);
                Color tex2_color = texture_2.GetPixel((int)rect_2.x + i, (int)rect_2.y + j);
                Color merged_color = Color.Lerp(tex1_color, tex2_color, (tex2_color.a * alpha) / 1);

                merge_result.SetPixel(i, j, merged_color);

            }
        }
        merge_result.Apply();
        //merge_result.Apply();
        return merge_result;

    }
}
