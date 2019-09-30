using System.Collections.Generic;
using System.Threading.Tasks;

namespace Decode
{
	public abstract class Player
	{
		public List<Pawn> Pawns;

		public abstract Task Play();
	}
}