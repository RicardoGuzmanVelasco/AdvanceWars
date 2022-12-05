using System.Threading.Tasks;
using AdvanceWars.Runtime.Application;
using TMPro;
using UnityEngine;

namespace AdvanceWars.Runtime.Presenters
{
    public class PlayerAmountText : MonoBehaviour, PlayersConfigurationView
    {
        public Task SetPlayers(int playerAmount)
        {
            GetComponent<TMP_Text>().text = "Players: " + playerAmount;
            return Task.CompletedTask;
        }
    }
}