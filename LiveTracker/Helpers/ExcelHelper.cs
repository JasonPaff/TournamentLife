using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SwiftExcel;
using System.Windows;
using System.Globalization;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace Tournament_Life.Helpers
{
    public static class ExcelHelper
    {
        public static void ExportToExcel(ObservableCollection<TournamentFinished> tournaments)
        {
            // null check
            if (tournaments is null) return;

            // save file dialog box
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Excel file (*.xlsx)|*.xlsx" };

            // show dialog, return if canceled
            if (saveFileDialog.ShowDialog() is false) return;

            // create new sheet
            var sheet = new Sheet
            {
                Name = "Results",
                ColumnsWidth = new List<double> { 50, 30, 30, 20, 20, 20, 50, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 }
            };

            // excel write
            ExcelWriter excelWriter = new ExcelWriter(saveFileDialog.FileName, sheet);

            // column headers
            excelWriter.Write("Tournament Name", 1, 1);
            excelWriter.Write("Start Time", 2, 1);
            excelWriter.Write("End Time", 3, 1);
            excelWriter.Write("Length", 4, 1);
            excelWriter.Write("Poker Site", 5, 1);
            excelWriter.Write("Game Type", 6, 1);
            excelWriter.Write("Format(s)", 7, 1);
            excelWriter.Write("Guarantee", 8, 1);
            excelWriter.Write("Entrants", 9, 1);
            excelWriter.Write("EntrantsPaid", 10, 1);
            excelWriter.Write("Finish #", 11, 1);
            excelWriter.Write("Prize Won", 12, 1);
            excelWriter.Write("Total Cost", 13, 1);
            excelWriter.Write("Profit", 14, 1);
            excelWriter.Write("Table Size", 15, 1);
            excelWriter.Write("Blind Levels", 16, 1);
            excelWriter.Write("Late Reg", 17, 1);
            excelWriter.Write("Buyin", 18, 1);
            excelWriter.Write("Buyin Rake", 19, 1);
            excelWriter.Write("Buyin Total", 20, 1);
            excelWriter.Write("Rebuy", 21, 1);
            excelWriter.Write("Rebuy Rake", 22, 1);
            excelWriter.Write("Rebuy Total", 23, 1);
            excelWriter.Write("Addon", 24, 1);
            excelWriter.Write("Addon Rake", 25, 1);
            excelWriter.Write("Addon Total", 26, 1);
            excelWriter.Write("Rebuy Count", 27, 1);
            excelWriter.Write("Addon Count", 28, 1);
            excelWriter.Write("Starting Stack", 29, 1);
            excelWriter.Write("Rebuy Stack", 30, 1);
            excelWriter.Write("Addon Stack", 31, 1);

            // loop for rows
            int rowNumber = 2;
            foreach (TournamentFinished tournament in tournaments)
            {
                excelWriter.Write(tournament.TournamentName, 1, rowNumber);
                excelWriter.Write(tournament.StartTime.ToString(new CultureInfo("en-US")), 2, rowNumber);
                excelWriter.Write(tournament.EndTime.ToString(new CultureInfo("en-US")), 3, rowNumber);
                excelWriter.Write(tournament.Length.ToString(), 4, rowNumber);
                excelWriter.Write(tournament.Venue, 5, rowNumber);
                excelWriter.Write(tournament.GameType, 6, rowNumber);
                excelWriter.Write(tournament.FormatString, 7, rowNumber);
                excelWriter.Write(tournament.Guarantee.ToString(new CultureInfo("en-US")), 8, rowNumber);
                excelWriter.Write(tournament.Entrants.ToString(new CultureInfo("en-US")), 9, rowNumber);
                excelWriter.Write(tournament.EntrantsPaid.ToString(new CultureInfo("en-US")), 10, rowNumber);
                excelWriter.Write(tournament.FinishPosition.ToString(new CultureInfo("en-US")), 11, rowNumber);
                excelWriter.Write(tournament.PrizeWon.ToString(new CultureInfo("en-US")), 12, rowNumber);
                excelWriter.Write(tournament.TotalCost.ToString(new CultureInfo("en-US")), 13, rowNumber);
                excelWriter.Write(tournament.Profit.ToString(new CultureInfo("en-US")), 14, rowNumber);
                excelWriter.Write(tournament.TableSize.ToString(new CultureInfo("en-US")), 15, rowNumber);
                excelWriter.Write(tournament.BlindLevels.ToString(new CultureInfo("en-US")), 16, rowNumber);
                excelWriter.Write(tournament.LateReg.ToString(new CultureInfo("en-US")), 17, rowNumber);
                excelWriter.Write(tournament.BuyinBaseCost.ToString(new CultureInfo("en-US")), 18, rowNumber);
                excelWriter.Write(tournament.BuyinRakeCost.ToString(new CultureInfo("en-US")), 19, rowNumber);
                excelWriter.Write(tournament.BuyinTotalCost.ToString(new CultureInfo("en-US")), 20, rowNumber);
                excelWriter.Write(tournament.RebuyBaseCost.ToString(new CultureInfo("en-US")), 21, rowNumber);
                excelWriter.Write(tournament.RebuyRakeCost.ToString(new CultureInfo("en-US")), 22, rowNumber);
                excelWriter.Write(tournament.RebuyTotalCost.ToString(new CultureInfo("en-US")), 23, rowNumber);
                excelWriter.Write(tournament.AddonBaseCost.ToString(new CultureInfo("en-US")), 24, rowNumber);
                excelWriter.Write(tournament.AddonRakeCost.ToString(new CultureInfo("en-US")), 25, rowNumber);
                excelWriter.Write(tournament.AddonTotalCost.ToString(new CultureInfo("en-US")), 26, rowNumber);
                excelWriter.Write(tournament.RebuyCount.ToString(new CultureInfo("en-US")), 27, rowNumber);
                excelWriter.Write(tournament.AddonCount.ToString(new CultureInfo("en-US")), 28, rowNumber);
                excelWriter.Write(tournament.StackSizeStarting.ToString(new CultureInfo("en-US")), 29, rowNumber);
                excelWriter.Write(tournament.StackSizeRebuy.ToString(new CultureInfo("en-US")), 30, rowNumber);
                excelWriter.Write(tournament.StackSizeAddon.ToString(new CultureInfo("en-US")), 31, rowNumber);

                rowNumber++;
            }

            // close excel writer
            excelWriter.Dispose();
        }
    }
}
