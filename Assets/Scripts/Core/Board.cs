using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Decode
{
    public class Board : MonoBehaviour
    {
        public Grid grid;
        public PrefabTilemap groundTilemap;
        
        public Dictionary<Vector3Int, Tile> Tiles = new Dictionary<Vector3Int, Tile>();

        public void Initialize()
        {
            foreach (var tile in groundTilemap.gameObjects)
            {
                var tileComponent = tile.Value.GetComponent<Tile>();
                tileComponent.position = tile.Key;
                Tiles.Add(tile.Key, tileComponent);
            }
        }
        
        public Vector3 PositionToWorldSpace(Vector3Int pos)
        {
            return grid.CellToWorld(pos) + new Vector3(.5f, 0f, .5f);
        }

        public Vector3Int WorldSpaceToPosition(Vector3 pos)
        {
            return grid.WorldToCell(pos);
        }
    }
}