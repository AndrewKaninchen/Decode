using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Decode
{
	public abstract class Player : MonoBehaviour
	{
		public List<Pawn> Pawns;

		public abstract Task Play();
	}
}