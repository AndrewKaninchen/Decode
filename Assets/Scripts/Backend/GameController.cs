using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asyncoroutine;
using UnityEngine;

namespace Decode
{
    public class Ability
    {
    }

    public class SimpleAttack : ActiveAbility
    {
        public override async Task Use()
        {
            Debug.Log("Aplicando socão em 3...");
            await new WaitForSeconds(1.0f);
            Debug.Log("2...");
            await new WaitForSeconds(1.0f);
            Debug.Log("1...");
            await new WaitForSeconds(1.0f); 
            Debug.Log("socão efetuado");
        }
    }
    
    public abstract class ActiveAbility
    {
        public abstract Task Use();
    }

    public abstract class TriggeredAbility
    {
        public abstract void Subscribe(GameController gameController, Pawn pawn);
        public abstract Task Trigger();
    }

    public class Player
    {
        public List<Pawn> Pawns;
    }
    
    public class GameController
    {
        public bool HasEnded;
        public List<Player> Players;
        public List<Pawn> Pawns;
        public int CurrentPlayerId;

        public GameController(List<Player> players, List<Pawn> pawns)
        {
            HasEnded = false;
            if (Players == null) Players = new List<Player>{new Player(), new Player()};
            CurrentPlayerId = 0;
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