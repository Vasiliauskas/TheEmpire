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

            var resultMoves = new List<Position>();

            for (int i = 0; i < view.GhostPositions.Count(); i++)
            {
                var position = GetNextPosition(view.Map, view.GhostPositions[i], view.TecmanPosition, view.PreviousGhostPositions, view.PreviousTecmanPosition);
                resultMoves.Add(position);
            }

            return resultMoves;
        }

        private Position GetNextPosition(EnMapData map, EnPoint enPoint, EnPoint tecmanPosition, EnPoint[] previousGhostPositions, EnPoint previousTecmanPosition)
        {
            throw new NotImplementedException();
        }

    }


    public static class CellExts
    {

    }
}
