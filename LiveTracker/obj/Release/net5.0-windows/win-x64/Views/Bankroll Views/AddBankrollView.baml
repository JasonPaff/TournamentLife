   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.0.1.2, Culture=neutral, PublicKeyToken=null5clr-namespace:LiveTracker.ViewsLiveTracker.Views  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager 6    0LiveTracker.Views.Bankroll_Views.AddBankrollView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:LiveTracker.Views     ` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a	 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf    	 
   @xctk)http://schemas.xceed.com/wpf/xaml/toolkit    	 
   msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    	 
     � Title$  New Bankroll�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�$	��450��$	��800��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$
 Text��.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $��5 �.+��� $��0��$��0��$��0,0,5,0q�$��Right=�$��Venue������ *
FontSize+�� $��0��$��1������ *
FontSize+���� *Venues+ �� UpdateSourceTrigger$ PropertyChanged=�	 �� SelectedItem	 �� *Venue+$ PropertyChanged=��� $��1��$��Right=�$��0��$��0,0,5,0q�$��Initial Amount������ *
FontSize+- 
 'Syncfusion.Windows.Shared.DoubleTextBox  -   
  Name$

 Text��$��1��$��1��$	��100��   MinValue$ 0Q����� *
FontSize+���� *Amount+�� $��2��$��0��$��2�� �� Orientation$ 
Horizontal=�.+��� $��	Add Venue��$��Center=�$��Center=�$	��100��$��25��$	��5,5q����� *
FontSize+���� *AddNewVenueCommand+�� $��Remove Venue��$��Center=�$��Center=�$	��110��$��25��$	��5,5q����� *
FontSize+���� *RemoveVenueCommand+�� $��3��$��0��$��2��$ 
Horizontal=�.+��� $��25��$	��100��$	��5,5q�$
��Save������ *
FontSize+���� *SaveCommand+�� $��25��$	��110��$	��5,5q� �� IsCancel$
 True��$��Cancel������ *
FontSize+���� *CancelCommand+