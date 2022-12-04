using AdvanceWars.Runtime.Application;
using UnityEngine;
using Zenject;

namespace AdvanceWars.Runtime.Presentation
{
    public class EndTurnInput : MonoBehaviour
    {
        [Inject] EndTurn endTurn;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                Interact();
        }

        public async void Interact()
        {
            await endTurn.Run();
        }
    }
}