using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTracker.Models.Sessions
{
    public class SessionListBoxItem
    {
        public string Description { get; set; }
        public string DisplayString { get; set; }
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public int SessionId { get; set; }
        public DateTime StartTime { get; set; }
        public int TemplateId { get; set; }
    }
}
