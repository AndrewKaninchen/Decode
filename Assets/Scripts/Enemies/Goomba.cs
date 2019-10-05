using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Decode
{
	
	[SelectionBase]
	public class Goomba : EnemyPawn
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