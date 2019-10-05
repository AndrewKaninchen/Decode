using System.Threading.Tasks;
using UnityEngine;

namespace Decode
{
	public class SimpleEnemy : EnemyPawn
	{
		public override async Task Act()
		{
			var dir = Direction.AsVector();
			
			var pos = new Vector3Int(position.x + dir.x, position.y + dir.y, 0);
			var board = GameController.Instance.board;

			if (board.Tiles.ContainsKey(pos))
			{
				var p = board.Tiles[pos].pawn;
						
				if (p != null && p is Hacker)
				{
					await Attack(p);
				}
			}
		}
	}
}