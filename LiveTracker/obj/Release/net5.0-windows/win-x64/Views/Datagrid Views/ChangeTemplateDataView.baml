   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.0.1.2, Culture=neutral, PublicKeyToken=null?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager =    7LiveTracker.Views.Datagrid_Views.ChangeTemplateDataView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a	 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf    	 
   A
converters$clr-namespace:LiveTracker.Converters    	 
   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    � Title$  Update Tournament Data�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�$	��450��$	��800��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$ NameTextBox�� � 	Resources 5   /LiveTracker.Converters.SngToVisibilityConverter   SngToVisibleConverter��%   &	          .���  � ColumnDefinitions �� $
��Auto ��� $
��Auto � � RowDefinitions �� $
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
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $
��Name��$��0��$��0��$��Right=����� *
FontSize+�� $��Venue��$��0��$��1��$��Right=����� *
FontSize+�� $��	Game Type��$��0��$��2��$��Right=����� *
FontSize+�� $��Formats��$��0��$��3��$��Right=����� *
FontSize+�� $��
Start Date��$��0��$��4��$��Right=����� *
FontSize+�� $��
Start Time��$��0��$��5��$��Right=����� *
FontSize+�� $��Buyin��$��0��$��6��$��Right=����� *
FontSize+�� $��
Buyin Rake��$��0��$��7��$��Right=����� *
FontSize+�� $��Rebuy��$��0��$��8��$��Right=����� *
FontSize+�� $��
Rebuy Rake��$��0��$��9��$��Right=����� *
FontSize+�� $��Addon��$��0��$��10��$��Right=����� *
FontSize+�� $��
Addon Rake��$��0��$��11��$��Right=����� *
FontSize+�� $��	Guarantee��$��0��$��12��$��Right=����� *
FontSize+�� $��Late Reg��$��0��$��13��$��Right=����� *
FontSize+�� $��Blind Levels��$��0��$��14��$��Right=����� *
FontSize+�� $��
Table Size��$��0��$��15��$��Right=����� *
FontSize+�� $��Starting Stack��$��0��$��16��$��Right=����� *
FontSize+�� $��Rebuy Stack��$��0��$��17��$��Right=����� *
FontSize+�� $��Addon Stack��$��0��$��18��$��Right=����� *
FontSize+�� $��Entrants��$��0��$��19��$��0,4,0,0q�$��Right=�y��� *SngCheckBox+	 �� 	Converter#	 [  ���� *
FontSize+�� $��Entrants Paid��$��0��$��20��$��0,4,0,0q�$��Right=�y��� *SngCheckBox+#	 [  ���� *
FontSize+�� $��Bovada Bounty��$��0��$��21��$��0,4,0,0q�$��Right=�y��� *BovadaCheckBox+#	 [  ���� *
FontSize+�� -   
 ��Name$
 NameTextBox��$��1��$��0�� �� TextAlignment$
 Left=�$��Center=�$��26�� � ToolTip$ Tournament Name��$	��100������ *TournamentData.TournamentName+���� *
FontSize+�� $��1��$��1��$
 Left=�$��Center=�$��26��$ Poker Site/Venue Name������ *TournamentData.Venue+���� *
FontSize+�� $��1��$��2��$
 Left=�$��Center=�$��26��$ 	Game Type������ *TournamentData.GameType+���� *
FontSize+�� $��1��$��3��$
 Left=�$��Center=�$��26��$8 2Tournament Formats (separate formats with a comma)������ *Format+���� *
FontSize+4  .Syncfusion.Windows.Controls.Input.SfDatePicker  $��1��$��4��$ Starting Date��   Value �� *	StartDate+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=����� *
FontSize+4  .Syncfusion.Windows.Controls.Input.SfTimePicker  $��1��$��5��$ Starting Time��   Value �� *	StartTime+$ PropertyChanged=�$ TwoWay=����� *
FontSize+/ 
 )Syncfusion.Windows.Shared.CurrencyTextBox  $��1��$��6��   ScrollInterval$	 1.0Q�   MinValue$ 0~�* 
 $Syncfusion.Windows.Shared.EditorBase   Culture$ en-US��$ Right=�$0 *Buyin base cost (buy-in cost without rake)��   Value �� *TournamentData.BuyinBaseCost+���� *
FontSize+  $��1��$��7��$	 1.0Q�$ 0~�$ en-US��$ Right=�$ Buyin Rake cost�� �� *TournamentData.BuyinRakeCost+���� *
FontSize+  $��1��$��8��$	 1.0Q�$ 0~�$ en-US��$ Right=�$0 *Rebuy base cost (re-buy cost without rake)�� �� *TournamentData.RebuyBaseCost+���� *
FontSize+  $��1��$��9��$	 1.0Q�$ 0~�$ en-US��$ Right=�$ Rebuy Rake cost�� �� *TournamentData.RebuyRakeCost+���� *
FontSize+  $��1��$��10��$	 1.0Q�$ 0~�$ en-US��$ Right=�$. (Addon base cost (addon cost without rake�� �� *TournamentData.AddonBaseCost+���� *
FontSize+  $��1��$��11��$	 1.0Q�$ 0~�$ en-US��$ Right=�$ Addon Rake cost�� �� *TournamentData.AddonRakeCost+���� *
FontSize+  $��1��$��12��$ 500.0Q�$ 0~�$ en-US��$ Right=�$ 	Guarantee�� �� *TournamentData.Guarantee+���� *
FontSize+. 
 (Syncfusion.Windows.Shared.IntegerTextBox  $��1��$��13��   ScrollInterval$ 5��   MinValue$ 0��$ Right=�$" Late Reg length (in minutes)��   Value �� *TournamentData.LateReg+���� *
FontSize+  $��1��$��14��$ 5��$ 0��$ Right=�$&  Blind Levels length (in minutes)�� �� *TournamentData.BlindLevels+���� *
FontSize+  $��1��$��15��$ 1��$ 2��   MaxValue$ 10��$ Right=�$ Players per table�� �� *TournamentData.TableSize+���� *
FontSize+  $��1��$��16��$	 500��$ 0��$ Right=�$ Starting Stack Size�� �� *" TournamentData.StackSizeStarting+���� *
FontSize+  $��1��$��17��$	 500��$ 0��$ Right=�$3 -Chip stack you get for a rebuy (if available)�� �� *TournamentData.StackSizeRebuy+���� *
FontSize+  $��1��$��18��$	 500��$ 0��$ Right=�$ Addon Stack Size�� �� *TournamentData.StackSizeAddon+���� *
FontSize+  -     Name$ SngText��$��1��$��19��$ 1��$ 0��$��0,5,0,0q�$��26��$ Right=�$D >Number of players who can enter the tournament (used for sngs)�� �� *TournamentData.Entrants+���� *
FontSize+y��� *SngCheckBox+#	 [    $��1��$��20��$ 1��$ 0��$��0,5,0,0q�$��26��$ Right=�$8 2Players who cash in the tournament (used for sngs)�� �� *TournamentData.EntrantsPaid+���� *
FontSize+y��� *SngCheckBox+#	 [    -     Name$ BovadaBounty��$��1��$��21��$ 1Q�$ 0~�$��0,5,0,0q�$��26��$ Right=�$1 +Bounty amount for Bovada bounty tournaments�� �� *TournamentData.Bounty+���� *
FontSize+y��� *BovadaCheckBox+#	 [  �� $��0��$��2��$��22��$	��5,5q�$
��Left=� �� Orientation$ Vertical=�.+��� $��Center=�$��Template is a Sit and Go��$- 'Tag this tournament as a sng tournament��$��0,0,0,5q� d� 	IsChecked �� *SngCheckBox+���� *
FontSize+�� $��Center=�$!��Template is a Bovada Bounty��$��
11,0,-13,5q�$} wThe bounty amount for Bovada/BetOnline tournaments where the bounty isn't counted in the final prize total in the lobby�� �� *BovadaCheckBox+ �� $ BovadaBounty������ *
FontSize+�� $��Center=�$��Update Saved Template��$��0,0,0,5q�$> 8Update this tournaments template file with these changes�� �� *UpdateTemplateCheckBox+���� *
FontSize+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +	  %Microsoft.Xaml.Behaviors.EventTrigger	   	  	EventName$ Checked��*
  $Microsoft.Xaml.Behaviors.TriggerBase 
  Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction      CommandParameter$  UpdateTemplate��!   Command! �� *CheckBoxToggleCommand+�� $��Center=�$��Save as a New Template��$��1,0,0,0q�$5 /Save this tournament template as a new template�� �� *SaveNewTemplateCheckBox+���� *
FontSize+ 	  $ Checked��.   $  SaveTemplate��! �� *CheckBoxToggleCommand+�� $ 
Horizontal=�$��23��$��0��$��2��$��Center=�$��0,5,0,5q�.+��� $��25��$��90��$	��5,5q�$ Save these changes��$
��Save������ *
FontSize+���� *SaveCommand+�� $��25��$��90��$	��5,5q�" �� IsCancel$
" True��$ Cancel these changes��$��Cancel������ *
FontSize+���� *CancelCommand+