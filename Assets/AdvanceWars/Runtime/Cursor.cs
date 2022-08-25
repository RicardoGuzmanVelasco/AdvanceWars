using System;

namespace AdvanceWars.Runtime
{
    internal class Cursor
    {
        public event Action<bool> CursorEnableChanged = _ => { };

        bool Enabled { get; set; }

        public void EnableCursor()
        {
            if(Enabled)
                return;

            Enabled = true;
            CursorEnableChanged.Invoke(Enabled);
        }

        public void DisableCursor()
        {
            if(!Enabled)
                return;

            Enabled = false;
            CursorEnableChanged.Invoke(Enabled);
        }
    }
}