﻿using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.Application
{
    public abstract class CursorView
    {
        public abstract void MoveTo(Vector2Int position);
    }

    public class MoveCursorController
    {
        readonly Game game;

        public MoveCursorController(Game game, CursorView view)
        {
            this.game = game;
            game.CursorMoved += view.MoveTo;
        }

        public void Towards(Vector2Int direction)
        {
            Require(direction.IsDirection()).True();
            game.MoveCursorTowards(direction);
        }
    }
}