   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5       Tournament LifeF   @Tournament_Life.Views.Datagrid_Views.CancelSelectTournamentsView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5   	   msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     5        � Title$  Cancel Tournament(s)��5
   	    � SizeToContent$ WidthAndHeight=�5   	    � 
ResizeMode$ NoResize=�5   	   $	��450��5   	   $	��800��5   	   ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5      �� *Theme+.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   .+�5   
   '  !Syncfusion.Windows.Shared.MenuAdv  -   5          Name$ LiveTrackerMenu��5      $��0��5         
ExpandMode$ ExpandOnClick=�5      .@�5!      +  %Syncfusion.Windows.Shared.MenuItemAdv  $��Options��5#      ��5"      �� *
FontSize+.@�5$        $'��!Sort Tournaments by Starting Time��5%       � ToolTip$J DSort the tournaments by the starting times instead of alphabetically��5&      	   IsCheckable$
	 True��5'      
   	IsChecked
 5(      �� *SortByStartTime+��5)      �� *
FontSize+   Command 5*      �� *SortCommand+5+      5,      �� 5.   
   $��1��5/      $	��5,5q�51      $	��500��52      $��1q�53       �� SelectionMode$ Multiple=�54      ��50      �� *
FontSize+��55      �� *Tournaments+��56      �� 57      .R�58      �� ��6    �� *Name+68   59      ��5;      �� 5<       �� 
TargetType" ��6   .�5=      ��  �� Property �@��
IsSelected �� Value 63   ��  �� Mode$ TwoWay=� �� Path$ 
IsSelected��� 5>       �@�ToolTip 60   �� *Description+5?      5A      �� 5C   
   $��2��5D      $��Center=�5E       �� Orientation$ 
Horizontal=�5F      .+�5G      �� $��30��5H      $	��100��5I      $	��5,5q�5K       �� 	IsDefault$
 True��5L      $% Cancel the selected tournaments��5M      $��Select��5O      ��5J      �� *
FontSize+��5N      �� *SaveCommand+5O      �� 5P      $��30��5Q      $	��100��5R      $	��5,5q�5T       �� IsCancel$
 True��5U      $4 .Close window without canceling any tournaments��5V      $��Close��5X      ��5S      �� *
FontSize+��5W      �� *CancelCommand+5X      5Y      5Z      5[      