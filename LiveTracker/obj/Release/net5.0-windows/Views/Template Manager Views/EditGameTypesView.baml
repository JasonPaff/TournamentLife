   M S B A M L   `   `   `  ���� h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager   Tournament Life@   :LiveTracker.Views.Template_Manager_Views.EditGameTypesView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  b	 ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf 	 
       msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF 	 
       -   0  *Syncfusion.Windows.Shared.ChromelessWindow   Name$  Window�� � 
ResizeMode$ NoResize=� � SizeToContent$ WidthAndHeight=� � Title$ Edit Game Types��,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$
 Text��.���  � ColumnDefinitions �� $
��Auto ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��1��$��0��$	��5,5q�	 �� Orientation$	 
Horizontal=�.+��� $��25��$��80��$	��5,5q�$	��5,5q�
 � ToolTip$)
 #Add the game type from the text box��$��Add Game Type������ *
FontSize+���� *
AddCommand+�� -    ��Name$
 Text��$��25��$	��157��$)
 #Enter new tournament game type here������ *
FontSize+���� *	AddText+�� $��2��$��0��$	��5,5q�$	 
Horizontal=�.+��� $��25��$	��120��$	��5,5q�$	��5,5q�$1
 +Delete the selected tournament game type(s)��$��Delete Game Type������ *
FontSize+���� *RemoveCommand+�� $��25��$	��105��$	��5,5q� �� IsCancel$
 True��$"
 Close edit game types window��$��Close Window������ *
FontSize+���� *CancelCommand+�� $��3��$��0��$	��5,5q�$	 
Horizontal=�.+��� $a
 [Set the default game type for the tournament manager and quick start tournament menu option��$��Default Game Type������ *
FontSize+�� $��Center=�$"
 Default tournament game type��$	��150������ *Items+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=� �� SelectedItem �� *SelectedItem+$ PropertyChanged=�$ TwoWay=����� *
FontSize+4  .Syncfusion.Windows.Tools.Controls.CheckListBox  $��4��$��0�� �� DisplayMemberPath$ DisplayString��$��Center=�$	��100��$	��100��   IsSelectAllEnabled$ False��$	��400��$��0,10,0,0q�$
 List of game types������ *
ItemList+$ PropertyChanged=�$ TwoWay=����� *
FontSize+����  �� 
TargetType8  2Syncfusion.Windows.Tools.Controls.CheckListBoxItem"  .���  �� Property �@ 	IsChecked �� Value �� $ TwoWay=� �� Path$ 
IsSelected����� .R��� 