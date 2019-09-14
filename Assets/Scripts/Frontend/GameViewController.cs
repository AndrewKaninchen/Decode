using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Decode
{
    public class GameViewController : MonoBehaviour
    {
        [SerializeField] private GameObject pawnsRoot;
        [SerializeField] private BoardView boardView;
        
        private GameController gameController;
        private List<TileView> tiles;
        private List<PawnView> pawns;

        public void GetViewElements()
        {
            pawns = pawnsRoot.GetComponentsInChildren<PawnView>().ToList();
        }
        
        public (List<Player> players, List<Pawn> pawns) CreateBackendElements()
        {
            var _pawns = new List<Pawn>();
            pawns.ForEach((x) => _pawns.Add(x.pawnAsset));

            //var _players = new List<Player>();
            //players.ForEach((x) => _players.Add(x.pawnType));

            return (null, _pawns);
        }
        
        private void Start()
        {
            GetViewElements();
            var (players, list) = CreateBackendElements();
            
            gameController = new GameController(players, list);
            gameController.Run();
        }
    }
}
