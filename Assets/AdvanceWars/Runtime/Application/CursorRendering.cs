using AdvanceWars.Runtime.Domain;

namespace AdvanceWars.Runtime.Application
{
    public class CursorRendering
    {
        readonly CursorView view;

        public CursorRendering(Game game, CursorView view)
        {
            this.view = view;
            game.CursorEnableChanged += HandleCursorRendering;
        }

        void HandleCursorRendering(bool enabled)
        {
            if(enabled)
                view.Show();
            else
                view.Hide();
        }
    }
}