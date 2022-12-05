using System;
using AdvanceWars.Runtime.Application;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AdvanceWars.Runtime.Presentation
{
    public class LoadGameInput : MonoBehaviour
    {
        [Inject] readonly LoadGameplay load;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Interact();
            }
        }

        public void Interact()
        {
            load.Run();
        }
    }
}