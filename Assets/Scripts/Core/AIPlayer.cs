using System.Threading.Tasks;
using UnityEngine;

namespace Decode
{
	public class AIPlayer : Player
	{
		public override async Task Play()
		{
			foreach (var pawn in Pawns)
			{
				await pawn.Act();
			}
		}
	}
}