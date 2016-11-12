using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheEmpire.Client.DTO;

namespace TheEmpire.Client
{
    class Ghost
    {
        public PerformMoveRequest PerformMove(GetPlayerViewResp view)
        {
            ///TODO hre things
        }

        static void Test()
        {
            var dto = new DTO.EnMapData();
            dto.Rows = new List<string>();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Maps\\classic.map.txt");

            foreach (var row in File.ReadAllLines(path))
            {
                Console.WriteLine(row);
                dto.Rows.Add(row);
            }

            Console.WriteLine("Done");

            var map = new Map(dto);
            var rowNums = map.Cells.Select(c => c.Point.Y).Distinct().OrderBy(c => c);

            foreach (var y in rowNums)
            {
                var rowString = "";
                var cells = map.Cells.Where(c => c.Point.Y == y);
                var maxX = cells.Max(c => c.Point.X);

                for (int i = 0; i < maxX; i++)
                {
                    var cell = cells.SingleOrDefault(c => c.Point.X == i);
                    if (cell == null)
                        rowString += "#";
                    else
                    {
                        switch (cell.Content)
                        {
                            case Content.Cookie:
                                rowString += ".";
                                break;
                            case Content.Empty:
                                rowString += " ";
                                break;
                            case Content.Pacman:
                                rowString += "C";
                                break;
                            case Content.Ghost:
                                rowString += "H";
                                break;
                            default:
                                break;
                        }
                    }
                }
                Console.WriteLine(rowString);
            }
            Console.ReadLine();
            //var client = new TacManClient(ConfigurationManager.AppSettings["connection"]);
        }
    }
}
