   M S B A M L   `   `   `  ���� h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager   Tournament Life6   0LiveTracker.Views.Bankroll_Views.AddBankrollView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
       � Title$  Create Bankroll�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�$	��450��$	��800��,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$
 Text��.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $��5 �.+��� $��0��$��0��$��0,0,5,0q�$��Right=� � ToolTip$! Venue name for new bankroll��$��Venue������ *
FontSize+�� $��0��$��1��$ 
Venue name������ *
FontSize+���� *Venues+	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� SelectedItem
 �� *Venue+$	 PropertyChanged=��� $��1��$��Right=�$��0��$��0,0,5,0q�$, &Initial bankroll amount for this venue��$��Initial Amount������ *
FontSize+-  'Syncfusion.Windows.Shared.DoubleTextBox  -     Name$
 Text��$��1��$��1��$, &Initial bankroll amount for this venue��$	��100��   MinValue$ 0Q����� *
FontSize+���� *Amount+�� $��2��$��0��$��2�� �� Orientation$ 
Horizontal=�.+��� $��	Add Venue��$��Center=�$��Center=�$	��100��$��25��$( "Add a new venue to the venues list��$	��5,5q����� *
FontSize+���� *AddNewVenueCommand+�� $��Remove Venue��$) #Remove a venue from the venues list��$��Center=�$��Center=�$	��110��$��25��$	��5,5q����� *
FontSize+���� *RemoveVenueCommand+�� $��3��$��0��$��2��$ 
Horizontal=�.+��� $��25��$	��100��$@ :Create a new bankroll of stated size at the selected venue��$	��5,5q�$��Create Bankroll������ *
FontSize+���� *SaveCommand+�� $��25��$	��110��$. (Cancel the bankroll and close the window��$	��5,5q� �� IsCancel$
 True��$��Close Window������ *
FontSize+���� *CancelCommand+