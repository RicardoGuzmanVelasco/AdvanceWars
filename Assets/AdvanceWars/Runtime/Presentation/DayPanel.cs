using System.Threading.Tasks;
using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Domain.Orders;
using TMPro;
using UnityEngine;

namespace AdvanceWars.Runtime.Presentation
{
    public class DayPanel : MonoBehaviour, DayView
    {
        public async Task StartDay(int day)
        {
            GetComponentInChildren<TMP_Text>().text = "CHANCHAN";
            await Task.Delay(500);
            GetComponentInChildren<TMP_Text>().text = "Day " + day;
            
            
            foreach (var battalionView in Object.FindObjectsOfType<BattalionView>())
            {
                battalionView.ShowFresh();
            }
        }
    }
}