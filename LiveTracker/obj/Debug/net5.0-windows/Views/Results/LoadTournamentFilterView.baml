   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5       Tournament Life<   6Tournament_Life.Views.Results.LoadTournamentFilterView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     5        � 
ResizeMode$  NoResize=�5       � SizeToContent$ WidthAndHeight=�5      $	��100��5      $	��100��5      ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5	      �� *Theme+ � Title 5      �� *Title+.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   .+�5   	   �� $��0��6   .+�5   
   �� $	��5,5q�5      $	��5,5q�5       �� SelectionMode$ Single=�5       ��5      �� *
FilterList+��5      �� *
FontSize+��5!      �� 5"      .R�5#      �� ��6    �� *Name+5$      ��5&      �� 5'       �� 
TargetType" ��6   .�5(      ��  �� Property �@��
IsSelected	 �� Value	 63   �� 
 �� Mode$
 TwoWay=� �� Path$ 
IsSelected��� 5)       �@�ToolTip	 60   �� *Description+5*      5,      5-      �� 5/   
   $��1��50      $��Center=�51       �� Orientation$ 
Horizontal=�52      .+�53      �� $��25��54      $��82��55       � ToolTip$ Load the selected filter��56      $	��5,5q�58      $
��Load��5:      ��57      �� *
FontSize+��59      �� *LoadCommand+5:      �� 5;      $��25��5<      $��82��5=      $  Rename the selected filter��5>      $	��5,5q�5@      $��Rename��5B      ��5?      �� *
FontSize+��5A      �� *RenameCommand+5B      �� 5C      $��25��5D      $��82��5E      $ Close the window��5F      $	��5,5q�5H       �� IsCancel$
 True��5I      $��Close��5K      ��5G      �� *
FontSize+��5J      �� *CancelCommand+5K      5L      5M      5N      