   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5	       Tournament LifeB   <Tournament_Life.Views.Template_Manager_Views.EditFormatsView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     5	      -   5        0  *Syncfusion.Windows.Shared.ChromelessWindow   Name$  Window��5       � 
ResizeMode$ NoResize=�5       � SizeToContent$ WidthAndHeight=�5       � Title$ Edit Formats��5      ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5
      �� *Theme+ +� FocusedElement 5      ��  �� ElementName$
 Text��.��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5!   
   �� $��1��5"      $��0��5#      $	��5,5q�5$      	 �� Orientation$	 
Horizontal=�5%      .+�5&      �� $��25��5'      $��80��5(      $	��5,5q�5)      
 � ToolTip$@
 :Add the format from the text box to the saved formats list��5*      $��
Add Format��5-      ��5+      �� *
FontSize+��5,      �� *
AddCommand+5-      �� 5.      -   5         ��Name$
 Text��5/      $��25��51      $	��157��52      $
 Enter the new format here��53      ��50      �� *
FontSize+��54      �� *	AddText+55      �� 58   
   $��2��59      $��0��5:      $	��5,5q�5;      $	 
Horizontal=�5<      .+�5=      �� $��25��5>      $	��120��5?      $	��5,5q�5@      $#
 Delete the selected format(s)��5A      $��Delete Format��5D      ��5B      �� *
FontSize+��5C      �� *RemoveCommand+5D      �� 5E      $��25��5F      $	��105��5G      $	��5,5q�5H       �� IsCancel$
 True��5I      $
 Close edit formats window��5J      $��Close Window��5M      ��5K      �� *
FontSize+��5L      �� *CancelCommand+5M      5N      �� 5Q   
   $��3��5R      $��0��5S      $	��5,5q�5T      $��Center=�5U      $	 
Horizontal=�5V      .+�5W      �� $'��!Set Selected as Default Format(s)��5X      $��Center=�5Z      ${
 uSet the selected format(s) as the default format(s) for the tournament manager and quick start tournament menu option��5[      $��25��5\      $
��10,5q�5]      $	��150��5^      ��5Y      �� *
FontSize+��5_      �� *SetDefaultsCommand+5`      5a      4  .Syncfusion.Windows.Tools.Controls.CheckListBox  5d   
   $��4��5e      $��0��5f       �� DisplayMemberPath$ DisplayString��5i      $��Center=�5j      $	��100��5k      $	��100��5l         IsSelectAllEnabled$ False��5m      $	��400��5n      $��0,10,0,0q�5o      $ 
 List of tournament formats��5p      ��5g      �� *
ItemList+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=���5h      �� *
FontSize+��5q      �� 5r       �� 
TargetType8  2Syncfusion.Windows.Tools.Controls.CheckListBoxItem"  6   .�5s      ��  �� Property �@ 	IsChecked �� Value 5u      �� $ TwoWay=� �� Path$ 
IsSelected�5v      ��5x      �� 5y      .R�5z      �� 5{      5}      5      5�      