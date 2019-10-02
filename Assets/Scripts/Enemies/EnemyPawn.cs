using System;

namespace Decode
{
	public abstract class EnemyPawn : Pawn
	{
		private void OnValidate()
		{
			var aiPlayer = FindObjectOfType<AIPlayer>();
			if (!aiPlayer.Pawns.Contains(this))
				aiPlayer.Pawns.Add(this);
		}
	}
}