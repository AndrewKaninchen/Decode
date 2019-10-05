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
    public class Board : MonoBehaviour, ISerializationCallbackReceiver
    {
        public Grid grid;
        public PrefabTilemap groundTilemap;
        
        public GameObject cubePrefab;
        public int size;
        public float spacing;

        [NonSerialized] public Dictionary<Position, Tile> tiles = new Dictionary<Position, Tile>();

        [SerializeField] private List<Tile> _tiles = new List<Tile>();
        private Vector2 center;

        public Vector3 PositionToWorldSpace(Position pos)
        {
            return grid.CellToWorld(new Vector3Int(pos.x, pos.y, 0)) + new Vector3(.5f, 0f, .5f);
//            return new Vector3((1 + spacing) * pos.x - center.x, 0f, (1 + spacing) * pos.y - center.y);
        }

        public Position WorldSpaceToPosition(Vector3 pos)
        {
            var v3p = grid.WorldToCell(pos);
            return new Position(v3p.x, v3p.y);
//            return new Position( (int) ((pos.x + center.x)/ (1+spacing) ), (int) ((pos.z + center.y)/ (1+spacing) ));
        }
        
        public void Start()
        {
            GetTiles();
//            GenerateTiles();
        }

        public void GetTiles()
        {
            foreach (var tile in groundTilemap.GetComponentsInChildren<Tile>())
            {
                tiles.Add(tile.position, tile);
            }
        }

        public void ClearTiles()
        {
            foreach (var tile in tiles.Values)
                DestroyImmediate(tile.gameObject);

            tiles.Clear();
        }
        public void GenerateTiles()
        {
            ClearTiles();            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var go = Instantiate(cubePrefab, transform);
                    var tile = go.GetComponent<Tile>();
                    //print(tile);
                    tile.position = new Position(i, j);
                    tiles.Add(tile.position, tile);
                }
            }
            
            RecalculateTilePositions();
        }

//        private void OnValidate()
//        {
//            RecalculateTilePositions();
//        }

        private void RecalculateTilePositions()
        {
            center = (size / 2f) * (1 + spacing) * Vector2.one;

            foreach (var tile in tiles)
            {
                var pos = PositionToWorldSpace(tile.Key);
                tile.Value.transform.position = pos;
                if (tile.Value.pawn != null)
                    tile.Value.pawn.transform.position = pos;
            }
        }

        public void OnBeforeSerialize()
        {
            _tiles.Clear();
            _tiles.AddRange(tiles.Values);
        }

        public void OnAfterDeserialize()
        {
            foreach (var tileView in _tiles)
            {
                if (!tiles.ContainsKey(tileView.position))
                    tiles.Add(tileView.position, tileView);
            }
        }
    }
}