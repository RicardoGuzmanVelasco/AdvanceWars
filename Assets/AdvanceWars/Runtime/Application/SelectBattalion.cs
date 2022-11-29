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

        public SelectBattalion(Game game, Map map, SelectionView selectionView)
        {
            this.game = game;
            this.map = map;
            this.selectionView = selectionView;
        }

        public Task Select()
        {
            return !map.SpaceAt(game.CursorCoord).IsOccupied
                ? Task.CompletedTask
                : selectionView.Show(game.CursorCoord);
        }

        public Task Deselect()
        {
            return selectionView.Hide();
        }
    }
}