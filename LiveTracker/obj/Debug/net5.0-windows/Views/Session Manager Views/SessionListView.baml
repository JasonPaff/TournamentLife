   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5       Tournament LifeA   ;Tournament_Life.Views.Session_Manager_Views.SessionListView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     5        � WindowStartupLocation$  CenterScreen=�5       � SizeToContent$ WidthAndHeight=�5       � 
ResizeMode$ NoResize=�5      ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5	      �� *Theme+ � Title 5      �� *Title+.��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��1��5      $��Center=�5       �� Orientation$ 
Horizontal=�5      .+�5   
   �� $��26��5"      $��80��5#      $	��5,5q�5$      $	��5,5q�5%      ��5       �� *
FontSize+��5!      �� *SaveCommand+��5&      �� *SaveButtonText+ � ToolTip 5'      �� *SaveButtonToolTipText+�� 5(   
   $��26��5+      $	��5,5q�5,      $	��5,5q�5-      $��Close Window��5.      $ Close window��5/      ��5)      �� *
FontSize+��5*      �� *CloseCommand+5/      50      �� 52   
   $��0��6   .+�53   
   �� $	��200��57      $	��500��58      $	��5,5q�59      $	��5,5q�5:      $��Center=�5;      $��1q�5<      	 �� SelectionMode$	 Single=�5=      ��54      �� *SessionList+
 �� UpdateSourceTrigger$
 PropertyChanged=� �� Mode$ TwoWay=� �� SelectedItem 55      �� *SelectedSession+$
 PropertyChanged=�$ TwoWay=���56      �� *
FontSize+��5>      �� 5?      .R�5@      �� ��6    �� *Name+5A      ��5C      �� 5D       �� 
TargetType" ��6   .�5E      ��  �� Property �@��
IsSelected �� Value 63   �� $ TwoWay=� �� Path$ 
IsSelected��� 5F       �@�ToolTip 60   �� *Description+5G      5I      5J      5L      5M      