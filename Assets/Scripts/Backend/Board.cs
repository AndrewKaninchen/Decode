using System.Collections.Generic;

namespace Decode
{
	public class Board
	{
		public Dictionary<Position, Tile> tiles;
	}

	public class Tile
	{
		public Position position;
	}
}