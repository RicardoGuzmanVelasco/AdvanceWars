using AdvanceWars.Runtime.Domain;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Application
{
    public class CursorMovement
    {
        readonly Game game;
        bool cursorIsEnabled = true;

        public CursorMovement(Game game, CursorView view)
        {
            this.game = game;
            game.CursorMoved += view.MoveTo;
            game.CursorEnableChanged += value => cursorIsEnabled = value;
        }

        public void Towards(Vector2Int direction)
        {
            Require(direction.IsDirection()).True();

            if(!cursorIsEnabled)
                return;

            game.MoveCursorTowards(direction);
        }
    }
}