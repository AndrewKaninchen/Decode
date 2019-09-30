using System;
using System.Collections;
using System.Collections.Generic;
using Decode;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
    public Pawn playerPawn;
    //public BoardView boardView;
    
    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Tile hoveredTile = null;
        if (Physics.Raycast(ray, out var hit))
        {
            var hitObject = hit.collider.gameObject;
            var tile = hitObject.GetComponentInParent<Tile>();
            var pawn = hitObject.GetComponent<Pawn>();
            if (tile)
            {
                var lastHoveredTile = hoveredTile;
                hoveredTile = hit.collider.GetComponentInParent<Tile>();

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
            else if (pawn)
            {
                if(Input.GetMouseButtonDown(0))
                    playerPawn.HitThing(pawn);
            }
        }
        
        
    }
}
