   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.0.1.2, Culture=neutral, PublicKeyToken=null;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager 2    ,LiveTracker.Views.Results.SessionResultsView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a	 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
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
 !Syncfusion.Windows.Shared.MenuAdv  $��0��$��0��$��2��
   
ExpandMode$
 ExpandOnClick=�.@�+ 
 %Syncfusion.Windows.Shared.MenuItemAdv  $
��File������ *
FontSize+.@�  $
��Exit��   CommandParameter$
 Exit������ *
FontSize+   Command �� 1   +LiveTracker.Commands.SessionResultsCommands   MenuItem �� Source# Z   $��Filter������ *
FontSize+.@�  $��
New Filter��$ 	NewFilter������ *
FontSize+ �� # Z   $��Load Filter��$ 
LoadFilter������ *
FontSize+ �� # Z   $��Show Current Filter��$ ShowCurrentFilter������ *
FontSize+ �� # Z   $��Clear Filter��$ ClearFilter������ *
FontSize+ �� # Z � $��Center=�$��2��$��0��$��7�� �� $
��Auto ��� $
��Auto �	 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�9� $��1��$��0�� 9� 
IsExpanded$
 True��$	��5,5q�$��Session Graphs  ��.���� .+��� $��Sessions Profit Graph��$��SessionProfitGraph��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $��View Tournaments��$��ViewTournaments��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $#��Single Session Position Graph��$��FinishPositionGraph��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+�� $!��Single Session Profit Graph��$��ProfitGraph��$	��5,5q�$	��5,5q����� # Z ���� *
FontSize+9� $��0��$��0��$
 True��$	��5,5q�$��Session Profit Stats  ��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Hourly:�� � ToolTip$R LMoney per hour made across all sessions (total profit / total session hours)��$��Right=����� *
FontSize+�� $��$/Tournament:��$W QMoney made per tournament across all sessions (total profit / tournaments played)��$��Right=����� *
FontSize+�� $��Total Profit:��$- 'Total profit made from all the sessions��$��Right=����� *
FontSize+�� $��Average Profit:��$&  Average profit made in a session��$��Right=����� *
FontSize+�� $��Most Profit:��$# Most profit made in a session��$��Right=����� *
FontSize+�� $��Least Profit:��$$ Least profit made in a session��$��Right=����� *
FontSize+�� $��1��.+��� $R LMoney per hour made across all sessions (total profit / total session hours)������ *Hourly+���� *
FontSize+�� $W QMoney made per tournament across all sessions (total profit / tournaments played)������ *DollarsPerTournament+���� *
FontSize+�� $- 'Total profit made from all the sessions������ *SessionsTotalProfit+���� *
FontSize+�� $&  Average profit made in a session������ *SessionsAverageProfit+���� *
FontSize+�� $# Most profit made in a session������ *SessionsMostProfit+���� *
FontSize+�� $$ Least profit made in a session������ *SessionsLeastProfit+���� *
FontSize+9� $��0��$��1��$
 True��$	��5,5q�$��Session Cost Stats  ��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Cost:��$$ Total cost of all the sessions��$��Right=����� *
FontSize+�� $��Highest Cost:��$ Highest cost of a session��$��Right=����� *
FontSize+�� $��Average Cost:��$ Average cost of a session��$��Right=����� *
FontSize+�� $��Lowest Cost:��$ Lowest cost of a session��$��Right=����� *
FontSize+�� $��1��.+��� $  Total cost of all sessions������ *SessionsTotalBuyins+���� *
FontSize+�� $ Highest cost of a session������ *SessionsMostBuyins+���� *
FontSize+�� $ Average cost of a session������ *SessionsAverageBuyins+���� *
FontSize+�� $ Lowest cost of a session������ *SessionsLeastBuyins+���� *
FontSize+9� $��0��$��2��$
 True��$	��5,5q�$��Session Prizes Stats  ��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Prizes:��$&  Total prizes won in all sessions��$��Right=����� *
FontSize+�� $��Most Prizes:��$$ Highest prize won in a session��$��Right=����� *
FontSize+�� $��Average Prizes:��$$ Average prize won in a session��$��Right=����� *
FontSize+�� $��Least Prizes:��$# Lowest prize won in a session��$��Right=����� *
FontSize+�� $��1��.+��� $&  Total prizes won in all sessions������ *SessionsTotalPrizes+���� *
FontSize+�� $$ Biggest prize won in a session������ *SessionsMostPrizes+���� *
FontSize+�� $$ Average prize won in a session������ *SessionsAveragePrizes+���� *
FontSize+�� $, &Lowest non-zero prize won in a session������ *SessionsLeastPrizes+���� *
FontSize+9� $��0��$��3��$
 True��$	��5,5q�$ ��Session Tournament Stats  ��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Total Tournaments:��$< 6Total number of tournaments played across all sessions��$��Right=����� *
FontSize+�� $��Most Tournaments:��$4 .Most number of tournaments played in a session��$��Right=����� *
FontSize+�� $��Average Tournament:��$6 0Average number of tournaments played per session��$��Right=����� *
FontSize+�� $��Least Tournaments:��$5 /Least number of tournaments played in a session��$��Right=����� *
FontSize+�� $��1��.+��� $8 2Total number of tournaments played in all sessions��$
��Left=����� *SessionsTotalTournaments+���� *
FontSize+�� $4 .Most number of tournaments played in a session��$
��Left=����� *SessionsMostTournaments+���� *
FontSize+�� $6 0Average number of tournaments played per session��$
��Left=����� *SessionsAverageTournaments+���� *
FontSize+�� $5 /Least number of tournaments played in a session��$
��Left=����� *SessionsLeastTournaments+���� *
FontSize+9� $��1��$��1��$
 True��$	��5,5q�$��Session Counting Stats  ��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Sessions Played:��$% Total number of sessions played��$��Right=����� *
FontSize+�� $��Sessions Won:��$" Total number of sessions won��$��Right=����� *
FontSize+�� $��Sessions Lost:��$# Total number of sessions lost��$��Right=����� *
FontSize+�� $��Losing Streak:��$# Most losing sessions in a row��$��Right=����� *
FontSize+�� $��Winning Streak:��$$ Most winning sessions in a row��$��Right=����� *
FontSize+�� $��1��.+��� $% Total number of sessions played��$
��Left=����� *SessionsPlayed+���� *
FontSize+�� $" Total number of sessions won��$
��Left=����� *SessionsWon+���� *
FontSize+�� $# Total number of sessions lost��$
��Left=����� *SessionsLost+���� *
FontSize+�� $# Most losing sessions in a row��$
��Left=����� *SessionsLosingStreak+���� *
FontSize+�� $$ Most winning sessions in a row��$
��Left=����� *SessionsWinningStreak+���� *
FontSize+9� $��1��$��2��$��2��$
 True��$	��5,5q�$��Session Time Stats  ��.��� 	 �� $
��Auto ��� $
��Auto �.+��� $��0��.+��� $��Average Time:��$% Average time played per session��$��Right=����� *
FontSize+�� $��Longest Time:��$ Longest session played��$��Right=����� *
FontSize+�� $��Shortest Time:��$ Shortest session played��$��Right=����� *
FontSize+�� $��Total Time:��$' !Total time of all sessions played��$��Right=����� *
FontSize+�� $��1��.+��� $% Average time played per session��$
��Left=����� *SessionsAverageTime+���� *
FontSize+�� $ Longest session played��$
��Left=����� *SessionsLongestTime+���� *
FontSize+�� $ Shortest session played��$
��Left=����� *SessionsShortestTime+���� *
FontSize+�� $' !Total time of all sessions played��$
��Left=����� *SessionsTotalTime+���� *
FontSize+9� $
 True��$��Sessions Played��$	��5,5q�$��4��$��0��.����  �� SelectionMode$ Single=�$	��300��$��1q�$	��300��$	��300��$��Center=� �� SelectedItem �� *SelectedSession+���� *SessionList+���� .R��� ���� *DisplayString+����  �� 
TargetType" ��.���  �� Property �@��
IsSelected �� Value ��  �� Mode$ TwoWay=� �� Path$ 
IsSelected���  �@�ToolTip �� *Description+9� $��Selected Session Stats��$��1q�$
 True��$	��5,5q�$��4��$��2��.��� 	 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0�� �� Orientation$ Vertical=�.+��� $��Tournament Count:��$6 0Number of tournaments playing during the session��$��Right=����� *
FontSize+�� $��Session ROI:��$9 3Total ROI for the session (total profit/total cost)��$��Right=����� *
FontSize+�� $��$/Tournament:��$Q KMoney made per tournament for the session (total profit/tournaments played)��$��Right=����� *
FontSize+�� $��Total Profit:��$H BTotal profit for the selected sessions (total prizes - total cost)��$��Right=����� *
FontSize+�� $��Total Cost:��$9 3Total cost of all tournaments played in the session��$��Right=����� *
FontSize+�� $��Total Prizes:��$< 6Total prizes won in all the tournaments in the session��$��Right=����� *
FontSize+�� $��Average Profit:��$2 ,Average profit per tournament in the session��$��Right=����� *
FontSize+�� $��Average Cost:��$0 *Average cost per tournament in the session��$��Right=����� *
FontSize+�� $��Average Prize:��$5 /Average prize won per tournament in the session��$��Right=����� *
FontSize+�� $��Session Length:��$! Total length of the session��$��Right=����� *
FontSize+�� $��Average Length:��$' !Average length of each tournament��$��Right=����� *
FontSize+�� $��1��$ Vertical=�.+��� $6 0Number of tournaments playing during the session������ *SelectedTournamentCount+���� *
FontSize+�� $9 3Total ROI for the session (total profit/total cost)������ *SelectedRoi+���� *
FontSize+�� $Q KMoney made per tournament for the session (total profit/tournaments played)������ *SelectedProfitPerTournament+���� *
FontSize+�� $H BTotal profit for the selected sessions (total prizes - total cost)������ *SelectedTotalProfit+���� *
FontSize+�� $9 3Total cost of all tournaments played in the session������ *SelectedTotalCost+���� *
FontSize+�� $< 6Total prizes won in all the tournaments in the session������ *SelectedTotalPrizesWon+���� *
FontSize+�� $2 ,Average profit per tournament in the session������ *SelectedAverageProfit+���� *
FontSize+�� $0 *Average cost per tournament in the session������ *SelectedAverageCost+���� *
FontSize+�� $5 /Average prize won per tournament in the session������ *SelectedAveragePrizeWon+���� *
FontSize+�� $! Total length of the session������ *SelectedSessionLength+���� *
FontSize+�� $' !Average length of each tournament������ *SelectedAverageLength+���� *
FontSize+�� $��2��$ Vertical=�.+��� $��Session ITM:��$- 'In the money percentage for the session��$��Right=����� *
FontSize+�� $��Late Finish:��$W QPercentage of tournaments finished in late position (90%-100% of the field beaten��$��Right=����� *
FontSize+�� $��Mid/Late Finish:��$Z TPercentage of tournaments finished in mid/late position (70%-90% of the field beaten��$��Right=����� *
FontSize+�� $��Mid Finish:��$U OPercentage of tournaments finished in mid position (30%-70% of the field beaten��$��Right=����� *
FontSize+�� $��Early/Mid Finish:��$[ UPercentage of tournaments finished in early/mid position (10%-30% of the field beaten��$��Right=����� *
FontSize+�� $��Early Finish:��$V PPercentage of tournaments finished in early position (0%-10% of the field beaten��$��Right=����� *
FontSize+�� $��Average Finish:��$; 5Average percentage of the field beaten in the session��$��Right=����� *
FontSize+�� $��Final Tables:��$; 5Final tables made in the session (includes satellite)��$��Right=����� *
FontSize+�� $��Min Cashes:��$O INumber of times finished a few places after the bubble broke this session��$��Right=����� *
FontSize+�� $��Near Bubbles:��$V PNumber of times finished within a few places of the bubble breaking this session��$��Right=����� *
FontSize+�� $��Stone Bubbles:��$B <Number of times your stone bubbled a tournament this session��$��Right=����� *
FontSize+�� $��3��$ Vertical=�.+��� $- 'In the money percentage for the session������ *SelectedItmPercentage+���� *
FontSize+�� $W QPercentage of tournaments finished in late position (90%-100% of the field beaten������ *SelectedLateFinish+���� *
FontSize+�� $Z TPercentage of tournaments finished in mid/late position (70%-90% of the field beaten������ *SelectedMidLateFinish+���� *
FontSize+�� $U OPercentage of tournaments finished in mid position (30%-70% of the field beaten������ *SelectedMidFinish+���� *
FontSize+�� $[ UPercentage of tournaments finished in early/mid position (10%-30% of the field beaten������ *SelectedEarlyMidFinish+���� *
FontSize+�� $V PPercentage of tournaments finished in early position (0%-10% of the field beaten������ *SelectedEarlyFinish+���� *
FontSize+�� $; 5Average percentage of the field beaten in the session������ *SelectedAverageFinish+���� *
FontSize+�� $; 5Final tables made in the session (includes satellite)������ *SelectedFinalTables+���� *
FontSize+�� $O INumber of times finished a few places after the bubble broke this session������ *SelectedMinCashes+���� *
FontSize+�� $V PNumber of times finished within a few places of the bubble breaking this session������ *SelectedNearBubbles+���� *
FontSize+�� $B <Number of times your stone bubbled a tournament this session������ *SelectedStoneBubbles+���� *
FontSize+