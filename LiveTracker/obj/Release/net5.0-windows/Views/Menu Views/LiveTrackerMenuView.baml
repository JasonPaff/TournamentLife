   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=null;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  6    0LiveTracker.Views.Menu_Views.LiveTrackerMenuView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k	 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89h
 dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf    	 
   =commands"clr-namespace:LiveTracker.Commands    	 
   .��� .+�'  !Syncfusion.Windows.Shared.MenuAdv  -      Name$  LiveTrackerMenu��   
ExpandMode$ ExpandOnClick=�.@�+  %Syncfusion.Windows.Shared.MenuItemAdv  $
��File������ *
FontSize+.@�  $��Profiles...�� � ToolTip$4 .Manager the Tournament Life user profiles here��   CommandParameter$ ProfilesMenuItem������ *
FontSize+   Command �� '   !LiveTracker.Commands.MenuCommands   MenuItem �� Source# Z   $��Database...��$9 3Manager the currently loaded profiles database here��$ DatabaseMenuItem������ *
FontSize+ �� # Z 1  +Syncfusion.Windows.Shared.MenuItemSeparator  $��65��$
��Left=�  $
��Exit��$} wExit the Tournament Life program (running tournaments data will be automatically saved upon exit and re-loaded at start��$ ExitMenuItem������ *
FontSize+ �� # Z   -     Name$ ViewMenu��$��Options������ *
FontSize+.@�  -   $ KeepWindowOnTopMenuItem��$,��&Keep the Tournament Life Window on Top��$> 8Keeps the Tournament Life window on top of other windows��   IsCheckable$
 True��$ KeepWindowOnTopMenuItem������ *
FontSize+	   	IsChecked	 �� *IsKeepWindowOnTopChecked+ �� # Z   -   $ GlobalPokerMenuItem��$$��Show the Currency Symbol as SC��$l fSwitch the currency symbol used by the tracker to SC instead of $ (Global Pokers sweeps coin currency)��$
 True��$ SweepsCoinMenuItem��	 �� *IsSweepsCoinChecked+���� *
FontSize+ �� # Z   -   $ TournamentsMenuItem��$��Tournaments������ *
FontSize+.@�  -   $ TemplateManagerMenuItem��$��Tournament Creator...��$( "Create and manage your tournaments��$ TemplateManagerMenuItem������ *
FontSize+ �� # Z   -   $ SelectTemplateMenuItem��$��Select Tournament(s)...��$1 +Selectively start tournament(s) from a list��$ SelectTemplateMenuItem������ *
FontSize+ �� # Z   -   $ QuickTemplateMenuItem��$��Quick Start Tournament...��$* $Quickly create and start tournaments��$ QuickTemplateMenuItem������ *
FontSize+ �� # Z   $	��140��$
��Left=�  -	   $ FavoriteTemplatesMenuItem��$��Favorite Tournaments��$; 5Start tournaments that have been marked as a favorite������ *
FontSize+���� *FavoriteTemplates+���� 
 �� 
TargetType"
  .���  �� Property � �� �� Value �� *
FontSize+��  � �� �� *	FontColor+��  � �� �� *TournamentName+��  �@ Command ��    FavoriteTemplateMenuItem# Z ��  �@ CommandParameter �� *TemplateIdString+��  �@�ToolTip �� * DescriptionWithoutDayMonthYear+��  � ��$ 
-10,0,10,0q���  �@�ContextMenu �� .@�m� 
����d� m� StaysOpenOnClick$
 True������ *TournamentName+���� *
FontSize+�� m� $��Remove from Favorites��$5 /Removes this tournament from the favorites list������ *
FontSize+ m� Command ��    UnpinFromFavorite# Z  m� CommandParameter �� *
TemplateId+�� m� $��Edit Tournament��$I COpens the Tournament Manager so this tournaments data can be edited������ *
FontSize+ ��    EditTemplateCommand# Z  �� *
TemplateId+m� $��Copy Tournament��$N HOpens the Tournament Manager with this tournaments data loaded as a copy������ *
FontSize+ ��    CopyTemplateCommand# Z  �� *
TemplateId+m� $��Delete Tournament��$; 5Deletes this tournament from the tournament save file������ *
FontSize+ ��    DeleteTemplateCommand# Z  �� *
TemplateId+  -
   $ SessionsMenuItem��$��Sessions������ *
FontSize+.@�  -   $ SessionManagerMenuItem��$��Session Creator...��$0 *Create and manage your tournament sessions��$ SessionManagerMenuItem������ *
FontSize+ �� # Z   -   $&  CreateSessionFromRunningMenuItem��$$��Create Session from Running...��$q kStarts the session manager with the currently running tournaments (that are saved in your tournaments file)��$&  CreateSessionFromRunningMenuItem������ *
FontSize+ �� # Z   $	��164��$
��Left=�  -   $ FavoriteSessionsMenuItem��$��Sessions��$/ )Select a session to start its tournaments������ *FavoriteSessions+���� "
  .���  � �� �� *SessionName+��  � �� �� *
FontSize+��  � �� �� *	FontColor+��  �@ Command ��    FavoriteSessionMenuItem# Z ��  �@ CommandParameter �� *SessionIdString+��  �@�ToolTip �� *Description+��  � ��$ 
-10,0,10,0q�  -   $ BankrollManagerMenuItem��$��Bankroll������ *
FontSize+.@�  -   $ OverallBankrollMenuItem������ *BankrollTotalHeader+���� *	Bankrolls+���� "
  .���  � �� �� *
FontSize+��  � �� �� *	FontColor+��  �@�ToolTip �� *	ToolTip+��  � �� �� *BankrollHeader+��  � ��$ 
-10,0,10,0q�  $	��100��$
��Left=�  -   $ DepositMenuItem��$��New Transaction...��$2 ,Log a deposit to one of your poker bankrolls��$ NewTransactionMenuItem������ *
FontSize+ �� # Z   -   $ ViewTransactionMenuItem��$��View Transactions...��$- 'View the transaction for your bankrolls��$ ViewTransactionsMenuItem������ *
FontSize+ �� # Z   $	��100��$
��Left=�  -   $ AddBankrollMenuItem��$��Create Bankroll...��$$ Add a new bankroll to tracking��$ AddBankrollMenuItem������ *
FontSize+ �� # Z   -   $ RemoveBankrollMenuItem��$��Delete Bankroll...��$% Remove a bankroll from tracking��$ RemoveBankrollMenuItem������ *
FontSize+ �� # Z   -   $ ResultsMenuItem��$��Results������ *
FontSize+.@�  -   $ QuickStatsMenuItem��$��Results Overview...��$D >Shows a small window with a overall stats view of your results��$ QuickResultsMenuItem������ *
FontSize+ �� # Z   -   $ SessionStatsMenuItem��$��Session Results...��$O IShows a window with all your results broken down into individual sessions��$ SessionResults������ *
FontSize+ �� # Z   -   $ TournamentStatsMenuItem��$��Tournament Results...��$5 /Shows a window with all your tournament results��$ TournamentResultsView������ *
FontSize+ �� # Z   $	��150��$
��Left=�  -   $" UndoRecentlyFinishedMenuItem��$��Undo Recently Finished��$f `Remove a recently finished tournament from the database and return it to the main tracker window������ *RecentlyFinishedTournaments+���� "
  .���  � ��$ 
-10,0,10,0q���  � �� �� *	FontColor+��  � �� �� *
FontSize+��  � �� �� *TournamentName+��  �@ Command �� -   &UndoRecentlyFinishedTournamentMenuItem# Z ��  �@ CommandParameter �� *
DatabaseId+��  �@�ToolTip �� *DatabaseDescription+  $
��Help������ *
FontSize+.@�  $��Check for Updates��$ Check for program updates��$ UpdateMenuItem������ *
FontSize+ �� # Z   $��Version��$ Get version number��$ VersionMenuItem������ *
FontSize+ �� # Z   $��Donate��$? 9Donation link for people who wish to support the software��$ DonateMenuItem������ *
FontSize+ �� # Z 