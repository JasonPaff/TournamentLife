   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5   	    Tournament Life:   4Tournament_Life.Views.Bankroll_Views.RemoveVenueView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5   	   msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     5   	     � Title$  Remove Venue(s)��5
   	    � SizeToContent$ WidthAndHeight=�5   	    � 
ResizeMode$ NoResize=�5   	   $	��450��5   	   $	��800��5   	   ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5	   	   �� *Theme+.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ 	VenueList��5      $��0��5      $��0��5      $	��5,5q�5        � ToolTip$ List of venues��5!       �� SelectionMode$ Multiple=�5#      ��5"      �� *
FontSize+	 �� SelectedItem	 5$      �� 
 �� Path$
 SelectedVenues� �� UpdateSourceTrigger$ PropertyChanged=���5%      �� *Venues+��5&      �� 5'      .R�5(      �� ��6    �� *Name+5)      ��5+      �� 5,       �� 
TargetType" ��6   .�5-      ��  �� Property �@��
IsSelected �� Value 5/      ��  �� Mode$ TwoWay=�$
 
IsSelected�50      52      �� 54   
   $��1��55      $��0��56      $��Center=�57       �� Orientation$ 
Horizontal=�58      .+�59      �� $��30��5:      $��90��5;      $ Remove selected venue(s)��5<      $	��5,5q�5>      $	��5,5q�5?      $��Remove Venue(s)��5A      ��5=      �� *
FontSize+��5@      �� *SaveCommand+5A      �� 5B      $��30��5C      $ Close the window��5D      $��90��5E      $	��5,5q�5G       �� IsCancel$
 True��5H      $��Close Window��5J      ��5F      �� *
FontSize+��5I      �� *CancelCommand+5J      5K      5M      5N      