using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Models.Bankroll_Model
{
    public class BankrollListBoxItem : NotificationObject
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public BankrollListBoxItem Copy()
        {
            var listBoxItem = new BankrollListBoxItem()
            {
                IsSelected = IsSelected,
                Name = Name,
            };
            return listBoxItem;
        }
    }
}
