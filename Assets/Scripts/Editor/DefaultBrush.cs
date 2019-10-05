
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
 
[CustomGridBrush(false, true, true, "Default Brush")]
public class DefaultBrush : GridBrush
{
    private Tilemap targetTilemap;

    public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pickStart)
    {
        base.Pick(gridLayout, brushTarget, position, pickStart);
        var prefabTile = cells[0].tile as PrefabTile;
        if (prefabTile == null) return;
            
        var tilemapName = prefabTile.tilemapName;
        var tilemaps = GameObject.FindObjectsOfType<Tilemap>().ToList();
        targetTilemap = tilemaps.Find(tilemap => tilemap.name.Equals(tilemapName));
        if (targetTilemap == null) return;

        GridPaintingState.scenePaintTarget = targetTilemap.gameObject;
    }

//    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
//    {
//        if (brushTarget != null)
//        {
//            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
//            base.Paint(gridLayout, brushTarget, position);
//        }
//    }
//
//    public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
//    {
//        if (brushTarget != null)
//        {
//            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
//            base.BoxFill(gridLayout, brushTarget, position);
//        }
//    }
//
//    public override void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
//    {
//        if (brushTarget != null)
//        {
//            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
//            base.FloodFill(gridLayout, brushTarget, position);
//        }
//    }
//
//    public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
//    {
//        if (brushTarget != null)
//        {
//            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
//            base.Erase(gridLayout, brushTarget, position);
//        }
//    }
//
//   
//    public override void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
//    {
//        if (brushTarget != null)
//        {
//            Undo.RegisterCompleteObjectUndo(this.targetTilemap, string.Empty);
//            base.BoxErase(gridLayout, brushTarget, position);
//        }
//    }
}