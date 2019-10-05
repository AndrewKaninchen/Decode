 // Creator: TextusGames

 using UnityEditor;
 using UnityEngine;
 using UnityEngine.Serialization;
 using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "TiledPrefab" ,fileName = "TiledPrefab", order = 360)]
public class PrefabTile : TileBase
{
    [FormerlySerializedAs("Prefab")]public GameObject prefab;
    [FormerlySerializedAs("TilemapName")] public string tilemapName;
//    public Vector3 PrefabOffset = new Vector3(0.5f,0.5f,0);

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);

        var prefabTilemap = tilemap.GetComponent<PrefabTilemap>();
        if (prefabTilemap != null) prefabTilemap.RefreshTile(position, tilemap);
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        return false;
    }
  
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        var previewTexture = AssetPreview.GetAssetPreview(prefab);
        
        if (tileData.sprite == null && previewTexture != null)
        {
            tileData.sprite = Sprite.Create(
                previewTexture,
                new Rect(
                    Vector2.zero,
                    .95f * previewTexture.height * Vector2.one),
                Vector2.one * .5f,
                previewTexture.width);
        }
    }
}

