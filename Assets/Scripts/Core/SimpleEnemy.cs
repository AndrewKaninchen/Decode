using System.Threading.Tasks;

namespace Decode
{
	public class SimpleEnemy : EnemyPawn
	{
		public override async Task Act()
		{
			var dir = Direction.AsVector();
			
			var pos = new Position(position.x + dir.x, position.y + dir.y);
			var board = GameController.Instance.board;

			if (board.tiles.ContainsKey(pos))
			{
				var p = board.tiles[pos].pawn;
						
				if (p != null && p is Hacker)
				{
					await Attack(p);
				}
			}
		}
	}
}