   M S B A M L   `   `   `  ���� h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager   Tournament Life6   0LiveTracker.Views.Menu_Views.SelectTemplatesView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a	 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf     	 
   msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF     	 
   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  -   0 
 *Syncfusion.Windows.Shared.ChromelessWindow   Name$  Window�� � 
ResizeMode$ NoResize=� � SizeToContent$ WidthAndHeight=� � Title$ Select Tournament(s)��,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$
 Text��.���  � ColumnDefinitions �� $
��Auto ��� $
��Auto � � RowDefinitions �� $��* ��� $��* ��� $
��Auto ��� $
��Auto �$	��600��.+��� $��0��$��0��$
��Left=�	 �� Orientation$	 
Horizontal=�.+��  �� $��* ��� $��* � �� $��* ��� $��* �.+��� $��0��$��0��$��Search��$��0,3,0,0q����� *
FontSize+�� -   
 ��Name$

 Text��$��0��$��1��$��35��$��0,3,0,5q�$��Center=� � ToolTip$ Search templates������ *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=����� *
FontSize+.��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ TextChanged��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *TextChangedCommand+.���� $��1��$��0��$��Venue��$��0,3,0,0q����� *
FontSize+�� -    ��Name$ Combo��$��1��$��1��$��35��$	��230������ *Venues+ �� $
 Text�� �� SelectedItem �� *SelectedVenue+$ PropertyChanged=�$ TwoWay=����� *
FontSize+   $ SelectionChanged��.    �� *TextChangedCommand+�� $��1��$��0��$��2��$
��Left=�$	��5,5q�$	 
Horizontal=�.+��� $��Reset��$��25��$��90��$
��Left=�$	��5,5q�$5 /Clear the text box and unselect all tournaments������ *ResetCommand+���� *
FontSize+ �� $
 Text���� $��25��$��90��$	��5,5q�$ Start the tournament(s)��$��Start������ *
FontSize+���� *SaveCommand+�� $��25��$��90��$	��5,5q� �� IsCancel$
 True��$ Close window��$��Cancel������ *
FontSize+���� *CancelCommand+�� $��2��$��0��.+��� -    ��Name$
 List��$	��705�� �� SelectionMode$ Multiple=�$��0,10,0,5q�$" Select tournament(s) to load������ *TemplateList+ �� NotifyOnSourceUpdated$
 True�� ��  �� Path$ TemplateSelectedItem�$ PropertyChanged=����� *
FontSize+���� .R��� ���� *DisplayString+����  �� 
TargetType" ��.���  �� Property �@��
IsSelected �� Value �� $ TwoWay=�$ 
IsSelected���  �@�ToolTip �� *Description+