   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5       Tournament LifeB   <Tournament_Life.Views.Session_Manager_Views.SessionStartView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     5        � 
ResizeMode$  NoResize=�5
       � SizeToContent$ WidthAndHeight=�5      ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5	      �� *Theme+ � Title 5      �� *Title+.��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   '  !Syncfusion.Windows.Shared.MenuAdv  $��0��5         
ExpandMode$ ExpandOnClick=�5      .@�5       +  %Syncfusion.Windows.Shared.MenuItemAdv  $��Options��5"      ��5!      �� *
FontSize+.@�5#        $+��%Select multiple tournaments at a time��5$       � ToolTip$u oWhen checked, allows you to select multiple tournaments at a time in the tournament list instead of just single��5%         IsCheckable$
 True��5&      	   	IsChecked	 5'      �� *MultiSelectionToggle+��5(      �� *
FontSize+
   Command
 5)      �� *SelectToggleCommand+  5*      $=��7Show a confirmation message before removing tournaments��5+      $b \When checked, displays a confirmation message prior to removing tournaments from the session��5,      $
 True��5-      	 5.      �� *ConfirmRemoveToggle+��5/      �� *
FontSize+
 50      �� *RemoveToggleCommand+  51      $=��7Show a confirmation message before starting the session��52      $Q KWhen checked, displays a confirmation message prior to starting the session��53      $
 True��54      	 55      �� *ConfirmStartToggle+��56      �� *
FontSize+
 57      �� *ConfirmStartCommand+  58      $D��>Sort the tournaments alphabetically instead of chronologically��59      $L FWhen checked sorts the tournaments by name instead of by starting time��5:      $
 True��5;      	 5<      �� *ConfirmAlphaToggle+��5=      �� *
FontSize+
 5>      �� *ConfirmAlphaCommand+5?      5A      �� 5D   
   $��1��6   .+�5E   
   �� -   5         ��Name$ TournamentList��5F      $	��250��5I      $	��500��5J      $	��5,5q�5K      $	��5,5q�5L      $��1q�5M      ��5G      �� *TournamentList+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=���5H      �� *
FontSize+ �� SelectionMode 5N      �� *SelectionMode+��5O      �� 5P      .R�5Q      �� ��6    �� *Name+5R      ��5T      �� 5U       �� 
TargetType" ��6   .�5V      ��  �� Property �@��
IsSelected �� Value 63   �� $ TwoWay=� �� Path$ 
IsSelected��� 5W       �@�ToolTip 60   �� *Description+5X      5Z      5[      �� 5^   
   $��2��5_      $��Center=�5`       �� Orientation$ 
Horizontal=�5a      .+�5b   
   �� $��26��5e      $��55��5f      $	��5,5q�5g      $	��5,5q�5h      $��Start��5i      $* $Starts the tournament(s) in the list��5j      ��5c      �� *
FontSize+��5d      �� *SaveCommand+5j      �� 5k   
   $��26��5n      $��55��5o      $	��5,5q�5p      $	��5,5q�5q      $/ )Cancels the session and closes the window��5r      $��Cancel��5s      ��5l      �� *
FontSize+��5m      �� *CloseCommand+5s      �� 5}   
   $��26��5�      $��55��5�      $	��5,5q�5�      $	��5,5q�5�      $��Remove Selected��5�      $8 2Remove the selected tournament(s) from the session��5�      ��5~      �� *
FontSize+��5      �� *RemoveCommand+5�      5�      5�      5�      