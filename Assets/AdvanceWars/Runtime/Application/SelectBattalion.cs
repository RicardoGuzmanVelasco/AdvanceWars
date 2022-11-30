using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;

namespace AdvanceWars.Runtime.Application
{
    public class SelectBattalion
    {
        readonly Game game;
        readonly Map map;
        readonly SelectionView selectionView;
        readonly MoveBattalion moveBattalion;

        Battalion selected = Battalion.Null;
        public SelectBattalion(Game game, Map map, SelectionView selectionView, MoveBattalion moveBattalion)
        {
            this.game = game;
            this.map = map;
            this.selectionView = selectionView;
            this.moveBattalion = moveBattalion;
        }

        public Task Select()
        {
            if (selected is not INull) return moveBattalion.Execute(selected, game.CursorCoord);
            
            selected = map.SpaceAt(game.CursorCoord).Occupant;
                
            return !map.SpaceAt(game.CursorCoord).IsOccupied
                ? Task.CompletedTask
                : selectionView.Show(game.CursorCoord);

        }

        public Task Deselect()
        {
            selected = Battalion.Null;
            return selectionView.Hide();
        }
    }
}