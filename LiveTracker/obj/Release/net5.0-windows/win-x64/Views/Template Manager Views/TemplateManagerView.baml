   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.0.1.2, Culture=neutral, PublicKeyToken=null;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  ?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager B    <LiveTracker.Views.Template_Manager_Views.TemplateManagerView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a	 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf    	 
   =commands"clr-namespace:LiveTracker.Commands    	 
   A
converters$clr-namespace:LiveTracker.Converters    	 
   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    � WindowStartupLocation$  CenterScreen=� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$ NameTextBox�� � Title �� *Title+ �� UpdateSourceTrigger$ PropertyChanged=� � 	Resources /   )LiveTracker.Converters.BoolToYesConverter   BoolToYesConverter5   /LiveTracker.Converters.SngToVisibilityConverter  SngToVisibleConverter0   *LiveTracker.Converters.DateToTimeConverter  DateToTimeConverter  DataGridContextMenu	 �� 
TargetType+ 
 %Syncfusion.Windows.Shared.MenuItemAdv
 �� Property �� Value  DataGridContextMenu2/ 	 )Syncfusion.UI.Xaml.Grid.GridRowHeaderCell �� 
TargetType ��Name �� TextAlignment �� RelativeSource��%�  &	        &	      &	 
     &	      &	 �     '  �           �� "	  .��� 
 � �� �� *	FontColor+�� 
 � �� �� *
FontSize+�� 
 � ��$ -50,0,-40,0q��� "	 m�.��� 
 � �� �� *	FontColor+�� 
 � �� �� *
FontSize+�� "	  .��� 
 � �� �� "  .R��� $ PART_RowHeaderCellBorder��#��z��#��z��._�� .+��� $��Center=�$��Center=�$ Center=����� *
RowIndex+ �� *TemplatedParent+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ Loaded��*	  $Microsoft.Xaml.Behaviors.TriggerBase 	  Actions. 2
  ,Microsoft.Xaml.Behaviors.InvokeCommandAction
   
  CommandParameter$ WindowLoaded�� 
  Command �� 2   ,LiveTracker.Commands.TemplateManagerCommands   MenuItem �� Source# Z .���  � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $��10 ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $��0 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�' 
 !Syncfusion.Windows.Shared.MenuAdv  $��0��$��0��$��2��   
ExpandMode$ ExpandOnClick=�.@�  $
��File������ *
FontSize+.@�  $��Import Tournaments��   CommandParameter$ ImportTemplates������ *
FontSize+   Command �� # Z   $��Export Tournaments��$ ExportTemplates������ *
FontSize+ �� # Z   $��Delete Tournaments��$ DeleteTemplates������ *
FontSize+ �� # Z   $
��Exit��$
 Exit������ *
FontSize+ �� # Z   $
��Edit������ *
FontSize+.@�  $��Edit Formats��$ EditFormats������ *
FontSize+ �� # Z   $��Edit Game Types��$ EditGameTypes������ *
FontSize+ �� # Z   $��Edit Venues��$ 
EditVenues������ *
FontSize+ �� # Z   $��Edit Default Values��$ EditDefaults������ *
FontSize+ �� # Z �� $��2��$��0�� �� Orientation$ Vertical=�.+��� $
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
FontSize+�� $��Bovada Bounty��$��0,2,0,0q�$��Right=�y��� *BovadaCheckBox+ �� 	Converter# [ ���� *
FontSize+�� $��Late Reg��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Blind Levels��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��
Table Size��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Starting Stack��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Rebuy Stack��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Addon Stack��$��0,2,0,0q�$��Right=����� *
FontSize+�� $��Entrants��$��0,2,0,0q�$��Right=�y��� *SngCheckBox+# [ ���� *
FontSize+�� $��Entrants Paid��$��0,2,0,0q�$��Right=�y��� *SngCheckBox+# [ ���� *
FontSize+�� $��2��$��1��.+��� -    ��Name$ NameTextBox�� �� TextAlignment$
 Left=�$��Center=�$��26��$	��100��  � ToolTip$  Tournament Name������ *Template.TournamentName+���� *
FontSize+�� $��26��$	��100��$:  4Tournament venue (add/remove venues under Edit menu)������ *
FontSize+���� *Venues+! �� SelectedItem! �� *SelectedVenue+�� $��26��$	��100��$B  <Tournament Game Type (add/remove game types under Edit menu)������ *
FontSize+���� *	GameTypes+! �� *SelectedGameType+4  .Syncfusion.Windows.Controls.Input.SfTimePicker  $��26��$  Starting Time��"   Value" �� *Template.StartTime+$ PropertyChanged=�# �� Mode$# TwoWay=����� *
FontSize+/ 
 )Syncfusion.Windows.Shared.CurrencyTextBox  $��26��$   ScrollInterval$	$ 1.0Q�%   MinValue$% 0~�* 
 $Syncfusion.Windows.Shared.EditorBase&   Culture$& en-US��$ Right=�$0  *Buyin base cost (buy-in cost without rake)��'   Value' �� *Template.BuyinBaseCost+���� *
FontSize+  $	$ 1.0Q�$% 0~�$& en-US��$��26��$ Right=�$  Buyin Rake cost��' �� *Template.BuyinRakeCost+���� *
FontSize+  $	$ 1.0Q�$% 0~�$& en-US��$��26��$ Right=�$0  *Rebuy base cost (re-buy cost without rake)��' �� *Template.RebuyBaseCost+���� *
FontSize+  $	$ 1.0Q�$% 0~�$& en-US��$��26��$ Right=�$  Rebuy Rake cost��' �� *Template.RebuyRakeCost+���� *
FontSize+  $	$ 1.0Q�$% 0~�$& en-US��$��26��$ Right=�$.  (Addon base cost (addon cost without rake��' �� *Template.AddonBaseCost+���� *
FontSize+  $	$ 1.0Q�$% 0~�$& en-US��$��26��$ Right=�$  Addon Rake cost��' �� *Template.AddonRakeCost+���� *
FontSize+  $$ 500.0Q�$% 0~�$& en-US��$��26��$ Right=�$  	Guarantee��' �� *Template.Guarantee+���� *
FontSize+  -   (  Name$( BovadaBounty��$$ 1Q�$% 0~�$��26��$ Right=�$1  +Bounty amount for Bovada bounty tournaments��' �� *Template.Bounty+���� *
FontSize+y��� *BovadaCheckBox+# [ . 
 (Syncfusion.Windows.Shared.IntegerTextBox  )   ScrollInterval$) 5��*   MinValue$* 0��$��26��$ Right=�$"  Late Reg length (in minutes)��+   Value+ �� *Template.LateReg+���� *
FontSize+  $) 5��$* 0��$��26��$ Right=�$&   Blind Levels length (in minutes)��+ �� *Template.BlindLevels+���� *
FontSize+  $) 1��$* 2��,   MaxValue$, 10��$��26��$ Right=�$  Players per table��+ �� *Template.TableSize+���� *
FontSize+  $	) 500��$* 0��$ Right=�$��26��$  Starting Stack Size��+ �� *Template.StackSizeStarting+���� *
FontSize+  $	) 500��$* 0��$��26��$ Right=�$3  -Chip stack you get for a rebuy (if available)��+ �� *Template.StackSizeRebuy+���� *
FontSize+  $	) 500��$* 0��$��26��$ Right=�$  Addon Stack Size��+ �� *Template.StackSizeAddon+���� *
FontSize+  -   -  Name$- SngEntrants��$) 1��$* 0��$��26��$ Right=�$D  >Number of players who can enter the tournament (used for sngs)��+ �� *Template.Entrants+���� *
FontSize+y��� *SngCheckBox+# [   $) 1��$* 0��$��26��$ Right=�$8  2Players who cash in the tournament (used for sngs)��+ �� *Template.EntrantsPaid+���� *
FontSize+y��� *SngCheckBox+# [ �� $��3��$��0��$��2��$��0,5,0,0q�.+�4  .Syncfusion.Windows.Tools.Controls.CheckListBox  -   .  Name$
. Grid��/ �� DisplayMemberPath$/ DisplayString��$
��Left=�$	��100��$��26��$	��150��$��10,0,0,0q�0   IsCheckOnFirstClick$
0 true��1   IsSelectAllEnabled$1 False��$=  7Tournament formats (add/remove formats under Edit menu)������ *FormatsList+$ PropertyChanged=�$# TwoWay=����� *
FontSize+   $ ItemChecked��. 
  $ ItemChecked�� �� # Z ���� 8  2Syncfusion.Windows.Tools.Controls.CheckListBoxItem"	  .��� 
 �@ 	IsChecked �� $# TwoWay=�2 �� Path$2 
IsSelected����� .R��� �� $��4��$��0��$��2��$ Vertical=�.+��� $��Center=�$#��Tournament is a Bovada Bounty��$}  wThe bounty amount for Bovada/BetOnline tournaments where the bounty isn't counted in the final prize total in the lobby��$��4,5,0,0q�3 d� 	IsChecked3 �� *BovadaCheckBox+ �� $ BovadaBounty������ *
FontSize+�� $��Center=�$ ��Tournament is a Sit and Go��$-  'Tag this tournament as a sng tournament��$��	-24,5,0,0q� �� $ SngEntrants��3 �� *SngCheckBox+���� *
FontSize+�� $��5��$��0��$��2��$	��5,5q�$��Center=�$ 
Horizontal=�.+��� $
��Save��$��25��$��60��$��0,0,5,0q�$��SaveTemplate������ *
FontSize+���� # Z �� $��Reset��$��25��$��60��$��0,0,5,0q�$��ResetTemplate�� �� $ NameTextBox������ *
FontSize+���� # Z ( 	 "Syncfusion.UI.Xaml.Grid.SfDataGrid  -   4  Name$4 TemplatesDataGrid��( 	 "Syncfusion.UI.Xaml.Grid.SfGridBase5   AutoGenerateColumns$5 False��6   AllowResizingColumns$
6 True��7   ColumnSizer$
7 Auto=�$��2��$��7��$��3��$	��728��$
��1000��8   SelectionMode$8 Single=�$	��Top=�9   ShowToolTip$
9 True��:   ItemsSource: �� *	Templates+$ PropertyChanged=�$# TwoWay=�;   SelectedItem; �� *SelectedTemplate+<   ShowRowHeader< �� *ShowRowHeader+   $ QueryColumnDragging��. 
  $ UpdateColumnOrder�� �� # Z   $ SizeChanged��. 
  $ UpdateColumnWidth�� �� # Z = � ContextMenu= �� > �� 	StaysOpen$
> True��? �� IsOpen? �� *IsContextMenuOpen+$# OneWayToSource=�.@�  $6  0Add/Remove template from favorite templates list��$ Favorite��#��[ ���� *%#SelectedTemplate.FavoriteMenuString+ �� # Z ���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�  $��Edit Template��$  Edit this template��$ EditTemplate��#��[  �� # Z ���� *
FontSize+���� *	FontColor+  $��Copy Template��$  Copy this template��$ CopyTemplate��#��[  �� # Z ���� *
FontSize+���� *	FontColor+�� $��	-20,0,5,0q�  $��Delete Template��$  Delete this template��$ DeleteTemplate��#��[  �� # Z ���� *
FontSize+���� *	FontColor+@   Columns@ , 	 &Syncfusion.UI.Xaml.Grid.GridTextColumn  , 	 &Syncfusion.UI.Xaml.Grid.GridColumnBaseA   MappingName$A TournamentName��B   
HeaderText$B Tournament Name��( 	 "Syncfusion.UI.Xaml.Grid.GridColumnC   AllowDragging$
C True��D   TextAlignment$D Center=�  $A Venue��$B Venue��$
C True��$D Center=�  $A GameType��$B 	Game Type��$
C True��$D Center=�  $A FormatString��$B 	Format(s)��$
C True��$D Center=�0 	 *Syncfusion.UI.Xaml.Grid.GridDateTimeColumn  $A 	StartTime��$B 
Start Time��$
C True��$D Center=�E   DisplayBindingE �� *	StartTime+# [ 0 	 *Syncfusion.UI.Xaml.Grid.GridCurrencyColumn  $A 	Guarantee��$B 	Guarantee��$D Center=�$
C True��  $A BuyinTotalCost��$B Buyin Total��$D Center=�$
C True��  $A BuyinBaseCost��$B 
Buyin Base��$D Center=�$
C True��  $A BuyinRakeCost��$B 
Buyin Rake��$D Center=�$
C True��  $A RebuyTotalCost��$B Rebuy Total��$D Center=�$
C True��  $A RebuyBaseCost��$B 
Rebuy Base��$D Center=�$
C True��  $A RebuyRakeCost��$B 
Rebuy Rake��$D Center=�$
C True��  $A AddonTotalCost��$B Addon Total��$D Center=�$
C True��  $A AddonBaseCost��$B 
Addon Base��$D Center=�$
C True��  $A AddonRakeCost��$B 
Addon Rake��$D Center=�$
C True��  $A Bounty��$B Bovada Bounty��$D Center=�$
C True��  $A StackSizeStarting��$B Starting Stack��$
C True��$D Center=�  $A StackSizeRebuy��$B Rebuy Stack��$
C True��$D Center=�  $A StackSizeAddon��$B Addon Stack��$
C True��$D Center=�  $A 	TableSize��$B 
Table Size��$
C True��$D Center=�  $A BlindLevels��$B Blind Levels��$
C True��$D Center=�  $A LateReg��$B Late Reg��$
C True��$D Center=�  $A Entrants��$B Entrants��$
C True��$D Center=�  $A EntrantsPaid��$B Entrants Paid��$
C True��$D Center=�  $A IsSng��$
B Sng?��$
C True��$D Center=�E �� *IsSng+# [    $A IsBovadaBounty��$B Bovada Bounty?��$
C True��$D Center=�E �� *IsBovadaBounty+# [  