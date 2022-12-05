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
            if (Input.GetKeyDown(KeyCode.A))
            {
                Add();
            }
        }

        public void Add()
        {
            configGameplay.AddPlayer();
        }
    }
}