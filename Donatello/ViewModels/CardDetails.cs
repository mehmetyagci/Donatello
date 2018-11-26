using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donatello.ViewModels
{
    public class CardDetails
    {
        public int Id { get; set; }

        public string Contents { get; set; }

        public string Notes { get; set; }

        public int Column { get; set; }
        public List<SelectListItem> Columns { get; set; } = new List<SelectListItem>();
    }
}
