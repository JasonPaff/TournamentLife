   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=nullS.clr-namespace:LiveTracker.Views.Bankroll_Views LiveTracker.Views.Bankroll_Views  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager 3    -LiveTracker.Views.Bankroll_Views.AddVenueView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @local.clr-namespace:LiveTracker.Views.Bankroll_Views     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
    @xctk)http://schemas.xceed.com/wpf/xaml/toolkit   	 
    msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
      � Title$  	Add Venue�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�$	��450��$	��800��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$
 Text��.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$	��5,5q� � ToolTip$ Venue name to add��$��Venue������ *
FontSize+�� -   	 ��Name$
	 Text��$��0��$��1��$	��5,5q�$	��100��$ Venue to add��0  *Syncfusion.UI.Xaml.Grid.FocusManagerHelper
   FocusedElement$

 True������ *
FontSize+���� *Venue+�� $��1��$��0��$��2��$��Center=� �� Orientation$ 
Horizontal=�.+��� $��30��$��90��$	��5,5q�$* $Add the new venue to the venues list��$��	Add Venue������ *
FontSize+���� *SaveCommand+�� $��30��$��90��$	��5,5q�$! Cancel and close the window�� �� IsCancel$
 True��$��Close Window������ *
FontSize+���� *CancelCommand+