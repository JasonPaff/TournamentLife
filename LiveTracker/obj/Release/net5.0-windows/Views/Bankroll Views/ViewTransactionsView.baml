   M S B A M L   `   `   `  ���� h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager   Tournament Life;   5LiveTracker.Views.Bankroll_Views.ViewTransactionsView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    � Title$  Transactions�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ CanMinimize=�0  *Syncfusion.Windows.Shared.ChromelessWindow   UseNativeChrome$
 True��   ShowMaximizeButton$ False��$	��750��$	��800��,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+��� $��0�� �� Orientation$ 
Horizontal=�.+��� $��Bankroll��$	��5,5q�	 � ToolTip$	 Venue bankroll���� $	��5,5q�$	��5,5q�$	��145��$	 Venue bankrolls������ *
FontSize+���� *	Bankrolls+
 �� UpdateSourceTrigger$
 PropertyChanged=� �� SelectedItem �� *SelectedBankroll+$
 PropertyChanged=�*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ SelectionChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *BankrollChangedCommand+�� -    ��Name$ 	VenueList��$��1��$	��600��$��0��$	��5,5q�$!	 Venue bankroll transactions�� �� SelectionMode$ Single=����� *
FontSize+ ��  �� Path$ SelectedTransactions�$
 PropertyChanged=����� *Transactions+$
 PropertyChanged=����� .R��� ���� *Header+����  �� 
TargetType" ��.���  �� Property �@��
IsSelected �� Value ��  �� Mode$ TwoWay=�$ 
IsSelected��� $��2��$��0��$��Center=�$ 
Horizontal=�.+��� $��24��$��60��$	��5,5q�$��Delete��$%	 Delete the selected transaction������ *
FontSize+���� *SaveCommand+�� $��24��$��60��$	��5,5q�$
��Edit��$+	 %Edit the selected transaction details������ *
FontSize+���� *EditCommand+�� $��24��$��60��$	��5,5q� �� IsCancel$
 True��$	 Close the window��$��Close������ *
FontSize+���� *CancelCommand+