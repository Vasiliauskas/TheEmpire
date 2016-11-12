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
                var position = GetNextPosition(view.Map, view.GhostPositions[i], view.TecmanPosition, view.PreviousGhostPositions[i], view.PreviousTecmanPosition, map);
                resultMoves.Add(position);
            }

            return resultMoves;
        }

        private Position GetNextPosition(EnMapData mapRaw, EnPoint ghostPosition, EnPoint tecmanPosition, EnPoint previousGhostPosition, EnPoint previousTecmanPosition, Map map)
        {
            var ghostOnMap = map.Cells.Where(c => c.Point.X == ghostPosition.Col && c.Point.Y == ghostPosition.Row).FirstOrDefault();

            //first move
            if (previousGhostPosition.Col == ghostPosition.Col && previousGhostPosition.Row == ghostPosition.Row)
            {
                //Return any legal move
                var destinationCell = ghostOnMap.Neighbours.OrderBy(o => Guid.NewGuid()).FirstOrDefault();
                return new Position() { Col = destinationCell.Point.X, Row = destinationCell.Point.Y };
            }

            var xDirction =  ghostPosition.Col - previousGhostPosition.Col;
            var yDirection = ghostPosition.Row - previousGhostPosition.Row;

            //Changing X
            if (xDirction != 0)
            {
                var nextX = ghostPosition.Col + xDirction;
                var nextY = ghostPosition.Row;
                if (ghostOnMap.Neighbours.Any(c => c.Point.X == nextX && c.Point.Y == nextY))
                {
                    return new Position() { Col = nextX, Row = nextY };
                }
                else
                {
                    var nextMove = ghostOnMap.Neighbours.OrderBy(o => Guid.NewGuid()).FirstOrDefault();
                    //TODO: this is not legit move sometimes
                    return new Position() { Col = nextMove.Point.X, Row = nextMove.Point.Y };
                }
            }

            //Changing Y
            else 
            {
                var nextX = ghostPosition.Col;
                var nextY = ghostPosition.Row + yDirection;
                if (ghostOnMap.Neighbours.Any(c => c.Point.X == nextX && c.Point.Y == nextY))
                {
                    return new Position() { Col = nextX, Row = nextY };
                }
                else
                {
                    var nextMove = ghostOnMap.Neighbours.OrderBy(o => Guid.NewGuid()).FirstOrDefault();
                    return new Position() { Col = nextMove.Point.X, Row = nextMove.Point.Y };
                }
            }
        }

    }

}
