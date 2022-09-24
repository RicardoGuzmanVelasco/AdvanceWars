using UnityEngine;

namespace AdvanceWars.Runtime.Application
{
    public interface CursorView
    {
        void MoveTo(Vector2Int position);

        void Show();
        void Hide();
    }
}