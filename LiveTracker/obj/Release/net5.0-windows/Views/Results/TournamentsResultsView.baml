   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=null;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager :    4Tournament_Life.Views.Results.TournamentsResultsView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
    =commands"clr-namespace:LiveTracker.Commands   	 
    _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    � 
ResizeMode$  CanMinimize=�0  *Syncfusion.Windows.Shared.ChromelessWindow   ShowMaximizeButton$ False��   UseNativeChrome$
 True�� � SizeToContent$ WidthAndHeight=�$	��450��$	��800��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � Title �� *Title+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ Loaded��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  	   Command	 �� *AfterLoadCommand+  $ Closed��.   	 �� *AfterCloseCommand+.��� 
 � RowDefinitions
 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�'  !Syncfusion.Windows.Shared.MenuAdv  $��0��$��0��$��10��   
ExpandMode$ ExpandOnClick=�.@�+  %Syncfusion.Windows.Shared.MenuItemAdv  $
��File������ *
FontSize+.@�  $��Close Window�� � ToolTip$ Close this window��   CommandParameter$
 Exit������ *
FontSize+   Command �� 4	   .LiveTracker.Commands.TournamentResultsCommands 	  MenuItemCommand �� Source# Z   $��Filters������ *
FontSize+.@�  $��Clear Current Filter��$Z TClear the tournament filter and reset the tournament results to their default values��$ ClearFilter������ *
FontSize+ �� # Z   $��Show Current Filter��$( "Show the current filter being used��$ ShowCurrentFilter������ *
FontSize+ �� # Z 1
  +Syncfusion.Windows.Shared.MenuItemSeparator
  $	��105��$
��Left=�  $��New Filter...��$4 .Create a new filter for the tournament results��$ CreateFilter������ *
FontSize+ �� # Z   $��Load Filter...��$9 3Load a previously created tournament results filter��$ 
LoadFilter������ *
FontSize+ �� # Z   $��Delete Filter...��$; 5Delete a previously created tournament results filter��$ DeleteFilter������ *
FontSize+ �� # Z   $��Quick Filters������ *
FontSize+.@�  $��Clear Current Filter��$Z TClear the tournament filter and reset the tournament results to their default values��$ ClearFilter������ *
FontSize+ �� # Z   $��Show Current Filter��$( "Show the current filter being used��$ ShowCurrentFilter������ *
FontSize+ �� # Z 
  $	��105��$
��Left=�  $��Last 12 Hours��$9 3Results for tournaments played in the last 12 hours��$ 12Hours������ *
FontSize+ �� # Z   $��Last 24 Hours��$9 3Results for tournaments played in the last 24 hours��$ 24Hours������ *
FontSize+ �� # Z   $��Last 7 Days��$7 1Results for tournaments played in the last 7 days��$ 7Days������ *
FontSize+ �� # Z   $��Last 30 Days��$8 2Results for tournaments played in the last 30 days��$ 30Days������ *
FontSize+ �� # Z   $��Last 90 Days��$8 2Results for tournaments played in the last 90 days��$ 90Days������ *
FontSize+ �� # Z 
  $	��100��$
��Left=�  $��	Yesterday��$. (Results for tournaments played yesterday��$ 	Yesterday������ *
FontSize+ �� # Z   $��Today��$* $Results for tournaments played today��$ Today������ *
FontSize+ �� # Z   $��Week to Date��$< 6Results for tournaments played during the current week��$ 
WeekToDate������ *
FontSize+ �� # Z   $��Month to Date��$= 7Results for tournaments played during the current month��$ MonthToDate������ *
FontSize+ �� # Z   $��Year to Date��$< 6Results for tournaments played during the current year��$ 
YearToDate������ *
FontSize+ �� # Z 
  $	��100��$
��Left=�  $��Previous 50��$G AResults for tournaments played during the previous 50 tournaments��$ Last50������ *
FontSize+ �� # Z   $��Previous 100��$H BResults for tournaments played during the previous 100 tournaments��$ Last100������ *
FontSize+ �� # Z   $��Previous 250��$H BResults for tournaments played during the previous 250 tournaments��$ Last250������ *
FontSize+ �� # Z   $��Previous 500��$H BResults for tournaments played during the previous 500 tournaments��$ Last500������ *
FontSize+ �� # Z   $��Previous 1000��$I CResults for tournaments played during the previous 1000 tournaments��$ Last1000������ *
FontSize+ �� # Z 9� $��1��$��0��$	��5,5q�$	��5,5q�$��Tournament Cost Stats�� 9� 
IsExpanded$
 True��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Cost: ��$��Right=����� *
FontSize+�� $��Highest Cost: ��$��Right=����� *
FontSize+�� $��Lowest Cost: ��$��Right=����� *
FontSize+�� $��Average Cost: ��$��Right=����� *
FontSize+�� $��Rake Paid: ��$��Right=����� *
FontSize+�� $��1��.+���  �� ContentStringFormat$ C��$T NThe total costs (buy-in + rebuy + addon) of all the tournaments in the results��$
��Left=����� *Stats.TotalCost+���� *
FontSize+�� $ C��$T NThe highest total cost (buy-in + rebuy + addon) of a tournament in the results��$
��Left=����� *Stats.HighestCost+���� *
FontSize+�� $ C��$S MThe lowest total cost (buy-in + rebuy + addon) of a tournament in the results��$
��Left=����� *Stats.LowestCost+���� *
FontSize+�� $ C��$T NThe average total cost (buy-in + rebuy + addon) of a tournament in the results��$
��Left=����� *Stats.AverageCost+���� *
FontSize+�� $ C��$? 9The total rake paid across all tournaments in the results��$
��Left=����� *Stats.RakePaid+���� *
FontSize+9� $��1��$��1��$	��5,5q�$	��5,5q�$��Tournament Buy-in Stats��$
 True��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Buy-in: ��$��Right=����� *
FontSize+�� $��Highest Buy-in: ��$��Right=����� *
FontSize+�� $��Lowest Buy-in: ��$��Right=����� *
FontSize+�� $��Average Buy-in: ��$��Right=����� *
FontSize+�� $��1��.+��� $ C��$= 7The total buy-ins of all the tournaments in the results��$
��Left=����� *Stats.TotalBuyinCost+���� *
FontSize+�� $ C��$7 1The highest buy-in of a tournament in the results��$
��Left=����� *Stats.HighestBuyin+���� *
FontSize+�� $ C��$6 0The lowest buy-in of a tournament in the results��$
��Left=����� *Stats.LowestBuyin+���� *
FontSize+�� $ C��$7 1The average buy-in of a tournament in the results��$
��Left=����� *Stats.AverageBuyin+���� *
FontSize+9� $��1��$��2��$	��5,5q�$	��5,5q�$��Tournament Profit Stats��$
 True��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Profit: ��$��Right=����� *
FontSize+�� $��Highest Profit: ��$��Right=����� *
FontSize+�� $��Lowest Profit: ��$��Right=����� *
FontSize+�� $��Average Profit: ��$��Right=����� *
FontSize+�� $��1��.+��� $ C��$< 6The total profit of all the tournaments in the results��$
��Left=����� *Stats.TotalProfit+���� *
FontSize+�� $ C��$3 -Highest profit of a tournament in the results��$
��Left=����� *Stats.HighestProfit+���� *
FontSize+�� $ C��$2 ,Lowest profit of a tournament in the results��$
��Left=����� *Stats.LowestProfit+���� *
FontSize+�� $ C��$3 -Average profit of a tournament in the results��$
��Left=����� *Stats.AverageProfit+���� *
FontSize+9� $��1��$��3��$	��5,5q�$	��5,5q�$��Tournament Prize Stats��$
 True��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Prizes: ��$��Right=����� *
FontSize+�� $��Highest Prizes: ��$��Right=����� *
FontSize+�� $��Lowest Prize: ��$��Right=����� *
FontSize+�� $��Average Prize: ��$��Right=����� *
FontSize+�� $��1��.+��� $ C��$@ :The total prizes won in all the tournaments in the results��$
��Left=����� *Stats.TotalPrizeWon+���� *
FontSize+�� $ C��$: 4The highest prize won in a tournament in the results��$
��Left=����� *Stats.HighestPrizeWon+���� *
FontSize+�� $ C��$9 3The lowest prize won in a tournament in the results��$
��Left=����� *Stats.LowestPrizeWon+���� *
FontSize+�� $ C��$: 4The average prize won in a tournament in the results��$
��Left=����� *Stats.AveragePrizeWon+���� *
FontSize+9� $��2��$��0��$	��5,5q�$	��5,5q�$��Tournament Overall Stats��$
 True��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Tournaments: ��$��Right=����� *
FontSize+�� $��Hourly: ��$��Right=����� *
FontSize+�� $��$/Tournament: ��$��Right=����� *
FontSize+�� $��ROI: ��$��Right=����� *
FontSize+�� $��ITM: ��$��Right=����� *
FontSize+�� $��1��.+��� $1 +The total tournaments played in the results��$
��Left=����� *Stats.TournamentsPlayed+���� *
FontSize+�� $ C��$7 1The hourly rate of the tournaments in the results��$
��Left=����� *Stats.Hourly+���� *
FontSize+�� $ C��$F @The profit per tournament made in the tournaments in the results��$
��Left=����� *Stats.ProfitPerTournament+���� *
FontSize+�� $ P2��$A ;The return on investment for the tournaments in the results��$
��Left=����� *	Stats.Roi+���� *
FontSize+�� $ P2��$D >The in the money percentage for the tournaments in the results��$
��Left=����� *	Stats.Itm+���� *
FontSize+9� $��2��$��1��$	��5,5q�$	��5,5q�$��Tournament Counting Stats��$
 True��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Winning Streak: ��$��Right=����� *
FontSize+�� $��Losing Streak: ��$��Right=����� *
FontSize+�� $��Cashing Streak: ��$��Right=����� *
FontSize+�� $��Stone Bubbles: ��$��Right=����� *
FontSize+�� $��Final Table Bubbles: ��$��Right=����� *
FontSize+�� $��1��.+��� $6 0The most tournaments won in a row in the results��$
��Left=����� *Stats.WinningStreak+���� *
FontSize+�� $7 1The most tournaments lost in a row in the results��$
��Left=����� *Stats.LosingStreak+���� *
FontSize+�� $9 3The most tournaments cashed in a row in the results��$
��Left=����� *Stats.CashingStreak+���� *
FontSize+�� $[ UThe total number of times finished on the stone bubble of a tournament in the results��$
��Left=����� *Stats.StoneBubbles+���� *
FontSize+�� $Q KThe total number of times finished on the final table bubble in the results��$
��Left=����� *Stats.FinalTableBubbles+���� *
FontSize+9� $��2��$��2��$	��5,5q�$	��5,5q�$"��Tournament Final Table Stats��$
 True��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Final Tables: ��$��Right=����� *
FontSize+�� $��Make Final Table: ��$��Right=����� *
FontSize+�� $��Win Final Table: ��$��Right=����� *
FontSize+�� $��Total Wins: ��$��Right=����� *
FontSize+�� $��Win Tournament: ��$��Right=����� *
FontSize+�� $��1��.+��� $: 4The total number of final tables made in the results��$
��Left=����� *Stats.FinalTables+���� *
FontSize+�� $ P2��$E ?The percentage of the time a final table is made in the results��$
��Left=����� *Stats.FinalTablePercentage+���� *
FontSize+�� $ P2��$a [The percentage of the time a final table is won after making the final table in the results��$
��Left=����� *#!Stats.FinalTableWinningPercentage+���� *
FontSize+�� $D >The total number of tournaments won (1st place) in the results��$
��Left=����� *Stats.TotalWins+���� *
FontSize+�� $ P2��$C =The percentage of the time a tournament is won in the results��$
��Left=����� *Stats.WinningPercentage+���� *
FontSize+9� $��2��$��3��$	��5,5q�$	��5,5q�$��Tournament Rebuy Stats��$
 True��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Most Rebuys: ��$��Right=����� *
FontSize+�� $��Least Rebuys: ��$��Right=����� *
FontSize+�� $��Average Rebuys: ��$��Right=����� *
FontSize+�� $��1��.+��� $K EThe highest number of rebuys used in a tournament that offered rebuys��$
��Left=����� *Stats.HighestRebuys+���� *
FontSize+�� $J DThe lowest number of rebuys used in a tournament that offered rebuys��$
��Left=����� *Stats.LowestRebuys+���� *
FontSize+�� $ N2��$H BThe average number of rebuys used when a tournament offered rebuys��$
��Left=����� *Stats.AverageRebuys+���� *
FontSize+9� $��3��$��0��$��Center=�$
 True��$	��5,5q�$��Tournament Graphs��.���� .+��� $��Buy-in/Roi Chart��$[ UView a bar graph of the roi for the different buy-in levels in the tournament results��$��BuyinRoiHistogram��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $��Finish Position Graph��$^ XView a bar graph of the roi for the different finish positions in the tournament results��$��FinishPositionGraph��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $��Format/Roi Chart��$U OView a bar graph of the roi for the different formats in the tournament results��$��FormatRoiChart��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $��GameType/Roi Chart��$X RView a bar graph of the roi for the different game types in the tournament results��$��GameTypeRoiChart��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $��Tournament Profit Graph��$@ :View a line graph of the profit for the tournament results��$��ProfitGraph��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $��View Tournaments��$7 1View the tournaments for these results in a table��$��ViewTournaments��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+9� $��3��$��1��$	��5,5q�$	��5,5q�$��Tournament Entrant Stats��$
 True��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Most Entrants: ��$��Right=����� *
FontSize+�� $��Lowest Entrants: ��$��Right=����� *
FontSize+�� $��Average Entrants: ��$��Right=����� *
FontSize+�� $��Best Finish: ��$��Right=����� *
FontSize+�� $��Worst Finish: ��$��Right=����� *
FontSize+�� $��Average Finish: ��$��Right=����� *
FontSize+�� $��1��.+��� $D >The highest number of entrants for a tournament in the results��$
��Left=����� *Stats.HighestEntrants+���� *
FontSize+�� $C =The lowest number of entrants for a tournament in the results��$
��Left=����� *Stats.LowestEntrants+���� *
FontSize+�� $D >The average number of entrants for a tournament in the results��$
��Left=����� *Stats.AverageEntrants+���� *
FontSize+�� $@ :The highest finish position in a tournament in the results��$
��Left=����� *Stats.LowestFinishPosition+���� *
FontSize+�� $? 9The lowest finish position in a tournament in the results��$
��Left=����� *Stats.HighestFinishPosition+���� *
FontSize+�� $@ :The average finish position in a tournament in the results��$
��Left=����� *Stats.AverageFinishPosition+���� *
FontSize+9� $��3��$��2��$	��5,5q�$	��5,5q�$��Tournament Finish % Stats��$
 True��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Average Beaten: ��$��Right=����� *
FontSize+�� $��Early Finish: ��$��Right=����� *
FontSize+�� $��Early/Mid Finish: ��$��Right=����� *
FontSize+�� $��Mid Finish: ��$��Right=����� *
FontSize+�� $��Mid/Late Finish: ��$��Right=����� *
FontSize+�� $��Late Finish: ��$��Right=����� *
FontSize+�� $��Most Tournaments: ��$��Right=����� *
FontSize+�� $��Least Tournaments: ��$��Right=����� *
FontSize+�� $��Average Tournaments: ��$��Right=����� *
FontSize+�� $��1��.+��� $ P2��$B <The percentage of the field beaten on average in the results��$
��Left=����� *Stats.AveragePercentBeaten+���� *
FontSize+�� $ P2��$] WThe percentage of the time finished within the first 10% of a tournament in the results��$
��Left=����� *Stats.EarlyFinish+���� *
FontSize+�� $ P2��$W QThe percentage of the time finished within 10%-30% of a tournament in the results��$
��Left=����� *Stats.EarlyMidFinish+���� *
FontSize+�� $ P2��$W QThe percentage of the time finished within 30%-70% of a tournament in the results��$
��Left=����� *Stats.MidFinish+���� *
FontSize+�� $ P2��$W QThe percentage of the time finished within 70%-90% of a tournament in the results��$
��Left=����� *Stats.MidLateFinish+���� *
FontSize+�� $ P2��$] WThe percentage of the time finished within the final 10% of a tournament in the results��$
��Left=����� *Stats.LateFinish+���� *
FontSize+�� $5 /The most tournaments played in a single session��$
��Left=����� *Stats.MaxTournaments+���� *
FontSize+�� $@ :The least amount of tournaments played in a single session��$
��Left=����� *Stats.MinTournaments+���� *
FontSize+�� $B <The average amount of tournaments played in a single session��$
��Left=����� *Stats.AverageTournaments+���� *
FontSize+9� $��3��$��3��$	��5,5q�$	��5,5q�$��Tournament Day/Time Stats��$
 True��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��First Date Played: ��$��Right=����� *
FontSize+�� $��Last Date Played: ��$��Right=����� *
FontSize+�� $��Longest Tournament: ��$��Right=����� *
FontSize+�� $��Shortest Tournament: ��$��Right=����� *
FontSize+�� $��Average Tournament: ��$��Right=����� *
FontSize+�� $��Total Days Played: ��$��Right=����� *
FontSize+�� $��Winning Days: ��$��Right=����� *
FontSize+�� $��Losing Days: ��$��Right=����� *
FontSize+�� $��Break-even Days: ��$��Right=����� *
FontSize+�� $��1��.+��� $ M/dd/yy��$A ;The earliest date a tournament was played on in the results��$
��Left=����� *Stats.EarliestStartDate+���� *
FontSize+�� $ M/dd/yy��$? 9The latest date a tournament was played on in the results��$
��Left=����� *Stats.LatestStartDate+���� *
FontSize+�� $ {0:hh}:{0:mm}��$K EThe longest length of time a tournament was played for in the results��$
��Left=����� *Stats.LongestLength+���� *
FontSize+�� $ {0:hh}:{0:mm}��$L FThe shortest length of time a tournament was played for in the results��$
��Left=����� *Stats.ShortestLength+���� *
FontSize+�� $ {0:hh}:{0:mm}��$K EThe average length of time a tournament was played for in the results��$
��Left=����� *Stats.AverageLength+���� *
FontSize+�� $O IThe total number of unique days a tournament was played on in the results��$
��Left=����� *Stats.TotalDays+���� *
FontSize+�� $5 /The total number of winning days in the results��$
��Left=����� *Stats.WinningDays+���� *
FontSize+�� $4 .The total number of losing days in the results��$
��Left=����� *Stats.LosingDays+���� *
FontSize+�� $8 2The total number of break-even days in the results��$
��Left=����� *Stats.BreakevenDays+���� *
FontSize+