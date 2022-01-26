using System;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Models
{
    public class ListBoxItem : NotificationObject
    {
        public decimal Buyin { get; set; }
        public string Description { get; set; }
        public int Id{get; set; }
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public string Venue { get; set; }

        public ListBoxItem Copy()
        {
            return new ListBoxItem
            {
                IsSelected = IsSelected,
                Name = Name,
                Id = Id,
                Description = Description,
                Buyin = Buyin,
                StartTime = StartTime,
                Venue = Venue,
            };
        }
    }
}
