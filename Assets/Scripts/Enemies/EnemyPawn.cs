using System;

namespace Decode
{
	public abstract class EnemyPawn : Pawn
	{
		private void OnValidate()
		{
			if (!gameObject.scene.IsValid()) return;
			var aiPlayer = FindObjectOfType<AIPlayer>();
			if (aiPlayer == null) return;
			
			if (!aiPlayer.Pawns.Contains(this))
				aiPlayer.Pawns.Add(this);
		}
	}
}