   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5	       Tournament Life5   /Tournament_Life.Views.Menu_Views.AddProfileView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     5	      -   5        0  *Syncfusion.Windows.Shared.ChromelessWindow   Name$  Window��5       � 
ResizeMode$ NoResize=�5       � SizeToContent$ WidthAndHeight=�5       � Title$ New Profile��5      ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5
      �� *Theme+ +� FocusedElement 5      ��  �� ElementName$
 Text��.��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��5      $��0��5      $	��5,5q�5      $
��Left=�5       	 �� Orientation$	 
Horizontal=�5!      .+�5"      �� $
��Name��5$      ��5#      �� *
FontSize+5$      �� 5%      -   5        
 ��Name$

 Text��5&       � ToolTip$( "Enter the name of your new profile��5'      $	��125��5(      ��5)      �� *
FontSize+��5*      �� *Text+5+      �� 5.   
   $��1��5/      $��0��50      $	��5,5q�51      $
��Left=�52      $	 
Horizontal=�53      .+�54      �� $��25��55      $��80��56      $	��5,5q�57      $% Saves and loads the new profile��58      $��Create��5;      ��59      �� *
FontSize+��5:      �� *SaveCommand+5;      �� 5<      $��25��5=      $��80��5>      $	��5,5q�5?       �� IsCancel$
 True��5@      $ Close window��5A      $��Cancel��5D      ��5B      �� *
FontSize+��5C      �� *CancelCommand+5D      5E      5G      5H      