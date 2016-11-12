using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;

namespace TheEmpire.Client.MapModels
{
    public class Map
    {
        protected readonly Dictionary<Point, Cell> _cells = new Dictionary<Point, Cell>();
        protected const char WALL = '#';
        protected const char SPACE = ' ';
        protected const char COOKIE = '.';

        public IEnumerable<Cell> Cells
        {
            get { return _cells.Values; }
        }

        public Map(EnMapData mapData, IEnumerable<EnPoint> ghosts, EnPoint tacman)
        {
            for (int j = 0; j < mapData.Rows.Length; j++)
            {
                var row = mapData.Rows[j];
                {
                    for (int i = 0; i < row.Length; i++)
                    {

                        var symbol = Char.ToUpper(row[i]);
                        var isLast = symbol != WALL && i == row.Length - 1;

                        switch (symbol)
                        {
                            case WALL:
                                break;
                            case SPACE:
                                Add(i, j, Content.Empty, isLast);
                                break;
                            case COOKIE:
                                Add(i, j, Content.Cookie, isLast);
                                break;
                            //case PACMAN:
                            //    Add(i, j, Content.Pacman);
                            //    break;
                            //case GHOST:
                            //    Add(i, j, Content.Ghost);
                            //break;
                            default:
                                break;
                        }


                    }
                }
            }

            FillNeighbours();

            foreach (var ghost in ghosts)
                _cells[new Point(ghost.Col, ghost.Row)].Content = Content.Ghost;

            _cells[new Point(tacman.Col, tacman.Row)].Content = Content.Pacman;


        }

        protected void Add(int x, int y, Content content, bool isLast)
        {
            var point = new Point(x, y);
            var cell = new Cell() { Point = point, Content = content };
            _cells.Add(point, new Cell() { Point = point, Content = content });
            cell.IsLst = isLast;
            //CreateNeighbourRelations(cell, isLast);
        }

        protected void FillNeighbours()
        {
            foreach (var cell in _cells.Values)
            {
                CreateNeighbourRelations(cell);
            }
        }

        protected void CreateNeighbourRelations(Cell cell)
        {
            var left = new Point(cell.Point.X -1, cell.Point.Y);
            var top = new Point(cell.Point.X, cell.Point.Y -1);
            var right = new Point(cell.Point.X +1, cell.Point.Y);
            var bottom = new Point(cell.Point.X, cell.Point.Y + 1);
            var points = new List<Point> { left, top, right, bottom };
            foreach (var point in points)
            {
                if (_cells.ContainsKey(point))
                {
                    var neighbor = _cells[point];
                    cell.AddNeighbour(neighbor);
                    neighbor.AddNeighbour(cell);
                }
                else
                {
                    Console.WriteLine($"Neighbour not found [{point.X},{point.Y}] for [{cell.Point.X},{cell.Point.Y}] ");
                }
            }
            if (cell.IsLst)
            {
                var teleportPoint = new Point(0, cell.Point.Y);
                if (_cells.ContainsKey(teleportPoint))
                {
                    var neighbor = _cells[teleportPoint];
                    cell.AddNeighbour(neighbor);
                    neighbor.AddNeighbour(cell);
                }
            }
        }
    }

    public class Cell
    {
        private List<Cell> _neighbours = new List<Cell>();
        public Point Point { get; set; }
        public Content Content { get; set; }
        public bool IsLst { get; set; }

        public List<Cell> Neighbours
        {
            get { return _neighbours; }
        }

        public void AddNeighbour(Cell cell)
        {
            if (_neighbours.Any(c => c.Point.X == cell.Point.X && c.Point.Y == cell.Point.Y))
                return;

            _neighbours.Add(cell);
        }
    }

    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }
        public int Y { get; }
    }

    public enum Content
    {
        Cookie,
        Empty,
        Pacman,
        Ghost
    }
}
