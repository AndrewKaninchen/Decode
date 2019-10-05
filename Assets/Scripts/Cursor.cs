using System;
using System.Collections;
using System.Collections.Generic;
using Decode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Cursor : MonoBehaviour
{
    [HideInInspector] public Pawn hacker;
    //public BoardView boardView;

    private void Start()
    {
        if (hacker == null) hacker = FindObjectOfType<Hacker>();
    }

    private async void Update()
    {
        if (!GameController.Instance.hasStarted || GameController.Instance.hasEnded)
            return;
        
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
                        await hacker.Move(hoveredTile.position);
                        var p = FindObjectOfType<HumanPlayer>();
                        if (p != null) p.EndTurn();
                    }
                    else
                    {
                        //setar os gráfico
                    }
                }
            }
            else if (pawn)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    await hacker.Attack(pawn);
                    var p = FindObjectOfType<HumanPlayer>();
                    if (p != null) p.EndTurn();
                }
            }
        }
        
        
    }
}
