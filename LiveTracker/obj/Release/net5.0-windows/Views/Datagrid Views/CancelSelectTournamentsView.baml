   M S B A M L   `   `   `  ���� h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager   Tournament LifeB   <LiveTracker.Views.Datagrid_Views.CancelSelectTournamentsView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
       � Title$  Cancel Tournament(s)�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�$	��450��$	��800��,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto �.+�'  !Syncfusion.Windows.Shared.MenuAdv  -     Name$ LiveTrackerMenu��$��0��   
ExpandMode$ ExpandOnClick=�.@�+  %Syncfusion.Windows.Shared.MenuItemAdv  $��Options������ *
FontSize+.@�  $'��!Sort Tournaments by Starting Time�� � ToolTip$J DSort the tournaments by the starting times instead of alphabetically��	   IsCheckable$
	 True��
   	IsChecked
 �� *SortByStartTime+���� *
FontSize+   Command �� *SortCommand+�� $��1��$	��5,5q�$	��500��$��1q� �� SelectionMode$ Multiple=����� *
FontSize+���� *Tournaments+���� .R��� ���� *Name+����  �� 
TargetType" ��.���  �� Property �@��
IsSelected �� Value ��  �� Mode$ TwoWay=� �� Path$ 
IsSelected���  �@�ToolTip �� *Description+�� $��2��$��Center=� �� Orientation$ 
Horizontal=�.+��� $��30��$	��100��$	��5,5q� �� 	IsDefault$
 True��$% Cancel the selected tournaments��$��Select������ *
FontSize+���� *SaveCommand+�� $��30��$	��100��$	��5,5q� �� IsCancel$
 True��$4 .Close window without canceling any tournaments��$��Close������ *
FontSize+���� *CancelCommand+