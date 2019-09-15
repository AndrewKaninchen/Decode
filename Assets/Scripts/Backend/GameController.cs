using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asyncoroutine;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Decode
{
//    public class Ability
//    {
//    }

//    public class SimpleAttack : ActiveAbility
//    {
//        public override async Task Use()
//        {
//            Debug.Log("Aplicando socão em 3...");
//            await new WaitForSeconds(1.0f);
//            Debug.Log("2...");
//            await new WaitForSeconds(1.0f);
//            Debug.Log("1...");
//            await new WaitForSeconds(1.0f); 
//            Debug.Log("socão efetuado");
//        }
//    }
//    
//    public abstract class ActiveAbility
//    {
//        public abstract Task Use(bool AI);
//    }
//
//    public abstract class TriggeredAbility
//    {
//        public abstract void Subscribe(GameController gameController, Pawn pawn);
//        public abstract Task Trigger();
//    }

    public class GameController
    {
        public bool HasEnded;
        public Board board;
        public List<Player> Players;
        public List<Pawn> Pawns;
        public int CurrentPlayerId;

        private static GameController _instance;
        public static GameController Instance => _instance;
    
        public GameController(List<Player> players, List<Pawn> pawns)
        {
            HasEnded = false;
            if (Players == null) Players = new List<Player>{new HumanPlayer(), new AIPlayer()};
            CurrentPlayerId = 0;
            _instance = this;
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
    }
}