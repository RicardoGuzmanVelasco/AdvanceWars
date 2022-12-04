using System.Threading.Tasks;
using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Domain.Troops;
using TMPro;
using UnityEngine;

namespace AdvanceWars.Runtime.Presentation
{
    public class TurnPanel : MonoBehaviour, TurnView
    {
        public async Task SetTurn(Nation nationInTurn)
        {
            GetComponentInChildren<TMP_Text>().text = "CHANCHAN";
            await Task.Delay(500);
            GetComponentInChildren<TMP_Text>().text = "Nation " + nationInTurn;
        }
    }
}