   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=null?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager ?    9Tournament_Life.Views.Datagrid_Views.FinishTournamentView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ;
converters$clr-namespace:LiveTracker.Converters     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
    msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
      � Title$  Finish Tournament�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � 	Resources 3   -LiveTracker.Converters.BoolToVisibleConverter   WindowVisibleConverter   	TrueValue   
FalseValue��%-   &	          $ Visible=�$ 	Collapsed=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��5 ��� $
��Auto ��� $
��Auto ��� $��5 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��5 ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��1��$��0��$��2��$��0,5,0,0q�$��Center=�
����d�	 � ToolTip	 �� *TournamentToolTip+���� *
FontSize+���� *Name+�� $��3��$��0��$��Right=�$,	 &The date and time the tournament began��$��Start Time:������ *
FontSize+�� $��3��$��1��$,	 &The date and time the tournament began��$��0,0,5,0q����� *
FontSize+���� *	StartTime+�� $��4��$��0��$��Right=�$,	 &The date and time the tournament ended��$��	End Time:������ *
FontSize+�� $��4��$��1��$,	 &The date and time the tournament ended������ *
FontSize+���� *	EndTime+�� $��6��$��0��$��Right=�$3	 -The total number of entries in the tournament��$��	Entrants:������ *
FontSize+�� $��6��$��1��$
��Left=�$3	 -The total number of entries in the tournament������ *EntrantsColor+���� *
FontSize+���� *
Entrants+�� $��7��$��0��$��Right=�$=	 7The total number of entries that cash in the tournament��$��Entrants Paid:������ *
FontSize+�� $��7��$��1��$
��Left=�$=	 7The total number of entries that cash in the tournament������ *EntrantsPaidColor+���� *
FontSize+���� *EntrantsPaid+�� $��8��$��0��$��Right=�$/	 )Your finishing position in the tournament��$��Finish Position:������ *
FontSize+�� $��8��$��1��$
��Left=�$/	 )Your finishing position in the tournament������ *FinishPositionColor+���� *
FontSize+���� *FinishPosition+�� $��10��$��0��$��Right=�$+	 %The total prize won in the tournament��$��
Prize Won:������ *
FontSize+�� $��10��$��1��$
��Left=�$+	 %The total prize won in the tournament��
 �� ContentStringFormat$
 C2������ *PrizeWonColor+���� *
FontSize+���� *
PrizeWon+�� $��12��$��0��$��Right=�$?	 9The total number of times you re-bought in the tournament��$��	Rebuy(s):������ *
FontSize+y��� *	ShowRebuy+ �� 	Converter# [  �� $��12��$��1��$
��Left=�$?	 9The total number of times you re-bought in the tournament������ *RebuyCountColor+y��� *	ShowRebuy+# [  ���� *
FontSize+���� *Rebuy+�� $��13��$��0��$��Right=�$>	 8The total number of times you added-on in the tournament��$��	Addon(s):������ *
FontSize+y��� *	ShowAddon+# [  �� $��13��$��1��$
��Left=�$>	 8The total number of times you added-on in the tournament��y��� *	ShowAddon+# [  ���� *AddonCountColor+���� *
FontSize+���� *Addon+�� $��14��$��0��$��Right=�$<	 6The total number of bounties you won in the tournament��$��	Bounties:������ *
FontSize+y��� *
ShowBounty+# [  �� $��14��$��1��$
��Left=�$<	 6The total number of bounties you won in the tournament��y��� *
ShowBounty+# [  ���� *
FontSize+���� *BountyWonColor+���� *BountyWonCount+�� $��14��$��0��$��Right=�$(	 "The multiplier for the jackpot sng��$��Multiplier:������ *
FontSize+y��� *ShowJackpot+# [  �� $��14��$��1��$
��Left=�$(	 "The multiplier for the jackpot sng��y��� *ShowJackpot+# [  ���� *
FontSize+���� *JackpotColor+���� *JackpotMultiplier+�� $��16��$��0��$��2��$��Center=� �� Orientation$ 
Horizontal=�.+��� $��25��$��80��$;	 5Finalizes the tournament and saves it to the database��$��5,5,5,10q� �� 	IsDefault$
 True��$	��Yes������ *
FontSize+���� *SaveCommand+�� $��25��$��80��$G	 ACancels the finalization of the tournament and closes this window��$��5,5,5,10q� �� IsCancel$
 True��$��No������ *
FontSize+���� *CancelCommand+