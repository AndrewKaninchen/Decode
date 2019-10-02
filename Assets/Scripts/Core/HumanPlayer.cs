using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Decode
{
	public class HumanPlayer : Player
	{
		public Button endTurnButton;
		private TaskCompletionSource<int> endTurnCompletionSource;

		private void Start()
		{
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				EndTurn();
			}
		}

		public void EndTurn()
		{
			endTurnCompletionSource?.TrySetResult(0);
		}
		
		public void OnEndTurnButtonPressed()
		{
			endTurnCompletionSource.SetResult(0); 
			endTurnButton.onClick.RemoveListener(OnEndTurnButtonPressed);
			endTurnButton.gameObject.SetActive(false);
		}

		public override async Task Play()
		{
//			endTurnButton.gameObject.SetActive(true);
			endTurnCompletionSource = new TaskCompletionSource<int>();
//			endTurnButton.onClick.AddListener(OnEndTurnButtonPressed);
			await endTurnCompletionSource.Task;
		}
	}
}