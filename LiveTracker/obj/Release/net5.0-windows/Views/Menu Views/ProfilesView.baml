   M S B A M L   `   `   `  ���� h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager   Tournament Life/   )LiveTracker.Views.Menu_Views.ProfilesView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     -   0  *Syncfusion.Windows.Shared.ChromelessWindow   Name$  Window�� � 
ResizeMode$ NoResize=� � SizeToContent$ WidthAndHeight=�,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � Title �� *Title+ �� UpdateSourceTrigger$ PropertyChanged=�.���  � ColumnDefinitions �� $
��Auto ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$	��5,5q�$��1��.+��� $��Center=�$��PROFILES������ *
FontSize+��  �� SelectionMode$ Single=�$��Center=�$	��100��$	��100��	 � ToolTip$	 List of profiles������ *ProfileList+
 �� NotifyOnSourceUpdated$

 True�� �� SelectedItem ��  �� Path$ ProfileSelectedItem�$ PropertyChanged=����� *
FontSize+���� .R��� ���� *Name+����  �� 
TargetType" ��.���  �� Property �@��
IsSelected �� Value �� $ 
IsSelected� �� Mode$ TwoWay=��� $��0��$��0��$	��5,5q�$
��Left=� �� Orientation$ Vertical=�.+��� $��25��$	��100��$	��5,5q�$	 Create a new profile��$��New Profile������ *
FontSize+���� *
NewCommand+�� $��25��$	��100��$	��5,5q�$!	 Delete the selected profile��$��Delete Profile������ *
FontSize+���� *DeleteCommand+�� $��25��$	��100��$	��5,5q�$	 Load the selected profile��$��Load Profile������ *
FontSize+���� *LoadCommand+�� $��25��$	��100��$	��5,5q� �� IsCancel$
 True��$	 Close the window��$��Close������ *
FontSize+���� *CancelCommand+