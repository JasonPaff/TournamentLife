   M S B A M L   `   `   `  ���� h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager   Tournament Life0   *LiveTracker.Views.Results.QuickResultsView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a	 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf     	 
   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    � SizeToContent$  WidthAndHeight=� � 
ResizeMode$ NoResize=�$	��450��$	��800��,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � Title �� *Title+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ Loaded��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *AfterLoadCommand+.���  � ColumnDefinitions �� $
��Auto ��� $
��Auto �	 � RowDefinitions	 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Tournaments Played: ��$��0��$��0��
 � ToolTip$"
 Number of tournaments played������ *
FontSize+�� $��1��$��0��$"
 Number of tournaments played������ *#!TournamentStats.TournamentsPlayed+���� *
FontSize+�� $��Total Profit: ��$��0��$��1��$��Right=�$I
 CTotal profit made in tournaments played (total prizes - total cost)������ *
FontSize+��  �� ContentStringFormat$ C2��$��1��$��1��$I
 CTotal profit made in tournaments played (total prizes - total cost)������ *TournamentStats.TotalProfit+���� *
FontSize+�� $��Hourly: ��$��0��$��2��$��Right=�$Y
 SMoney made per hour of tournament session play (total profit / total session hours)������ *
FontSize+�� $ C2��$��1��$��2��$Y
 SMoney made per hour of tournament session play (total profit / total session hours)������ *SessionStats.Hourly+���� *
FontSize+�� $��$/Tournament: ��$��0��$��3��$��Right=�$J
 DMoney made per tournament played (total profit / tournaments played)������ *
FontSize+�� $ C2��$��1��$��3��$J
 DMoney made per tournament played (total profit / tournaments played)������ *%#TournamentStats.ProfitPerTournament+���� *
FontSize+�� $��ROI: ��$��0��$��4��$��Right=�$6
 0Return on investment (total profit / total cost)������ *
FontSize+�� $ P2��$��1��$��4��$6
 0Return on investment (total profit / total cost)������ *TournamentStats.Roi+���� *
FontSize+�� $��ITM: ��$��0��$��5��$��Right=�$5
 /Percentage of tournaments finished in the money������ *
FontSize+�� $ P2��$��1��$��5��$5
 /Percentage of tournaments finished in the money������ *TournamentStats.Itm+���� *
FontSize+�� $��Average Buyin: ��$��0��$��6��$��Right=�$C
 =Average cost per tournament (total cost / tournaments played)������ *
FontSize+�� $ C2��$��1��$��6��$G
 AAverage buy-in per tournament (total buy-in / tournaments played)������ *TournamentStats.AverageBuyin+���� *
FontSize+�� $��Average Total Cost: ��$��0��$��7��$��Right=�$C
 =Average cost per tournament (total cost / tournaments played)������ *
FontSize+�� $ C2��$��1��$��7��$a
 [Average buy-in per tournament (total cost (buy-in + rebuy(s) + addon) / tournaments played)������ *TournamentStats.AverageTotal+���� *
FontSize+�� $��Total Cost: ��$��0��$��8��$��Right=�$&
  Total cost of tournaments played������ *
FontSize+�� $ C2��$��1��$��8��$&
  Total cost of tournaments played������ *TournamentStats.TotalCost+���� *
FontSize+�� $��Total Prizes Won: ��$��0��$��9��$��Right=�$,
 &Total prizes won in tournaments played������ *
FontSize+�� $ C2��$��1��$��9��$,
 &Total prizes won in tournaments played������ *TournamentStats.TotalPrizeWon+���� *
FontSize+�� $��Time Played: ��$��0��$��10��$��Right=�$
 Total session time played������ *
FontSize+�� $ C2��$��1��$��10������ *
TimePlayed+���� *
FontSize+
 �� *Hours+