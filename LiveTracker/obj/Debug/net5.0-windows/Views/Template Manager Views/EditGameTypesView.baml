   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5       Tournament LifeD   >Tournament_Life.Views.Template_Manager_Views.EditGameTypesView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      b	 ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf 	 
       5
      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF 	 
       5      -   5        0  *Syncfusion.Windows.Shared.ChromelessWindow   Name$  Window��5       � 
ResizeMode$ NoResize=�5       � SizeToContent$ WidthAndHeight=�5       � Title$ Edit Game Types��5      ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5      �� *Theme+ +� FocusedElement 5      ��  �� ElementName$
 Text��.��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5#   
   �� $��1��5$      $��0��5%      $	��5,5q�5&      	 �� Orientation$	 
Horizontal=�5'      .+�5(      �� $��25��5)      $��80��5*      $	��5,5q�5+      $	��5,5q�5,      
 � ToolTip$)
 #Add the game type from the text box��5-      $��Add Game Type��50      ��5.      �� *
FontSize+��5/      �� *
AddCommand+50      �� 51      -   5         ��Name$
 Text��52      $��25��54      $	��157��55      $)
 #Enter new tournament game type here��56      ��53      �� *
FontSize+��57      �� *	AddText+58      �� 5;   
   $��2��5<      $��0��5=      $	��5,5q�5>      $	 
Horizontal=�5?      .+�5@      �� $��25��5A      $	��120��5B      $	��5,5q�5C      $	��5,5q�5D      $1
 +Delete the selected tournament game type(s)��5E      $��Delete Game Type��5H      ��5F      �� *
FontSize+��5G      �� *RemoveCommand+5H      �� 5I      $��25��5J      $	��105��5K      $	��5,5q�5L       �� IsCancel$
 True��5M      $"
 Close edit game types window��5N      $��Close Window��5Q      ��5O      �� *
FontSize+��5P      �� *CancelCommand+5Q      5R      �� 5U   
   $��3��5V      $��0��5W      $	��5,5q�5X      $	 
Horizontal=�5Y      .+�5Z      �� $a
 [Set the default game type for the tournament manager and quick start tournament menu option��5\      $��Default Game Type��5]      ��5[      �� *
FontSize+5]      �� 5^      $��Center=�5b      $"
 Default tournament game type��5c      $	��150��5d      ��5_      �� *Items+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=� �� SelectedItem 5`      �� *SelectedItem+$ PropertyChanged=�$ TwoWay=���5a      �� *
FontSize+5e      5f      4  .Syncfusion.Windows.Tools.Controls.CheckListBox  5i   
   $��4��5j      $��0��5k       �� DisplayMemberPath$ DisplayString��5n      $��Center=�5o      $	��100��5p      $	��100��5q         IsSelectAllEnabled$ False��5r      $	��400��5s      $��0,10,0,0q�5t      $
 List of game types��5u      ��5l      �� *
ItemList+$ PropertyChanged=�$ TwoWay=���5m      �� *
FontSize+��5v      �� 5w       �� 
TargetType8  2Syncfusion.Windows.Tools.Controls.CheckListBoxItem"  6   .�5x      ��  �� Property �@ 	IsChecked �� Value 5z      �� $ TwoWay=� �� Path$ 
IsSelected�5{      ��5}      �� 5~      .R�5      �� 5�      5�      5�      5�      