using System;
using UnityEditor;

namespace Decode
{
    public class DecodeEditor : EditorWindow
    {
        private GameController gameController;
        private Board board;
        
        [MenuItem ("Decode/Level Editor")]
        public static void  ShowWindow () {
            EditorWindow.GetWindow(typeof(DecodeEditor));
        }

        private void Update()
        {
            if(!gameController) gameController = FindObjectOfType<GameController>();
            if(!board) board = FindObjectOfType<Board>();
         
            SnapPawnsFromWorldSpaceToGridSpace();
        }
        private void SnapPawnsFromWorldSpaceToGridSpace()
        {
            foreach (var player in gameController.Players)
            {
                foreach (var pawn in player.Pawns)
                {
                    if (!pawn || !pawn.transform.hasChanged) return;

                    var pos = board.WorldSpaceToPosition(pawn.transform.position);
                    pawn.position = pos;
                    pawn.transform.position = pawn.position.ToWorldSpace;
                }
            }
        }            

    }
}