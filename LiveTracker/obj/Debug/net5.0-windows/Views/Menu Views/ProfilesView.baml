   M S B A M L   `   `   `  ����h  dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  5	       Tournament Life3   -Tournament_Life.Views.Menu_Views.ProfilesView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
     5      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   	 
     5	      -   5        0  *Syncfusion.Windows.Shared.ChromelessWindow   Name$  Window��5       � 
ResizeMode$ NoResize=�5       � SizeToContent$ WidthAndHeight=�5      ,   &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5
      �� *Theme+ � Title 5      �� *Title+ �� UpdateSourceTrigger$ PropertyChanged=�.��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��5      $	��5,5q�5      $��1��5       .+�5!      �� $��Center=�5#      $��PROFILES��5$      ��5"      �� *
FontSize+5$      �� 5%       �� SelectionMode$ Single=�5(      $��Center=�5)      $	��100��5+      $	��100��5,      	 � ToolTip$	 List of profiles��5-      ��5&      �� *ProfileList+
 �� NotifyOnSourceUpdated$

 True�� �� SelectedItem 5'      ��  �� Path$ ProfileSelectedItem�$ PropertyChanged=���5*      �� *
FontSize+��5.      �� 5/      .R�50      �� ��51      �� *Name+52      ��54      �� 55       �� 
TargetType" ��56      .�57      ��  �� Property �@��
IsSelected �� Value 59      �� $ 
IsSelected� �� Mode$ TwoWay=�5:      5<      5=      �� 5@   
   $��0��5A      $��0��5B      $	��5,5q�5C      $
��Left=�5D       �� Orientation$ Vertical=�5E      .+�5F      �� $��25��5G      $	��100��5H      $	��5,5q�5I      $	 Create a new profile��5J      $��New Profile��5M      ��5K      �� *
FontSize+��5L      �� *
NewCommand+5M      �� 5N      $��25��5O      $	��100��5P      $	��5,5q�5Q      $!	 Delete the selected profile��5R      $��Delete Profile��5U      ��5S      �� *
FontSize+��5T      �� *DeleteCommand+5U      �� 5V      $��25��5W      $	��100��5X      $	��5,5q�5Y      $	 Load the selected profile��5Z      $��Load Profile��5]      ��5[      �� *
FontSize+��5\      �� *LoadCommand+5]      �� 5^      $��25��5_      $	��100��5`      $	��5,5q�5a       �� IsCancel$
 True��5b      $	 Close the window��5c      $��Close��5f      ��5d      �� *
FontSize+��5e      �� *CancelCommand+5f      5g      5i      5j      