   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.3.0.9, Culture=neutral, PublicKeyToken=null?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager >    8LiveTracker.Views.Bankroll_Views.BankrollTransactionView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     ;
converters$clr-namespace:LiveTracker.Converters     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
    msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
      � SizeToContent$  WidthAndHeight=� � 
ResizeMode$ NoResize=�$	��450��$	��800��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$
 Text�� � Title �� *Title+ � 	Resources 3   -LiveTracker.Converters.BoolToVisibleConverter   WindowVisibleConverter   	TrueValue   
FalseValue��%-   &	          $ Visible=�$ 	Collapsed=�.��� 	 � RowDefinitions	 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �
 � ColumnDefinitions
 �� $
��Auto ��� $
��Auto �.+��� $��0��$��0�� � ToolTip$ Bankroll being used��$��Right=�$��Bankroll������ *
FontSize+�� $��0��$��1��$ Bankroll being used������ *
FontSize+���� *	Bankrolls+ �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem �� *SelectedBankroll+$ PropertyChanged=��� $��1��$��0��$��Right=�$ Transaction type��$
��Type������ *
FontSize+�� $��1��$��1��$ Transaction Type������ *
FontSize+���� *TransactionTypes+$ PropertyChanged=� �� *SelectedTransactionType+$ PropertyChanged=��� $��2��$��0��$ Bonus type given��$��Right=�$��Bonus������ *
FontSize+y��� *BonusVisible+ �� 	Converter# [  �� $��2��$��1��$ Bonus type given������ *
FontSize+y��� *BonusVisible+# [  ���� *	BonusList+$ PropertyChanged=� �� *SelectedBonus+$ PropertyChanged=��� $��3��$��0��$ Amount of transaction��$��Right=�$��Amount������ *
FontSize+/  )Syncfusion.Windows.Shared.CurrencyTextBox  -     Name$
 Text��$��3��$��1��$ Amount of transaction��   CurrencySymbol$ $��*  $Syncfusion.Windows.Shared.EditorBase   Culture$ en-US��$	��100��   MinValue$
 0.00~����� *
FontSize+   Value �� *Amount+$ PropertyChanged=��� $��4��$��0��$ Date of transaction��$��Right=�$
��Date������ *
FontSize+4 	 .Syncfusion.Windows.Controls.Input.SfDatePicker  -     Name$ 
DatePicker��$��4��$��1��$ Date of transaction������ *
FontSize+   Value �� *Date+$ PropertyChanged=��� $��5��$��0��$��2��$��Center=� �� Orientation$ 
Horizontal=�.+��� $��24��$��90��$ Complete transaction��$	��5,5q�$	��5,5q����� *
FontSize+���� *SaveCommand+���� *
SaveButton+�� $��24��$��90��$ Close the window��$	��5,5q�$	��5,5q� �� IsCancel$
 True��$��Close������ *
FontSize+���� *CancelCommand+