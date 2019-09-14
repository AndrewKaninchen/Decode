using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
    public class Pawn : ScriptableObject
    {
        public (int x, int y) position;
        public List<Ability> Abilities;
    }    
}