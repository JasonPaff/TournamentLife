   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=null;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  ?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager B    <LiveTracker.Views.Template_Manager_Views.TemplateManagerView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
    =commands"clr-namespace:LiveTracker.Commands   	 
    A
converters$clr-namespace:LiveTracker.Converters   	 
    _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF  0  *Syncfusion.Windows.Shared.ChromelessWindow    UseNativeChrome$
  True�� � SizeToContent$ WidthAndHeight=�$	��500��$	��800��   ShowMaximizeButton$ False�� � 
ResizeMode$ CanMinimize=�,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$ NameTextBox�� � Title �� *Title+ �� UpdateSourceTrigger$ PropertyChanged=�	 � 	Resources	 /   )LiveTracker.Converters.BoolToYesConverter   BoolToYesConverter5   /LiveTracker.Converters.SngToVisibilityConverter  SngToVisibleConverter0   *LiveTracker.Converters.DateToTimeConverter  DateToTimeConverter  DataGridContextMenu
 �� 
TargetType+  %Syncfusion.Windows.Shared.MenuItemAdv �� Property �� Value  DataGridContextMenu2/  )Syncfusion.UI.Xaml.Grid.GridRowHeaderCell �� 
TargetType ��Name �� TextAlignment �� RelativeSource��%�  &	        &	      &	 
     &	      &	 �     '  �           �� "
  .���  � �� �� *	FontColor+��  � �� �� *
FontSize+��  � ��$ -50,0,-40,0q��� "
 m�.���  � �� �� *	FontColor+��  � �� �� *
FontSize+�� "
  .���  � �� �� "  .R��� $ PART_RowHeaderCellBorder��#��z��#��z��._�� .+��� $��Center=�$��Center=�$ Center=����� *
RowIndex+ �� *TemplatedParent+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +	  %Microsoft.Xaml.Behaviors.EventTrigger	   	  	EventName$ Loaded��*
  $Microsoft.Xaml.Behaviors.TriggerBase 
  Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     CommandParameter$ WindowLoaded��   Command �� 2   ,LiveTracker.Commands.TemplateManagerCommands   MenuItem �� Source# Z .���  � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $��10 ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $��0 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�'  !Syncfusion.Windows.Shared.MenuAdv  $��0��$��0��$��2��   
ExpandMode$ ExpandOnClick=�.@�  $
��File������ *
FontSize+.@�  $��Import Tournaments...�� � ToolTip$6 0Import tournaments from a saved tournaments file��   CommandParameter$ ImportTemplates������ *
FontSize+   Command �� # Z   $��Export Tournaments...��$. (Export tournaments to a tournaments file��$ ExportTemplates������ *
FontSize+ �� # Z 1  +Syncfusion.Windows.Shared.MenuItemSeparator  $	��110��$
��Left=�  $��Delete Tournaments...��$; 5Delete tournaments from a list of all the tournaments��$ DeleteTemplates������ *
FontSize+ �� # Z   $	��110��$
��Left=�  $
��Exit��$ Exit tournament manager��$
 Exit������ *
FontSize+ �� # Z   $
��Edit������ *
FontSize+.@�  $��Edit Formats...��$9 3Add/Remove formats from the tournament formats list��$ EditFormats������ *
FontSize+ �� # Z   $��Edit Game Types...��$? 9Add/Remove game types from the tournament game types list��$ EditGameTypes������ *
FontSize+ �� # Z   $��Edit Venues...��$7 1Add/Remove venues from the tournament venues list��$ 
EditVenues������ *
FontSize+ �� # Z   $��Edit Default Values...��$a [Change the default values for the tournament manager and quick start tournament menu option��$ EditDefaults������ *
FontSize+ �� # Z �� $��2��$��0�� �� Orientation$ Vertical=�.+��� $
��Name��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Venue��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��	Game Type��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��
Start Time��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Buyin��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��
Buyin Rake��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Rebuy��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��
Rebuy Rake��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Addon��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��
Addon Rake��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��	Guarantee��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Bovada Bounty��$��0,2,0,0q�$��Right=�y��� *BovadaCheckBox+ �� 	Converter# [ ���� *
FontSize+�� $��Late Reg��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Blind Levels��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��
Table Size��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Starting Stack��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Rebuy Stack��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Addon Stack��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Entrants��$��0,2,0,0q�$��Right=�y��� *SngCheckBox+# [ ���� *
FontSize+�� $��Entrants Paid��$��0,2,0,0q�$��Right=�y��� *SngCheckBox+# [ ���� *
FontSize+�� $��Sng Payouts��$��0,4,0,0q�$��Right=�y��� *SngCheckBox+# [ ���� *
FontSize+�� $��2��$��1��.+��� -     ��Name$  NameTextBox��! �� TextAlignment$
! Left=�$��Center=�$��26��$	��100��$ Tournament Name������ *Template.TournamentName+$ PropertyChanged=����� *
FontSize+�� $��26��$	��100��$: 4Tournament venue (add/remove venues under Edit menu)������ *
FontSize+���� *Venues+" �� SelectedItem" �� *SelectedVenue+�� $��26��$	��100��$B <Tournament Game Type (add/remove game types under Edit menu)������ *
FontSize+���� *	GameTypes+" �� *SelectedGameType+4 	 .Syncfusion.Windows.Controls.Input.SfTimePicker  $��26��$ Starting Time��#   Value# �� *Template.StartTime+$ PropertyChanged=�$ �� Mode$$ TwoWay=����� *
FontSize+/  )Syncfusion.Windows.Shared.CurrencyTextBox  $��26��%   ScrollInterval$	% 1.0Q�&   MinValue$& 0~�*  $Syncfusion.Windows.Shared.EditorBase'   Culture$' en-US��$! Right=�$0 *Buyin base cost (buy-in cost without rake)��(   Value( �� *Template.BuyinBaseCost+���� *
FontSize+  $	% 1.0Q�$& 0~�$' en-US��$��26��$! Right=�$ Buyin Rake cost��( �� *Template.BuyinRakeCost+���� *
FontSize+  $	% 1.0Q�$& 0~�$' en-US��$��26��$! Right=�$0 *Rebuy base cost (re-buy cost without rake)��( �� *Template.RebuyBaseCost+���� *
FontSize+  $	% 1.0Q�$& 0~�$' en-US��$��26��$! Right=�$ Rebuy Rake cost��( �� *Template.RebuyRakeCost+���� *
FontSize+  $	% 1.0Q�$& 0~�$' en-US��$��26��$! Right=�$. (Addon base cost (addon cost without rake��( �� *Template.AddonBaseCost+���� *
FontSize+  $	% 1.0Q�$& 0~�$' en-US��$��26��$! Right=�$ Addon Rake cost��( �� *Template.AddonRakeCost+���� *
FontSize+  $% 500.0Q�$& 0~�$' en-US��$��26��$! Right=�$ 	Guarantee��( �� *Template.Guarantee+���� *
FontSize+  -   )  Name$) BovadaBounty��$% 1Q�$& 0~�$��26��$! Right=�$1 +Bounty amount for Bovada bounty tournaments��( �� *Template.Bounty+���� *
FontSize+y��� *BovadaCheckBox+# [ .  (Syncfusion.Windows.Shared.IntegerTextBox  *   ScrollInterval$* 5��+   MinValue$+ 0��$��26��$! Right=�$" Late Reg length (in minutes)��,   Value, �� *Template.LateReg+���� *
FontSize+  $* 5��$+ 0��$��26��$! Right=�$&  Blind Levels length (in minutes)��, �� *Template.BlindLevels+���� *
FontSize+  $* 1��$+ 2��-   MaxValue$- 10��$��26��$! Right=�$ Players per table��, �� *Template.TableSize+���� *
FontSize+  $	* 500��$+ 0��$! Right=�$��26��$ Starting Stack Size��, �� *Template.StackSizeStarting+���� *
FontSize+  $	* 500��$+ 0��$��26��$! Right=�$3 -Chip stack you get for a rebuy (if available)��, �� *Template.StackSizeRebuy+���� *
FontSize+  $	* 500��$+ 0��$��26��$! Right=�$ Addon Stack Size��, �� *Template.StackSizeAddon+���� *
FontSize+  -   .  Name$. SngEntrants��$* 1��$+ 0��$��26��$! Right=�$D >Number of players who can enter the tournament (used for sngs)��, �� *Template.Entrants+���� *
FontSize+y��� *SngCheckBox+# [   $* 1��$+ 0��$��26��$! Right=�$8 2Players who cash in the tournament (used for sngs)��, �� *Template.EntrantsPaid+���� *
FontSize+y��� *SngCheckBox+# [ �� $
! Left=�$��0,5,0,0q�$��26��$	��160��$� �The payouts for the sit and go, start with 1st place prize and separate each value with a comma
For example if the sng pays out top 3 places then you would enter the sng payouts as 45,27,15������ *Template.SngPayouts+���� *
FontSize+y��� *SngCheckBox+# [ �� $��3��$��0��$��2��$��0,5,0,0q�.+�4  .Syncfusion.Windows.Tools.Controls.CheckListBox  -   /  Name$
/ Grid��0 �� DisplayMemberPath$0 DisplayString��$
��Left=�$	��100��$��26��$	��138��$��10,0,0,0q�1   IsCheckOnFirstClick$
1 true��2   IsSelectAllEnabled$2 False��$= 7Tournament formats (add/remove formats under Edit menu)������ *FormatsList+$ PropertyChanged=�$$ TwoWay=����� *
FontSize+ 	  $ ItemChecked��.   $ ItemChecked�� �� # Z ���� 8  2Syncfusion.Windows.Tools.Controls.CheckListBoxItem"
  .���  �@ 	IsChecked �� $$ TwoWay=�3 �� Path$3 
IsSelected����� .R��� �� $��4��$��0��$��2��$ Vertical=�.+��� $��Center=�$��Fixed Bounty Tournament��$� �-This will tag the tournament as a fixed bounty tournament
 (the amount you win from knocking someone out is fixed in size)

-This tag is mainly useful for when you are playing at a venue
 (Bovada,Ignition,ect..) that doesn't track the fixed limit bounties
 won for you in the lobby. Tag it with the fixed bounty tag and use
 the bounty column in the main tracker to track the bounties won

-Use the bounty column on the main tracker to track fixed limit bounties won.
 To turn on the bounty column right click any columns header on the
 main tracker and select the bounty column from the menu��$��-6,5,0,0q�4 d� 	IsChecked4 �� *BovadaCheckBox+ �� $ BovadaBounty������ *
FontSize+�� $��Center=�$��Sit and Go Tournament��$� �-This will tag the tournament as a Sit and Go tournament
  (a tournament that starts after a set number of people
   have registered instead of at a set time)

-When started by the tracker any tournament flagged as
 a Sit and Go will have its starting time set to the current
 time instead of the starting time selected here

-When started by the tracker any tournament flagged as a
 Sit and Go will have its Entrants and Entrants Paid updated��$��	-20,5,0,0q� �� $ SngEntrants��4 �� *SngCheckBox+���� *
FontSize+�� $��5��$��0��$��15,5,0,0q�$��2��$��Center=�$ 
Horizontal=�.+��� $��Save Tournament��$ Save this tournament��$��25��$��60��$	��5,5q�$��0,0,5,0q�$��SaveTemplate������ *
FontSize+���� # Z �� $��Reset Values��$a [Reset tournament settings to default values, default values can be changed in the Edit menu��$��25��$��60��$	��5,5q�$��0,0,5,0q�$��ResetTemplate�� �� $ NameTextBox������ *
FontSize+���� # Z (  "Syncfusion.UI.Xaml.Grid.SfDataGrid  -   5  Name$5 TemplatesDataGrid��(  "Syncfusion.UI.Xaml.Grid.SfGridBase6   AutoGenerateColumns$6 False��7   AllowResizingColumns$
7 True��8   ColumnSizer$
8 Auto=�$��2��$��7��$��3��$	��755��$
��1000��9   SelectionMode$9 Single=�$	��Top=�:   ShowToolTip$
: True��;   ItemsSource; �� *	Templates+$ PropertyChanged=�$$ TwoWay=�<   SelectedItem< �� *SelectedTemplate+=   ShowRowHeader= �� *ShowRowHeader+ 	  $ QueryColumnDragging��.   $ UpdateColumnOrder�� �� # Z 	  $ SizeChanged��.   $ UpdateColumnWidth�� �� # Z > � ContextMenu> �� ? �� 	StaysOpen$
? True��@ �� IsOpen@ �� *IsContextMenuOpen+$$ OneWayToSource=�.@�  $= 7Add/Remove tournament to/from favorite tournaments list��$ Favorite��#��[ ���� *%#SelectedTemplate.FavoriteMenuString+ �� # Z ���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�  $��Edit Tournament��$ Edit this tournament��$ EditTemplate��#��[  �� # Z ���� *
FontSize+���� *	FontColor+  $��Copy Tournament��$ Copy this tournament��$ CopyTemplate��#��[  �� # Z ���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�  $��Delete Tournament��$ Delete this tournament��$ DeleteTemplate��#��[  �� # Z ���� *
FontSize+���� *	FontColor+A   ColumnsA ,  &Syncfusion.UI.Xaml.Grid.GridTextColumn  ,  &Syncfusion.UI.Xaml.Grid.GridColumnBaseB   MappingName$B TournamentName��C   
HeaderText$C Tournament Name��(  "Syncfusion.UI.Xaml.Grid.GridColumnD   AllowDragging$
D True��E   TextAlignment$E Center=�  $B Venue��$C Venue��$
D True��$E Center=�  $B GameType��$C 	Game Type��$
D True��$E Center=�  $B FormatString��$C 	Format(s)��$
D True��$E Center=�0  *Syncfusion.UI.Xaml.Grid.GridDateTimeColumn  $B 	StartTime��$C 
Start Time��$
D True��$E Center=�F   DisplayBindingF �� *	StartTime+# [ 0  *Syncfusion.UI.Xaml.Grid.GridCurrencyColumn  $B 	Guarantee��$C 	Guarantee��$E Center=�$
D True��  $B BuyinTotalCost��$C Buyin Total��$E Center=�$
D True��  $B BuyinBaseCost��$C 
Buyin Base��$E Center=�$
D True��  $B BuyinRakeCost��$C 
Buyin Rake��$E Center=�$
D True��  $B RebuyTotalCost��$C Rebuy Total��$E Center=�$
D True��  $B RebuyBaseCost��$C 
Rebuy Base��$E Center=�$
D True��  $B RebuyRakeCost��$C 
Rebuy Rake��$E Center=�$
D True��  $B AddonTotalCost��$C Addon Total��$E Center=�$
D True��  $B AddonBaseCost��$C 
Addon Base��$E Center=�$
D True��  $B AddonRakeCost��$C 
Addon Rake��$E Center=�$
D True��  $B Bounty��$C Bovada Bounty��$E Center=�$
D True��  $B StackSizeStarting��$C Starting Stack��$
D True��$E Center=�  $B StackSizeRebuy��$C Rebuy Stack��$
D True��$E Center=�  $B StackSizeAddon��$C Addon Stack��$
D True��$E Center=�  $B 	TableSize��$C 
Table Size��$
D True��$E Center=�  $B BlindLevels��$C Blind Levels��$
D True��$E Center=�  $B LateReg��$C Late Reg��$
D True��$E Center=�  $B Entrants��$C Entrants��$
D True��$E Center=�  $B EntrantsPaid��$C Entrants Paid��$
D True��$E Center=�  $B IsSng��$
C Sng?��$
D True��$E Center=�F �� *IsSng+# [    $B IsBovadaBounty��$C Bovada Bounty?��$
D True��$E Center=�F �� *IsBovadaBounty+# [  