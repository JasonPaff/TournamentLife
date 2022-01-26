using LiveTracker.Helpers;
using LiveTracker.Models;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Tournament_Life.Views.Datagrid_Views;

namespace Tournament_Life.ViewModels.Datagrid_ViewModels
{
    public class FinishTournamentViewModel : NotificationObject
    {
        public FinishTournamentViewModel(TournamentRunning tournament)
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // blue font color
            TournamentNameColor = new SolidColorBrush(Color.FromRgb(5, 190, 255));

            // copy tournament
            Tournament = tournament;

            // flag for add-ons
            if (Tournament.AddonCount > 0 || Tournament.AddonBaseCost > 0) ShowAddon = true;

            // flag for rebuys
            if (Tournament.RebuyCount > 0 || Tournament.RebuyBaseCost > 0) ShowRebuy = true;

            // flag for bounties
            if (Tournament.BountyCount > 0 || Tournament.Bounty > 0) ShowBounty = true;

            // flag for jackpot multiplier
            if (Tournament.JackpotSpinMultiplier > 0) ShowJackpot = true;

            // set tool tip
            TournamentToolTip = tournament.DatabaseDescription;

            // initialize color brushes
            InitBrushes();
        }

        public int Addon => Tournament.AddonCount;
        public decimal AddonCost => Tournament.AddonBaseCost;
        public SolidColorBrush AddonCostColor { get; set; }
        public SolidColorBrush AddonCountColor { get; set; }
        public decimal AddonTotalCost => Tournament.AddonTotalCost * Tournament.AddonCount;
        public SolidColorBrush AddonTotalCostColor { get; set; }
        public decimal BountyPrizeWon => Tournament.Bounty;
        public decimal BountyTotalPrizeWon => BountyPrizeWon * BountyWonCount;
        public SolidColorBrush BountyTotalPrizeColor { get; set; }
        public decimal BountyWonCount => Tournament.BountyCount;
        public SolidColorBrush BountyWonColor { get; set; }
        public decimal BuyinCost => Tournament.BuyinTotalCost;
        public SolidColorBrush BuyinCostColor { get; set; }
        public DateTime EndTime => Tournament.EndTime;
        public int Entrants => Tournament.Entrants;
        public SolidColorBrush EntrantsColor { get; set; }
        public int EntrantsPaid => Tournament.EntrantsPaid;
        public SolidColorBrush EntrantsPaidColor { get; set; }
        public int FinishPosition => Tournament.FinishPosition;
        public SolidColorBrush FinishPositionColor { get; set; }
        public int FontSize { get; set; }
        public int JackpotMultiplier => Tournament.JackpotSpinMultiplier;
        public SolidColorBrush JackpotColor { get; set; }
        public string Name => Tournament.TournamentName;
        public decimal PrizeWon => Tournament.PrizeWon;
        public SolidColorBrush PrizeWonColor { get; set; }
        public decimal Profit => Tournament.PrizeWon - Tournament.TotalCost;
        public SolidColorBrush ProfitColor { get; set; }
        public int Rebuy => Tournament.RebuyCount;
        public decimal RebuyCost => Tournament.RebuyBaseCost;
        public SolidColorBrush RebuyCostColor { get; set; }
        public SolidColorBrush RebuyCountColor { get; set; }
        public decimal RebuyTotalCost => Tournament.RebuyTotalCost * Tournament.RebuyCount;
        public SolidColorBrush RebuyTotalCostColor { get; set; }
        public bool Saved { get; set; } = false;
        public bool ShowAddon { get; set; }
        public bool ShowAddonBaseCost { get; set; }
        public bool ShowAddonTotalCost { get; set; }
        public bool ShowBounty { get; set; }
        public bool ShowJackpot { get; set; }
        public bool ShowRebuy { get; set; }
        public bool ShowRebuyBaseCost { get; set; }
        public bool ShowRebuyTotalCost { get; set; }
        public DateTime StartTime => Tournament.StartTime;
        public string Theme { get; set; }
        public decimal TotalCost => Tournament.TotalCost;
        public SolidColorBrush TotalCostColor { get; set; }
        public SolidColorBrush TotalPrizeColor { get; set; }
        public TournamentRunning Tournament { get; set; }
        public SolidColorBrush TournamentNameColor { get; set; }
        public string TournamentToolTip { get; set; }
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand SaveCommand => new BaseCommand(Save);

        /// <summary>
        /// Cancel any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<FinishTournamentView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// initialize color brushes
        /// </summary>
        private void InitBrushes()
        {
            var blueBrush = new SolidColorBrush(Color.FromRgb(100, 149, 237));
            var greenBrush = new SolidColorBrush(Color.FromRgb(4, 130, 40));
            var redBrush = new SolidColorBrush(Color.FromRgb(205, 50, 50));
            var whiteBrush = new SolidColorBrush(Color.FromRgb(232, 232, 232));

            // addon count color
            if (Addon == 0) AddonCountColor = whiteBrush;
            else if (Addon > 0) AddonCountColor = blueBrush;

            // addon total cost color
            if (AddonTotalCost == 0) AddonTotalCostColor = whiteBrush;
            else if (AddonTotalCost > 0) AddonTotalCostColor = redBrush;

            // buyin color
            if (BuyinCost >= 0) BuyinCostColor = redBrush;

            // bounty won color
            if (BountyWonCount == 0) BountyWonColor = whiteBrush;
            else if (BountyWonCount > 0) BountyWonColor = blueBrush;

            // bounty total color
            if (BountyTotalPrizeWon == 0) BountyTotalPrizeColor = whiteBrush;
            else if (BountyTotalPrizeWon > 0) BountyTotalPrizeColor = greenBrush;

            // entrants color
            if (Entrants == 0) EntrantsColor = redBrush;
            else if (Entrants > 0) EntrantsColor = blueBrush;

            // entrants paid color
            if (EntrantsPaid == 0) EntrantsPaidColor = redBrush;
            else if (EntrantsPaid > 0) EntrantsPaidColor = blueBrush;

            // finish position color
            if (FinishPosition == 0) FinishPositionColor = redBrush;
            else if (FinishPosition > 0) FinishPositionColor = blueBrush;

            // jackpot spin multiplier
            if (JackpotMultiplier == 0) JackpotColor = whiteBrush;
            else if (JackpotMultiplier > 0) JackpotColor = blueBrush;

            // prize won color
            if (PrizeWon == 0) PrizeWonColor = whiteBrush;
            else if (PrizeWon > 0) PrizeWonColor = greenBrush;

            // set profit font color based on profit won or lost
            if (Profit == 0) ProfitColor = whiteBrush;
            else if (Profit > 0) ProfitColor = greenBrush;
            else if (Profit < 0) ProfitColor = redBrush;

            // rebuy count color
            if (Rebuy == 0) RebuyCountColor = whiteBrush;
            else if (Rebuy > 0) RebuyCountColor = blueBrush;

            // rebuy total cost color
            if (RebuyTotalCost == 0) RebuyTotalCostColor = whiteBrush;
            else if (RebuyTotalCost > 0) RebuyTotalCostColor = redBrush;

            // total cost color
            if (TotalCost == 0) TotalCostColor = whiteBrush;
            else if (TotalCost > 0) TotalCostColor = redBrush;

            // total prize color
            if (PrizeWon <= 0) TotalPrizeColor = whiteBrush;
            else if (PrizeWon > 0) TotalPrizeColor = greenBrush;
        }

        /// <summary>
        /// Save any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // saved
            Saved = true;

            // close window
            Cancel(null);
        }
    }
}
