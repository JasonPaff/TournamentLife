   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.0.1.2, Culture=neutral, PublicKeyToken=null5clr-namespace:LiveTracker.ViewsLiveTracker.Views  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager >    8LiveTracker.Views.Bankroll_Views.BankrollTransactionView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:LiveTracker.Views     ` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a	 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf    	 
   @xctk)http://schemas.xceed.com/wpf/xaml/toolkit    	 
   msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    	 
     � SizeToContent$  WidthAndHeight=� � 
ResizeMode$ NoResize=�$	��450��$	��800��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$
 Text�� � Title �� *Title+.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��Bankroll������ *
FontSize+�� $��0��$��1������ *
FontSize+���� *	Bankrolls+ �� UpdateSourceTrigger$ PropertyChanged=�	 �� SelectedItem	 �� *SelectedBankroll+$ PropertyChanged=��� $��1��$��0��$��Amount������ *
FontSize+/ 
 )Syncfusion.Windows.Shared.CurrencyTextBox  -   
  Name$

 Text��$��1��$��1��   CurrencySymbol$ $��* 
 $Syncfusion.Windows.Shared.EditorBase   Culture$ en-US��$	��100��   MinValue$
 0.00~����� *
FontSize+���� *	FontColor+���� *Amount+$ PropertyChanged=��� $��2��$��0��$
��Date������ *
FontSize+4  .Syncfusion.Windows.Controls.Input.SfDatePicker  -     Name$ 
DatePicker��$��2��$��1������ *
FontSize+   Value �� *Date+$ PropertyChanged=��� $��3��$��0��$��2��$��Center=� �� Orientation$ 
Horizontal=�.+��� $��40��$	��100��$	��5,5q�$	��5,5q�$
��Save������ *
FontSize+���� *SaveCommand+�� $��40��$	��100��$	��5,5q�$	��5,5q� �� IsCancel$
 True��$��Cancel������ *
FontSize+���� *CancelCommand+