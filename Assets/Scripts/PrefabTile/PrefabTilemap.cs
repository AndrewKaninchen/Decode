// Creator: TextusGames

using System;
using System.Collections.Generic;
using System.Linq;
using Decode;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Tile = Decode.Tile;

[ExecuteInEditMode]
[RequireComponent(typeof(Tilemap))]
public class PrefabTilemap : MonoBehaviour, ISerializationCallbackReceiver
{
    public Tilemap map;
    public Grid grid;
    public Dictionary<Vector3Int, GameObject> gameObjects = new Dictionary<Vector3Int, GameObject>();
    [SerializeField] private List<GameObject> _gameObjects = new List<GameObject>();
    [SerializeField] private List<Vector3Int> _gameObjectPositions = new List<Vector3Int>();
    
    private void OnEnable()
    {
        Undo.undoRedoPerformed += VerifyTileDeletion;
    }

    private void OnDisable()
    {
        // ReSharper disable once DelegateSubtraction
        Undo.undoRedoPerformed -= VerifyTileDeletion;
    }

    private void VerifyTileDeletion()
    {
        Debug.Log($"{map.name} ({map.cellBounds})");
        for (int i = map.cellBounds.xMin; i < map.cellBounds.xMax; i++)
        {
            for (int j = map.cellBounds.yMin; j < map.cellBounds.yMax; j++)
            {
                for (int k = map.cellBounds.zMin; k < map.cellBounds.zMax; k++)
                {
                    var pos = new Vector3Int(i, j, k);
                    if (map.HasTile(pos))
                        RefreshTile(pos);
                }
            }
        }
//        map.GetTilesBlock(map.cellBounds).ToList().ForEach((x)=>
//        {
//            if (x is PrefabTile prefabTile)
//            {
//                if (!gameObjects.ContainsKey(prefabTile.pos))
//                    Debug.Log(prefabTile.name);
//                RefreshTile(prefabTile.pos);
//            }
//        });
        gameObjects.Keys.ToList().ForEach(RefreshTile);
    }

    public void RefreshTile(Vector3Int position)
    {
//        Debug.Log("teste");
        if (map.HasTile(position))
        {
            var prefabTile = map.GetTile<PrefabTile>(position);
            var flags = map.GetTileFlags(position);
            
            if (prefabTile != null)
            {
                if (!gameObjects.ContainsKey(position))
                {
                    CreateGameObjectFromPrefab(position, prefabTile);
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
                Debug.Log("mas que cacete");
                DestroyImmediate(gameObjects[position]);
                gameObjects.Remove(position);
            }
        }
    }

    private void CreateGameObjectFromPrefab(Vector3Int position, PrefabTile prefabTile)
    {
        var instance = Instantiate(prefabTile.prefab,
            parent: transform,
            position: grid.CellToWorld(position) + new Vector3(.5f, 0f, .5f),
            rotation: Quaternion.identity);
        gameObjects.Add(position, instance);

        var pawn = instance.GetComponent<Pawn>();
        if (pawn != null)
        {
            pawn.position = position;
        }
        else
        {
            var tile = instance.GetComponent<Tile>();
            if (tile != null) tile.position = position;
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
