using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asyncoroutine;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Decode
{
    public class GameController : MonoBehaviour
    {
        public bool hasStarted = false;
        public bool hasEnded;
        public Board board;
        [HideInInspector] public List<Player> players;
        public int currentPlayerId;

        public GameObject gameOverText;
        public GameObject stageClearText;
        public Scene nextLevel;
        
        [HideInInspector] public StageGoal stageGoal;
        private TaskCompletionSource<int> goToNextLevel;

        public static GameController Instance { get; private set; }
        
        private void Start()
        {
            players = GetComponents<Player>().ToList();
            if(board == null) board = FindObjectOfType<Board>();
            if(stageGoal == null) stageGoal = FindObjectOfType<StageGoal>();
            hasStarted = true;
            Instance = this;
            Run();
        }

        private async void Run()
        {
            Initialize();

            while (!hasEnded)
            {
                await Turn(players[currentPlayerId]);
                currentPlayerId++;
                currentPlayerId %= players.Count;
            }

            await GoToNextLevel();
        }

        private void Initialize()
        {
            board.Initialize();
            print("Game Started!");
            for (var i = 0; i < players.Count; i++)
            {
                foreach (var pawn in players[i].Pawns)
                    pawn.Initialize(this, i);
            }

            stageGoal.Initialize(this, -1);
        }

        public void GameOver()
        {
            hasEnded = true;
            gameOverText.SetActive(true);
        }

        public void StageClear()
        {
            hasEnded = true;
            stageClearText.SetActive(true);
        }

        private async Task GoToNextLevel()
        {
            goToNextLevel = new TaskCompletionSource<int>();
            await goToNextLevel.Task;

            SceneManager.LoadScene(nextLevel.handle);
        }

        private void Update()
        {
            if (Input.anyKey)
            {
                goToNextLevel?.SetResult(0);
            }
        }

        private async Task Turn(Player player)
        {
            print($"Player {players.FindIndex((x)=> x == player)} Turn");
            await player.Play();
        }

    }
}