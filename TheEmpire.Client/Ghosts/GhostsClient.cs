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
        private List<SimpleGhost> _ghosts = new List<SimpleGhost>();

        public List<Position> PerformMove(GetPlayerViewResp view)
        {
            var map = new Map(view.Map);
            
            var resultMoves = new List<Position>();

            

            var ghosts = map.Cells.Where(c => c.Content == Content.Ghost).ToList();
            for(int i = 0; i < ghosts.Count(); i++)
            {
                if(_ghosts[i] == null)
                {
                    _ghosts[i] = new SimpleGhost();
                }
            }

            var tacman = map.Cells.Where(c => c.Content == Content.Pacman).FirstOrDefault();
            if (tacman == null)
            {
                Console.WriteLine("No Tacman");
            }
            
            
            var response = new List<Position>();
            response.Add(new Position() { Col = targetCell.Point.X, Row = targetCell.Point.Y });

            return response;
        }
    }

    //public class SimpleGhost
    //{
    //    public Cell PreviousPosition { get; set; }
        
    //    public Position GetNextPosion(Map map, Cell currentPosition)
    //    {
    //        throw new NotImplementedException();
    //        PreviousPosition = currentPosition;
    //    }
    //}

    public static class CellExts
    {
        
    }
}
