   M S B A M L   `   `   `  ���� h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager   Tournament Life6   0LiveTracker.Views.Menu_Views.SelectTemplatesView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  -   0  *Syncfusion.Windows.Shared.ChromelessWindow   Name$  Window�� � 
ResizeMode$ CanMinimize=�   UseNativeChrome$
 True�� � SizeToContent$ WidthAndHeight=� � Title$ Select Tournament(s)��   ShowMaximizeButton$ False��$	��550��$	��250��,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$
 Text��*  $Microsoft.Xaml.Behaviors.Interaction	   Triggers	 +  %Microsoft.Xaml.Behaviors.EventTrigger  
   	EventName$
 Loaded��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *WindowLoadedCommand+.���  � ColumnDefinitions �� $
��Auto ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�'  !Syncfusion.Windows.Shared.MenuAdv  -     Name$ LiveTrackerMenu��$��0��   
ExpandMode$ ExpandOnClick=�.@�+  %Syncfusion.Windows.Shared.MenuItemAdv  $��Options������ *
FontSize+.@�  $E��?Close the select tournaments window after starting a tournament�� � ToolTip$c ]Toggles whether to close the select tournaments window after starting your tournaments or not��   IsCheckable$
 True��   	IsChecked �� *CloseOnStartToggle+���� *
FontSize+   Command �� *CloseOnStartCommand+  $=��7Show a confirmation message before starting tournaments��$a [Toggles whether to display a confirmation message prior to starting your tournaments or not��$
 True�� �� *ConfirmStartToggle+���� *
FontSize+ �� *ConfirmStartCommand+  $5��/Select multiple tournaments in tournaments list��$P JToggles between single and multiple selection mode in the tournaments list��$
 True�� �� *MultiSelectionToggle+���� *
FontSize+ �� *SelectToggleCommand+1	  +Syncfusion.Windows.Shared.MenuItemSeparator	  $	��150��$
��Left=�  $��Close Window��$0 *Cancel the tournament and close the window������ *
FontSize+ �� *ExitCommand+�� $��1��$��0��$
��Left=� �� Orientation$ 
Horizontal=�.+��  �� $��* ��� $��* � �� $��* ��� $��* �.+��� $��0��$��0��$��Search��$��5,4,0,0q����� *
FontSize+�� -    ��Name$
 Text��$��0��$��1��$��35��$��Center=�$* $Search tournaments for specific text������ *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=����� *
FontSize+.��.
  (System.Windows.Interactivity.Interaction 
  Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ TextChanged��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *TextChangedCommand+.���� $��1��$��0��$��5,4,0,0q�$��Venue������ *
FontSize+�� -    ��Name$ Combo��$��1��$��1��$��35��$	��175��$$ Filter the tournament by venue������ *Venues+ �� $
 Text�� �� SelectedItem �� *SelectedVenue+$ PropertyChanged=�$ TwoWay=����� *
FontSize+   $ SelectionChanged��.    �� *TextChangedCommand+�� $��2��$��0��$��2��$
��Left=�$	��5,5q�$ 
Horizontal=�.+��� $��Reset��$��25��$��65��$
��Left=�$	��5,5q�$5 /Clear the text box and unselect all tournaments������ *ResetCommand+���� *
FontSize+ �� $
 Text���� $��25��$��65��$	��5,5q�$ Start the tournament(s)��$��Start������ *
FontSize+���� *SaveCommand+�� $��25��$��65��$	��5,5q� �� IsCancel$
 True��$ Close window��$��Close������ *
FontSize+���� *CancelCommand+�� $��3��$��0��.+��� -     ��Name$  TournamentList��$	��500��! �� SelectionMode$! Multiple=�$��3,10,0,5q�$" Select tournament(s) to load������ *TemplateList+" �� NotifyOnSourceUpdated$
" True�� �� # �� Path$# TemplateSelectedItem�$ PropertyChanged=����� *
FontSize+���� .R��� ���� *DisplayString+���� $ �� 
TargetType"$ ��.��� % �� Property% �@��
IsSelected& �� Value& �� $ TwoWay=�$# 
IsSelected��� % �@�ToolTip& �� *Description+