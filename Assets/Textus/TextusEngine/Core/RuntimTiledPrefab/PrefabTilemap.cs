// Creator: TextusGames

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class PrefabTilemap : MonoBehaviour, ISerializationCallbackReceiver
{
    public Tilemap map;
    public Dictionary<Vector3Int, GameObject> tileDictionary = new Dictionary<Vector3Int, GameObject>();
    [SerializeField] private List<GameObject> _tiles = new List<GameObject>();
    [SerializeField] private List<Vector3Int> _tilePositions = new List<Vector3Int>();

    public void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        if (map.HasTile(position))
        {
            var tile = map.GetTile(position);
            var prefabTile = tile as PrefabTile;
            if (prefabTile != null)
            {
                if (!tileDictionary.ContainsKey(position))
                {
                    var instance = Instantiate(prefabTile.Prefab,
                        parent: transform,
                        position: new Vector3(position.x + .5f, 0f, position.y + .5f),
                        rotation: Quaternion.identity);
                    tileDictionary.Add(position, instance);
                }
            }
        }
        else
        {
            if (tileDictionary.ContainsKey(position))
            {
                DestroyImmediate(tileDictionary[position]);
                tileDictionary.Remove(position);
            }
        }
    }


    public void DeletaTudo()
    {
        foreach (var go in tileDictionary.Values)
        {
            DestroyImmediate(go);
        }
        tileDictionary.Clear();
        _tiles.Clear();
        _tilePositions.Clear();
    }

    public void OnBeforeSerialize()
    {
        _tilePositions.Clear();
        _tiles.Clear();
        _tilePositions.AddRange(tileDictionary.Keys);
        _tiles.AddRange(tileDictionary.Values);
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < _tiles.Count; i++)
        {
            tileDictionary.Add(_tilePositions[i], _tiles[i]);
        }
    }
}
