using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        (int x, int y) input;
        input.x = (Input.GetKeyDown(KeyCode.D) ? 1 : 0) + (Input.GetKeyDown(KeyCode.A) ? -1 : 0);
        input.y = (Input.GetKeyDown(KeyCode.W) ? 1 : 0) + (Input.GetKeyDown(KeyCode.S) ? -1 : 0);
        
        if (input.x != 0 || input.y != 0)
        {
            if (input.x != 0)
            {
                var targetPosition = hacker.position + new Vector3Int(input.x, 0, 0);
                await MoveOrAttack(targetPosition);
            }
            else
            {
                var targetPosition = hacker.position + new Vector3Int(0, input.y, 0);
                await MoveOrAttack(targetPosition);
            }

            return;
        }
        
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
                        await Move(hoveredTile.position);
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
                    await Attack(pawn);
                }
            }
        }
    }

    private async Task MoveOrAttack(Vector3Int position)
    {
        if (GameController.Instance.board.Tiles.ContainsKey(position))
        {
            var targetTile = GameController.Instance.board.Tiles[position];
            if (targetTile.pawn is EnemyPawn)
                await Attack(targetTile.pawn);
            else 
                await Move(position);
        }
    }
    
    private async Task Move(Vector3Int position)
    {
        await hacker.Move(position);
        var p = FindObjectOfType<HumanPlayer>();
        if (p != null) p.EndTurn();
    }
    
    private async Task Attack(Pawn pawn)
    {
        await hacker.Attack(pawn);
        var p = FindObjectOfType<HumanPlayer>();
        if (p != null) p.EndTurn();
    }
}
