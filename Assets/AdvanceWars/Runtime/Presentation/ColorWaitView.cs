using UnityEngine;
using System.Threading.Tasks;
using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Runtime.Presentation
{
    public class ColorWaitView : WaitView
    {
        public Task Wait(Battalion battalion)
        {
            Object.FindObjectOfType<BattalionView>().ShowTired();
            return Task.CompletedTask;
        }
    }
}