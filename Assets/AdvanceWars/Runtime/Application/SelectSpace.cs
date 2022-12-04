using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;

namespace AdvanceWars.Runtime.Application
{
    public class SelectSpace
    {
        readonly Game game;
        readonly Map map;
        readonly SelectionView selectionView;

        public SelectSpace(Game game, Map map, SelectionView selectionView)
        {
            this.game = game;
            this.map = map;
            this.selectionView = selectionView;
        }

        public Task Select()
        {
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