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
        protected const char GHOST = 'H';
        protected const char PACMAN = 'C';

        public IEnumerable<Cell> Cells
        {
            get { return _cells.Values; }
        }

        public Map(EnMapData mapData)
        {
            for (int j = 0; j < mapData.Rows.Count; j++)
            {
                var row = mapData.Rows[j];
                {
                    for (int i = 0; i < row.Length; i++)
                    {
                        var symbol = Char.ToUpper(row[i]);
                        switch (symbol)
                        {
                            case WALL:
                                break;
                            case SPACE:
                                Add(i, j, Content.Empty);
                                break;
                            case COOKIE:
                                Add(i, j, Content.Cookie);
                                break;
                            case PACMAN:
                                Add(i, j, Content.Pacman);
                                break;
                            case GHOST:
                                Add(i, j, Content.Ghost);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        protected void Add(int x, int y, Content content)
        {
            var point = new Point(x, y);
            var cell = new Cell() { Point = point, Content = content };
            _cells.Add(point, new Cell() { Point = point, Content = content });
            CreateNeighbourRelations(cell);
        }

        protected void CreateNeighbourRelations(Cell cell)
        {
            int left = cell.Point.X - 1;
            int top = cell.Point.Y - 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 1 && i == 1)
                        break;

                    var point = new Point(i, j);
                    if (_cells.ContainsKey(point))
                    {
                        var neighbor = _cells[point];
                        cell.AddNeighbour(neighbor);
                        neighbor.AddNeighbour(cell);
                    }
                }
            }
        }
    }

    public class Cell
    {
        private List<Cell> _neighbours = new List<Cell>();
        public Point Point { get; set; }
        public Content Content { get; set; }

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
