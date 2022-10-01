using AdvanceWars.Runtime.Domain;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Application
{
    public class CursorController
    {
        readonly Game game;
        readonly CursorView view;

        public CursorController(Game game, CursorView view)
        {
            this.game = game;
            this.view = view;

            game.CursorMoved += view.MoveTo;
            game.CursorEnableChanged += HandleCursorRendering;
        }

        public void Towards(Vector2Int direction)
        {
            Require(direction.IsDirection()).True();

            if(!game.CursorIsEnabled)
                return;

            game.MoveCursorTowards(direction);
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