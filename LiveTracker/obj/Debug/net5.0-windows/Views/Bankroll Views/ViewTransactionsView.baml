   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5
       Tournament Life?   9Tournament_Life.Views.Bankroll_Views.ViewTransactionsView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5      asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF  5
        � Title$  Transactions��5       � SizeToContent$ WidthAndHeight=�5       � 
ResizeMode$ CanMinimize=�5      0  *Syncfusion.Windows.Shared.ChromelessWindow   UseNativeChrome$
 True��5         ShowMaximizeButton$ False��5      $	��750��5      $	��800��5      ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5      �� *Theme+.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5    
   �� $��0��5!       �� Orientation$ 
Horizontal=�5"      .+�5$      �� $��Bankroll��5%      $	��5,5q�5&      	 � ToolTip$	 Venue bankroll��5'      �� 5)      $	��5,5q�5*      $	��5,5q�5+      $	��145��5,      $	 Venue bankrolls��5-      ��5.      �� *
FontSize+��5/      �� *	Bankrolls+
 �� UpdateSourceTrigger$
 PropertyChanged=� �� SelectedItem 50      �� *SelectedBankroll+$
 PropertyChanged=�*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 51      +  %Microsoft.Xaml.Behaviors.EventTrigger  52         	EventName$ SelectionChanged��6%   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 53      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 60   �� *BankrollChangedCommand+54      56      57      �� 59   
   -   5         ��Name$ 	VenueList��5:      $��1��5;      $	��600��5<      $��0��5=      $	��5,5q�5>      $!	 Venue bankroll transactions��5?       �� SelectionMode$ Single=�5A      ��5@      �� *
FontSize+ 5B      ��  �� Path$ SelectedTransactions�$
 PropertyChanged=���5C      �� *Transactions+$
 PropertyChanged=���5D      �� 5E      .R�5F      �� ��6    �� *Header+5G      ��5I      �� 5J       �� 
TargetType" ��6   .�5K      ��  �� Property �@��
IsSelected �� Value 5M      ��  �� Mode$ TwoWay=�$ 
IsSelected�5N      5P      �� 5R   
   $��2��5S      $��0��5T      $��Center=�5U      $ 
Horizontal=�5V      .+�5W      �� $��24��5X      $��60��5Z      $	��5,5q�5[      $��Delete��5]      $%	 Delete the selected transaction��5^      ��5Y      �� *
FontSize+��5\      �� *SaveCommand+5^      �� 5_      $��24��5`      $��60��5b      $	��5,5q�5c      $
��Edit��5e      $+	 %Edit the selected transaction details��5f      ��5a      �� *
FontSize+��5d      �� *EditCommand+5f      �� 5g      $��24��5h      $��60��5j      $	��5,5q�5k       �� IsCancel$
 True��5l      $	 Close the window��5m      $��Close��5o      ��5i      �� *
FontSize+��5n      �� *CancelCommand+5o      5p      5r      5s      