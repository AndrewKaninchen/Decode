using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Decode
{
    public class BoardView : MonoBehaviour, ISerializationCallbackReceiver
    {
        public GameObject cubePrefab;
        public int size;
        public float spacing;

        public Dictionary<Position, TileView> tiles = new Dictionary<Position, TileView>();

        [SerializeField] private List<TileView> _tiles = new List<TileView>();
        private Vector2 center;

        public Vector3 Position(Position pos)
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
                    var tile = go.GetComponent<TileView>();
                    print(tile);
                    tile.position = new Position(i, j);
                    tiles.Add(tile.position, tile);
                    tile.backend = new Tile();
                }
            }
            OnValidate();
        }

        private void OnValidate()
        {
            center = (size / 2f) * (1 + spacing) * Vector2.one;

            foreach (var tile in tiles)
            {
                tile.Value.transform.localPosition = Position(tile.Key);
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