   M S B A M L   `   `   `  ���� h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  J FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=null?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters 1   +LiveTracker.Views.Menu_Views.QuickStartView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     A
converters$clr-namespace:LiveTracker.Converters   	 
       � 
ResizeMode$  CanMinimize=� � SizeToContent$ WidthAndHeight=�0  *Syncfusion.Windows.Shared.ChromelessWindow   UseNativeChrome$
 True��   ShowMaximizeButton$ False��$	��550��$	��250��,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$ NameTextBox�� � Title �� *Title+ � 	Resources 5  /LiveTracker.Converters.SngToVisibilityConverter   SngToVisibleConverter3  -LiveTracker.Converters.BoolToVisibleConverter  WindowVisibleConverter	   	TrueValue
   
FalseValue��%<   &	        &	          $	 Visible=�$
 	Collapsed=�.���  � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $��5 � � RowDefinitions �� $
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
��Auto ��� $
��Auto ��� $
��Auto ��� $��2 �.+�'  !Syncfusion.Windows.Shared.MenuAdv  -     Name$ LiveTrackerMenu��   
ExpandMode$ ExpandOnClick=�.@�+  %Syncfusion.Windows.Shared.MenuItemAdv  $��0�� � ToolTip$9 3Various options to customize the Quick Start window��$��Options������ *
FontSize+.@�  $_ YToggles whether the Quick Start window should close or not after you start the tournament��   IsCheckable$
 True��   CommandParameter$ CloseAfterStartOption������ *%#CloseAfterStartMenuOptionHeaderText+���� *
FontSize+   	IsChecked �� *$"CloseAfterStartMenuOptionIsChecked+ �� UpdateSourceTrigger$ PropertyChanged=�   Command �� *OptionsCommand+  $f `Toggles whether the Save to Tournaments File check box is selected by default at start up or not��$
 True��$ SaveNewTournamentOption������ *'%SaveNewTournamentMenuOptionHeaderText+���� *
FontSize+ �� *&$SaveNewTournamentMenuOptionIsChecked+$ PropertyChanged=� �� *OptionsCommand+  $^ XToggles whether you will be shown a confirmation message before tournaments start or not��$
 True��$ ShowStartTournamentOption������ *)'ShowStartTournamentMenuOptionHeaderText+���� *
FontSize+ �� *(&ShowStartTournamentMenuOptionIsChecked+$ PropertyChanged=� �� *OptionsCommand+1  +Syncfusion.Windows.Shared.MenuItemSeparator  $	��300��$
��Left=�  $��Close Window��$0 *Cancel the tournament and close the window������ *
FontSize+ �� *CloseCommand+�� $
��Name��$��1��$��0��$��0,2,0,0q�$��Right=����� *
FontSize+�� -    ��Name$ NameTextBox��$��1��$��1�� �� TextAlignment$
 Left=�$��Center=�$��26��$	��160��$��0,2,0,0q�$? 9The name for the tournament

-Tournament name is required������ *TournamentData.TournamentName+���� *
FontSize+�� $��Venue��$��2��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+�� $��2��$��1��$
 Left=�$��Center=�$��26��$��0,5,0,0q�$4 .The venue where the tournament is being played������ *TournamentData.Venue+���� *
FontSize+�� $��	Game Type��$��3��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+�� $��3��$��1��$
 Left=�$��Center=�$��26��$��0,6,0,0q�$] WThe game type for the tournament

-PLO, Pot Limit Omaha, NLHE, No-Limit Hold Em, etc...������ *TournamentData.GameType+���� *
FontSize+�� $��Formats��$��4��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+�� $��4��$��1��$
 Left=�$��Center=�$��26��$��0,5,0,0q�$p jThe format(s) for the tournament

-Freezeout, Rebuy, Satellite, Sng, ect...
-Separate formats with a comma������ *	Formats+���� *
FontSize+�� $��
Start Date��$��5��$��0��$��0,6,0,0q�$��Right=����� *
FontSize+4 	 .Syncfusion.Windows.Controls.Input.SfDatePicker  $��5��$��1��$��0,5,0,0q�$��26��$* $The starting date for the tournament��   Value �� *	StartDate+$ PropertyChanged=� �� Mode$ TwoWay=����� *
FontSize+�� $��
Start Time��$��6��$��0��$��0,6,0,0q�$��Right=����� *
FontSize+4	 	 .Syncfusion.Windows.Controls.Input.SfTimePicker	  $��6��$��1��$��0,5,0,0q� ^� ShowDuration$ 100000��$��26��$� �The starting time of the tournament

-Any tournament tagged as a Sit and Go
 will have its starting time updated to the
 current time when it is started by the tracker�� 	  Value �� *	StartTime+$ PropertyChanged=�$ TwoWay=����� *
FontSize+�� $��Buyin��$��7��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+/
  )Syncfusion.Windows.Shared.CurrencyTextBox
  $��7��$��1��$��0,5,0,0q�$��26�� 
  ScrollInterval$	 1.0Q� 
  MinValue$ 0~�*  $Syncfusion.Windows.Shared.EditorBase   Culture$ en-US��$ Right=�$T NThe base cost of the buy-in (the cost of the buy-in without the rake included)�� 
  Value �� *TournamentData.BuyinBaseCost+���� *
FontSize+�� $��
Buyin Rake��$��8��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+
  $��8��$��1��$	 1.0Q�$ 0~�$ en-US��$��0,5,0,0q�$��26��$ Right=�$e _The rake cost of the buy-in (the cost of the rake without the base cost of the buy-in included)�� �� *TournamentData.BuyinRakeCost+���� *
FontSize+�� $��Rebuy��$��9��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+
  $��9��$��1��$	 1.0Q�$ 0~�$ en-US��$��0,5,0,0q�$��26��$ Right=�$P JThe base cost of a rebuy (the cost of the rebuy without the rake included)�� �� *TournamentData.RebuyBaseCost+���� *
FontSize+�� $��
Rebuy Rake��$��10��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+
  $��10��$��1��$	 1.0Q�$ 0~�$ en-US��$��0,5,0,0q�$��26��$ Right=�$a [The rake cost of a rebuy (the cost of the rake without the base cost of the rebuy included)�� �� *TournamentData.RebuyRakeCost+���� *
FontSize+�� $��Addon��$��11��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+
  $��11��$��1��$	 1.0Q�$ 0~�$ en-US��$��0,5,0,0q�$��26��$ Right=�$S MThe base cost of an add-on (the cost of the add-on without the rake included)�� �� *TournamentData.AddonBaseCost+���� *
FontSize+�� $��
Addon Rake��$��12��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+
  $��12��$��1��$	 1.0Q�$ 0~�$ en-US��$��0,5,0,0q�$��26��$ Right=�$b \The rake cost of an addon (the cost of the rake without the base cost of the addon included)�� �� *TournamentData.AddonRakeCost+���� *
FontSize+�� $��	Guarantee��$��13��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+
  $��13��$��1��$ 500.0Q�$ 0~�$ en-US��$��0,5,0,0q�$��26��$ Right=�$+ %The guaranteed size of the prize pool�� �� *TournamentData.Guarantee+���� *
FontSize+�� $��Late Reg��$��14��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+.  (Syncfusion.Windows.Shared.IntegerTextBox  $��14��$��1��   ScrollInterval$ 5��    MinValue$  0��$��0,5,0,0q�$��26��$ Right=�$B <The late registration length (in minutes) for the tournament��!   Value! �� *TournamentData.LateReg+���� *
FontSize+�� $��Blind Levels��$��15��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+  $��15��$��1��$ 5��$  0��$��0,5,0,0q�$��26��$ Right=�$< 6The blind level length (in minutes) for the tournament��! �� *TournamentData.BlindLevels+���� *
FontSize+�� $��
Table Size��$��16��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+  $��16��$��1��$ 1��$  2��"   MaxValue$" 10��$��0,5,0,0q�$��26��$ Right=�$, &The number of players per table (2-10)��! �� *TournamentData.TableSize+���� *
FontSize+�� $��Starting Stack��$��17��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+  $��17��$��1��$	 500��$  0��$��0,5,0,0q�$ Right=�$��26��$3 -The stack size at the start of the tournament��! �� *" TournamentData.StackSizeStarting+���� *
FontSize+�� $��Rebuy Stack��$��18��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+  $��18��$��1��$	 500��$  0��$��0,5,0,0q�$��26��$ Right=�$; 5The stack size received for a rebuy in the tournament��! �� *TournamentData.StackSizeRebuy+���� *
FontSize+�� $��Addon Stack��$��19��$��0��$��0,4,0,0q�$��Right=����� *
FontSize+  $��19��$��1��$	 500��$  0��$��0,5,0,0q�$��26��$ Right=�$= 7The stack size received for an add-on in the tournament��! �� *TournamentData.StackSizeAddon+���� *
FontSize+�� $��Entrants��$��20��$��0��$��0,4,0,0q�$��Right=�y��� *SngCheckBox+# �� 	Converter## [  ���� *
FontSize+  -   $  Name$$ SngText��$��20��$��1��$ 1��$  0��$��0,5,0,0q�$��26��$ Right=�$? 9The total number of entrants in the sit and go tournament��! �� *TournamentData.Entrants+���� *
FontSize+y��� *SngCheckBox+## [  �� $��Entrants Paid��$��21��$��0��$��0,4,0,0q�$��Right=�y��� *SngCheckBox+## [  ���� *
FontSize+  $��21��$��1��$ 1��$  0��$��0,5,0,0q�$��26��$ Right=�$M GThe total number of entrants who will cash in the sit and go tournament��! �� *TournamentData.EntrantsPaid+���� *
FontSize+y��� *SngCheckBox+## [  �� $��Sng Payouts��$��22��$��0��$��0,4,0,0q�$��Right=�y��� *SngCheckBox+## [  ���� *
FontSize+�� $��22��$��1��$��Center=�$
 Left=�$��0,5,0,0q�$��26��$	��160��$� �The payouts for the sit and go, start with 1st place prize and separate each value with a comma
For example if the sng pays out top 3 places then you would enter the sng payouts as 45,27,15������ *TournamentData.SngPayouts+���� *
FontSize+y��� *SngCheckBox+## [  �� $��Fixed Bounty��$��23��$��0��$��0,4,0,0q�$��Right=�$ 25000��y��� *BovadaCheckBox+## [  ���� *
FontSize+
  -   % 
 Name$% BovadaBounty��$��23��$��1��$ 50000��$ 1Q�$ 0~�$��0,5,0,0q�$��26��$ Right=�$S MThe fixed bounty amount you win when you knock a player out of the tournament�� �� *TournamentData.Bounty+���� *
FontSize+y��� *BovadaCheckBox+## [  �� $��24��$��0��$��2��& �� Orientation$& Vertical=�.+��� $��Center=�$ 100000��$��Sit and Go Tournament��$� �-This will tag the tournament as a Sit and Go tournament
  (a tournament that starts after a set number of people
   have registered instead of at a set time)

-When started by the tracker any tournament flagged as
 a Sit and Go will have its starting time set to the current
 time instead of the starting time selected here

-When started by the tracker any tournament flagged as a
 Sit and Go will have its Entrants and Entrants Paid updated��$��
-14,10,0,0q� �� $ SngText��' d� 	IsChecked' �� *SngCheckBox+���� *
FontSize+�� $��Center=�$��Fixed Bounty Tournament��$ 100000��$� �-This will tag the tournament as a fixed bounty tournament
 (the amount you win from knocking someone out is fixed in size)

-This tag is mainly useful for when you are playing at a venue
 (Bovada,Ignition,ect..) that doesn't track the fixed limit bounties
 won for you in the lobby. Tag it with the fixed bounty tag and use
 the bounty column in the main tracker to track the bounties won

-Use the bounty column on the main tracker to track fixed limit bounties won.
 To turn on the bounty column right click any columns header on the
 main tracker and select the bounty column from the menu��$��1,5,3,0q�' �� *BovadaCheckBox+ �� $ BovadaBounty������ *
FontSize+�� $��Center=�$��Save to Tournaments File��$- 'Save this tournament to re-use it later��$��-3,5,2,0q�' �� *SaveNewTemplateCheckBox+���� *
FontSize+�� $��25��$��0��$��2��$& 
Horizontal=�$��Center=�$	��5,5q�.+��� $��25��$��90��$��5,5,5,0q�$	��5,5q�( �� 	IsDefault$
( True������ *
FontSize+ �� *StartButtonToolTipText+���� *StartCommand+���� *StartButtonText+�� $��25��$��90��$��5,5,5,0q�) �� IsCancel$
) True��$1 +Cancel this tournament and close the window��$��Cancel������ *
FontSize+���� *CloseCommand+