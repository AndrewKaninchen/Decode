// Creator: TextusGames

using System;
using System.Collections.Generic;
using Decode;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class PrefabTilemap : MonoBehaviour, ISerializationCallbackReceiver
{
    public Tilemap map;
    public Grid grid;
    public Dictionary<Vector3Int, GameObject> tileDictionary = new Dictionary<Vector3Int, GameObject>();
    [SerializeField] private List<GameObject> _tiles = new List<GameObject>();
    [SerializeField] private List<Vector3Int> _tilePositions = new List<Vector3Int>();

    public void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        if (map.HasTile(position))
        {
            var prefabTile = map.GetTile(position) as PrefabTile;
            
            if (prefabTile != null)
            {
                if (!tileDictionary.ContainsKey(position))
                {
                    var instance = Instantiate(prefabTile.prefab,
                        parent: transform,
                        position: grid.CellToWorld(position) + new Vector3(.5f, 0f, .5f),
                        rotation: Quaternion.identity);
                    tileDictionary.Add(position, instance);
                    instance.GetComponent<Decode.Tile>().position = new Position(position.x, position.y);
                }
                else
                {
                    tileDictionary[position].transform.position =
                        grid.CellToWorld(position) + new Vector3(.5f, 0f, .5f);
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
