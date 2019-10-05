using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Decode
{
	
	[SelectionBase]
	public class Hacker : Pawn
	{
		public override async Task Die()
		{
			await base.Die();
			GameController.Instance.GameOver();
		}

		public override async Task Move(Position targetPosition)
		{
			var win = GameController.Instance.board.tiles[targetPosition].pawn is StageGoal;
			await base.Move(targetPosition);
			if (win)
				GameController.Instance.StageClear();
		}

		private void OnValidate()
		{
			if (!gameObject.scene.IsValid()) return;
			var humanPlayer = FindObjectOfType<HumanPlayer>();
			if (humanPlayer == null) return;
			
			if (!humanPlayer.Pawns.Contains(this))
				humanPlayer.Pawns.Add(this);
		}
	}
}