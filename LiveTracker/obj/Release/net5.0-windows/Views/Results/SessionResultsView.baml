   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=null;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager 2    ,LiveTracker.Views.Results.SessionResultsView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
    =commands"clr-namespace:LiveTracker.Commands   	 
    msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
    0  *Syncfusion.Windows.Shared.ChromelessWindow    UseNativeChrome$
  True�� � 
ResizeMode$ CanMinimize=�   ShowMaximizeButton$ False�� � SizeToContent$ WidthAndHeight=�$	��450��$	��400��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � Title �� *Title+.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�'  !Syncfusion.Windows.Shared.MenuAdv  $��0��$��0��$��2��   
ExpandMode$ ExpandOnClick=�.@�+  %Syncfusion.Windows.Shared.MenuItemAdv  $
��File������ *
FontSize+.@�  $��Close Window��	 � ToolTip$&	  Close the Session Results window��
   CommandParameter$

 Exit������ *
FontSize+   Command �� 1   +LiveTracker.Commands.SessionResultsCommands   MenuItem �� Source# Z � $��Center=�$��2��$��0��$��7�� �� $
��Auto ��� $
��Auto � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�9� $��1��$��0�� 9� 
IsExpanded$
 True��$	��5,5q�$��Session Graphs  ��.���� .+��� $��Sessions Profit Graph��$��SessionProfitGraph��$2	 ,Shows a profit graph for the sessions played��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $��View Tournaments��$��ViewTournaments��$J	 DView/Edit the tournaments in the sessions you have played in a table��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $#��Single Session Position Graph��$��FinishPositionGraph��$Z	 TShows a bar graph of the finish positions for the tournaments in the sessions played��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $!��Single Session Profit Graph��$1	 +Shows a profit graph for the session played��$��ProfitGraph��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+9� $��0��$��0��$
 True��$	��5,5q�$��Session Profit Stats  ��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Hourly:��$��Right=����� *
FontSize+�� $��Right=����� *MoneyPerTournamentHeader+���� *
FontSize+�� $��Total Profit:��$��Right=����� *
FontSize+�� $��Average Profit:��$��Right=����� *
FontSize+�� $��Most Profit:��$��Right=����� *
FontSize+�� $��Least Profit:��$��Right=����� *
FontSize+�� $��1��.+��� $R	 LMoney per hour made across all sessions (total profit / total session hours)������ *Hourly+���� *
FontSize+�� $W	 QMoney made per tournament across all sessions (total profit / tournaments played)������ *DollarsPerTournament+���� *
FontSize+�� $-	 'Total profit made from all the sessions������ *SessionsTotalProfit+���� *
FontSize+�� $&	  Average profit made in a session������ *SessionsAverageProfit+���� *
FontSize+�� $#	 Most profit made in a session������ *SessionsMostProfit+���� *
FontSize+�� $$	 Least profit made in a session������ *SessionsLeastProfit+���� *
FontSize+9� $��0��$��1��$
 True��$	��5,5q�$��Session Cost Stats  ��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Cost:��$��Right=����� *
FontSize+�� $��Highest Cost:��$��Right=����� *
FontSize+�� $��Average Cost:��$��Right=����� *
FontSize+�� $��Lowest Cost:��$��Right=����� *
FontSize+�� $��1��.+��� $ 	 Total cost of all sessions������ *SessionsTotalBuyins+���� *
FontSize+�� $	 Highest cost of a session������ *SessionsMostBuyins+���� *
FontSize+�� $	 Average cost of a session������ *SessionsAverageBuyins+���� *
FontSize+�� $	 Lowest cost of a session������ *SessionsLeastBuyins+���� *
FontSize+9� $��0��$��2��$
 True��$	��5,5q�$��Session Prizes Stats  ��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Prizes:��$��Right=����� *
FontSize+�� $��Most Prizes:��$��Right=����� *
FontSize+�� $��Average Prizes:��$��Right=����� *
FontSize+�� $��Least Prizes:��$��Right=����� *
FontSize+�� $��1��.+��� $&	  Total prizes won in all sessions������ *SessionsTotalPrizes+���� *
FontSize+�� $$	 Biggest prize won in a session������ *SessionsMostPrizes+���� *
FontSize+�� $$	 Average prize won in a session������ *SessionsAveragePrizes+���� *
FontSize+�� $,	 &Lowest non-zero prize won in a session������ *SessionsLeastPrizes+���� *
FontSize+9� $��0��$��3��$
 True��$	��5,5q�$ ��Session Tournament Stats  ��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Tournaments:��$��Right=����� *
FontSize+�� $��Most Tournaments:��$��Right=����� *
FontSize+�� $��Average Tournament:��$��Right=����� *
FontSize+�� $��Least Tournaments:��$��Right=����� *
FontSize+�� $��1��.+��� $8	 2Total number of tournaments played in all sessions��$
��Left=����� *SessionsTotalTournaments+���� *
FontSize+�� $4	 .Most number of tournaments played in a session��$
��Left=����� *SessionsMostTournaments+���� *
FontSize+�� $6	 0Average number of tournaments played per session��$
��Left=����� *SessionsAverageTournaments+���� *
FontSize+�� $5	 /Least number of tournaments played in a session��$
��Left=����� *SessionsLeastTournaments+���� *
FontSize+9� $��1��$��1��$
 True��$	��5,5q�$��Session Counting Stats  ��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Sessions Played:��$��Right=����� *
FontSize+�� $��Sessions Won:��$��Right=����� *
FontSize+�� $��Sessions Lost:��$��Right=����� *
FontSize+�� $��Losing Streak:��$��Right=����� *
FontSize+�� $��Winning Streak:��$��Right=����� *
FontSize+�� $��1��.+��� $%	 Total number of sessions played��$
��Left=����� *SessionsPlayed+���� *
FontSize+�� $"	 Total number of sessions won��$
��Left=����� *SessionsWon+���� *
FontSize+�� $#	 Total number of sessions lost��$
��Left=����� *SessionsLost+���� *
FontSize+�� $#	 Most losing sessions in a row��$
��Left=����� *SessionsLosingStreak+���� *
FontSize+�� $$	 Most winning sessions in a row��$
��Left=����� *SessionsWinningStreak+���� *
FontSize+9� $��1��$��2��$��2��$
 True��$	��5,5q�$��Session Time Stats  ��.���  �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Average Session:��$��Right=����� *
FontSize+�� $��Longest Session:��$��Right=����� *
FontSize+�� $��Shortest Session:��$��Right=����� *
FontSize+�� $��Total Sessions:��$��Right=����� *
FontSize+�� $��1��.+��� $%	 Average time played per session��$
��Left=����� *SessionsAverageTime+���� *
FontSize+�� $	 Longest session played��$
��Left=����� *SessionsLongestTime+���� *
FontSize+�� $	 Shortest session played��$
��Left=����� *SessionsShortestTime+���� *
FontSize+�� $'	 !Total time of all sessions played��$
��Left=����� *SessionsTotalTime+���� *
FontSize+9� $
 True��$��Sessions Played��$	��5,5q�$��4��$��0��.����  �� SelectionMode$ Single=�$	��300��$��1q�$	��300��$	��300��$��Center=� �� SelectedItem �� *SelectedSession+���� *SessionList+���� .R��� ���� *DisplayString+����  �� 
TargetType" ��.���  �� Property �@��
IsSelected �� Value ��  �� Mode$ TwoWay=� �� Path$ 
IsSelected���  �@�ToolTip �� *Description+9� $��Selected Session Stats��$��1q�$
 True��$	��5,5q�$��4��$��2��.���  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0�� �� Orientation$ Vertical=�.+��� $��Tournament Count:��$��Right=����� *
FontSize+�� $��Session ROI:��$��Right=����� *
FontSize+�� $��Right=����� *MoneyPerTournamentHeader+���� *
FontSize+�� $��Total Profit:��$��Right=����� *
FontSize+�� $��Total Cost:��$��Right=����� *
FontSize+�� $��Total Prizes:��$��Right=����� *
FontSize+�� $��Average Profit:��$��Right=����� *
FontSize+�� $��Average Cost:��$��Right=����� *
FontSize+�� $��Average Prize:��$��Right=����� *
FontSize+�� $��Session Length:��$��Right=����� *
FontSize+�� $��Average Length:��$��Right=����� *
FontSize+�� $��1��$ Vertical=�.+��� $6	 0Number of tournaments playing during the session������ *SelectedTournamentCount+���� *
FontSize+�� $9	 3Total ROI for the session (total profit/total cost)������ *SelectedRoi+���� *
FontSize+�� $Q	 KMoney made per tournament for the session (total profit/tournaments played)������ *SelectedProfitPerTournament+���� *
FontSize+�� $H	 BTotal profit for the selected sessions (total prizes - total cost)������ *SelectedTotalProfit+���� *
FontSize+�� $9	 3Total cost of all tournaments played in the session������ *SelectedTotalCost+���� *
FontSize+�� $<	 6Total prizes won in all the tournaments in the session������ *SelectedTotalPrizesWon+���� *
FontSize+�� $2	 ,Average profit per tournament in the session������ *SelectedAverageProfit+���� *
FontSize+�� $0	 *Average cost per tournament in the session������ *SelectedAverageCost+���� *
FontSize+�� $5	 /Average prize won per tournament in the session������ *SelectedAveragePrizeWon+���� *
FontSize+�� $!	 Total length of the session������ *SelectedSessionLength+���� *
FontSize+�� $'	 !Average length of each tournament������ *SelectedAverageLength+���� *
FontSize+�� $��2��$ Vertical=�.+��� $��Session ITM:��$��Right=����� *
FontSize+�� $��Late Finish:��$��Right=����� *
FontSize+�� $��Mid/Late Finish:��$��Right=����� *
FontSize+�� $��Mid Finish:��$��Right=����� *
FontSize+�� $��Early/Mid Finish:��$��Right=����� *
FontSize+�� $��Early Finish:��$��Right=����� *
FontSize+�� $��Average Finish:��$��Right=����� *
FontSize+�� $��Final Tables:��$��Right=����� *
FontSize+�� $��Min Cashes:��$��Right=����� *
FontSize+�� $��Near Bubbles:��$��Right=����� *
FontSize+�� $��Stone Bubbles:��$��Right=����� *
FontSize+�� $��3��$ Vertical=�.+��� $-	 'In the money percentage for the session������ *SelectedItmPercentage+���� *
FontSize+�� $W	 QPercentage of tournaments finished in late position (90%-100% of the field beaten������ *SelectedLateFinish+���� *
FontSize+�� $Z	 TPercentage of tournaments finished in mid/late position (70%-90% of the field beaten������ *SelectedMidLateFinish+���� *
FontSize+�� $U	 OPercentage of tournaments finished in mid position (30%-70% of the field beaten������ *SelectedMidFinish+���� *
FontSize+�� $[	 UPercentage of tournaments finished in early/mid position (10%-30% of the field beaten������ *SelectedEarlyMidFinish+���� *
FontSize+�� $V	 PPercentage of tournaments finished in early position (0%-10% of the field beaten������ *SelectedEarlyFinish+���� *
FontSize+�� $;	 5Average percentage of the field beaten in the session������ *SelectedAverageFinish+���� *
FontSize+�� $;	 5Final tables made in the session (includes satellite)������ *SelectedFinalTables+���� *
FontSize+�� $O	 INumber of times finished a few places after the bubble broke this session������ *SelectedMinCashes+���� *
FontSize+�� $V	 PNumber of times finished within a few places of the bubble breaking this session������ *SelectedNearBubbles+���� *
FontSize+�� $B	 <Number of times your stone bubbled a tournament this session������ *SelectedStoneBubbles+���� *
FontSize+