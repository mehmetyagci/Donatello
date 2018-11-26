using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donatello.Models
{
    public class Column
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();

        public int BoardId { get; set; }
    }
}
