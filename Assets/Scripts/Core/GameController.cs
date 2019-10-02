using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asyncoroutine;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Decode
{
    public class GameController : MonoBehaviour
    {
        public bool HasStarted = false;
        public bool HasEnded;
        public Board board;
        public List<Player> Players;
        public int CurrentPlayerId;

        public GameObject GameOverText;
        public GameObject StageClearText;
        public StageGoal StageGoal;

        public static GameController Instance { get; private set; }
        
        public async void Run()
        {
            print("Game Started!");
            for (int i = 0; i < Players.Count; i++)
            {
                foreach (var pawn in Players[i].Pawns)
                    pawn.Initialize(i);    
            }
            StageGoal.Initialize(-1);
            
            while (!HasEnded)
            {
                await Turn(Players[CurrentPlayerId]);
                CurrentPlayerId++;
                CurrentPlayerId %= Players.Count;
            }
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
            HasStarted = true;
            Instance = this;
            Run();
        }

        private void Update()
        {
            if (!HasStarted && Input.GetKeyDown(KeyCode.Space))
            {
                HasStarted = true;
                Instance = this;
                Run();
            }
        }
    }
}