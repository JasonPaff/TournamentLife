   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=null?$clr-namespace:LiveTracker.ViewModelsLiveTracker.ViewModels  _4clr-namespace:LiveTracker.ViewModels.Menu_ViewModels&LiveTracker.ViewModels.Menu_ViewModels  g8clr-namespace:LiveTracker.ViewModels.Datagrid_ViewModels*LiveTracker.ViewModels.Datagrid_ViewModels  K*clr-namespace:LiveTracker.Views.Menu_ViewsLiveTracker.Views.Menu_Views  ;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem ?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters  U QSystem.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aO8clr-namespace:System.Collections;assembly=System.RuntimeSystem.Collections h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager -    'LiveTracker.Views.LiveTrackerWindowView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     :	viewmodel$clr-namespace:LiveTracker.ViewModels     Omenuviewmodels4clr-namespace:LiveTracker.ViewModels.Menu_ViewModels     WdatagridViewModels8clr-namespace:LiveTracker.ViewModels.Datagrid_ViewModels     @	menuviews*clr-namespace:LiveTracker.Views.Menu_Views     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b	 ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf 	 
      =commands"clr-namespace:LiveTracker.Commands 	 
      ?System&clr-namespace:System;assembly=mscorlib 	 
      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5
converters$clr-namespace:LiveTracker.Converters  Jcollections8clr-namespace:System.Collections;assembly=System.Runtime  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF  -   0  *Syncfusion.Windows.Shared.ChromelessWindow   Name$  window��   UseNativeChrome$
 True�� � WindowStartupLocation$ CenterScreen=� � SizeToContent$ WidthAndHeight=�   TitleTextAlignment$
 Left=�   ShowMinimizeButton$
 True��$	��100��$	��300��   ShowMaximizeButton$ False�� � 
ResizeMode$ CanMinimize=�,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+	 � Title	 �� *Title+
 � Topmost
 �� *KeepWindowOnTop+ � DataContext 1   +LiveTracker.ViewModels.LiveTrackerViewModel   � 	Resources +   %LiveTracker.Converters.ThemeConverter   ThemeConverterB   <LiveTracker.ViewModels.Datagrid_ViewModels.DataGridViewModel  DataGridViewModel:   4LiveTracker.ViewModels.Menu_ViewModels.MenuViewModel  MenuViewModel7   1LiveTracker.Converters.ColumnVisibleMenuConverter  ColumnVisibleMenuConverter3   -LiveTracker.Converters.BoolToVisibleConverter  WindowVisibleConverter   	TrueValue   
FalseValue0	   *LiveTracker.Converters.DateToTimeConverter  DateToTimeConverter/
 
 )Syncfusion.UI.Xaml.Grid.GridRowHeaderCell �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� TextAlignment �� RelativeSource  TemplateDesriptionToolTip  TotalCostDescriptionToolTip �� UpdateSourceTrigger   RebuyCountDescriptionToolTip  	 AddonCountDescriptionToolTip 
 EntrantsDescriptionToolTip " EntrantsPaidDescriptionToolTip $  FinishPositionDescriptionToolTip  PrizeWonDescriptionToolTip  ColumnHeaderTemplate  TimeDescriptionToolTip��%A  &	        &	      &	 
     &	      &	      &	 7     '
  <     &	     &	 )    &	 n    &		 �    &	
 �    &	 4    &	 }    &	 �    &	     &	 m              $ Visible=�$ 	Collapsed=�	  �� " 
 .���  � �� �� " 
 .R��� $ PART_RowHeaderCellBorder��#��z��#��z��._�� .+��� $��Center=�$��Center=�$ Center=����� *
RowIndex+ �� *TemplatedParent+�� .R��� ���� *Description+�� .R��� ���� *TotalCostDescription+$ PropertyChanged=��� .R��� ���� *RebuyCountDescription+$ PropertyChanged=��� .R��� ���� *AddonCountDescription+$ PropertyChanged=��� .R��� $,��& Number of entrants in the tournament ���� .R��� $;��5 Number of entrants that will cash in the tournament ���� .R��� $4��. Position that you finished in the tournament ���� .R��� $?��9 Prize amount that you won in the tournament (if any :P) ���� .R��� $&�� Right click to show/hide columns������ *
FontSize+���� *	FontColor+�� .R��� ���� *TimeDescription+   RightHeaderItemsSource "  System.Collections.ArrayList  3  -Syncfusion.Windows.Tools.Controls.ComboBoxAdv  $��0,0,25,0q� � ToolTip$@ :Adjust how tournaments are displayed in the tracker window�� �� SelectedItem �� *SelectedVisibility+  �� BasedOn��%   '��       �� " �� Z� ���� .R��� $��20�� �� Padding$ 4q����� ��  ��Show All Tournaments����Show Started Tournaments����*&Show Started and Starting in 5 Minutes����+'Show Started and Starting in 15 Minutes����+'Show Started and Starting in 30 Minutes����+'Show Started and Starting in 60 Minutes��*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ SelectionChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� '   !LiveTracker.Commands.MenuCommands    VisibilityChangedCommand! �� Source#! Z  "   CommandParameter" �� *SelectedVisibility+   $ Loaded��.   $" WindowLoaded�� �� +   %LiveTracker.Commands.DataGridCommands#   ContextMenuItem#! Z# .��� .+��� -   $ ��Name$$ LiveTrackerMenu��# [ .+�6   0LiveTracker.Views.Menu_Views.LiveTrackerMenuView  ( 
 "Syncfusion.UI.Xaml.Grid.SfDataGrid  -   %  Name$% TournamentsDataGrid��( 
 "Syncfusion.UI.Xaml.Grid.SfGridBase&   AutoGenerateColumns$& False��'   AllowResizingColumns$
' True��(   ColumnSizer$
( Star=�# [ )   EditTrigger$) OnDoubleTap=�$	��Top=�*   SelectionMode$* Single=�%+   AllowSelectionOnPointerPressed$
+ True��$��Hidden=�,   ShowToolTip$
, True��-   ItemsSource- �� *VisibleTournaments+.   SelectedItem. �� *SelectedTournament+/   ShowRowHeader/ �� *ShowRowHeader+   $ SizeChanged��.   $" UpdateColumnWidth�� �� #! Z#   $ QueryColumnDragging��.   $" UpdateColumnOrder�� �� #! Z# 0   HeaderContextMenu0 �� .@�m� $��Show/Hide Columns��.@�m� $��Addon Count��$7 1Track if you take any add-ons in your tournaments��1 m� StaysOpenOnClick$
1 True��2 m� CommandParameter$2 
AddonCount��3 m� Command3 �� 4   ChangeColumnVisibility#! Z4 m� $��Bounty Count��$� �Used to track the number of bounties received for Bovada Bounty tournaments that don't put bounty info in the lobby prize for you (this only gets pulled into the tournament data if Bovada Bounty is checked for the tournament��$
1 True��$2 Bounty��3 �� #! Z4 m� $��Entrants��$7 1Tracks the number of entrants in your tournaments��$
1 True��$2 Entrants��3 �� #! Z4 m� $��Entrants Paid��$D >Tracks the number of players who will cash in your tournaments��$
1 True��$2 EntrantsPaid��3 �� #! Z4 m� $��Finish Position��$? 9Tracks the position that you finished in your tournaments��$
1 True��$2 FinishPosition��3 �� #! Z4 m� $��Jackpot Spin Multiplier��$2 ,Track the multiplier in a jackpot sit and go��$
1 True��$2 JackpotSpinMultiplier��3 �� #! Z4 m� $��	Prize Won��$3 -Tracks the prizes you win in your tournaments��$
1 True��$2 PrizeWon��3 �� #! Z4 m� $��Rebuy Count��$C =Tracks the number of times your re-bought in your tournaments��$
1 True��$2 
RebuyCount��3 �� #! Z4 m� $��
Start Time��$1 +Shows the starting time of your tournaments��$
1 True��$2 	StartTime��3 �� #! Z4 m� $��
Total Cost��$p jShows the total cost of your tournaments (total buy-in + (total add-on * addons) + (total rebuy + addons))��$
1 True��$2 	TotalCost��3 �� #! Z4 m� $+ %Shows the venues for your tournaments��$��Venue��$
1 True��$2 Venue��3 �� #! Z4 5 � ContextMenu5 �� 6 �� 	StaysOpen$
6 True��7 �� IsOpen7 �� *IsContextMenuOpen+8 �� Mode$8 OneWayToSource=�   $ GotMouseCapture��.   $" UpdateTimeStrings�� �� #! Z# .@�m� $��Center=�$��DemiBold"�
����d�$
1 True������ *#!SelectedTournament.TournamentName+ �� *#!SelectedTournament.TournamentName+���� *
FontSize+�� $��0,0,5,0q�m� $��Finish Tournament��$� �Finishes the tournament and adds it to the database (updates the tournaments ending time to the current time if you haven't manually updated the tournaments ending time)��$1 False��3 �� 9   FinishTournamentMenuItem#! Z9 2 �� *SelectedTournament+���� *
FontSize+���� *	FontColor+�� $��0,0,5,0q�m� $��Starting Time������ *
FontSize+���� *	FontColor+.@�m� $��Update Starting Time...��$2 UpdateStartTimeSelected��$ Select a starting time��$1 False��3 �� #! Z# ���� *
FontSize+���� *	FontColor+m� $2 UpdateStartTime��$1 False������ *UpdateStartTimeNow+3 �� #! Z#  �� *$"SelectedTournament.StartTimeString+$ PropertyChanged=����� *
FontSize+���� *	FontColor+m� $��Ending Time������ *
FontSize+���� *	FontColor+.@�m� $��Update Ending Time...��$2 UpdateEndTimeSelected��$ Select an ending time��$1 False��3 �� #! Z# ���� *
FontSize+���� *	FontColor+m� $2 UpdateEndTime��$1 False������ *UpdateEndTimeNow+3 �� #! Z#  �� *" SelectedTournament.EndTimeString+$ PropertyChanged=����� *
FontSize+���� *	FontColor+�� $��0,0,5,0q�m� $��Tournament Data��.@�m� $��Update Tournament Data...��$; 5Modify/update the data used to create this tournament��$1 False��3 �� !:   ChangeTemplateDataMenuItem#! Z: 2 �� *SelectedTournament+���� *
FontSize+���� *	FontColor+m� $$��Create Duplicate Tournament...��$2 StartDuplicate��$1 False��3 �� #! Z#  �� *-+SelectedTournament.StartDuplicateMenuString+���� *
FontSize+���� *	FontColor+m� $��Cancel������ *
FontSize+���� *	FontColor+.@�m� $��Cancel Tournament��$2 CancelTournament��$1 False��3 �� #! Z#  �� *%#SelectedTournament.CancelMenuString+���� *
FontSize+���� *	FontColor+m� $��Cancel Tournaments...��$2 CancelSelectTournaments��$? 9Selectively cancel from any tournaments currently running��$1 False��3 �� #! Z# ���� *
FontSize+���� *	FontColor+m� $��Cancel All Tournaments��$2 CancelAllTournaments��$. (Cancel all tournaments currently running��$1 False��3 �� #! Z# ���� *
FontSize+���� *	FontColor+m� $��Session������ *
FontSize+���� *	FontColor+.@�m� $��Add to Session(s)...��$2 AddToSession��$P JSelectively add this tournament to any session you have previously created��$1 False��3 �� #! Z# ���� *
FontSize+���� *	FontColor+m� $��Remove from Session(s)...��$2 RemoveFromSession��$P JSelectively remove this tournament from any session that it is included in��$1 False��3 �� #! Z# ���� *
FontSize+���� *	FontColor+�� $��0,0,5,0q�m� $2 PinToFavorites��$F @Toggles this tournaments status on the favorite tournaments menu��$1 False������ *'%SelectedTournament.FavoriteMenuString+3 �� #! Z# ���� *
FontSize+���� *	FontColor+�� $��0,0,5,0q�m� $��
Screenshot������ *
FontSize+���� *	FontColor+.@�m� $��Attach Screenshot...��$� �Attach an image file to this tournament that is saved in the database with the rest of the tournament data after it finishes (great for saving those final table podium screen captures)��$2 AttachScreenshot��3 �� #! Z# ���� *
FontSize+���� *	FontColor+m� $��Remove Screenshot��$J DRemove an image file that was previously attached to this tournament��$2 RemoveScreenshot��3 �� #! Z# ���� *
FontSize+���� *	FontColor+m� $��View Screenshot��$F @View the image file (if any) that is attached to this tournament��$2 ViewScreenshot��3 �� #! Z# ���� *
FontSize+���� *	FontColor+m� $��Notes...��$� }Attach text notes to this tournament that are saved in the database with the rest of the tournament data after it is finished��$2 
AttachNote��3 �� #! Z# ���� *
FontSize+���� *	FontColor+;   Columns; , 
 &Syncfusion.UI.Xaml.Grid.GridTextColumn  , 
 &Syncfusion.UI.Xaml.Grid.GridColumnBase<   MappingName$< Venue��=   HeaderToolTipTemplate#= [ >   ShowHeaderToolTip$
> True��?   ToolTipTemplate#? [ ( 
 "Syncfusion.UI.Xaml.Grid.GridColumn@   AllowDragging$
@ True��A   TextAlignment$A Center=�B   
HeaderTextB �� *VenueHeader+  $< TournamentName��#= [ $
> True��#? [ $
@ True��$A Center=�B �� *TournamentNameHeader+0 
 *Syncfusion.UI.Xaml.Grid.GridDateTimeColumn  $< 	StartTime��#= [ $
> True��#? [ $
@ True��$A Center=�C   DisplayBindingC �� *	StartTime+D �� 	Converter#D [ B �� *StartTimeHeader+0 
 *Syncfusion.UI.Xaml.Grid.GridCurrencyColumn  $< BuyinTotalCost��#? [ #= [ $
> True��$A Center=�$
@ True��E   AllowEditing$E False��B �� *BuyinHeader+F   CurrencySymbolF �� *CurrencySymbol+$ PropertyChanged=�  $< RebuyTotalCost��#= [ $
> True��#? [ $A Center=�$
@ True��$E False��B �� *RebuyHeader+F �� *CurrencySymbol+$ PropertyChanged=�  $< AddonTotalCost��#= [ $
> True��#? [ $A Center=�$
@ True��$E False��B �� *AddonHeader+F �� *CurrencySymbol+$ PropertyChanged=�0 
 *Syncfusion.UI.Xaml.Grid.GridTemplateColumn  #= [ $
> True��#? [ $< BountyCount��0 
 *Syncfusion.UI.Xaml.Grid.FocusManagerHelperG   WantsKeyInput$
G True��$
@ True��B �� *BountyHeader+H   CellTemplateH �� .R�.  (Syncfusion.Windows.Shared.IntegerTextBox  I   MinValue$I 0��J �� TextAlignment$J Right=�$��0q�*  $Syncfusion.Windows.Shared.EditorBaseK   PositiveForeground
K ��d�$��25��$��-1,-8,-1,-8q�L   ShowSpinButton$
L True��M   ValueM �� N �� Path$N BountyCount�$ PropertyChanged=�  #? [ #= [ $
> True��$< JackpotSpinMultiplier��$
G True��$
@ True��B �� *JackpotSpinMultiplierHeader+H �� .R�  $I 0��$J Right=�
K ��d�$��0q�$��25��$��-1,-8,-1,-8q�$
L True��M �� $N JackpotSpinMultiplier�$ PropertyChanged=�  #? [ #= [ $
> True��$< 
RebuyCount��$
G True��$
@ True��B �� *RebuyCountHeader+H �� .R�  $I 0��$J Right=�$��0q�
K ��d�$��25��$��-1,-8,-1,-8q�$
L True��M �� $N 
RebuyCount�$ PropertyChanged=�  #? [	 #= [ $
> True��$< 
AddonCount��$
G True��$
@ True��B �� *AddonCountHeader+H �� .R�  $I 0��$J Right=�$��0q�
K ��d�$��25��$��-1,-8,-1,-8q�$
L True��M �� $N 
AddonCount�$ PropertyChanged=�  $< 	TotalCost��#= [ $
> True��#? [ $
@ True��$A Center=�$E False��B �� *TotalCostHeader+$ PropertyChanged=�F �� *CurrencySymbol+$ PropertyChanged=�  #? [
 #= [ $
> True��$< Entrants��$
G True��$
@ True��B �� *EntrantsHeader+H �� .R�  $I 0��$��25��
K ��d�$��-1,-8,-1,-8q�$��0q�$J Right=�M �� $N Entrants�$ PropertyChanged=�  #? [ #= [ $
> True��$< EntrantsPaid��$
G True��$
@ True��B �� *EntrantsPaidHeader+H �� .R�  $I 0��$��25��
K ��d�$��-1,-8,-1,-8q�$��0q�$J Right=�M �� $N EntrantsPaid�$ PropertyChanged=�  #? [ #= [ $
> True��$< FinishPosition��$
G True��$
@ True��B �� *FinishPositionHeader+H �� .R�  $I 0��$��25��
K ��d�$��-1,-8,-1,-8q�$��0q�$J Right=�M �� $N FinishPosition�$ PropertyChanged=�  #? [ #= [ $
> True��$< PrizeWon��$
G True��$
@ True��B �� *PrizeWonHeader+H �� .R�/  )Syncfusion.Windows.Shared.CurrencyTextBox  O   MinValue$O 0~�$��25��$��-1,-8,-1,-8q�$��0q�$J Right=�P   ScrollInterval$	P 1.0Q�Q   Culture$Q en-US��R   ValueR �� $N PrizeWon�$ PropertyChanged=�S   CurrencySymbolS �� *CurrencySymbol+$ PropertyChanged=�