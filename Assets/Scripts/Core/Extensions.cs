using UnityEngine;

namespace Decode
{
    public static class Extensions
    {
        public static Vector3Int AsVector3Int(this Direction direction)
        {
            Vector3Int dir;
            switch (direction)
            {
                case Direction.East:
                    dir = Vector3Int.right;
                    break;
                case Direction.South:
                    dir = Vector3Int.down;
                    break;
                case Direction.West:
                    dir = Vector3Int.left;
                    break;
                case Direction.North:
                    dir = Vector3Int.up;
                    break;
                default:
                    dir = Vector3Int.zero;
                    break;
            }

            return dir;
        }
    }
}