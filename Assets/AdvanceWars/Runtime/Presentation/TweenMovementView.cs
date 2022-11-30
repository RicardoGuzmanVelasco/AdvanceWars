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
            //No se encuentran los metodos asincronos por algo del api compatibility level y Dotween.
            //#if UNITY_2018_1_OR_NEWER && (NET_4_6 || NET_STANDARD_2_0) Esa es la condicion y las opciones con esta version de unity es .NetFramework y .NetStandard2.1
            //Al ponerlo en 2.1 y añadir la condición, sigue sin funcionar.
            // Object.FindObjectOfType<BattalionView>().transform.DOMove((Vector2) targetPos, 1f); 
            Object.FindObjectOfType<BattalionView>().transform.position = (Vector2) targetPos;
            return Task.CompletedTask;
        }
    }
}