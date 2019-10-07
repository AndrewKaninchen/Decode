using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Decode
{
    public class HumanPlayer : Player
    {
        [HideInInspector] public Pawn hacker;
        
        private TaskCompletionSource<int> takeInputCompletionSource;
        private bool takingInput = false;

        public override async Task Play()
        {
            takingInput = true;
            takeInputCompletionSource = new TaskCompletionSource<int>();
            await takeInputCompletionSource.Task;
        }

        private void EndTurn()
        {
            takeInputCompletionSource?.TrySetResult(0);
        }

        private void Start()
        {
            if (hacker == null) hacker = FindObjectOfType<Hacker>();
        }

        private async void Update()
        {
            if (!GameController.Instance.hasStarted || GameController.Instance.hasEnded || !takingInput)
                return;

            if (await CheckKeyboardInput());
            else await CheckMouseInput();
        }

        private async Task<bool> CheckKeyboardInput()
        {
            (int x, int y) input;
            input.x = (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0);
            input.y = (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);

            if (input.x == 0 && input.y == 0) return false;
            
            var targetPosition = hacker.position + ((input.x != 0)? 
                                     new Vector3Int(input.x, 0, 0) : 
                                     new Vector3Int(0, input.y, 0));

            await MoveOrAttack(targetPosition);

            return true;

        }

        private async Task CheckMouseInput()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit)) return;
            
            var hitObject = hit.collider.gameObject;
            var tile = hitObject.GetComponentInParent<Tile>();
            var pawn = hitObject.GetComponent<Pawn>();
            if (tile)
            {
                var hoveredTile = hit.collider.GetComponentInParent<Tile>();

                if (!hoveredTile) return;

                if (!Input.GetMouseButtonDown(0)) return;
                await Move(hoveredTile.position);
                return;

            }

            if (!pawn || !Input.GetMouseButtonDown(0)) return;
            await Attack(pawn);

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
            takingInput = false;
            await hacker.Move(position);
            var p = FindObjectOfType<HumanPlayer>();
            if (p != null) p.EndTurn();
        }

        private async Task Attack(Pawn pawn)
        {
            takingInput = false;
            await hacker.Attack(pawn);
            var p = FindObjectOfType<HumanPlayer>();
            if (p != null) p.EndTurn();
        }
    }
}