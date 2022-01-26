using Syncfusion.Windows.Shared;
using System;

namespace LiveTracker.Models.Tournaments
{
    public class TemplateListBoxItem : NotificationObject
    {
        public double Buyin { get; set; }
        public string Description { get; set; }
        public string DisplayString { get; set; }
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public int TemplateId {get; set; }
        public string Venue { get; set; }

    }
}
