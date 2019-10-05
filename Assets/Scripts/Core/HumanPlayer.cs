using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Decode
{
	public class HumanPlayer : Player
	{
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
		
		public override async Task Play()
		{
			endTurnCompletionSource = new TaskCompletionSource<int>();
			await endTurnCompletionSource.Task;
		}
	}
}