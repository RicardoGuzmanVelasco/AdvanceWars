using System.Threading.Tasks;
using AdvanceWars.Runtime.Application;
using UnityEngine;
using Zenject;

namespace AdvanceWars.Runtime.Presentation
{
    public class EndTurnInput : MonoBehaviour
    {
        private bool interacted; 
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                Interact();
        }

        public void Interact()
        {
            interacted = true;
        }
        
        public async Task Listen()
        {
            while (!interacted)
            {
                await Task.Yield();
            }

            interacted = false;
        }
    }
}