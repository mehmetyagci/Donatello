using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donatello.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();
    }
}
