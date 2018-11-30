using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donatello.ViewModels
{
    public class SetTitleCommand
    {
        public string Title { get; set; }
        public int BoardId { get; set; }
    }
}
