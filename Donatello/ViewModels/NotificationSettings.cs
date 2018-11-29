using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donatello.ViewModels
{
    public class NotificationSettings
    {
        public int BoardId { get; set; }
        public int ColumnId { get; set; }
        public string EmailAddress { get; set; }
    }
}
