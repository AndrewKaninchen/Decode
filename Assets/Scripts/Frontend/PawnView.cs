using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Asyncoroutine;
using UnityEngine.Serialization;

namespace Decode
{ 
    public class PawnView : MonoBehaviour
    {
        [FormerlySerializedAs("pawnType")] public Pawn pawnAsset;
        [NonSerialized] public Pawn pawn;
        
        public async Task Move(Position targetPosition)
        {
            var boardView = FindObjectOfType<BoardView>();
            await transform.DOMove(boardView.Position(targetPosition), .2f).IsComplete();
        }
        
        public async Task HitThing(Pawn target)//, Func<Task> effect)
        {
            var originalPos = transform.position;
            await transform.DOMove(new Vector3(target.position.x, originalPos.y, target.position.y), 0.1f).IsComplete();
            
            //await effect.Invoke();
            //evento de ataque
            await transform.DOMove(originalPos, .1f).IsComplete();
            //evento de ataque terminado?
        }
    }
}