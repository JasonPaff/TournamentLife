   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.0.1.2, Culture=neutral, PublicKeyToken=null;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  6    0LiveTracker.Views.Menu_Views.LiveTrackerMenuView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a	 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf    	 
   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  1commands"clr-namespace:LiveTracker.Commands  .��� .+�' 
 !Syncfusion.Windows.Shared.MenuAdv  -      Name$  LiveTrackerMenu��   
ExpandMode$ ExpandOnClick=�.@�+ 
 %Syncfusion.Windows.Shared.MenuItemAdv  $
��File������ *
FontSize+.@�  $��Profiles...��   CommandParameter$ ProfilesMenuItem������ *
FontSize+   Command �� 2   ,LiveTracker.Commands.LiveTrackerMenuCommands   MenuItem �� Source# Z   $��Database...��$ DatabaseMenuItem������ *
FontSize+ �� # Z   $
��Exit��$ ExitMenuItem������ *
FontSize+ �� # Z   -     Name$ ViewMenu��$
��View������ *
FontSize+.@�  -   $ KeepWindowOnTopMenuItem��$%��Keep Live Tracker Window on Top��   IsCheckable$
 True��$ KeepWindowOnTopMenuItem��   	IsChecked �� *IsKeepWindowOnTopChecked+���� *
FontSize+ �� # Z   -   $ TournamentsMenuItem��$��Tournaments������ *
FontSize+.@�  -   $ TemplateManagerMenuItem��$��Tournament Manager...��	 � ToolTip$;	 5Create templates for the tournaments you plan to play��$ TemplateManagerMenuItem������ *
FontSize+ �� # Z   -   $ SelectTemplateMenuItem��$��Select Tournament...��$$	 Select template(s) from a list��$ SelectTemplateMenuItem������ *
FontSize+ �� # Z   -   $ QuickTemplateMenuItem��$��Quick Start Tournament...��$c	 ]Start a tournament without creating a template first (has an option to save it as a template)��$ QuickTemplateMenuItem������ *
FontSize+ �� # Z   -   $ FavoriteTemplatesMenuItem��$��Favorite Tournaments��$=	 7Quickly start tournaments you have marked as a favorite������ *
FontSize+���� *FavoriteTemplates+���� 
 �� 
TargetType"
  .���  �� Property � �� �� Value �� *
FontSize+��  � �� �� *	FontColor+��  � �� �� *TournamentName+��  �@ Command ��    FavoriteTemplateMenuItem# Z ��  �@ CommandParameter �� *TemplateIdString+��  �@�ToolTip �� *Description+��  � ��$ 
-10,0,10,0q�  -	   $ SessionsMenuItem��$��Sessions������ *
FontSize+.@�  -
   $ SessionManagerMenuItem��$��Session Manager...��$6	 0Create sessions out of your tournament templates��$ SessionManagerMenuItem������ *
FontSize+ �� # Z   -   $ FavoriteSessionsMenuItem��$��Sessions��$/	 )Select a session to start its tournaments������ *FavoriteSessions+���� "
  .���  � �� �� *SessionName+��  � �� �� *
FontSize+��  � �� �� *	FontColor+��  �@ Command ��    FavoriteSessionMenuItem# Z ��  �@ CommandParameter �� *SessionIdString+��  �@�ToolTip �� *Description+��  � ��$ 
-10,0,10,0q���  �@�ContextMenu �� .@�m�  m� StaysOpenOnClick$
 True������ *SessionName+���� *	FontColor+���� *
FontSize+�� m� $��Edit Session������ *	FontColor+���� *
FontSize+ m� Command �� *EditSessionCommand+ m� CommandParameter �� m� $��Copy Session������ *	FontColor+���� *
FontSize+ �� *CopySessionCommand+ �� �� m� $��Delete Session������ *	FontColor+���� *
FontSize+ �� *DeleteSessionCommand+ ��   -   $&  CreateSessionFromRunningMenuItem��$0��*Create session from running tournaments...��$y	 sStarts the session manager with the currently running tournaments (that have templates) already listed in a session��$&  CreateSessionFromRunningMenuItem������ *
FontSize+ �� # Z   -   $ BankrollManagerMenuItem��$��Bankroll������ *
FontSize+.@�  -   $ OverallBankrollMenuItem��$&	  Total off all bankrolls combined������ *BankrollTotalHeader+���� *	Bankrolls+���� "
  .���  � �� �� *
FontSize+��  � �� �� *	FontColor+��  � �� �� *BankrollHeader+��  � ��$ 
-10,0,10,0q�  -   $ DepositMenuItem��$��
Deposit...��$2	 ,Log a deposit to one of your poker bankrolls��$ BankrollDepositMenuItem������ *
FontSize+ �� # Z   -   $ WithdrawalMenuItem��$��Withdrawal...��$7	 1Log a withdrawal from one of your poker bankrolls��$  BankrollWithdrawalMenuItem������ *
FontSize+ �� # Z   -   $ ViewTransactionMenuItem��$��View Transactions...��$-	 'View the transaction for your bankrolls��$ ViewTransactionsMenuItem������ *
FontSize+ �� # Z   -   $ AddBankrollMenuItem��$��Add Bankroll...��$$	 Create a new bankroll to track��$ AddBankrollMenuItem������ *
FontSize+ �� # Z   -   $ RemoveBankrollMenuItem��$��Remove Bankroll...��$%	 Remove a bankroll from tracking��$ RemoveBankrollMenuItem������ *
FontSize+ �� # Z   -   $ ResultsMenuItem��$��Results������ *
FontSize+.@�  -   $ QuickStatsMenuItem��$��Results Overview...��$D	 >Shows a small window with a overall stats view of your results��$ QuickResultsMenuItem������ *
FontSize+ �� # Z   -   $ SessionStatsMenuItem��$��Session Results...��$O	 IShows a window with all your results broken down into individual sessions��$ SessionResults������ *
FontSize+ �� # Z   -   $ TournamentStatsMenuItem��$��Tournament Results...��$5	 /Shows a window with all your tournament results��$ TournamentResultsView������ *
FontSize+ �� # Z   -   $" UndoRecentlyFinishedMenuItem��$��Undo Recently Finished��$k	 eRemove a recently finished tournament from the database and return it to the running tournaments list������ *RecentlyFinishedTournaments+���� "
  .���  � ��$ 
-10,0,10,0q���  � �� �� *	FontColor+��  � �� �� *
FontSize+��  � �� �� *TournamentName+��  �@ Command �� -   &UndoRecentlyFinishedTournamentMenuItem# Z ��  �@ CommandParameter �� *
DatabaseId+��  �@�ToolTip �� *DatabaseDescription+  $
��Help������ *
FontSize+.@�  $��Register��$	 Register the program��$ RegisterMenuItem������ *
FontSize+ �� # Z   $��Usage Guide��$	 Launch usage guide��$ UsageGuideMenuItem������ *
FontSize+ �� # Z   $��Check for Updates��$	 Check for program updates��$ UpdateMenuItem������ *
FontSize+ �� # Z   $��Version��$	 Get version number��$ VersionMenuItem������ *
FontSize+ �� # Z 