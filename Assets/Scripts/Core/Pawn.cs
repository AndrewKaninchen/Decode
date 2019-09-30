using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Decode
{
    public class Pawn : MonoBehaviour
    {
        public Position position;

        [FormerlySerializedAs("pawnType")] public Pawn pawnAsset;
        [NonSerialized] public Pawn pawn;
        
        public async Task Move(Position targetPosition)
        {
            var boardView = FindObjectOfType<Board>();
            await transform.DOMove(boardView.PositionToWorldSpace(targetPosition), .2f).IsComplete();
            this.position = targetPosition;

        }
        
        public async Task HitThing(Pawn target)//, Func<Task> effect)
        {
            var originalPos = transform.position;
            var atkPos = Vector3.Lerp(originalPos, target.position.ToWorldSpace, .5f);
            await transform.DOMove(atkPos, 0.1f).IsComplete();
            target.TakeDamage();
            await transform.DOMove(originalPos, .1f).IsComplete();
            //await effect.Invoke();
            //evento de ataque
            //evento de ataque terminado?
        }
        
        public async Task TakeDamage()//, Func<Task> effect)
        {
            var originalPos = transform.position;
            await transform.DOShakePosition(.3f, .1f).IsComplete();
        }
        
        protected virtual void Initialize(Position position)
        {
            this.position = position;
        }
    }

    public class Goomba : Pawn
    {
        public int direction;
    }
}