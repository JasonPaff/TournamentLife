   M S B A M L   `   `   `  ���� h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager   Tournament LifeB   <Tournament_Life.Views.Session_Manager_Views.SessionStartView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
       � 
ResizeMode$  NoResize=� � SizeToContent$ WidthAndHeight=�,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � Title �� *Title+.���  � ColumnDefinitions �� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�'  !Syncfusion.Windows.Shared.MenuAdv  $��0��   
ExpandMode$ ExpandOnClick=�.@�+  %Syncfusion.Windows.Shared.MenuItemAdv  $��Options������ *
FontSize+.@�  $+��%Select multiple tournaments at a time�� � ToolTip$u oWhen checked, allows you to select multiple tournaments at a time in the tournament list instead of just single��   IsCheckable$
 True��	   	IsChecked	 �� *MultiSelectionToggle+���� *
FontSize+
   Command
 �� *SelectToggleCommand+  $=��7Show a confirmation message before removing tournaments��$b \When checked, displays a confirmation message prior to removing tournaments from the session��$
 True��	 �� *ConfirmRemoveToggle+���� *
FontSize+
 �� *RemoveToggleCommand+  $=��7Show a confirmation message before starting the session��$Q KWhen checked, displays a confirmation message prior to starting the session��$
 True��	 �� *ConfirmStartToggle+���� *
FontSize+
 �� *ConfirmStartCommand+  $D��>Sort the tournaments alphabetically instead of chronologically��$L FWhen checked sorts the tournaments by name instead of by starting time��$
 True��	 �� *ConfirmAlphaToggle+���� *
FontSize+
 �� *ConfirmAlphaCommand+�� $��1��.+��� -    ��Name$ TournamentList��$	��250��$	��500��$	��5,5q�$	��5,5q�$��1q����� *TournamentList+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=����� *
FontSize+ �� SelectionMode �� *SelectionMode+���� .R��� ���� *Name+����  �� 
TargetType" ��.���  �� Property �@��
IsSelected �� Value �� $ TwoWay=� �� Path$ 
IsSelected���  �@�ToolTip �� *Description+�� $��2��$��Center=� �� Orientation$ 
Horizontal=�.+��� $��26��$��55��$	��5,5q�$	��5,5q�$��Start��$* $Starts the tournament(s) in the list������ *
FontSize+���� *SaveCommand+�� $��26��$��55��$	��5,5q�$	��5,5q�$/ )Cancels the session and closes the window��$��Cancel������ *
FontSize+���� *CloseCommand+�� $��26��$��55��$	��5,5q�$	��5,5q�$��Remove Selected��$8 2Remove the selected tournament(s) from the session������ *
FontSize+���� *RemoveCommand+