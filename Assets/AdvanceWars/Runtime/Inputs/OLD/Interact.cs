using System.Threading.Tasks;
using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Domain;
using UnityEngine;
using Zenject;

namespace AdvanceWars.Runtime
{
    public class Interact : MonoBehaviour
    {
        [Inject] SelectSpace selectSpace;
        [Inject] OrderBattalion orderBattalion;
        [Inject] Game game; //Esto no se puede conocer de aqui
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Z))
                Select();

            if(Input.GetKeyDown(KeyCode.X))
                Deselect();
        }

        public async Task Select()
        {
            if (game.AnythingSelected)
            {
                await orderBattalion.Execute(game.CursorCoord);
                return;
            }
            
            await selectSpace.Select();
        }

        public async Task Deselect()
        {
            await selectSpace.Deselect();
        }
    }
}