using AdvanceWars.Runtime.Application;
using UnityEngine;

namespace AdvanceWars.Runtime.Presentation
{
    public class Selector : MonoBehaviour, CursorView
    {
        public void MoveTo(Vector2Int position)
        {
            transform.position = new Vector3(position.x, position.y, 0);
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }

        public void Hide()
        {
            throw new System.NotImplementedException();
        }
    }
}