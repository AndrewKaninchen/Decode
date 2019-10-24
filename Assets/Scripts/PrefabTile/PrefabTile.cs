 // Creator: TextusGames

 using UnityEditor;
 using UnityEngine;
 using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "TiledPrefab" ,fileName = "TiledPrefab", order = 360)]
public class PrefabTile : TileBase
{
    public GameObject prefab;
    public string tilemapName;
//    public Vector3 PrefabOffset = new Vector3(0.5f,0.5f,0);

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
        var prefabTilemap = tilemap.GetComponent<PrefabTilemap>();
        if (prefabTilemap != null) prefabTilemap.RefreshTile(position);
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        return false;
    }
  
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        tileData.flags = TileFlags.None;
        var previewTexture = AssetPreview.GetAssetPreview(prefab);
        
        if (tileData.sprite == null && previewTexture != null)
        {
            tileData.sprite = Sprite.Create(
                previewTexture,
                new Rect(
                    Vector2.zero,
                    previewTexture.height * Vector2.one),
                Vector2.one * .5f,
                previewTexture.height * 6.5f);
        }
    }
}

