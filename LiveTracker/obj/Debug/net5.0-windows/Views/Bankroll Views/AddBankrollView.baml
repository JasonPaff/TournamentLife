   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5       Tournament Life:   4Tournament_Life.Views.Bankroll_Views.AddBankrollView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     5        � Title$  Create Bankroll��5
       � SizeToContent$ WidthAndHeight=�5       � 
ResizeMode$ NoResize=�5      $	��450��5      $	��800��5      ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5	      �� *Theme+ +� FocusedElement 5      ��  �� ElementName$
 Text��.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��5 �6   .+�5      �� $��0��5      $��0��5       $��0,0,5,0q�5!      $��Right=�5"       � ToolTip$! Venue name for new bankroll��5$      $��Venue��5%      ��5#      �� *
FontSize+5%      �� 5&      $��0��5'      $��1��5(      $ 
Venue name��5*      ��5)      �� *
FontSize+��5+      �� *Venues+	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� SelectedItem
 5,      �� *Venue+$	 PropertyChanged=��� 5.      $��1��5/      $��Right=�50      $��0��51      $��0,0,5,0q�52      $, &Initial bankroll amount for this venue��54      $��Initial Amount��55      ��53      �� *
FontSize+55      -  'Syncfusion.Windows.Shared.DoubleTextBox  56      -   5          Name$
 Text��57      $��1��58      $��1��59      $, &Initial bankroll amount for this venue��5:      $	��100��5=         MinValue$ 0Q�5>      ��5;      �� *
FontSize+��5<      �� *Amount+5>      �� 5@      $��2��5A      $��0��5B      $��2��5C       �� Orientation$ 
Horizontal=�5D      .+�5E      �� $��	Add Venue��5F      $��Center=�5G      $��Center=�5H      $	��100��5I      $��25��5J      $( "Add a new venue to the venues list��5K      $	��5,5q�5M      ��5L      �� *
FontSize+��5N      �� *AddNewVenueCommand+�� 5P      $��Remove Venue��5Q      $) #Remove a venue from the venues list��5R      $��Center=�5S      $��Center=�5T      $	��110��5U      $��25��5V      $	��5,5q�5X      ��5W      �� *
FontSize+��5Y      �� *RemoveVenueCommand+5Z      �� 5\      $��3��5]      $��0��5^      $��2��5_      $ 
Horizontal=�5`      .+�5a      �� $��25��5b      $	��100��5c      $@ :Create a new bankroll of stated size at the selected venue��5d      $	��5,5q�5f      $��Create Bankroll��5h      ��5e      �� *
FontSize+��5g      �� *SaveCommand+5h      �� 5i      $��25��5j      $	��110��5k      $. (Cancel the bankroll and close the window��5l      $	��5,5q�5n       �� IsCancel$
 True��5o      $��Close Window��5q      ��5m      �� *
FontSize+��5p      �� *CancelCommand+5q      5r      5t      5u      