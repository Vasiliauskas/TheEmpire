using System;
using System.Collections.Generic;

namespace TheEmpire.Client.DTO
{
    public class EnMapData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<String> Rows { get; set; }
    }
    public class EnPoint
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }
}
