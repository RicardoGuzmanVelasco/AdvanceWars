using System;
using UnityEngine;

namespace AdvanceWars.Runtime.Domain
{
    public class Cursor
    {
        public bool IsEnabled { get; private set; } = true;
        public Vector2Int WhereIs { get; set; }

        public event Action<bool> EnableChanged = _ => { };

        public void Enable() => ChangeCursorTo(true);
        public void Disable() => ChangeCursorTo(false);

        void ChangeCursorTo(bool toEnabled)
        {
            if(IsEnabled == toEnabled)
                return;

            IsEnabled = toEnabled;

            EnableChanged.Invoke(IsEnabled);
        }
    }
}