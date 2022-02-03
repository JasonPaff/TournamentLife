using System;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Tournament_Life.Enums;

namespace Tournament_Life.Models.Tournaments
{

    public class TournamentRunning : TournamentTemplate
    {
        private int _addonCount = 0;
        private int _bountyCount = 0;
        private string _currencySymbol = "$";
        private DateTime _endTime;
        private int _entrants = 0;
        private int _entrantsPaid = 0;
        private int _finishPosition = 0;
        private int _jackpotSpinMultiplier = 0;
        private decimal _prizeWon = 0;
        private int _rebuyCount = 0;
        private DateTime _startTime;
        private TournamentVisibility _tournamentStartingVisibility;

        public TournamentRunning(TournamentTemplate template)
        {
            if (template is null)
                return;

            AddonBaseCost = template.AddonBaseCost;
            AddonCount = 0;
            AddonRakeCost = template.AddonRakeCost;
            BlindLevels = template.BlindLevels;
            Bounty = template.Bounty;
            BountyCount = 0;
            BuyinBaseCost = template.BuyinBaseCost;
            BuyinRakeCost = template.BuyinRakeCost;
            EndTime = template.StartTime;
            Entrants = template.Entrants;
            EntrantsPaid = template.EntrantsPaid;
            FinishPosition = 0;
            Formats = template.Formats;
            GameType = template.GameType;
            Guarantee = template.Guarantee;
            IsBovadaBounty = template.IsBovadaBounty;
            IsFavorite = template.IsFavorite;
            IsSng = template.IsSng;
            LateReg = template.LateReg;
            Note = "";
            PrizeWon = 0;
            RebuyBaseCost = template.RebuyBaseCost;
            RebuyCount = 0;
            RebuyRakeCost = template.RebuyRakeCost;
            ScreenshotFilename = "";
            SngPayouts = template.SngPayouts;
            StartTime = template.StartTime;
            StackSizeAddon = template.StackSizeAddon;
            StackSizeRebuy = template.StackSizeRebuy;
            StackSizeStarting = template.StackSizeStarting;
            TableSize = template.TableSize;
            TemplateId = template.TemplateId;
            TimeZoneName = template.TimeZoneName;
            TournamentName = template.TournamentName;
            Venue = template.Venue;

            AdjustStartTime();
            SetInitialTournamentStatus();
        }
        public TournamentRunning(TournamentFinished tournament)
        {
            if (tournament is null) 
                return;

            AddonBaseCost = tournament.AddonBaseCost;
            AddonCount = tournament.AddonCount;
            AddonRakeCost = tournament.AddonRakeCost;
            BlindLevels = tournament.BlindLevels;
            Bounty = tournament.Bounty;
            BountyCount = tournament.BountyCount;
            BuyinBaseCost = tournament.BuyinBaseCost;
            BuyinRakeCost = tournament.BuyinRakeCost;
            DatabaseId = tournament.DatabaseId;
            EndTime = tournament.EndTime;
            Entrants = tournament.Entrants;
            EntrantsPaid = tournament.EntrantsPaid;
            FinishPosition = tournament.FinishPosition;
            Formats = tournament.Formats;
            GameType = tournament.GameType;
            Guarantee = tournament.Guarantee;
            IsBovadaBounty = tournament.IsBovadaBounty;
            IsFavorite = tournament.IsFavorite;
            IsSng = tournament.IsSng;
            JackpotSpinMultiplier = tournament.JackpotSpinMultiplier;
            LateReg = tournament.LateReg;
            Note = tournament.Note;
            PrizeWon = tournament.PrizeWon;
            RebuyBaseCost = tournament.RebuyBaseCost;
            RebuyCount = tournament.RebuyCount;
            RebuyRakeCost = tournament.RebuyRakeCost;
            ScreenshotFilename = tournament.ScreenshotFilename;
            SngPayouts = tournament.SngPayouts;
            StartTime = tournament.StartTime;
            StackSizeAddon = tournament.StackSizeAddon;
            StackSizeRebuy = tournament.StackSizeRebuy;
            StackSizeStarting = tournament.StackSizeStarting;
            TableSize = tournament.TableSize;
            TemplateId = tournament.TemplateId;
            TimeZoneName = tournament.TimeZoneName;
            TournamentName = tournament.TournamentName;
            Venue = tournament.Venue;

            AdjustStartTime();
            SetInitialTournamentStatus();
        }
        public TournamentRunning(TournamentRunning tournament)
        {
            if (tournament is null) 
                return;

            AddonBaseCost = tournament.AddonBaseCost;
            AddonCount = tournament.AddonCount;
            AddonRakeCost = tournament.AddonRakeCost;
            BlindLevels = tournament.BlindLevels;
            Bounty = tournament.Bounty;
            BountyCount = tournament.BountyCount;
            BuyinBaseCost = tournament.BuyinBaseCost;
            BuyinRakeCost = tournament.BuyinRakeCost;
            DatabaseId = tournament.DatabaseId;
            EndTime = tournament.EndTime;
            Entrants = tournament.Entrants;
            EntrantsPaid = tournament.EntrantsPaid;
            FinishPosition = tournament.FinishPosition;
            Formats = tournament.Formats;
            GameType = tournament.GameType;
            Guarantee = tournament.Guarantee;
            IsBovadaBounty = tournament.IsBovadaBounty;
            IsFavorite = tournament.IsFavorite;
            IsSng = tournament.IsSng;
            JackpotSpinMultiplier = tournament.JackpotSpinMultiplier;
            LateReg = tournament.LateReg;
            Note = tournament.Note;
            PrizeWon = tournament.PrizeWon;
            RebuyBaseCost = tournament.RebuyBaseCost;
            RebuyCount = tournament.RebuyCount;
            RebuyRakeCost = tournament.RebuyRakeCost;
            ScreenshotFilename = tournament.ScreenshotFilename;
            SngPayouts = tournament.SngPayouts;
            StartTime = tournament.StartTime;
            StackSizeAddon = tournament.StackSizeAddon;
            StackSizeRebuy = tournament.StackSizeRebuy;
            StackSizeStarting = tournament.StackSizeStarting;
            TableSize = tournament.TableSize;
            TemplateId = tournament.TemplateId;
            TimeZoneName = tournament.TimeZoneName;
            TournamentName = tournament.TournamentName;
            Venue = tournament.Venue;

            AdjustStartTime();
            SetInitialTournamentStatus();
        }
        public TournamentRunning()
        {
            SetInitialTournamentStatus();
        }

        public int AddonCount
        {
            get => _addonCount;
            set
            {
                _addonCount = value;
                UpdateProperties?.Execute(null);
                RaisePropertyChanged(nameof(AddonCount));
            }
        }
        public string AddonCountDescription
        {
            get
            {
                // description string builder
                var sb = new StringBuilder();

                sb.AppendLine($"Addon Base: {AddonBaseCost.ToString("C2", new CultureInfo("en-US"))}");
                sb.AppendLine($"Addon Rake: {AddonRakeCost.ToString("C2", new CultureInfo("en-US"))}");
                sb.AppendLine();
                sb.AppendLine($"Addon Cost: {AddonTotalCost.ToString("C2", new CultureInfo("en-US"))}");
                sb.Append($"Addon(s): {AddonCount}\n");
                sb.AppendLine();
                sb.Append($"Total Cost: {(AddonTotalCost * AddonCount).ToString("C2", new CultureInfo("en-US"))}");

                return sb.ToString();
            }
        }
        public int BountyCount
        {
            get => _bountyCount;
            set
            {
                _bountyCount = value;
                UpdateProperties?.Execute(null);
                RaisePropertyChanged(nameof(BountyCount));
            }
        }
        public string CancelMenuString => $"Cancel {TournamentName}";
        public string CurrencySymbol
        {
            set
            {
                _currencySymbol = value;
                RaisePropertyChanged(nameof(CurrencySymbol));
            }
            get => _currencySymbol;
        }
        public string DatabaseDescription
        {
            get
            {
                var sb = new StringBuilder();

                sb.AppendFormat(new CultureInfo("en-US"), "Name: {0}", TournamentName).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Formats: {0}", FormatString).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Game Type: {0}", GameType).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Venue: {0}", Venue).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Start Date: {0:M/dd/y}", StartTime).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Start Time: {0:h:mm tt}", StartTime).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "End Date: {0:M/dd/y}", EndTime).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "End Time: {0:h:mm tt}", EndTime).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Buyin: {0:C2}", BuyinTotalCost).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Rebuy: {0:C2}", RebuyTotalCost).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Addon: {0:C2}", AddonTotalCost).AppendLine();
                if (IsBovadaBounty) sb.AppendFormat(new CultureInfo("en-US"), "Bounty: {0}", Bounty).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Guarantee: {0:C0}", Guarantee).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Late Reg: {0} minutes", LateReg).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Blind Levels: {0} minutes", BlindLevels).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Table Size: {0}", TableSize).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Starting Stack: {0}", StackSizeStarting).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Rebuy Stack: {0}", StackSizeRebuy).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Addon Stack: {0}", StackSizeAddon).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Rebuy Count: {0}", RebuyCount).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Addon Count: {0}", AddonCount).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Entrants: {0}", Entrants).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Entrants Paid: {0}", EntrantsPaid).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Finish Position: {0}", FinishPosition).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Prize Won: {0:C2}", PrizeWon).AppendLine();

                return sb.ToString().Trim();
            }
        }
        public int DatabaseId { get; set; }
        public new string Description
        {
            get
            {
                // holds the description string
                var sb = new StringBuilder();

                sb.AppendLine($"Name: {TournamentName}");

                if (FormatString.Length > 0)
                    sb.AppendLine($"\nFormat(s): {FormatString}");
                if (GameType.Length > 0)
                    sb.AppendLine($"Game Type: {GameType}");
                if (Venue.Length > 0)
                    sb.AppendLine($"Venue: {Venue}");
                if (FormatString.Length > 0 || GameType.Length > 0 || Venue.Length > 0)
                    sb.AppendLine();

                sb.AppendFormat(new CultureInfo("en-US"), "Start Date: {0:M/dd/y}", StartTime).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Start Time: {0:h:mm tt}", StartTime).AppendLine();
                sb.AppendLine();

                if (EndTime.Equals(StartTime) is false)
                {
                    sb.AppendFormat(new CultureInfo("en-US"), "End Date: {0:M/dd/y}", EndTime).AppendLine();
                    sb.AppendFormat(new CultureInfo("en-US"), "End Time: {0:h:mm tt}", EndTime).AppendLine();
                    sb.AppendLine();
                }

                if (BuyinTotalCost > 0)
                    sb.AppendLine(string.Format(new CultureInfo("en-US"), "Buyin: {0:C2}", BuyinTotalCost));
                if (RebuyTotalCost > 0)
                    sb.AppendFormat(new CultureInfo("en-US"), "Rebuy: {0:C2}", RebuyTotalCost).AppendLine();
                if (AddonTotalCost > 0)
                    sb.AppendLine(string.Format(new CultureInfo("en-US"), "Addon: {0:C2}", AddonTotalCost));
                if (Bounty > 0)
                    sb.AppendFormat(new CultureInfo("en-US"), "Bounty: {0:C2}", Bounty).AppendLine();
                if (BuyinTotalCost > 0 || RebuyTotalCost > 0 || AddonTotalCost > 0 || Bounty > 0)
                    sb.AppendLine();

                if (Guarantee > 0 )
                    sb.AppendFormat(new CultureInfo("en-US"), "Guarantee: {0:C0}\n", Guarantee).AppendLine();

                if (LateReg > 0)
                    sb.AppendLine(string.Format(new CultureInfo("en-US"), "Late Reg: {0} minutes", LateReg));
                if (BlindLevels > 0)
                    sb.AppendFormat(new CultureInfo("en-US"), "Blind Levels: {0} minutes", BlindLevels).AppendLine();
                if (LateReg > 0 || BlindLevels > 0)
                    sb.AppendLine();

                if (TableSize > 0)
                    sb.AppendFormat(new CultureInfo("en-US"), "Table Size: {0}\n", TableSize).AppendLine();

                if (StackSizeStarting > 0)
                    sb.AppendFormat(new CultureInfo("en-US"), "Starting Stack: {0}", StackSizeStarting).AppendLine();
                if (StackSizeRebuy > 0)
                    sb.AppendFormat(new CultureInfo("en-US"), "Rebuy Stack: {0}", StackSizeRebuy).AppendLine();
                if (StackSizeAddon > 0)
                    sb.AppendFormat(new CultureInfo("en-US"), "Addon Stack: {0}", StackSizeAddon).AppendLine();
                if (StackSizeStarting > 0 || StackSizeAddon > 0 || StackSizeRebuy > 0)
                    sb.AppendLine();

                if (Entrants > 0) 
                    sb.AppendLine($"Entrants: {Entrants}");
                if (EntrantsPaid > 0)
                    sb.AppendLine($"Entrants Paid: {EntrantsPaid}");

                return sb.ToString().Trim();
            }
        }
        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                UpdateProperties?.Execute(null);
                RaisePropertyChanged(nameof(EndTime));
            }
        }
        public string EndTimeString => $"Update the ending time from \n\n{EndTime.ToShortDateString()} {EndTime.ToShortTimeString()} \nto\n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
        public new int Entrants
        {
            get => _entrants;
            set
            {
                _entrants = value;
                UpdateProperties?.Execute(null);
                RaisePropertyChanged(nameof(Entrants));
            }
        }
        public new int EntrantsPaid
        {
            get => _entrantsPaid;
            set
            {
                _entrantsPaid = value;
                UpdateProperties?.Execute(null);
                RaisePropertyChanged(nameof(EntrantsPaid));
            }
        }
        public string FinishDescription
        {
            get
            {
                var finishDescription = new StringBuilder();

                finishDescription.AppendLine("Are you sure you want to finish?");
                finishDescription.AppendLine();
                finishDescription.AppendLine(TournamentName);
                finishDescription.AppendLine();
                finishDescription.Append("Start Time: ").Append(StartTime).AppendLine();
                finishDescription.Append("End Time: ").Append(EndTime).AppendLine();
                finishDescription.AppendLine();
                finishDescription.Append("Total Cost: ").AppendLine(TotalCost.ToString("C2", new CultureInfo("en-US")));
                finishDescription.Append("Prize Won: ").AppendLine(PrizeWon.ToString("C2", new CultureInfo("en-US")));
                finishDescription.Append("Profit: ").AppendLine((PrizeWon - TotalCost).ToString("C2", new CultureInfo("en-US")));
                finishDescription.AppendLine();
                finishDescription.Append("Entrants: ").Append(Entrants).AppendLine();
                finishDescription.Append("Entrants Paid: ").Append(EntrantsPaid).AppendLine();
                finishDescription.Append("Finished: ").Append(FinishPosition).AppendLine();
                finishDescription.AppendLine();
                finishDescription.Append("Rebuy(s): ").Append(RebuyCount).AppendLine();
                finishDescription.Append("Addon(s): ").Append(AddonCount).AppendLine();

                if(IsBovadaBounty)
                {
                    finishDescription.AppendLine();
                    finishDescription.Append("Bovada Bounties").AppendLine();
                    finishDescription.Append("Bounty Amount: ").Append(Bounty.ToString("C2", new CultureInfo("en-US"))).AppendLine();
                    finishDescription.Append("Bounty Count: ").Append(IsBovadaBounty).AppendLine();
                }

                return finishDescription.ToString();
            }
        }
        public int FinishPosition
        {
            get => _finishPosition;
            set
            {
                _finishPosition = value;

                // auto fill payout if sng payouts exist
                if (IsSng)
                {
                    // split payouts string on comma
                    var payouts = SngPayouts.Split(',');

                    try
                    {
                        // try to find the payout that matches finish position
                        PrizeWon = Decimal.Parse(payouts[_finishPosition - 1]);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        // no matching payout
                        PrizeWon = 0;
                    }
                    catch (FormatException ex)
                    {
                        // format error, probably used a letter instead of number in sng payout info
                        PrizeWon = 0;
                    }
                }

                UpdateProperties?.Execute(null);
                RaisePropertyChanged(nameof(FinishPosition));
            }
        }
        public bool IsScreenshotAttached => ScreenshotFilename.Trim() != "";
        public int JackpotSpinMultiplier
        {
            get => _jackpotSpinMultiplier;
            set
            {
                _jackpotSpinMultiplier = value;
                if (UpdateProperties is not null) UpdateProperties.Execute(null);
                RaisePropertyChanged(nameof(JackpotSpinMultiplier));
            }
        }
        public string Note { get; set; } = "";
        public decimal PrizeWon
        {
            get
            {
                if(IsBovadaBounty) return _prizeWon + (Bounty * BountyCount);

                return _prizeWon;
            }
            set
            {
                _prizeWon = value;
                UpdateProperties?.Execute(null);
                RaisePropertyChanged(nameof(PrizeWon));
            }
        }
        public int RebuyCount
        {
            get => _rebuyCount;
            set
            {
                _rebuyCount = value;
                UpdateProperties?.Execute(null);
                RaisePropertyChanged(nameof(RebuyCount));
            }
        }
        public string RebuyCountDescription
        {
            get
            {
                // description string builder
                var sb = new StringBuilder();

                sb.AppendLine("Rebuy Base: " + RebuyBaseCost.ToString("C2", new CultureInfo("en-US")));
                sb.AppendLine("Rebuy Rake: " + RebuyRakeCost.ToString("C2", new CultureInfo("en-US")));
                sb.AppendLine();
                sb.Append("Rebuy Cost: ").AppendLine(RebuyTotalCost.ToString("C2", new CultureInfo("en-US")));
                sb.Append("Rebuy(s): ").Append(RebuyCount).AppendLine();
                sb.AppendLine();
                sb.Append("Total Cost: ").Append((RebuyTotalCost * RebuyCount).ToString("C2", new CultureInfo("en-US")));

                return sb.ToString();
            }
        }
        public string StartDuplicateMenuString => $"Quickly create a duplicate copy of {TournamentName}";
        public string ScreenshotFilename { get; set; } = "";
        public new DateTime StartTime
        {
            get => _startTime;
            set
            {
                // remove seconds anytime starttime is changed
                _startTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
                UpdateProperties?.Execute(null);
                RaisePropertyChanged(nameof(StartTime));
            }
        }
        public string StartTimeString => $"Update the starting time from \n\n{StartTime.ToShortDateString()} {StartTime.ToShortTimeString()} \nto\n{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
        public string TimeDescription
        {
            get
            {
                var s = new StringBuilder();

                // show starting date and time
                s.AppendLine($"Starting Date: {StartTime.ToShortDateString()}");
                s.AppendLine($"Starting Time: {StartTime.ToShortTimeString()}");

                // show ending date and time if updated, message if not
                if (EndTime > StartTime)
                {
                    s.AppendLine();
                    s.AppendLine($"Ending Date: {EndTime.ToShortDateString()}");
                    s.AppendLine($"Ending Time: {EndTime.ToShortTimeString()}");

                    s.AppendLine();
                    s.AppendLine($"Length: {new TimeSpan((EndTime - StartTime).Hours, (EndTime - StartTime).Minutes, 0)}");
                }
                else
                {
                    s.AppendLine();
                    s.AppendLine("You haven't set an ending time for this tournament yet");
                }

                return s.ToString().Trim();
            }
        }
        public decimal TotalCost
        {
            get
            {
                decimal total = BuyinTotalCost;

                if(RebuyCount > 0) total += (RebuyCount * RebuyTotalCost);

                if(AddonCount > 0) total += (AddonCount * AddonTotalCost);

                return total;
            }
        }
        public string TotalCostDescription
        {
            get
            {
                // description string builder
                var sb = new StringBuilder();

                sb.AppendLine($"Buyin Base: {BuyinBaseCost.ToString("C2", new CultureInfo("en-US"))}");
                sb.AppendLine($"Buyin Rake: {BuyinRakeCost.ToString("C2", new CultureInfo("en-US"))}");
                sb.AppendLine($"Buyin Cost: {BuyinTotalCost.ToString("C2", new CultureInfo("en-US"))}");
                sb.AppendLine();

                if (RebuyBaseCost > 0)
                    sb.AppendLine($"Rebuy Base: {RebuyBaseCost.ToString("C2", new CultureInfo("en-US"))}");
                if (RebuyRakeCost > 0 || RebuyBaseCost > 0)
                    sb.AppendLine($"Rebuy Rake: {RebuyRakeCost.ToString("C2", new CultureInfo("en-US"))}");
                if (RebuyTotalCost > 0)
                    sb.AppendLine($"Rebuy Cost: {RebuyTotalCost.ToString("C2", new CultureInfo("en-US"))}");
                if (RebuyBaseCost > 0 || RebuyRakeCost > 0 || RebuyTotalCost > 0)
                    sb.AppendLine();

                if (AddonBaseCost > 0)
                    sb.AppendLine($"Addon Base: {AddonBaseCost.ToString("C2", new CultureInfo("en-US"))}");
                if (AddonRakeCost > 0 || AddonBaseCost > 0)
                    sb.AppendLine($"Addon Rake: {AddonRakeCost.ToString("C2", new CultureInfo("en-US"))}");
                if (AddonTotalCost > 0)
                    sb.AppendLine($"Addon Cost: {AddonTotalCost.ToString("C2", new CultureInfo("en-US"))}");
                if (AddonBaseCost > 0 || AddonRakeCost > 0 || AddonTotalCost > 0)
                    sb.AppendLine();

                if (RebuyCount > 0)
                    sb.AppendLine($"Rebuy(s): {RebuyCount}");
                if ((RebuyTotalCost * RebuyCount) > 0)
                    sb.AppendLine($"Rebuy(s) Cost: {((RebuyTotalCost * RebuyCount).ToString("C2", new CultureInfo("en-US")))}");
                if (RebuyCount > 0 || ((RebuyTotalCost * RebuyCount) > 0))
                    sb.AppendLine();

                if (AddonCount > 0)
                    sb.AppendLine($"Addon(s): {AddonCount}");
                if ((AddonTotalCost * AddonCount) > 0)
                    sb.AppendLine($"Addon(s) Cost: {(AddonTotalCost * AddonCount).ToString("C2", new CultureInfo("en-US"))}");
                if (AddonCount > 0 || ((AddonTotalCost * AddonCount) > 0))
                    sb.AppendLine();

                sb.AppendLine($"Total Cost: {TotalCost.ToString("C2", new CultureInfo("en-US"))}");

                return sb.ToString().Trim();
            }
        }
        public TournamentVisibility TournamentStartingVisibility
        {
            get => _tournamentStartingVisibility;
            set
            {
                if (_tournamentStartingVisibility == value) 
                    return;

                _tournamentStartingVisibility = value;

                UpdateVisibleStatus?.Execute(TournamentStartingVisibility);
            }
        }
        public ICommand UpdateProperties { get; set; }
        public ICommand UpdateVisibleStatus { get; set; }

        /// <summary>
        /// Adjust starting time to account for sng tournaments,
        /// after midnight tournaments and to remove seconds
        /// </summary>
        public void AdjustStartTime()
        {
            // get current time
            var currentTime = DateTime.Now;

            // adjust date to today
            StartTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, StartTime.Hour, StartTime.Minute, 0);

            // adjust for sng
            if (IsSng) 
                StartTime = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day, currentTime.Hour, currentTime.Minute, currentTime.Second);

            // update end time
            EndTime = StartTime;

            // sng tournaments are done
            if(IsSng) 
                return;

            // if the current time is between midnight and 7 a.m. or if the tournament starting time is between 5 a.m. and midnight leave
            if (currentTime.Hour <= 8 || StartTime.Hour > 6) 
                return;

            // add a day to the start time
            StartTime = StartTime.AddDays(1);

            // update end time
            EndTime = StartTime;
        }

        /// <summary>
        /// Sets the tournaments initial starting status
        /// </summary>
        public void SetInitialTournamentStatus()
        {
            // holds current time
            var currentTime = DateTime.Now;

            // holds new default status
            var newStatus = TournamentVisibility.Queued;

            // check start time against current time and set initial status
            if (StartTime <= currentTime) newStatus = TournamentVisibility.ShowStarted;
            else if (StartTime > currentTime && StartTime <= currentTime.AddMinutes(5)) newStatus = TournamentVisibility.ShowFive;
            else if (StartTime > currentTime.AddMinutes(5) && StartTime <= currentTime.AddMinutes(15)) newStatus = TournamentVisibility.ShowFifteen;
            else if (StartTime > currentTime.AddMinutes(15) && StartTime <= currentTime.AddMinutes(30)) newStatus = TournamentVisibility.ShowThirty;
            else if (StartTime > currentTime.AddMinutes(30) && StartTime <= currentTime.AddMinutes(60)) newStatus = TournamentVisibility.ShowSixty;

            // update status
            TournamentStartingVisibility = newStatus;
        }

        /// <summary>
        /// Called by the tournament in the background to
        /// update visibility status
        /// </summary>
        public void UpdateTournamentStatus()
        {
            // holds current status
            var startingStatus = TournamentStartingVisibility;

            // holds new status
            var newStatus = startingStatus;

            // holds current time
            var currentTime = DateTime.Now;

            // check start time against current time and starting status and set new status
            if (StartTime <= currentTime && startingStatus is not TournamentVisibility.ShowStarted) newStatus = TournamentVisibility.ShowStarted;
            else if (StartTime > currentTime && StartTime <= currentTime.AddMinutes(5) && startingStatus is not TournamentVisibility.ShowFive) newStatus = TournamentVisibility.ShowFive;
            else if (StartTime > currentTime.AddMinutes(5) && StartTime <= currentTime.AddMinutes(15) && startingStatus is not TournamentVisibility.ShowFifteen) newStatus = TournamentVisibility.ShowFifteen;
            else if (StartTime > currentTime.AddMinutes(15) && StartTime <= currentTime.AddMinutes(30) && startingStatus is not TournamentVisibility.ShowThirty) newStatus = TournamentVisibility.ShowThirty;
            else if (StartTime > currentTime.AddMinutes(30) && StartTime <= currentTime.AddMinutes(60) && startingStatus is not TournamentVisibility.ShowSixty) newStatus = TournamentVisibility.ShowSixty;
            else if (StartTime > currentTime.AddMinutes(60) && startingStatus is not TournamentVisibility.ShowStarted && startingStatus is not TournamentVisibility.ShowFive && startingStatus is not TournamentVisibility.ShowFifteen
                     && startingStatus is not TournamentVisibility.ShowThirty && startingStatus is not TournamentVisibility.ShowSixty && startingStatus is not TournamentVisibility.Queued) newStatus = TournamentVisibility.Queued;

            // update status, if needed
            if (startingStatus != newStatus) 
                TournamentStartingVisibility = newStatus;
        }

        /// <summary>
        /// Compares 2 tournaments for equality
        /// </summary>
        /// <param name="x">tournament one</param>
        /// <param name="y">tournament two</param>
        /// <returns></returns>
        public static bool Equals(TournamentRunning x, TournamentRunning y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x._addonCount == y._addonCount && x._endTime.Hour.Equals(y._endTime.Hour) && x._endTime.Minute.Equals(y._endTime.Minute) && x.IsBovadaBounty == y.IsBovadaBounty && x.IsSng == y.IsSng &&
                   x._finishPosition == y._finishPosition && x._prizeWon == y._prizeWon && x._rebuyCount == y._rebuyCount && x.IsSng == y.IsSng && x.JackpotSpinMultiplier == y.JackpotSpinMultiplier &&
                   x.AddonBaseCost == y.AddonBaseCost && x.AddonRakeCost == y.AddonRakeCost && x.BlindLevels == y.BlindLevels && x.Bounty == y.Bounty && x.BountyCount == y.BountyCount &&
                   x.BuyinBaseCost == y.BuyinBaseCost && x.BuyinRakeCost == y.BuyinRakeCost && x.Entrants == y.Entrants && x.EntrantsPaid == y.EntrantsPaid &&
                   string.Equals(x.FormatString.Trim(), y.FormatString.Trim(), StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(x.GameType.Trim(), y.GameType.Trim(), StringComparison.CurrentCultureIgnoreCase) && x.Guarantee == y.Guarantee && x.LateReg == y.LateReg &&
                   x.RebuyBaseCost == y.RebuyBaseCost && x.RebuyRakeCost == y.RebuyRakeCost && x.SngPayouts == y.SngPayouts && x.StackSizeAddon == y.StackSizeAddon &&
                   x.StackSizeRebuy == y.StackSizeRebuy && x.StackSizeStarting == y.StackSizeStarting &&
                   x.StartTime.Hour == y.StartTime.Hour && x.StartTime.Minute == y.StartTime.Minute && x.TableSize == y.TableSize &&
                   string.Equals(x.TimeZoneName.Trim(), y.TimeZoneName.Trim(), StringComparison.CurrentCultureIgnoreCase) &&
                   x.JackpotSpinMultiplier == y.JackpotSpinMultiplier &&
                   string.Equals(x.TournamentName.Trim(), y.TournamentName.Trim(), StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(x.Venue.Trim(), y.Venue.Trim(), StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// return hash code for hash lookup table
        /// </summary>
        /// <param name="obj">tournament to hash</param>
        /// <returns>returns hash code</returns>
        public int GetHashCode(TournamentRunning obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.AddonBaseCost);
            hashCode.Add(obj.AddonCount);
            hashCode.Add(obj.AddonRakeCost);
            hashCode.Add(obj.BlindLevels);
            hashCode.Add(obj.BuyinBaseCost);
            hashCode.Add(obj.BuyinRakeCost);
            hashCode.Add(obj.EndTime);
            hashCode.Add(obj.Entrants);
            hashCode.Add(obj.EntrantsPaid);
            hashCode.Add(obj.FinishPosition);
            hashCode.Add(obj.GameType);
            hashCode.Add(obj.Guarantee);
            hashCode.Add(obj.LateReg);
            hashCode.Add(obj.PrizeWon);
            hashCode.Add(obj.RebuyBaseCost);
            hashCode.Add(obj.RebuyCount);
            hashCode.Add(obj.RebuyRakeCost);
            hashCode.Add(obj.StackSizeAddon);
            hashCode.Add(obj.StackSizeRebuy);
            hashCode.Add(obj.StackSizeStarting);
            hashCode.Add(obj.TableSize);
            hashCode.Add(obj.TournamentName);
            hashCode.Add(obj.Venue);
            return hashCode.ToHashCode();
        }

        /// <summary>
        /// Update tournaments data
        /// </summary>
        public void UpdateData(TournamentRunning tournament)
        {
            AddonBaseCost = tournament.AddonBaseCost;
            AddonCount = tournament.AddonCount;
            AddonRakeCost = tournament.AddonRakeCost;
            BlindLevels = tournament.BlindLevels;
            Bounty = tournament.Bounty;
            BountyCount = tournament.BountyCount;
            BuyinBaseCost = tournament.BuyinBaseCost;
            BuyinRakeCost = tournament.BuyinRakeCost;
            EndTime = tournament.EndTime;
            Entrants = tournament.Entrants;
            EntrantsPaid = tournament.EntrantsPaid;
            FinishPosition = tournament.FinishPosition;
            Formats = tournament.Formats;
            GameType = tournament.GameType;
            Guarantee = tournament.Guarantee;
            JackpotSpinMultiplier = tournament.JackpotSpinMultiplier;
            IsBovadaBounty = tournament.IsBovadaBounty;
            IsFavorite = tournament.IsFavorite;
            IsSng = tournament.IsSng;
            LateReg = tournament.LateReg;
            Note = tournament.Note;
            PrizeWon = tournament.PrizeWon;
            RebuyBaseCost = tournament.RebuyBaseCost;
            RebuyCount = tournament.RebuyCount;
            RebuyRakeCost = tournament.RebuyRakeCost;
            ScreenshotFilename = tournament.ScreenshotFilename;
            SngPayouts = tournament.SngPayouts;
            StartTime = tournament.StartTime;
            StackSizeAddon = tournament.StackSizeAddon;
            StackSizeRebuy = tournament.StackSizeRebuy;
            StackSizeStarting = tournament.StackSizeStarting;
            TableSize = tournament.TableSize;
            TemplateId = tournament.TemplateId;
            TimeZoneName = tournament.TimeZoneName;
            TournamentName = tournament.TournamentName;
            Venue = tournament.Venue;

            SetInitialTournamentStatus();
        }
    }
}
