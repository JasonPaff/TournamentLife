   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.0.1.2, Culture=neutral, PublicKeyToken=null?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager 2    ,Tournament_Life.Views.Results.EditRecordView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a	 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf    	 
   A
converters$clr-namespace:LiveTracker.Converters    	 
   msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    	 
     � Title$  Edit Record�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�$	��450��$	��800��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � 	Resources 5   /LiveTracker.Converters.SngToVisibilityConverter   SngToVisibleConverter��%   &	          .���  � ColumnDefinitions �� $
��Auto ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0�� �� Orientation$ Vertical=�.+��� $
��Name��$��Right=����� *
FontSize+�� $��Venue��$��Right=����� *
FontSize+�� $��	Game Type��$��Right=����� *
FontSize+�� $��Formats��$��Right=����� *
FontSize+�� $��
Start Date��$��Right=����� *
FontSize+�� $��
Start Time��$��Right=����� *
FontSize+�� $��End Date��$��Right=����� *
FontSize+�� $��End Time��$��Right=����� *
FontSize+�� $��Buyin��$��Right=����� *
FontSize+�� $��
Buyin Rake��$��Right=����� *
FontSize+�� $��Rebuy��$��Right=����� *
FontSize+�� $��
Rebuy Rake��$��Right=����� *
FontSize+�� $��Addon��$��Right=����� *
FontSize+�� $��
Addon Rake��$��Right=����� *
FontSize+�� $��Bovada Bounty��$��Right=����� *
FontSize+�� $��	Guarantee��$��Right=����� *
FontSize+�� $��Late Reg��$��Right=����� *
FontSize+�� $��Blind Levels��$��Right=����� *
FontSize+�� $��
Table Size��$��Right=����� *
FontSize+�� $��Starting Stack��$��Right=����� *
FontSize+�� $��Rebuy Stack��$��Right=����� *
FontSize+�� $��Addon Stack��$��Right=����� *
FontSize+�� $��Entrants��$��Right=����� *
FontSize+�� $��Entrants Paid��$��Right=����� *
FontSize+�� $��Finish Position��$��Right=����� *
FontSize+�� $��	Prize Won��$��Right=����� *
FontSize+�� $��Addon Count��$��Right=����� *
FontSize+�� $��Rebuy Count��$��Right=����� *
FontSize+�� $��Bounty Count��$��Right=����� *
FontSize+�� $��Jackpot Spin��$��Right=����� *
FontSize+�� $��0��$��1��$ Vertical=�.+���  �� TextAlignment$
 Left=�$��0,5,0,0q�$��Center=�	 � ToolTip$	 Tournament Name��$	��100������ *TournamentData.TournamentName+���� *
FontSize+�� $
 Left=�$��0,6,0,0q�$��Center=�$	 Poker Site/Venue Name������ *TournamentData.Venue+���� *
FontSize+�� $
 Left=�$��0,10,0,0q�$��Center=�$	 	Game Type������ *TournamentData.GameType+���� *
FontSize+�� $
 Left=�$��0,8,0,0q�$��Center=�$8	 2Tournament Formats (separate formats with a comma)������ *Format+���� *
FontSize+4  .Syncfusion.Windows.Controls.Input.SfDatePicker  $��0,5,0,0q�$	 Starting Date��
   Value
 �� *	StartDate+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=����� *
FontSize+4  .Syncfusion.Windows.Controls.Input.SfTimePicker  $��0,0,0,0q�$	 Starting Time��   Value �� *	StartTime+$ PropertyChanged=�$ TwoWay=����� *
FontSize+  $	 Ending Date��
 �� *	EndDate+$ PropertyChanged=�$ TwoWay=����� *
FontSize+  $	 Ending Time�� �� *	EndTime+$ PropertyChanged=�$ TwoWay=����� *
FontSize+/ 
 )Syncfusion.Windows.Shared.CurrencyTextBox  $��0,5,0,0q�   ScrollInterval$	 1.0Q�   MinValue$ 0~�* 
 $Syncfusion.Windows.Shared.EditorBase   Culture$ en-US��$ Right=�$0	 *Buyin base cost (buy-in cost without rake)��   Value �� *TournamentData.BuyinBaseCost+���� *
FontSize+  $��0,5,0,0q�$	 1.0Q�$ 0~�$ en-US��$ Right=�$	 Buyin Rake cost�� �� *TournamentData.BuyinRakeCost+���� *
FontSize+  $	 1.0Q�$��0,7,0,0q�$ 0~�$ en-US��$ Right=�$0	 *Rebuy base cost (re-buy cost without rake)�� �� *TournamentData.RebuyBaseCost+���� *
FontSize+  $��0,7,0,0q�$	 1.0Q�$ 0~�$ en-US��$ Right=�$	 Rebuy Rake cost�� �� *TournamentData.RebuyRakeCost+���� *
FontSize+  $��0,7,0,0q�$	 1.0Q�$ 0~�$ en-US��$ Right=�$.	 (Addon base cost (addon cost without rake�� �� *TournamentData.AddonBaseCost+���� *
FontSize+  $��0,8,0,0q�$	 1.0Q�$ 0~�$ en-US��$ Right=�$	 Addon Rake cost�� �� *TournamentData.AddonRakeCost+���� *
FontSize+  -     Name$ BovadaBounty��$ 1Q�$ 0~�$��26��$ Right=�$1	 +Bounty amount for Bovada bounty tournaments�� �� *TournamentData.Bounty+���� *
FontSize+  $ 500.0Q�$��0,7,0,0q�$ 0~�$ en-US��$ Right=�$	 	Guarantee�� �� *TournamentData.Guarantee+���� *
FontSize+. 
 (Syncfusion.Windows.Shared.IntegerTextBox  $��0,6,0,0q�   ScrollInterval$ 5��   MinValue$ 0��$ Right=�$"	 Late Reg length (in minutes)��   Value �� *TournamentData.LateReg+���� *
FontSize+  $��0,8,0,0q�$ 5��$ 0��$ Right=�$&	  Blind Levels length (in minutes)�� �� *TournamentData.BlindLevels+���� *
FontSize+  $��0,5,0,0q�$ 1��$ 2��   MaxValue$ 10��$ Right=�$	 Players per table�� �� *TournamentData.TableSize+���� *
FontSize+  $��0,7,0,0q�$	 500��$ 0��$ Right=�$	 Starting Stack Size�� �� *" TournamentData.StackSizeStarting+���� *
FontSize+  $��0,7,0,0q�$	 500��$ 0��$ Right=�$3	 -Chip stack you get for a rebuy (if available)�� �� *TournamentData.StackSizeRebuy+���� *
FontSize+  $��0,7,0,0q�$	 500��$ 0��$ Right=�$	 Addon Stack Size�� �� *TournamentData.StackSizeAddon+���� *
FontSize+  $��0,8,0,0q�$ 1��$ 0��$ Right=�$2	 ,Number of players who entered the tournament�� �� *TournamentData.Entrants+���� *
FontSize+  $��0,5,0,0q�$ 1��$ 0��$ Right=�$*	 $Players who cashed in the tournament�� �� *TournamentData.EntrantsPaid+���� *
FontSize+  $��0,8,0,0q�$ 1��$ 0��$ Right=�$)	 #Position finished in the tournament�� �� *TournamentData.FinishPosition+���� *
FontSize+  $��0,9,0,0q�$ 1Q�$ 0~�$ en-US��$ Right=�$!	 Prize won in the tournament�� �� *TournamentData.PrizeWon+���� *
FontSize+  $��0,7,0,0q�$ 1��$ 0��$ Right=�$)	 #Position finished in the tournament�� �� *TournamentData.AddonCount+���� *
FontSize+  $��0,5,0,0q�$ 1��$ 0��$ Right=�$)	 #Position finished in the tournament�� �� *TournamentData.RebuyCount+���� *
FontSize+  $��0,8,0,0q�$ 1��$ 0��$ Right=�$/	 )Bounties won in Bovada bounty tournaments�� �� *TournamentData.BountyCount+���� *
FontSize+  $��0,8,0,0q�$ 1��$ 0��$ Right=�$&	  Spin multiplier for jackpot sngs�� �� *&$TournamentData.JackpotSpinMultiplier+���� *
FontSize+�� $��0��$��2��$��2��$	��5,5q�$
��Left=�$ Vertical=�.+��� $��Center=�$��Template is a Sit and Go��$-	 'Tag this tournament as a sng tournament��$��0,0,0,5q� d� 	IsChecked �� *SngCheckBox+���� *
FontSize+�� $��Center=�$!��Template is a Bovada Bounty��$��
11,0,-13,5q�$}	 wThe bounty amount for Bovada/BetOnline tournaments where the bounty isn't counted in the final prize total in the lobby�� �� *BovadaCheckBox+���� *
FontSize+�� $ 
Horizontal=�$��3��$��0��$��2��$��Center=�$��0,0,0,0q�.+��� $��25��$��90��$	��5,5q�$	 Save these changes��$
��Save������ *
FontSize+���� *SaveCommand+�� $��25��$��90��$	��5,5q� �� IsCancel$
 True��$	 Cancel these changes��$��Cancel������ *
FontSize+���� *CancelCommand+