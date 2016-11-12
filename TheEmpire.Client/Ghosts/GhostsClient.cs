using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;
using TheEmpire.Client.MapModels;

namespace TheEmpire.Client.Ghosts
{
    class GhostClient
    {
        public List<Position> PerformMove(GetPlayerViewResp view)
        {
            var map = new Map(view.Map, view.GhostPositions, view.TecmanPosition);
            var cell = map.Cells.Where(c => c.Content == Content.Ghost).Single();
            var targetCell = cell.Neighbours.First();

            var response = new List<Position>();
            response.Add(new Position() { Col = targetCell.Point.X, Row = targetCell.Point.Y });

            return response;
        }
    }
}
