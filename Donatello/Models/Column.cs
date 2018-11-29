using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Donatello.Models
{
    [Table("Column")]
    public class Column
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NotificationEmail { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();

        public int BoardId { get; set; }
    }
}
