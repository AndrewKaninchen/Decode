// Creator: TextusGames

using System;
using System.Collections.Generic;
using Decode;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Tile = Decode.Tile;

[RequireComponent(typeof(Tilemap))]
public class PrefabTilemap : MonoBehaviour, ISerializationCallbackReceiver
{
    public Tilemap map;
    public Grid grid;
    public Dictionary<Vector3Int, GameObject> gameObjects = new Dictionary<Vector3Int, GameObject>();
    [SerializeField] private List<GameObject> _gameObjects = new List<GameObject>();
    [SerializeField] private List<Vector3Int> _gameObjectPositions = new List<Vector3Int>();

    public void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        if (map.HasTile(position))
        {
            var prefabTile = map.GetTile(position) as PrefabTile;
            
            if (prefabTile != null)
            {
                if (!gameObjects.ContainsKey(position))
                {
                    Undo.RecordObject(this, string.Empty);
                    var instance = Instantiate(prefabTile.prefab,
                        parent: transform,
                        position: grid.CellToWorld(position) + new Vector3(.5f, 0f, .5f),
                        rotation: Quaternion.identity);
                    gameObjects.Add(position, instance);
                    Undo.RegisterCreatedObjectUndo(instance, string.Empty);

                    var pawn = instance.GetComponent<Pawn>();
                    if (pawn != null)
                        pawn.position = position;
                    else
                    {
                        var tile = instance.GetComponent<Tile>();
                        if (tile != null) tile.position = position;
                    }
                }
                else
                {
                    gameObjects[position].transform.position =
                        grid.CellToWorld(position) + new Vector3(.5f, 0f, .5f);
                }
            }
        }
        else
        {
            if (gameObjects.ContainsKey(position))
            {
                DestroyImmediate(gameObjects[position]);
                gameObjects.Remove(position);
            }
        }
    }


    public void DeletaTudo()
    {
        foreach (var go in gameObjects.Values)
        {
            if(go != null)
                DestroyImmediate(go);
        }
        gameObjects.Clear();
        _gameObjects.Clear();
        _gameObjectPositions.Clear();
    }

    public void OnBeforeSerialize()
    {
        _gameObjectPositions.Clear();
        _gameObjects.Clear();
        _gameObjectPositions.AddRange(gameObjects.Keys);
        _gameObjects.AddRange(gameObjects.Values);
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            gameObjects.Add(_gameObjectPositions[i], _gameObjects[i]);
        }
    }
}
