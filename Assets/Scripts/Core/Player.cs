using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Decode
{
	public abstract class Player : MonoBehaviour
	{
		public List<Pawn> Pawns;

		public abstract Task Play();

		private void OnValidate()
		{
			var toRemove = new List<int>();
			for (int i = 0; i < Pawns.Count; i++)
			{
				if (Pawns[i] == null)
				{
					Pawns.RemoveAt(i);
					i--;
				}
			}
		}
	}
}