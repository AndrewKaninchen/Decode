using System.Threading.Tasks;
using UnityEngine;

namespace Decode
{
	public class AIPlayer : Player
	{
		public override async Task Play()
		{
			foreach (var pawn1 in Pawns)
			{
				if (pawn1 is Goomba pawn)
				{
					(int x, int y) dir;
					switch (pawn.direction)
					{
						case 0:
							dir = (1, 0);
							break;
						case 1:
							dir = (0, 1);
							break;
						case 2:
							dir = (-1, 0);
							break;
						case 3:
							dir = (0, -1);
							break;
						default:
							dir = (0, 0);
							break;
					}

					var pos = new Position(pawn.position.x + dir.x, pawn.position.y + dir.y);

					if (GameController.Instance.board.tiles.ContainsKey(pos))
					{
						await pawn.Move(pos);
					}
					else
					{
						pawn.direction = (pawn.direction + 2) % 4;
						var mpos = new Position(pawn.position.x - dir.x, pawn.position.y - dir.y);
						await pawn.Move(mpos);
						Debug.Log("viradona");
					}
				}
			}
		}
	}
}