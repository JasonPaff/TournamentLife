   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=null?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager A    ;LiveTracker.Views.Template_Manager_Views.TemplateImportView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ;
converters$clr-namespace:LiveTracker.Converters     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
    msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
    -   0  *Syncfusion.Windows.Shared.ChromelessWindow   Name$  Window�� � 
ResizeMode$ NoResize=� � SizeToContent$ WidthAndHeight=� � Title$ Import Tournaments��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � 	Resources 3   -LiveTracker.Converters.BoolToVisibleConverter   BoolToVisibleConverter��%   &	          .���  � ColumnDefinitions �� $
��Auto ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$	��5,5q�$��Center=� �� Orientation$ 
Horizontal=�.+��� $��25��$	��120��$	��5,5q�	 � ToolTip$	 Select Import File��$��Select Import File������ *
FontSize+���� *ImportFileCommand+�� $��25��$	��120��$	��5,5q�$%	 Import the selected tournaments��$��Import Selected������ *
FontSize+���� *ImportCommand+�� $��25��$	��120��$	��5,5q�
 �� IsCancel$

 True��$	 Close window��$��Close Window������ *
FontSize+���� *CancelCommand+�� $��1��$��0��$	��5,5q�$��Center=�$ 
Horizontal=�.+��� $��Check for Duplicates�� d� 	IsChecked �� *CheckForDuplicates+�� $��2��$��0��$��Center=�$��LOADING....PLEASE WAIT��y��� *LoadingLabel+ �� 	Converter# [   �� UpdateSourceTrigger$ PropertyChanged=�4  .Syncfusion.Windows.Tools.Controls.CheckListBox  -     Name$ ImportListBox��$��3��$��0�� �� DisplayMemberPath$ DisplayString��$��Center=�$	��100��$	��100��$	��700��$��0,10,0,0q�$#	 List of tournaments to import������ *TemplatesList+���� *
FontSize+����  �� 
TargetType8  2Syncfusion.Windows.Tools.Controls.CheckListBoxItem"  .���  �� Property �@ 	IsChecked �� Value ��  �� Mode$ TwoWay=� �� Path$ 
IsSelected���  �@�ToolTip �� *Description+���� .R��� 