using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Decode
{
    public class Board : MonoBehaviour, ISerializationCallbackReceiver
    {
        public GameObject cubePrefab;
        public int size;
        public float spacing;

        public Dictionary<Position, Tile> tiles = new Dictionary<Position, Tile>();

        [SerializeField] private List<Tile> _tiles = new List<Tile>();
        private Vector2 center;

        public Vector3 PositionToWorldSpace(Position pos)
        {
            return new Vector3((1 + spacing) * pos.x - center.x, 0f, (1 + spacing) * pos.y - center.y);
        }

        public void Start()
        {
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                CreateCubes();
        }
        
        public void CreateCubes()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var go = Instantiate(cubePrefab, transform);
                    var tile = go.GetComponent<Tile>();
                    //print(tile);
                    tile.position = new Position(i, j);
                    tiles.Add(tile.position, tile);
                    _tiles.Add(tile);
                }
            }
            OnValidate();
        }

        private void OnValidate()
        {
            center = (size / 2f) * (1 + spacing) * Vector2.one;

            foreach (var tile in tiles)
            {
                tile.Value.transform.position = PositionToWorldSpace(tile.Key);
            }
        }

        public void OnBeforeSerialize()
        {
            
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