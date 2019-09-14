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

        public Dictionary<(int x, int y), TileView> tiles = new Dictionary<(int x, int y), TileView>();

        [SerializeField] private List<TileView> _tiles = new List<TileView>();
        private Vector2 center;

        public Vector3 Position((int x, int y) pos)
        {
            var (x, y) = pos;
            return Position(x, y);
        }

        public Vector3 Position(int i, int j)
        {
            return new Vector3((1 + spacing) * i - center.x, 0f, (1 + spacing) * j - center.y);
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
                    tile.position = (i, j);
                    tiles.Add(tile.position, tile);
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