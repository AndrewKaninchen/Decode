using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Decode
{
    public abstract class Pawn : MonoBehaviour
    {
        public int owner;
        public Position position;
        
        public Direction Direction { get; private set; }
        
        public async Task ChangeDirection(Direction direction)
        {
            Direction = direction;
            await transform.DORotate(90f * (int)direction * Vector3.up, .1f).IsComplete();
        }
        
        public virtual async Task Move(Position targetPosition)
        {
            var board = GameController.Instance.board;
            board.tiles[position].pawn = null;
            await transform.DOMove(targetPosition.ToWorldSpace, .2f).IsComplete();
            board.tiles[targetPosition].pawn = this;
            this.position = targetPosition;
        }
        
        public async Task Attack(Pawn target)//, Func<Task> effect)
        {
            var originalPos = transform.position;
            var atkPos = Vector3.Lerp(originalPos, target.position.ToWorldSpace, .5f);
            await transform.DOMove(atkPos, 0.1f).IsComplete();
            target.TakeDamage();
            await transform.DOMove(originalPos, .1f).IsComplete();
            Debug.Log($"{this.gameObject.name} attacks {target.gameObject.name}");
            //await effect.Invoke();
            //evento de ataque
            //evento de ataque terminado?
        }
        
        public async Task TakeDamage()//, Func<Task> effect)
        {
            var originalPos = transform.position;
            await transform.DOShakePosition(.3f, .1f).IsComplete();
            await Die();
        }

        public virtual async Task Die()
        {
            await transform.DOScale(0f, .05f).IsComplete();
            GameController.Instance.Players[owner].Pawns.Remove(this);
            Destroy(gameObject);
        }

        public virtual Task Act()
        {
            return null;
            
        }
        
        public virtual void Initialize(int owner)
        {
            this.owner = owner;
            GameController.Instance.board.tiles[position].pawn = this;
            transform.position = position.ToWorldSpace;
        }
    }

    public enum Direction : int
    {
        East, South, West, North
        
    }
    
    public static class Extensions
    {
        public static (int x, int y) AsVector(this Direction direction)
        {
            (int x, int y) dir;
            switch (direction)
            {
                case Direction.East:
                    dir = (1, 0);
                    break;
                case Direction.South:
                    dir = (0, 1);
                    break;
                case Direction.West:
                    dir = (-1, 0);
                    break;
                case Direction.North:
                    dir = (0, -1);
                    break;
                default:
                    dir = (0, 0);
                    break;
            }

            return dir;
        }
    }
    
    
    
}