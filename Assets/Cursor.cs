using System;
using System.Collections;
using System.Collections.Generic;
using Decode;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
    public PawnView playerPawn;
    //public BoardView boardView;
    
    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        TileView hoveredTile = null;
        if (Physics.Raycast(ray, out var hit))
        {
            var lastHoveredTile = hoveredTile;
            hoveredTile = hit.collider.GetComponentInParent<TileView>();
            
            if (lastHoveredTile != hoveredTile)
            {
                //resetar os gráfico  
            }
            
            if (hoveredTile)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    playerPawn.Move(hoveredTile.position);
                }
                else
                {
                    //setar os gráfico
                }
            }
        }
        
        
    }
}
