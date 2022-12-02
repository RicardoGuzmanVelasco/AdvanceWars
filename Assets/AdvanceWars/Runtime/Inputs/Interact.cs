using System.Threading.Tasks;
using AdvanceWars.Runtime.Application;
using UnityEngine;
using Zenject;

namespace AdvanceWars.Runtime
{
    public class Interact : MonoBehaviour
    {
        [Inject] SelectBattalion controller;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Z))
                Select();

            if(Input.GetKeyDown(KeyCode.X))
                Deselect();
        }

        public async Task Select()
        {
            await controller.Select();
        }

        public async Task Deselect()
        {
            await controller.Deselect();
        }
    }
}