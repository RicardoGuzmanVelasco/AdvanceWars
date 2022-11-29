using System.Threading.Tasks;
using AdvanceWars.Runtime.Application;
using TMPro;
using UnityEngine;

namespace AdvanceWars.Runtime.Presentation
{
    public class SelectionArea : MonoBehaviour, SelectionView
    {
        void Awake()
        {
            GetComponent<TMP_Text>().text = "";
        }

        public Task Show(Vector2Int position)
        {
            GetComponent<TMP_Text>().text = position.ToString();
            return Task.CompletedTask;
        }

        public Task Hide()
        {
            GetComponent<TMP_Text>().text = "";
            return Task.CompletedTask;
        }
    }
}