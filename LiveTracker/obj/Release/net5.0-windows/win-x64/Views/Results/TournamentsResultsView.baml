   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.0.1.2, Culture=neutral, PublicKeyToken=null;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager :    4Tournament_Life.Views.Results.TournamentsResultsView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a	 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf    	 
   =commands"clr-namespace:LiveTracker.Commands    	 
   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    � 
ResizeMode$  NoResize=� � SizeToContent$ WidthAndHeight=�$	��450��$	��800��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � Title �� *Title+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ Loaded��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *AfterLoadCommand+.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �	 � ColumnDefinitions	 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�' 
 !Syncfusion.Windows.Shared.MenuAdv  $��0��$��0��$��10��
   
ExpandMode$
 ExpandOnClick=�.@�+ 
 %Syncfusion.Windows.Shared.MenuItemAdv  $
��File������ *
FontSize+.@�  $
��Exit��   CommandParameter$
 Exit������ *
FontSize+   Command �� 4   .LiveTracker.Commands.TournamentResultsCommands   MenuItemCommand �� Source# Z   $��Filters������ *
FontSize+.@�  $��
New Filter��$ 	NewFilter������ *
FontSize+ �� # Z   $��Load Filter��$ 
LoadFilter������ *
FontSize+ �� # Z   $��Show Current Filter��$ ShowCurrentFilter������ *
FontSize+ �� # Z   $��Clear Filter��$ ClearFilter������ *
FontSize+ �� # Z   $��Quick Filters������ *
FontSize+.@�  $��Last 12 Hours��$ 12Hours������ *
FontSize+ �� # Z   $��Last 24 Hours��$ 24Hours������ *
FontSize+ �� # Z   $��Last 7 Days��$ 7Days������ *
FontSize+ �� # Z   $��Last 30 Days��$ 30Days������ *
FontSize+ �� # Z   $��Year to Date��$ 
YearToDate������ *
FontSize+ �� # Z 9� $��1��$��0��$	��5,5q�$	��5,5q�$��Tournament Cost Stats�� 9� 
IsExpanded$
 True��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Cost: �� � ToolTip$* $Total buy-ins of all the tournaments��$��Right=����� *
FontSize+�� $��Highest Cost: ��$$ Highest buy-in of a tournament��$��Right=����� *
FontSize+�� $��Lowest Cost: ��$# Lowest buy-in of a tournament��$��Right=����� *
FontSize+�� $��Average Cost: ��$$ Average buy-in of a tournament��$��Right=����� *
FontSize+�� $��1��.+���  �� ContentStringFormat$ C��$* $Total buy-ins of all the tournaments��$
��Left=����� *Stats.TotalCost+���� *
FontSize+�� $ C��$$ Highest buy-in of a tournament��$
��Left=����� *Stats.HighestCost+���� *
FontSize+�� $ C��$# Lowest buy-in of a tournament��$
��Left=����� *Stats.LowestCost+���� *
FontSize+�� $ C��$$ Average buy-in of a tournament��$
��Left=����� *Stats.AverageCost+���� *
FontSize+9� $��1��$��1��$	��5,5q�$	��5,5q�$��Tournament Buy-in Stats��$
 True��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Buy-in: ��$' !Total cost of all the tournaments��$��Right=����� *
FontSize+�� $��Highest Buy-in: ��$" Highest cost of a tournament��$��Right=����� *
FontSize+�� $��Lowest Buy-in: ��$! Lowest cost of a tournament��$��Right=����� *
FontSize+�� $��Average Buy-in: ��$" Average cost of a tournament��$��Right=����� *
FontSize+�� $��1��.+��� $ C��$' !Total cost of all the tournaments��$
��Left=����� *Stats.TotalBuyinCost+���� *
FontSize+�� $ C��$" Highest cost of a tournament��$
��Left=����� *Stats.HighestBuyin+���� *
FontSize+�� $ C��$! Lowest cost of a tournament��$
��Left=����� *Stats.LowestBuyin+���� *
FontSize+�� $ C��$" Average cost of a tournament��$
��Left=����� *Stats.AverageBuyin+���� *
FontSize+9� $��1��$��2��$	��5,5q�$	��5,5q�$��Tournament Profit Stats��$
 True��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Profit: ��$) #Total profit of all the tournaments��$��Right=����� *
FontSize+�� $��Highest Profit: ��$$ Highest profit of a tournament��$��Right=����� *
FontSize+�� $��Lowest Profit: ��$# Lowest profit of a tournament��$��Right=����� *
FontSize+�� $��Average Profit: ��$$ Average profit of a tournament��$��Right=����� *
FontSize+�� $��1��.+��� $ C��$) #Total profit of all the tournaments��$
��Left=����� *Stats.TotalProfit+���� *
FontSize+�� $ C��$$ Highest profit of a tournament��$
��Left=����� *Stats.HighestProfit+���� *
FontSize+�� $ C��$# Lowest profit of a tournament��$
��Left=����� *Stats.LowestProfit+���� *
FontSize+�� $ C��$$ Average profit of a tournament��$
��Left=����� *Stats.AverageProfit+���� *
FontSize+9� $��1��$��3��$	��5,5q�$	��5,5q�$��Tournament Prize Stats��$
 True��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Prizes: ��$- 'Total prizes won of all the tournaments��$��Right=����� *
FontSize+�� $��Highest Prizes: ��$' !Highest prize won in a tournament��$��Right=����� *
FontSize+�� $��Lowest Prize: ��$&  Lowest prize won in a tournament��$��Right=����� *
FontSize+�� $��Average Prize: ��$' !Average prize won in a tournament��$��Right=����� *
FontSize+�� $��1��.+��� $ C��$) #Total profit of all the tournaments��$
��Left=����� *Stats.TotalPrizeWon+���� *
FontSize+�� $ C��$$ Highest profit of a tournament��$
��Left=����� *Stats.HighestPrizeWon+���� *
FontSize+�� $ C��$# Lowest profit of a tournament��$
��Left=����� *Stats.LowestPrizeWon+���� *
FontSize+�� $ C��$$ Average profit of a tournament��$
��Left=����� *Stats.AveragePrizeWon+���� *
FontSize+9� $��2��$��0��$	��5,5q�$	��5,5q�$��Tournament Overall Stats��$
 True��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Hourly: ��$- 'Total prizes won of all the tournaments��$��Right=����� *
FontSize+�� $��$/Tournament: ��$' !Highest prize won in a tournament��$��Right=����� *
FontSize+�� $��ROI: ��$&  Lowest prize won in a tournament��$��Right=����� *
FontSize+�� $��ITM: ��$' !Average prize won in a tournament��$��Right=����� *
FontSize+�� $��1��.+��� $ C��$C =hourly rate for the sessions including just these tournaments��$
��Left=����� *Stats.Hourly+���� *
FontSize+�� $ C��$/ )profit per tournament for the tournaments��$
��Left=����� *Stats.ProfitPerTournament+���� *
FontSize+�� $ P2��$. (return on investment for the tournaments��$
��Left=����� *	Stats.Roi+���� *
FontSize+�� $ P2��$1 +In the money percentage for the tournaments��$
��Left=����� *	Stats.Itm+���� *
FontSize+9� $��2��$��1��$	��5,5q�$	��5,5q�$��Tournament Counting Stats��$
 True��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Tournaments: ��$ Total tournaments played��$��Right=����� *
FontSize+�� $��Winning Streak: ��$# Most tournaments won in a row��$��Right=����� *
FontSize+�� $��Losing Streak: ��$$ Most tournaments lost in a row��$��Right=����� *
FontSize+�� $��Cashing Stream: ��$&  Most tournaments cashed in a row��$��Right=����� *
FontSize+�� $��Stone Bubbles: ��$J DNumbers of times you've finished on the stone bubble of a tournament��$��Right=����� *
FontSize+�� $��Win Tournament: ��$1 +Percentage of the time you win a tournament��$��Right=����� *
FontSize+�� $��1��.+��� $ Total tournaments played��$
��Left=����� *Stats.TournamentsPlayed+���� *
FontSize+�� $# Most tournaments won in a row��$
��Left=����� *Stats.WinningStreak+���� *
FontSize+�� $$ Most tournaments lost in a row��$
��Left=����� *Stats.LosingStreak+���� *
FontSize+�� $&  Most tournaments cashed in a row��$
��Left=����� *Stats.CashingStreak+���� *
FontSize+�� $I CNumber of times you've finished on the stone bubble of a tournament��$
��Left=����� *Stats.StoneBubbles+���� *
FontSize+�� $ P2��$1 +Percentage of the time you win a tournament��$
��Left=����� *Stats.WinningPercentage+���� *
FontSize+9� $��2��$��2��$	��5,5q�$	��5,5q�$"��Tournament Final Table Stats��$
 True��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Final Tables: ��$$ Total final tables you've made��$��Right=����� *
FontSize+�� $��Make Final Table: ��$3 -Percentage of the time you make a final table��$��Right=����� *
FontSize+�� $��Total Wins: ��$ Total final table wins��$��Right=����� *
FontSize+�� $��Win Final Table: ��$? 9Percentage of the time you win after making a final table��$��Right=����� *
FontSize+�� $��Final Table Bubbles: ��$: 4Number of times you finish on the final table bubble��$��Right=����� *
FontSize+�� $��1��.+��� $$ Total final tables you've make��$
��Left=����� *Stats.FinalTables+���� *
FontSize+�� $ P2��$3 -Percentage of the time you make a final table��$
��Left=����� *Stats.FinalTablePercentage+���� *
FontSize+�� $ Total final table wins��$
��Left=����� *Stats.TotalWins+���� *
FontSize+�� $ P2��$? 9Percentage of the time you win after making a final table��$
��Left=����� *#!Stats.FinalTableWinningPercentage+���� *
FontSize+�� $: 4Number of times you finish on the final table bubble��$
��Left=����� *Stats.FinalTableBubbles+���� *
FontSize+9� $��2��$��3��$	��5,5q�$	��5,5q�$��Tournament Finish % Stats��$
 True��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Average Beaten: ��$1 +Percentage of the field you beat on average��$��Right=����� *
FontSize+�� $��Late Finish: ��$H BPercentage of the time you finish in the final 10% of a tournament��$��Right=����� *
FontSize+�� $��Mid/Late Finish: ��$C =Percentage of the time you within 70%-90% of the field beaten��$��Right=����� *
FontSize+�� $��Mid Finish: ��$C =Percentage of the time you within 30%-70% of the field beaten��$��Right=����� *
FontSize+�� $��Early/Mid Finish: ��$C =Percentage of the time you within 10%-30% of the field beaten��$��Right=����� *
FontSize+�� $��Early Finish: ��$L FPercentage of the time you finish within the first 10% of a tournament��$��Right=����� *
FontSize+�� $��1��.+��� $ P2��$$ Total final tables you've make��$
��Left=����� *Stats.AveragePercentBeaten+���� *
FontSize+�� $ P2��$3 -Percentage of the time you make a final table��$
��Left=����� *Stats.LateFinish+���� *
FontSize+�� $ P2��$ Total final table wins��$
��Left=����� *Stats.MidLateFinish+���� *
FontSize+�� $ P2��$? 9Percentage of the time you win after making a final table��$
��Left=����� *Stats.MidFinish+���� *
FontSize+�� $ P2��$ Total final table wins��$
��Left=����� *Stats.EarlyMidFinish+���� *
FontSize+�� $ P2��$ Total final table wins��$
��Left=����� *Stats.EarlyFinish+���� *
FontSize+9� $��3��$��0��$
 True��$	��5,5q�$��Tournament Graphs��.���� .+��� $��View Tournaments��$��ViewTournaments��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $��Finish Position Graph��$��FinishPositionGraph��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $��Tournament Profit Graph��$��ProfitGraph��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+9� $��3��$��1��$	��5,5q�$	��5,5q�$��Tournament Entrant Stats��$
 True��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Most Entrants: ��$- 'Most number of entrants in a tournament��$��Right=����� *
FontSize+�� $��Lowest Entrants: ��$/ )Lowest number of entrants in a tournament��$��Right=����� *
FontSize+�� $��Average Entrants: ��$0 *Average number of entrants in a tournament��$��Right=����� *
FontSize+�� $��Best Finish: ��$- 'Highest you've finished in a tournament��$��Right=����� *
FontSize+�� $��Worst Finish: ��$, &Lowest you've finished in a tournament��$��Right=����� *
FontSize+�� $��Average Finish: ��$. (Average place you finish in a tournament��$��Right=����� *
FontSize+�� $��1��.+��� $- 'Most number of entrants in a tournament��$
��Left=����� *Stats.HighestEntrants+���� *
FontSize+�� $/ )Lowest number of entrants in a tournament��$
��Left=����� *Stats.LowestEntrants+���� *
FontSize+�� $0 *Average number of entrants in a tournament��$
��Left=����� *Stats.AverageEntrants+���� *
FontSize+�� $- 'Most number of entrants in a tournament��$
��Left=����� *Stats.LowestFinishPosition+���� *
FontSize+�� $- 'Most number of entrants in a tournament��$
��Left=����� *Stats.HighestFinishPosition+���� *
FontSize+�� $- 'Most number of entrants in a tournament��$
��Left=����� *Stats.AverageFinishPosition+���� *
FontSize+9� $��3��$��2��$	��5,5q�$	��5,5q�$��Tournament Rebuy Stats��$
 True��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Most Rebuys: ��$> 8Highest number of times you've re-bought in a tournament��$��Right=����� *
FontSize+�� $��Lowest Rebuys: ��$F @Lowest non-zero number of times you've re-bought in a tournament��$��Right=����� *
FontSize+�� $��Average Rebuys: ��$; 5Average number of times you re-bought in a tournament��$��Right=����� *
FontSize+�� $��1��.+��� $> 8Highest number of times you've re-bought in a tournament��$
��Left=����� *Stats.HighestRebuys+���� *
FontSize+�� $= 7Lowest number of times you've re-bought in a tournament��$
��Left=����� *Stats.LowestRebuys+���� *
FontSize+�� $ G2��$Q KAverage number of times you re-bought in a tournament when you've re-bought��$
��Left=����� *Stats.AverageRebuys+���� *
FontSize+9� $��3��$��3��$	��5,5q�$	��5,5q�$��Tournament Time Stats��$
 True��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��First Date Played: ��$. (Earliest date a tournament was played on��$��Right=����� *
FontSize+�� $��Last Date Played: ��$, &Latest date a tournament was played on��$��Right=����� *
FontSize+�� $��Total Days Played: ��$: 4Total number of days you have played a tournament on��$��Right=����� *
FontSize+�� $��Longest Tournament: ��$8 2Longest length of time a tournament was played for��$��Right=����� *
FontSize+�� $��Shortest Tournament: ��$9 3Shortest length of time a tournament was played for��$��Right=����� *
FontSize+�� $��Average Tournament: ��$8 2Average length of time a tournament was played for��$��Right=����� *
FontSize+�� $��1��.+��� $ M/dd/yy��$. (Earliest date a tournament was played on��$
��Left=����� *Stats.EarliestStartDate+���� *
FontSize+�� $ M/dd/yy��$. (Earliest date a tournament was played on��$
��Left=����� *Stats.LatestStartDate+���� *
FontSize+�� $. (Earliest date a tournament was played on��$
��Left=����� *Stats.TotalDays+���� *
FontSize+�� $ {0:hh}:{0:mm}��$. (Earliest date a tournament was played on��$
��Left=����� *Stats.LongestLength+���� *
FontSize+�� $ {0:hh}:{0:mm}��$. (Earliest date a tournament was played on��$
��Left=����� *Stats.ShortestLength+���� *
FontSize+�� $ {0:hh}:{0:mm}��$. (Earliest date a tournament was played on��$
��Left=����� *Stats.AverageLength+���� *
FontSize+