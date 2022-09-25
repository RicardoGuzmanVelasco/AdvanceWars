using System;
using UnityEngine;

namespace AdvanceWars.Runtime.Domain
{
    public class Cursor
    {
        bool enabled;
        public event Action<bool> EnableChanged = _ => { };

        public Vector2Int WhereIs { get; set; }

        public void Enable() => ChangeCursorTo(true);
        public void Disable() => ChangeCursorTo(false);

        void ChangeCursorTo(bool toEnabled)
        {
            if(enabled == toEnabled)
                return;
            
            enabled = toEnabled;

            EnableChanged.Invoke(enabled);
        }
    }
}