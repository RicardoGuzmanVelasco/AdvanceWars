using System.Threading.Tasks;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using RGV.DesignByContract.Runtime;
using UnityEngine;

namespace AdvanceWars.Runtime.Application
{
    public class OrderBattalion
    {
        readonly MoveBattalion moveBattalion;
        readonly WaitBattalion waitBattalion;
        readonly Game game;
        readonly Map map;
        readonly SelectSpace selectSpace;

        public OrderBattalion(MoveBattalion moveBattalion, WaitBattalion waitBattalion, Game game, Map map, SelectSpace selectSpace)
        {
            this.moveBattalion = moveBattalion;
            this.waitBattalion = waitBattalion;
            this.game = game;
            this.map = map;
            this.selectSpace = selectSpace;
        }

        public async Task Execute(Vector2Int targetPos)
        {
            Contract.Require(game.AnythingSelected).True();
            var selectedBattalion = game.SelectedBattalion;
            var originSpace = map.WhereIs(selectedBattalion)!;
            if (map.SpaceAt(targetPos) == originSpace)
                await waitBattalion.Execute();
            await moveBattalion.Execute(targetPos);
            await selectSpace.Deselect();
        }
    }
}