   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=null?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager 3    -Tournament_Life.Views.Results.TournamentsView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
    A
converters$clr-namespace:LiveTracker.Converters   	 
    _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF  -   0  *Syncfusion.Windows.Shared.ChromelessWindow   Name$
  Wind�� � 
ResizeMode$ 	CanResize=�   UseNativeChrome$
 True�� � SizeToContent$ Height=�   ShowMaximizeButton$ False��$
��1000��$	��600��$
��1000��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � Title �� *Title+ � 	Resources /   )LiveTracker.Converters.BoolToYesConverter   BoolToYesConverter5   /LiveTracker.Converters.SngToVisibilityConverter  SngToVisibleConverter0   *LiveTracker.Converters.DateToTimeConverter  DateToTimeConverter0   *LiveTracker.Converters.DateToDateConverter  DateToDateConverter*   $LiveTracker.Converters.LinkConverter  LinkConverter  DataGridContextMenu �� 
TargetType+  %Syncfusion.Windows.Shared.MenuItemAdv	 �� Property
 �� Value  DataGridContextMenu2/	  )Syncfusion.UI.Xaml.Grid.GridRowHeaderCell �� 
TargetType ��Name �� TextAlignment �� RelativeSource  TournamentDesriptionToolTip��%,  &	        &	      &	 
     &	      &	      &	      &	 �     '	  �     &	 �              �� "  .��� 	 � ��
 �� *	FontColor+�� 	 � ��
 �� *
FontSize+�� 	 � ��$
 -50,0,-40,0q��� " m�.��� 	 � ��
 �� *	FontColor+�� 	 � ��
 �� *
FontSize+�� " 	 .��� 	 � ��
 �� " 	 .R��� $ PART_RowHeaderCellBorder��#��z��#��z��._�� .+��� $��Center=�$��Center=�$ Center=����� *
RowIndex+ �� *TemplatedParent+�� .R��� ���� *GraphDescription+*
  $Microsoft.Xaml.Behaviors.Interaction 
  Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ Loaded��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *WindowLoadedCommand+.���  � RowDefinitions �� $
��Auto ��� $��* �.+�'  !Syncfusion.Windows.Shared.MenuAdv     
ExpandMode$ ExpandOnClick=�.@�  $
��File��$��0������ *
FontSize+.@�  $��Export to Excel File�� � ToolTip$6 0Export this windows data to an excel spreadsheet������ *
FontSize+   Command �� *ExcelCommand+  $
��Exit��$ Close this window������ *
FontSize+ �� *ExitCommand+(  "Syncfusion.UI.Xaml.Grid.SfDataGrid  -     Name$ TournamentsDataGrid��$��1��(  "Syncfusion.UI.Xaml.Grid.SfGridBase   AutoGenerateColumns$ False��   AllowResizingColumns$
 True��   ColumnSizer$
 Auto=�$	��5,5q�   EditTrigger$ OnTap=�   SelectionMode$ Single=�$	��Top=�   ShowToolTip$
 True��   ItemsSource �� *Tournaments+ �� UpdateSourceTrigger$ PropertyChanged=�  �� Mode$  TwoWay=�!   SelectedItem! �� *SelectedTournament+"   ShowRowHeader" �� *ShowRowHeader+   $ QueryColumnDragging��.    �� *UpdateColumnOrderCommand+# � ContextMenu# �� $ �� 	StaysOpen$
$ True��% �� IsOpen% �� *IsContextMenuOpen+$  OneWayToSource=�.@�  $��Edit Record...��$! Edit this tournament record��#��[  �� *EditRecordCommand+���� *
FontSize+���� *	FontColor+  $��Delete Record��$5 /Delete this tournament record from the database��#��[  �� *DeleteRecordCommand+���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�  $��View Screenshot��$1 +View any screenshot attached to this record��#��[  �� *ViewScreenshotCommand+���� *
FontSize+���� *	FontColor+  $��Save Screenshot...��$&  Save a screenshot to this record��#��[  �� *AddScreenshotCommand+���� *
FontSize+���� *	FontColor+  $��Delete Screenshot��$0 *Delete any screenshot saved to this record��#��[  �� *RemoveScreenshotCommand+���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�  $��View/Edit Notes...��$1 +Show/Edit any notes saved to the tournament��#��[  �� *ViewNotesCommand+���� *
FontSize+���� *	FontColor+&   Columns& ,  &Syncfusion.UI.Xaml.Grid.GridTextColumn  ,  &Syncfusion.UI.Xaml.Grid.GridColumnBase'   MappingName$' TournamentName��(   
HeaderText$( Tournament Name��)   ToolTipTemplate#) [ (  "Syncfusion.UI.Xaml.Grid.GridColumn*   AllowDragging$
* True��+   TextAlignment$+ Center=�  $' Venue��$( Venue��#) [ $
* True��$+ Center=�  $' GameType��$( 	Game Type��#) [ $
* True��$+ Center=�  $' FormatString��$( 	Format(s)��#) [ $
* True��$+ Center=�0  *Syncfusion.UI.Xaml.Grid.GridDateTimeColumn  $' 	StartDate��$( 
Start Date��#) [ $
* True��$+ Center=�,   DisplayBinding, �� *	StartTime+- �� 	Converter#- [   $' 	StartTime��$( 
Start Time��#) [ $
* True��$+ Center=�, �� *	StartTime+#- [   $' EndDate��$( End Date��#) [ $
* True��$+ Center=�, �� *	EndTime+#- [   $' EndTime��$( End Time��#) [ $
* True��$+ Center=�, �� *	EndTime+#- [ 0  *Syncfusion.UI.Xaml.Grid.GridTimeSpanColumn  $' Length��$( Length��.   Format$. hh:mm��#) [ $
* True��$+ Center=�0  *Syncfusion.UI.Xaml.Grid.GridCurrencyColumn  $' 	Guarantee��$( 	Guarantee��#) [ $+ Center=�$
* True��  $' 	TotalCost��$( 
Total Cost��#) [ $+ Center=�$
* True��  $' BuyinTotalCost��$( Buyin Total��#) [ $+ Center=�$
* True��  $' BuyinBaseCost��$( 
Buyin Base��#) [ $+ Center=�$
* True��  $' BuyinRakeCost��$( 
Buyin Rake��#) [ $+ Center=�$
* True��  $' RebuyTotalCost��$( Rebuy Total��#) [ $+ Center=�$
* True��  $' RebuyBaseCost��$( 
Rebuy Base��#) [ $+ Center=�$
* True��  $' RebuyRakeCost��$( 
Rebuy Rake��#) [ $+ Center=�$
* True��  $' AddonTotalCost��$( Addon Total��#) [ $+ Center=�$
* True��  $' AddonBaseCost��$( 
Addon Base��#) [ $+ Center=�$
* True��  $' AddonRakeCost��$( 
Addon Rake��#) [ $+ Center=�$
* True��  $' BountyCount��$( Bounty Count��#) [ $+ Center=�$
* True��  $' Bounty��$( Bovada Bounty��#) [ $+ Center=�$
* True��  $' 
AddonCount��$( Addon Count��#) [ $+ Center=�$
* True��  $' PrizeWon��$( 	Prize Won��#) [ $+ Center=�$
* True��  $' Profit��$( Profit��#) [ $+ Center=�$
* True��  $' FinishPosition��$( Finish��#) [ $+ Center=�$
* True��  $' JackpotSpinMultiplier��$( Jackpot Multiplier��#) [ $+ Center=�$
* True��  $' 
RebuyCount��$( Rebuy Count��#) [ $+ Center=�$
* True��  $' StackSizeStarting��$( Starting Stack��#) [ $
* True��$+ Center=�  $' StackSizeRebuy��$( Rebuy Stack��#) [ $
* True��$+ Center=�  $' StackSizeAddon��$( Addon Stack��#) [ $
* True��$+ Center=�  $' 	TableSize��$( 
Table Size��#) [ $
* True��$+ Center=�  $' BlindLevels��$( Blind Levels��#) [ $
* True��$+ Center=�  $' LateReg��$( Late Reg��#) [ $
* True��$+ Center=�  $' Entrants��$( Entrants��#) [ $
* True��$+ Center=�  $' EntrantsPaid��$( Entrants Paid��#) [ $
* True��$+ Center=�  $
' Note��$( Note?��#) [ $
* True��$+ Center=�, �� *Note+#- [   $' ScreenshotFilename��$( Screenshot?��#) [ $
* True��$+ Center=�, �� *ScreenshotFilename+#- [   $' IsSng��$
( Sng?��$
* True��$+ Center=�, �� *IsSng+#- [    $' IsBovadaBounty��$( Fixed Bounty?��#) [ $
* True��$+ Center=�, �� *IsBovadaBounty+#- [  