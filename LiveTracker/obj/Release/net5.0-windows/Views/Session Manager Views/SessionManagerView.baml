   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=null;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager @    :LiveTracker.Views.Session_Manager_Views.SessionManagerView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
    =commands"clr-namespace:LiveTracker.Commands   	 
    c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF  0  *Syncfusion.Windows.Shared.ChromelessWindow    UseNativeChrome$
  True�� � SizeToContent$ WidthAndHeight=�   ShowMaximizeButton$ False�� � 
ResizeMode$ CanMinimize=�$	��550��$	��150��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$ SessionName�� � Title �� *Title+.���  � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �	 � RowDefinitions	 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��1��$��0,45,0,0q�$��1��.+��� $
��Save��$��26��$	��5,5q�$	��5,5q�$��Save Session��
 � ToolTip$
 Save the session������ *
FontSize+���� 1   +LiveTracker.Commands.SessionManagerCommands   MenuItemCommand �� Source# Z �� $��1q��� $	��Add��$��26��$	��5,5q�$	��5,5q�$��Add Tournament(s)��$4
 .Add the selected tournaments(s) to the session������ *
FontSize+���� # Z �� $��Remove��$��26��$	��5,5q�$	��5,5q�$��Remove Tournament(s)��$9
 3Remove the selected tournaments(s) from the session������ *
FontSize+���� # Z �� $��1q��� $	��New��$��26��$	��5,5q�$	��5,5q�$��New Session��$
 Start a new session������ *
FontSize+���� # Z  �� $ SessionName���� $
��Edit��$��26��$	��5,5q�$	��5,5q�$��Edit Session��$
 Edit an existing session������ *
FontSize+���� # Z �� $
��Copy��$��26��$	��5,5q�$	��5,5q�$��Copy Session��$(
 "Make a copy of an existing session������ *
FontSize+���� # Z �� $��Delete��$��26��$	��5,5q�$	��5,5q�$��Delete Session��$ 
 Delete an existing session������ *
FontSize+���� # Z �� $��1q��� $��Close��$��26��$	��5,5q�$	��5,5q�$��Close Window��$
 Closes the window������ *
FontSize+���� # Z �� $��1��$��2��$��0��.+���  �� Orientation$ 
Horizontal=�$��Center=�.+��� $��Search��$��0,5,0,0q����� *
FontSize+�� -    ��Name$
 Text��$	��230��$��0,6,0,0q�$��Center=�$9
 3Filters the tournaments based on a text search term������ *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=����� *
FontSize+.��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ TextChanged��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *TextChangedCommand+.���� $ 
Horizontal=�$��Center=�.+��� $��1��$��0��$��0,10,0,0q�$��Venue������ *
FontSize+�� -    ��Name$ Combo��$��1��$��1��$��0,0,0,0q�$��3,10,0,0q�$D
 >Filters the tournaments based on the venue they were played at��$��35��$	��230������ *Venues+ �� SelectedItem �� *SelectedVenue+$ PropertyChanged=�$ TwoWay=����� *
FontSize+   $ SelectionChanged��.    �� *TextChangedCommand+�� $	��300��$	��500��$	��500��$	��5,5q�$	��5,5q�$��1q� �� SelectionMode$ Multiple=����� *TemplateList+ �� NotifyOnSourceUpdated$
 True��$ TwoWay=����� *
FontSize+���� .R��� ���� *DisplayString+����  �� 
TargetType" ��.���  �� Property �@��
IsSelected �� Value �� $ TwoWay=� �� Path$ 
IsSelected���  �@�ToolTip �� *Description+�� $��1��$��2��$��2��$��	0,-11,0,0q�.+��� $ 
Horizontal=�$��0,55,0,0q�$��Center=�.+��� $��Session Name��$��0,-3,0,0q����� *
FontSize+�� -   $ SessionName��$	��200��$��0,-1,0,0q� �� 	MaxLength$ 46��$��Center=�$;
 5The name of the session (max length is 46 characters)������ *SessionName+$ PropertyChanged=����� *
FontSize+�� $	��300��$	��500��$	��500��$1
 +List of tournaments included in the session��$	��5,5q�$	��5,5q�$��1q�$ Multiple=����� *SessionList+$
 True��$ TwoWay=����� *
FontSize+���� .R��� ���� *DisplayString+���� " ��.���  �@��
IsSelected �� $ TwoWay=�$ 
IsSelected���  �@�ToolTip �� *Description+