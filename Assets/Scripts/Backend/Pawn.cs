using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Decode
{
    public struct Position
    {
        public int x, y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    
    public abstract class Pawn
    {
        public Position position;
        public PawnView view;

        protected Pawn(Position position, PawnView view)
        {
            this.position = position;
            this.view = view;
        }

        public async Task Move(Position targetPosition)
        {
            await view.Move(targetPosition);
            this.position = targetPosition;
        }
    }

    public class Goomba : Pawn
    {
        public int direction;
        public Goomba(Position position, PawnView view) : base(position, view)
        {
        }
        
        
    }
}