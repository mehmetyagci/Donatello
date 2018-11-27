using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donatello.ViewModels
{
    public class SetColorCommand
    {
        public string Color { get; set; }
        public int BoardId { get; set; }
    }
}
