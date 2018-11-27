using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donatello.ViewModels
{
    public class MoveCardCommand
    {
        public int CardId { get; set; }
        public int ColumnId { get; set; }
    }
}
