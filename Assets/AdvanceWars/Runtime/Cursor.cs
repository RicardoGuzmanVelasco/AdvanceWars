using System;

namespace AdvanceWars.Runtime
{
    internal class Cursor
    {
        bool enabled;
        public event Action<bool> EnableChanged = _ => { };

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