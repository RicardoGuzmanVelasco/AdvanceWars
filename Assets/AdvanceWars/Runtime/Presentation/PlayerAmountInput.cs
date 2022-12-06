using System;
using AdvanceWars.Runtime.Application;
using UnityEngine;
using Zenject;

namespace AdvanceWars.Runtime.Presentation
{
    public class PlayerAmountInput : MonoBehaviour
    {
        [Inject] ConfigGameplay configGameplay;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Add();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                Remove();
            }
        }

        public void Add()
        {
            configGameplay.AddPlayer();
        }

        public void Remove()
        {
            configGameplay.RemovePlayer();
        }
    }
}