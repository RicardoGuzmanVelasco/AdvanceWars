using AdvanceWars.Runtime.Domain;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Application
{
    public class CursorMovement
    {
        readonly Game game;

        public CursorMovement(Game game, CursorView view)
        {
            this.game = game;
            game.CursorMoved += view.MoveTo;
        }

        public void Towards(Vector2Int direction)
        {
            Require(direction.IsDirection()).True();

            if(!game.CursorIsEnabled)
                return;

            game.MoveCursorTowards(direction);
        }
    }
}