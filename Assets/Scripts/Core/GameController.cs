using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asyncoroutine;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Decode
{
    public class GameController : MonoBehaviour
    {
        public bool HasStarted = false;
        public bool HasEnded;
        [FormerlySerializedAs("board")] public Board Board;
        public List<Player> Players;
        public int CurrentPlayerId;

        public GameObject GameOverText;
        public GameObject StageClearText;
        [HideInInspector] public StageGoal StageGoal;

        public static GameController Instance { get; private set; }
        
        public async void Run()
        {
            Initialize();

            while (!HasEnded)
            {
                await Turn(Players[CurrentPlayerId]);
                CurrentPlayerId++;
                CurrentPlayerId %= Players.Count;
            }
        }

        private void Initialize()
        {
            Board.Initialize();
            print("Game Started!");
            for (int i = 0; i < Players.Count; i++)
            {
                foreach (var pawn in Players[i].Pawns)
                    pawn.Initialize(this, i);
            }

            StageGoal.Initialize(this, -1);
        }

        public void GameOver()
        {
            HasEnded = true;
            GameOverText.SetActive(true);
        }

        public void StageClear()
        {
            HasEnded = true;
            StageClearText.SetActive(true);
        }
        
        public async Task Turn(Player player)
        {
            print($"Player {Players.FindIndex((x)=> x == player)} Turn");
            await player.Play();
        }

        private void Start()
        {
            if(StageGoal == null) StageGoal = FindObjectOfType<StageGoal>();
            HasStarted = true;
            Instance = this;
            Run();
        }
    }
}