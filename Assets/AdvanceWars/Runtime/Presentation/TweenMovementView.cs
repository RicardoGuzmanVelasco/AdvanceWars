using System.Threading.Tasks;
using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Domain.Troops;
using DG.Tweening;
using UnityEngine;

namespace AdvanceWars.Runtime.Presentation
{
    public class TweenMovementView : MovementView
    {
        public Task Move(Battalion battalion, Vector2Int targetPos)
        {
            return Object.FindObjectOfType<BattalionView>()
                .transform.DOMove((Vector2)targetPos, .25f)
                .AsyncWaitForCompletion();
        }
    }
}