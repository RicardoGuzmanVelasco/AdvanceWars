using AdvanceWars.Runtime.Application;
using DG.Tweening;
using UnityEngine;

namespace AdvanceWars.Runtime.Presentation
{
    public class Selector : MonoBehaviour, CursorView
    {
        void Start()
        {
            Show();
        }

        public void MoveTo(Vector2Int position)
        {
            transform.position = new Vector3(position.x, position.y, 0);
        }

        public void Show()
        {
            DOTween.Sequence(this)
                .Append(transform.DOScale(0.8f, 0.67f).SetLoops(1, LoopType.Yoyo))
                .AppendInterval(0.4f)
                .SetLoops(-1);
        }

        public void Hide()
        {
            DOTween.CompleteAll(this);
        }
    }
}