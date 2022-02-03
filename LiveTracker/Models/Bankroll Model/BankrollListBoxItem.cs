using Syncfusion.Windows.Shared;

namespace Tournament_Life.Models.Bankroll_Model
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
