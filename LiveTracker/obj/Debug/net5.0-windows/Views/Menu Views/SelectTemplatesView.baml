   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5	       Tournament Life:   4Tournament_Life.Views.Menu_Views.SelectTemplatesView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     5	      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5
      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      -   5        0  *Syncfusion.Windows.Shared.ChromelessWindow   Name$  Window��5       � 
ResizeMode$ CanMinimize=�5         UseNativeChrome$
 True��5       � SizeToContent$ WidthAndHeight=�5       � Title$ Select Tournament(s)��5         ShowMaximizeButton$ False��5      $	��550��5      $	��250��5      ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5      �� *Theme+ +� FocusedElement 5      ��  �� ElementName$
 Text��*  $Microsoft.Xaml.Behaviors.Interaction	   Triggers	 5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5   
   
   	EventName$
 Loaded��6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6$   �� *WindowLoadedCommand+5      .��5      �  � ColumnDefinitions 5   
   �� 5       $
��Auto �6   �� 5!      $
��Auto �6    � RowDefinitions 5#   
   �� 5$      $
��Auto �6   �� 5%      $
��Auto �6   �� 5&      $
��Auto �6   �� 5'      $
��Auto �6   �� 5(      $
��Auto �6   .+�5,   
   '  !Syncfusion.Windows.Shared.MenuAdv  -   5          Name$ LiveTrackerMenu��5-      $��0��5.         
ExpandMode$ ExpandOnClick=�5/      .@�52      +  %Syncfusion.Windows.Shared.MenuItemAdv  $��Options��54      ��53      �� *
FontSize+.@�55        $E��?Close the select tournaments window after starting a tournament��56       � ToolTip$c ]Toggles whether to close the select tournaments window after starting your tournaments or not��57         IsCheckable$
 True��58         	IsChecked 59      �� *CloseOnStartToggle+��5:      �� *
FontSize+   Command 5;      �� *CloseOnStartCommand+  5<      $=��7Show a confirmation message before starting tournaments��5=      $a [Toggles whether to display a confirmation message prior to starting your tournaments or not��5>      $
 True��5?       5@      �� *ConfirmStartToggle+��5A      �� *
FontSize+ 5B      �� *ConfirmStartCommand+  5C      $5��/Select multiple tournaments in tournaments list��5D      $P JToggles between single and multiple selection mode in the tournaments list��5E      $
 True��5F       5G      �� *MultiSelectionToggle+��5H      �� *
FontSize+ 5I      �� *SelectToggleCommand+1	  +Syncfusion.Windows.Shared.MenuItemSeparator	  5K      $	��150��6/   $
��Left=�6;     5M      $��Close Window��5N      $0 *Cancel the tournament and close the window��5O      ��5P      �� *
FontSize+ 5Q      �� *ExitCommand+5R      5S      �� 5V   
   $��1��5W      $��0��5X      $
��Left=�5Y       �� Orientation$ 
Horizontal=�5Z      .+�5\      �  5]      �� 5^      $��* �6'   �� 5_      $��* �6'    5a      �� 5b      $��* �6$   �� 5c      $��* �6$   .+�5g      �� $��0��5h      $��0��5i      $��Search��5j      $��5,4,0,0q�5k      ��5l      �� *
FontSize+�� 5n      -   5         ��Name$
 Text��5q      $��0��5o      $��1��5p      $��35��5s      $��Center=�5t      $* $Search tournaments for specific text��5u      ��5r      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=���5v      �� *
FontSize+.��63   .
  (System.Windows.Interactivity.Interaction 
  Triggers 5w      /  )System.Windows.Interactivity.EventTrigger  5x         	EventName$ TextChanged��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5y      6  0System.Windows.Interactivity.InvokeCommandAction     Command 64   �� *TextChangedCommand+5z      .��5{   .   5|      �� 5~      $��1��5      $��0��5�      $��5,4,0,0q�5�      $��Venue��5�      ��5�      �� *
FontSize+�� 5�      -   5         ��Name$ Combo��5�      $��1��5�      $��1��5�      $��35��5�      $	��175��5�      $$ Filter the tournament by venue��5�      ��5�      �� *Venues+ 5�      �� $
 Text�� �� SelectedItem 5�      �� *SelectedVenue+$ PropertyChanged=�$ TwoWay=���5�      �� *
FontSize+ 5�        5�      $ SelectionChanged��6)   . 5�         64   �� *TextChangedCommand+5�      5�      5�      5�      �� 5�   
   $��2��5�      $��0��5�      $��2��5�      $
��Left=�5�      $	��5,5q�5�      $ 
Horizontal=�5�      .+�5�      �� $��Reset��5�      $��25��5�      $��65��5�      $
��Left=�5�      $	��5,5q�5�      $5 /Clear the text box and unselect all tournaments��5�      ��5�      �� *ResetCommand+��5�      �� *
FontSize+ 5�      �� $
 Text���� 5�      $��25��5�      $��65��5�      $	��5,5q�5�      $ Start the tournament(s)��5�      $��Start��5�      ��5�      �� *
FontSize+��5�      �� *SaveCommand+5�      �� 5�      $��25��5�      $��65��5�      $	��5,5q�5�       �� IsCancel$
 True��5�      $ Close window��5�      $��Close��5�      ��5�      �� *
FontSize+��5�      �� *CancelCommand+5�      5�      �� 5�   
   $��3��5�      $��0��5�      .+�5�      �� -   5          ��Name$  TournamentList��5�      $	��500��5�      ! �� SelectionMode$! Multiple=�5�      $��3,10,0,5q�5�      $" Select tournament(s) to load��5�      ��5�      �� *TemplateList+" �� NotifyOnSourceUpdated$
" True�� 5�      �� # �� Path$# TemplateSelectedItem�$ PropertyChanged=���5�      �� *
FontSize+��5�      �� 5�      .R�5�      �� ��5�   !   �� *DisplayString+5�      ��5�      �� 5�      $ �� 
TargetType"$ ��5�      .�5�      �� % �� Property% �@��
IsSelected& �� Value& 5�   !   �� $ TwoWay=�$# 
IsSelected��� 5�      % �@�ToolTip& 5�   !   �� *Description+5�      5�      5�      5�      5�      