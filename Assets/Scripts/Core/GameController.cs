using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asyncoroutine;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Decode
{
    public class GameController : MonoBehaviour
    {
        public bool HasEnded;
        public Board board;
        public List<Player> Players;
        public List<Pawn> Pawns;
        public int CurrentPlayerId;

        public static GameController Instance { get; private set; }

        public void Initialize(List<Player> players, List<Pawn> pawns)
        {
            HasEnded = false;
            if (Players == null) Players = new List<Player>{new HumanPlayer(), new AIPlayer()};
            CurrentPlayerId = 0;
            Instance = this;
            Pawns = pawns;
        }
        
        public async void Run()
        {
            while (!HasEnded)
            {
                await Turn(Players[CurrentPlayerId]);
                CurrentPlayerId++;
                CurrentPlayerId %= Players.Count;
            }
        }

        public async Task Turn(Player player)
        {
        }
        
        private void Start()
        {
            Initialize(null, null);
            Task.Run(Run);
        }
    }
}