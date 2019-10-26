
using System.Collections.Generic;
using System.Linq;
using Decode;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;


[CustomGridBrush(false, true, true, "Default Brush")]
public class DefaultBrush : GridBrush
{
    private Tilemap targetTilemap;
    private PrefabTilemap targetPrefabTilemap;
    public Decode.Direction tileDirection = Direction.East;

    
    public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pickStart)
    {
        base.Pick(gridLayout, brushTarget, position, pickStart);
        SelectRelevantPrefabTilemap();
    }
    
    private void SelectRelevantPrefabTilemap()
    {
        var prefabTile = cells[0].tile as PrefabTile;
        if (prefabTile == null) return;

        var tilemapName = prefabTile.tilemapName;
        var tilemaps = FindObjectsOfType<Tilemap>().ToList();
        targetTilemap = tilemaps.Find(tilemap => tilemap.name.Equals(tilemapName));
        if (targetTilemap == null) return;
        targetPrefabTilemap = targetTilemap.GetComponent<PrefabTilemap>();
        GridPaintingState.scenePaintTarget = targetTilemap.gameObject;
    }

    public override void Rotate(RotationDirection direction, GridLayout.CellLayout layout)
    {
        tileDirection = (Direction)(((int)tileDirection + ((direction == RotationDirection.Clockwise) ? 1 : 3)) % 4);
        Debug.Log("girando girando girando pro lado");
    }


    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget != null)
        {
            base.Paint(gridLayout, brushTarget, position);
            if (targetTilemap != null) targetTilemap.RefreshTile(position);
            
            if (targetPrefabTilemap == null) return;
            var pawn = targetPrefabTilemap.gameObjects[position].GetComponent<Pawn>();
            if (pawn != null) pawn.Direction = tileDirection;
        }
    }

    public override void Select(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
    {
        base.Select(gridLayout, brushTarget, position);
    }

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