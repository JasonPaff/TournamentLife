   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5       Tournament Life4   .Tournament_Life.Views.Results.QuickResultsView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     5      0  *Syncfusion.Windows.Shared.ChromelessWindow    UseNativeChrome$
  True��5       � SizeToContent$ WidthAndHeight=�5       � 
ResizeMode$ CanMinimize=�5         ShowMaximizeButton$ False��5      $	��300��5      $	��100��5      ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5	      �� *Theme+ � Title 5      �� *Title+.��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5"      $
��Auto �6   �� 5#      $
��Auto �6   �� 5$      $
��Auto �6   .+�5=      �� $��Tournaments Played: ��6   $��0��6+   $��1��6;    � ToolTip$" Number of tournaments played��6f   ��6H   �� *
FontSize+6f   �� 5>      $��1��6K   $��1��6[   $" Number of tournaments played��6�   ��6   �� *#!TournamentStats.TournamentsPlayed+��6i   �� *
FontSize+6�   �� 5@      $��Total Profit: ��6   $��0��6-   $��2��6=   $��Right=�6J   $I CTotal profit made in tournaments played (total prizes - total cost)��6�   ��6n   �� *
FontSize+6�   �� 5A      	 �� ContentStringFormat$	 C2��6D   $��1��6]   $��2��6m   $I CTotal profit made in tournaments played (total prizes - total cost)��6�   ��6   �� *TournamentStats.TotalProfit+��6{   �� *
FontSize+6�   �� 5C      $��Hourly: ��6   $��0��6'   $��3��67   $��Right=�6D   $Y SMoney made per hour of tournament session play (total profit / total session hours)��6�   ��6h   �� *
FontSize+6�   �� 5D      $	 C2��6?   $��1��6X   $��3��6h   $Y SMoney made per hour of tournament session play (total profit / total session hours)��6�   ��6   �� *TournamentStats.Hourly+��6v   �� *
FontSize+6�   �� 5F      $��$/Tournament: ��6   $��0��6-   $��4��6=   $��Right=�6J   $J DMoney made per tournament played (total profit / tournaments played)��6�   ��6n   �� *
FontSize+6�   �� 5G      $	 C2��6M   $��1��6f   $��4��6v   $J DMoney made per tournament played (total profit / tournaments played)��6�   ��6   �� *%#TournamentStats.ProfitPerTournament+��6�   �� *
FontSize+6�   �� 5I      $��ROI: ��6   $��0��6%   $��5��65   $��Right=�6B   $6 0Return on investment (total profit / total cost)��6�   ��6f   �� *
FontSize+6�   �� 5J      $	 P2��6=   $��1��6V   $��5��6f   $6 0Return on investment (total profit / total cost)��6�   ��6   �� *TournamentStats.Roi+��6t   �� *
FontSize+6�   �� 5L      $��ITM: ��6   $��0��6$   $��6��64   $��Right=�6A   $5 /Percentage of tournaments finished in the money��6�   ��6e   �� *
FontSize+6�   �� 5M      $	 P2��6<   $��1��6U   $��6��6e   $5 /Percentage of tournaments finished in the money��6�   ��6   �� *TournamentStats.Itm+��6s   �� *
FontSize+6�   �� 5O      $��Average Buy-in: ��6   $��0��60   $��7��6@   $��Right=�6M   $G AAverage buy-in per tournament (total buy-in / tournaments played)��6�   ��6p   �� *
FontSize+6�   �� 5P      $	 C2��6F   $��1��6_   $��7��6o   $G AAverage buy-in per tournament (total buy-in / tournaments played)��6�   ��6   �� *TournamentStats.AverageBuyin+��6}   �� *
FontSize+6�   �� 5R      $��Average Total Cost: ��6   $��0��64   $��8��6D   $��Right=�6Q   $_ YAverage cost per tournament (total cost (buy-in + rebuy(s) + addon) / tournaments played)��6�   ��6u   �� *
FontSize+6�   �� 5S      $	 C2��6E   $��1��6^   $��8��6n   $_ YAverage cost per tournament (total cost (buy-in + rebuy(s) + addon) / tournaments played)��6�   ��6   �� *TournamentStats.AverageCost+��6|   �� *
FontSize+6�   �� 5U      $��Total Cost: ��6   $��0��6,   $��9��6<   $��Right=�6I   $* $Total cost of all tournaments played��6�   ��6m   �� *
FontSize+6�   �� 5V      $	 C2��6C   $��1��6\   $��9��6l   $* $Total cost of all tournaments played��6�   ��6   �� *TournamentStats.TotalCost+��6z   �� *
FontSize+6�   �� 5X      $��Total Prizes Won: ��6   $��0��62   $��10��6B   $��Right=�6P   $, &Total prizes won in tournaments played��6�   ��6t   �� *
FontSize+6�   �� 5Y      $	 C2��6G   $��1��6`   $��10��6p   $, &Total prizes won in tournaments played��6�   ��6   �� *TournamentStats.TotalPrizeWon+��6   �� *
FontSize+6�   �� 5[      $��Hours Played: ��6   $��0��6-   $��11��6=   $��Right=�6K   $ Total session time played��6�   ��6o   �� *
FontSize+6�   �� 5\      $	 P2��6/   $��1��6H   $��11��6X   ��6   �� *Hours+��6g   �� *
FontSize+ 6�   �� *
TimePlayed+5]      5^      