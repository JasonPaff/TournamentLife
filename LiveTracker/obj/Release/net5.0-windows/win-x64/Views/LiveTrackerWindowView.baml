   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.0.1.2, Culture=neutral, PublicKeyToken=null?$clr-namespace:LiveTracker.ViewModelsLiveTracker.ViewModels  _4clr-namespace:LiveTracker.ViewModels.Menu_ViewModels&LiveTracker.ViewModels.Menu_ViewModels  g8clr-namespace:LiveTracker.ViewModels.Datagrid_ViewModels*LiveTracker.ViewModels.Datagrid_ViewModels  K*clr-namespace:LiveTracker.Views.Menu_ViewsLiveTracker.Views.Menu_Views  ;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem ?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters  U QSystem.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aO8clr-namespace:System.Collections;assembly=System.RuntimeSystem.Collections h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager -    'LiveTracker.Views.LiveTrackerWindowView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     :	viewmodel$clr-namespace:LiveTracker.ViewModels     Omenuviewmodels4clr-namespace:LiveTracker.ViewModels.Menu_ViewModels     WdatagridViewModels8clr-namespace:LiveTracker.ViewModels.Datagrid_ViewModels     @	menuviews*clr-namespace:LiveTracker.Views.Menu_Views     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     `	 \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b
 ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf  	 
     =commands"clr-namespace:LiveTracker.Commands  	 
     ?System&clr-namespace:System;assembly=mscorlib  	 
     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5
converters$clr-namespace:LiveTracker.Converters  Jcollections8clr-namespace:System.Collections;assembly=System.Runtime  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    � WindowStartupLocation$  CenterScreen=�$	��450��$	��800�� � SizeToContent$ WidthAndHeight=�0  *Syncfusion.Windows.Shared.ChromelessWindow   TitleTextAlignment$
 Left=�   ShowMinimizeButton$
 True�� � 
ResizeMode$ NoResize=�,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � Title �� *Title+ � Topmost �� *KeepWindowOnTop+ � DataContext 1   +LiveTracker.ViewModels.LiveTrackerViewModel  	 � 	Resources	 +   %LiveTracker.Converters.ThemeConverter   ThemeConverterB   <LiveTracker.ViewModels.Datagrid_ViewModels.DataGridViewModel  DataGridViewModel:   4LiveTracker.ViewModels.Menu_ViewModels.MenuViewModel  MenuViewModel7   1LiveTracker.Converters.ColumnVisibleMenuConverter  ColumnVisibleMenuConverter0   *LiveTracker.Converters.DateToTimeConverter  DateToTimeConverter/	  )Syncfusion.UI.Xaml.Grid.GridRowHeaderCell
 �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� TextAlignment �� RelativeSource  TemplateDesriptionToolTip  TotalCostDescriptionToolTip  EntrantsDescriptionToolTip " EntrantsPaidDescriptionToolTip $	  FinishPositionDescriptionToolTip 
 PrizeWonDescriptionToolTip  TimeDescriptionToolTip��%�  &	        &	      &	 
     &	      &	      '	       &	 �     &	     &	 5    &	 o    &		 �    &	
 �    &	 G              �� "
 	 .���  � �� �� " 	 .R��� $ PART_RowHeaderCellBorder��#��z��#��z��._�� .+��� $��Center=�$��Center=�$ Center=����� *
RowIndex+ �� *TemplatedParent+�� .R��� ���� *Description+�� .R��� ���� *TotalCostDescription+�� .R��� $,��& Number of entrants in the tournament ���� .R��� $;��5 Number of entrants that will cash in the tournament ���� .R��� $4��. Position that you finished in the tournament ���� .R��� $?��9 Prize amount that you won in the tournament (if any :P) ���� .R��� ���� *TimeDescription+   RightHeaderItemsSource "
  System.Collections.ArrayList
  3  -Syncfusion.Windows.Tools.Controls.ComboBoxAdv  $��0,0,25,0q� �� SelectedItem �� *SelectedVisibility+	  �� BasedOn��%   '��       �� "
 �� Z� ���� .R��� $��20�� �� Padding$ 4q����� ��
  ��Show All Tournaments����Show Started Tournaments����Show Starting in 5 Minutes����Show Starting in 15 Minutes����Show Starting in 30 Minutes����Show Starting in 60 Minutes��*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ SelectionChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� 2   ,LiveTracker.Commands.LiveTrackerMenuCommands   VisibilityChangedCommand �� Source# Z    CommandParameter �� *SelectedVisibility+�� $��0,0,10,0q�$��	Dark Mode��$��
IsDarkMode�� d� 	IsChecked �� *
IsDarkMode+���� 4   .LiveTracker.Commands.LiveTrackerWindowCommands   MenuItem# Z    $ Loaded��.   $ WindowLoaded�� �� +   %LiveTracker.Commands.DataGridCommands   ContextMenuItem# Z .���  � RowDefinitions �� $
��Auto ��� $
��Auto �.+�� -     �Name$  LiveTrackerMenu��$��0��# [ .+�6   0LiveTracker.Views.Menu_Views.LiveTrackerMenuView  � # [ $��1��.+�(  "Syncfusion.UI.Xaml.Grid.SfDataGrid  -   !  Name$! TournamentsDataGrid��(  "Syncfusion.UI.Xaml.Grid.SfGridBase"   AutoGenerateColumns$" False��#   AllowResizingColumns$
# True��$   ColumnSizer$
$ None=�$��0��$��0��%   SelectionMode$% Single=�$��Hidden=�&   ShowToolTip$
& True��'   ItemsSource' �� *VisibleTournaments+(   SelectedItem( �� *SelectedTournament+)   ShowRowHeader) �� *ShowRowHeader+   $ SizeChanged��.   $ UpdateColumnWidth�� �� # Z   $ QueryColumnDragging��.   $ UpdateColumnOrder�� �� # Z *   HeaderContextMenu* �� .@�m� $��Show/Hide Columns��.@�m� $��Addon Count��+ � ToolTip$7+ 1Track if you take any add-ons in your tournaments��, m� StaysOpenOnClick$
, True��- m� CommandParameter$- 
AddonCount��. m� Command. �� /   ChangeColumnVisibility# Z/ m� $��Addon Total Cost��$Z+ Tshows the cost of the addon in the tournament (base cost + rake) in your tournaments��$
, True��$- Addon��. �� # Z/ m� $��Bounty Count��$�+ �Used to track the number of bounties received for Bovada Bounty tournaments that don't put bounty info in the lobby prize for you (this only gets pulled into the tournament data if Bovada Bounty is checked for the tournament��$
, True��$- Bounty��. �� # Z/ m� $��Buyin Total Cost��$I+ CShows the cost of the buy-in (base cost + rake) in your tournaments��$
, True��$- Buyin��. �� # Z/ m� $��Entrants��$7+ 1Tracks the number of entrants in your tournaments��$
, True��$- Entrants��. �� # Z/ m� $��Entrants Paid��$D+ >Tracks the number of players who will cash in your tournaments��$
, True��$- EntrantsPaid��. �� # Z/ m� $��Finish Position��$?+ 9Tracks the position that you finished in your tournaments��$
, True��$- FinishPosition��. �� # Z/ m� $��Jackpot Spin Multiplier��$++ %Track the multiplier in a jackpot sng��$
, True��$- JackpotSpinMultiplier��. �� # Z/ m� $��	Prize Won��$3+ -Tracks the prizes you win in your tournaments��$
, True��$- PrizeWon��. �� # Z/ m� $��Rebuy Count��$C+ =Tracks the number of times your re-bought in your tournaments��$
, True��$- 
RebuyCount��. �� # Z/ m� $��Rebuy Total Cost��$O+ IShows the total cost of the rebuys (base cost + rake) in your tournaments��$
, True��$- Rebuy��. �� # Z/ m� $��
Start Time��$1+ +Shows the starting time of your tournaments��$
, True��$- 	StartTime��. �� # Z/ m� $��
Total Cost��$p+ jShows the total cost of your tournaments (total buy-in + (total add-on * addons) + (total rebuy + addons))��$
, True��$- 	TotalCost��. �� # Z/ m� $++ %Shows the venues for your tournaments��$��Venue��$
, True��$- Venue��. �� # Z/ 0 � ContextMenu0 �� 1 �� 	StaysOpen$
1 True��2 �� IsOpen2 �� *IsContextMenuOpen+3 �� Mode$3 OneWayToSource=�   $ GotMouseCapture��.   $ UpdateTimeStrings�� �� # Z .@�m� $��DemiBold"�$
, True������ *#!SelectedTournament.TournamentName+���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�m� $��Update Starting Time...��$- UpdateStartTimeSelected��$@+ :Update this tournaments starting time to a time you select��$, False��. �� # Z ���� *
FontSize+���� *	FontColor+m� $- UpdateStartTime��$8+ 2Update this tournaments starting time to this time��$, False������ *UpdateStartTimeNow+. �� # Z ���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�m� $��Update Ending Time...��$- UpdateEndTimeSelected��$>+ 8Update this tournaments ending time to a time you select��$, False��. �� # Z ���� *
FontSize+���� *	FontColor+m� $- UpdateEndTime��$6+ 0Update this tournaments ending time to this time��$, False������ *UpdateEndTimeNow+. �� # Z ���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�m� $��Finish Tournament��$m+ gFinishes this tournament by removing it from the running tournaments view and adding it to the database��$, False��. �� 4   FinishTournamentMenuItem# Z4 - �� *SelectedTournament+���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�m� $��Change Template Data��$v+ pChange the template data used to create this tournament (can update the template file or save as a new template)��$, False��. �� !5   ChangeTemplateDataMenuItem# Z5 - �� *SelectedTournament+���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�m� $��Cancel Tournament��$- CancelTournament��$+ Cancel this tournament��$, False��. �� # Z ���� *
FontSize+���� *	FontColor+m� $��Cancel Select Tournaments��$- CancelSelectTournaments��$,+ &Cancel running tournaments from a list��$, False��. �� # Z ���� *
FontSize+���� *	FontColor+m� $��Cancel All Tournaments��$- CancelAllTournaments��$$+ Cancel all running tournaments��$, False��. �� # Z ���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�m� $- PinToFavorites��$\+ VAdd/Remove this tournaments template in the Favorites sub-menu of the Tournaments menu��$, False������ *'%SelectedTournament.FavoriteMenuString+. �� # Z ���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�m� $��Add to Session(s)��$- AddToSession��$,+ &Add this tournament to select sessions��$, False��. �� # Z ���� *
FontSize+���� *	FontColor+m� $��Remove from Session(s)��$- RemoveFromSession��$/+ )Remove this tournament to select sessions��$, False��. �� # Z ���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�m� $��Attach Screenshot��$K+ EAttach an image file to this tournament that is saved in the database��$- AttachScreenshot��. �� # Z ���� *
FontSize+���� *	FontColor+m� $��Remove Screenshot��$J+ DRemove an image file that was previously attached to this tournament��$- RemoveScreenshot��. �� # Z ���� *
FontSize+���� *	FontColor+m� $��Attach Note��$I+ CAttach a text note to this tournament that is saved in the database��$- 
AttachNote��. �� # Z ���� *
FontSize+���� *	FontColor+6   Columns6 ,  &Syncfusion.UI.Xaml.Grid.GridTextColumn  ,  &Syncfusion.UI.Xaml.Grid.GridColumnBase7   MappingName$7 Venue��8   ToolTipTemplate#8 [ (  "Syncfusion.UI.Xaml.Grid.GridColumn9   AllowDragging$
9 True��:   TextAlignment$: Center=�;   
HeaderText; �� *VenueHeader+  $7 TournamentName��#8 [ $
9 True��$: Center=�; �� *TournamentNameHeader+0  *Syncfusion.UI.Xaml.Grid.GridDateTimeColumn  $7 	StartTime��#8 [ $
9 True��$: Center=�<   DisplayBinding< �� *	StartTime+= �� 	Converter#= [ ; �� *StartTimeHeader+0  *Syncfusion.UI.Xaml.Grid.GridCurrencyColumn  $7 BuyinTotalCost��#8 [ $: Center=�$
9 True��>   AllowEditing$> False��; �� *BuyinHeader+  $7 RebuyTotalCost��#8 [ $: Center=�$
9 True��$> False��; �� *RebuyHeader+  $7 AddonTotalCost��#8 [ $: Center=�$
9 True��$> False��; �� *AddonHeader+0  *Syncfusion.UI.Xaml.Grid.GridTemplateColumn  #8 [ $7 BountyCount��0  *Syncfusion.UI.Xaml.Grid.FocusManagerHelper?   WantsKeyInput$
? True��$
9 True��; �� *BountyHeader+@   CellTemplate@ �� .R�.  (Syncfusion.Windows.Shared.IntegerTextBox  A   MinValue$A 0��B �� TextAlignment$B Right=�$��25��$��-1,-8,-1,-8q�*  $Syncfusion.Windows.Shared.EditorBaseC   ShowSpinButton$
C True��D   ValueD �� E �� Path$E BountyCount�F �� UpdateSourceTrigger$F PropertyChanged=�  #8 [ $7 JackpotSpinMultiplier��$
? True��$
9 True��; �� *JackpotSpinMultiplierHeader+@ �� .R�  $A 0��$B Right=�$��0q�$��25��$��-1,-8,-1,-8q�$
C True��D �� $E JackpotSpinMultiplier�$F PropertyChanged=�  #8 [ $7 
RebuyCount��$
? True��$
9 True��; �� *RebuyCountHeader+@ �� .R�  $A 0��$B Right=�$��0q�$��25��$��-1,-8,-1,-8q�$
C True��D �� $E 
RebuyCount�$F PropertyChanged=�  #8 [ $7 
AddonCount��$
? True��$
9 True��; �� *AddonCountHeader+@ �� .R�  $A 0��$B Right=�$��0q�$��25��$��-1,-8,-1,-8q�$
C True��D �� $E 
AddonCount�$F PropertyChanged=�  $7 	TotalCost��#8 [ $
9 True��$: Center=�$> False��; �� *TotalCostHeader+$F PropertyChanged=�  #8 [ $7 Entrants��$
? True��$
9 True��; �� *EntrantsHeader+@ �� .R�  $A 0��$��25��$��-1,-8,-1,-8q�$��0q�$B Right=�D �� $E Entrants�$F PropertyChanged=�  #8 [ $7 EntrantsPaid��$
? True��$
9 True��; �� *EntrantsPaidHeader+@ �� .R�  $A 0��$��25��$��-1,-8,-1,-8q�$��0q�$B Right=�D �� $E EntrantsPaid�$F PropertyChanged=�  #8 [	 $7 FinishPosition��$
? True��$
9 True��; �� *FinishPositionHeader+@ �� .R�  $A 0��$��25��$��-1,-8,-1,-8q�$��0q�$B Right=�D �� $E FinishPosition�$F PropertyChanged=�  #8 [
 $7 PrizeWon��$
? True��$
9 True��; �� *PrizeWonHeader+@ �� .R�/  )Syncfusion.Windows.Shared.CurrencyTextBox  G   MinValue$G 0~�$��25��$��-1,-8,-1,-8q�$��0q�$B Right=�H   ScrollInterval$	H 1.0Q�I   Culture$I en-US��J   ValueJ �� $E PrizeWon�$F PropertyChanged=�