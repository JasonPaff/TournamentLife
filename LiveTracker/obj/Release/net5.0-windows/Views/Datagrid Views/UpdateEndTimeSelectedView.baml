   M S B A M L   `   `   `  ���� h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager   Tournament Life@   :LiveTracker.Views.Datagrid_Views.UpdateEndTimeSelectedView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
       � Title$  Ending Time�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�$	��450��$	��800��,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto �.+�4 	 .Syncfusion.Windows.Controls.Input.SfDatePicker  $��0��$	��5,5q� � ToolTip$# The date the tournament ended������ *
FontSize+   Value �� *	EndDate+ �� UpdateSourceTrigger$ PropertyChanged=�	 �� Mode$	 TwoWay=�4 	 .Syncfusion.Windows.Controls.Input.SfTimePicker  $��1��$	��5,5q�$# The time the tournament ended������ *
FontSize+
   Value
 �� *	EndTime+$ PropertyChanged=�$	 TwoWay=��� $��2��$��Center=� �� Orientation$ 
Horizontal=�.+��� $��25��$��82��$	��5,5q� �� 	IsDefault$
 True��$ Update the ending time��$��Update������ *
FontSize+���� *SaveCommand+�� $��25��$��82��$	��5,5q� �� IsCancel$
 True��$ Close the window��$��Close������ *
FontSize+���� *CancelCommand+