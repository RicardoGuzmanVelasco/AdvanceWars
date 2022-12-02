using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;

namespace AdvanceWars.Runtime.Application
{
    public class SelectBattalion
    {
        readonly Game game;
        readonly Map map;
        readonly SelectionView selectionView;
        readonly MoveBattalion moveBattalion;

        public SelectBattalion(Game game, Map map, SelectionView selectionView, MoveBattalion moveBattalion)
        {
            this.game = game;
            this.map = map;
            this.selectionView = selectionView;
            this.moveBattalion = moveBattalion;
        }

        public Task Select()
        {
            if(game.AnythingSelected)
                return moveBattalion.Execute(game.CursorCoord);

            game.SelectAtCursor();

            return selectionView.Show(game.CursorCoord);
        }

        public Task Deselect()
        {
            game.Deselect();
            return selectionView.Hide();
        }
    }
}