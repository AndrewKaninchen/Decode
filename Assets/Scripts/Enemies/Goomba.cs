using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Decode
{
	public class Goomba : EnemyPawn
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
					Debug.Log($"{gameObject.name} tá tentando atacar eu acho");
					await Attack(p);
				}
				else if(p is null)
				{
					await Move(pos);
				}
			}
			else
			{
				await ChangeDirection((Direction)(((int)Direction + 2) % 4));
			}
		}
	}
}