using System;
using UnityEngine;

namespace Decode
{
	[Serializable]
	public struct Position
	{
		public int x, y;

		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector3 ToWorldSpace => GameController.Instance.board.PositionToWorldSpace(this);
	}
}