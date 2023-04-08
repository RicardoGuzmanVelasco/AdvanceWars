using System.Threading.Tasks;
using UnityEngine;

namespace AdvanceWars.Runtime.Inputs
{
    public class EndTurnInput : MonoBehaviour
    {
        private bool listening;
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