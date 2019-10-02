using System.Threading.Tasks;

namespace Decode
{
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
	}
}