   M S B A M L   `   `   `  ����b  ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89eGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPFSyncfusion.UI.Xaml.Charts  5      h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager 5	      U QSystem.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aO8clr-namespace:System.Collections;assembly=System.RuntimeSystem.Collections 5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5       Tournament Life?   9Tournament_Life.Views.Graph_Views.FinishPositionGraphView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R	 NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml   	  5      @d2http://schemas.microsoft.com/expression/blend/2008   	  5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006   	  5      a
 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   
      5      _chartGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPF   
      5      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   
      5	      Vcollections8clr-namespace:System.Collections;assembly=System.Runtime   
      5      ?System&clr-namespace:System;assembly=mscorlib   
      5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5        � 
ResizeMode$  	CanResize=�5       � Title$ Finish Position Graph��5      $	��800��5      $	��800��5      ,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5
      �� *Theme+0  *Syncfusion.Windows.Shared.ChromelessWindow   RightHeaderItemsSource 5      "  System.Collections.ArrayList  5   
   �� 5      $"��Maximum Positions To Display��6   3  -Syncfusion.Windows.Tools.Controls.ComboBoxAdv  5      $��0,0,25,0q�5       �� SelectedItem 5      �� *Max+ � 	Resources 5       �� 
TargetType �� BasedOn��%5   '��       �� 5      " ��6/    6   Z� 6/   5      ��5      �� 5      .R�5       �� $��20��5!       �� Padding$ 4q�5"      ��5#      �� 5$      ��5&        5'      ��5(      6��6+   ��5)      9��6+   ��5*      18��6,   ��5+      27��6,   ��5,      36��6,   ��5-      45��6,   ��5.      100��6-   ��5/      250��6-   ��50      500��6-   ��51      1000��6.   ��52      2000��6.   ��53      5000��6.   54      *  $Microsoft.Xaml.Behaviors.Interaction	   Triggers	 56      +  %Microsoft.Xaml.Behaviors.EventTrigger  57      
   	EventName$
 SelectionChanged��6%   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 58      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 59      �� *FilterCommand+   CommandParameter 5:      �� *Max+5;      5=      5>      .��5A      � .+�5C   
   '	  !Syncfusion.Windows.Shared.MenuAdv	  $��0��5D      $��0��5E      $��2��5F       	  
ExpandMode$ ExpandOnClick=�5G      .@�5J      +
  %Syncfusion.Windows.Shared.MenuItemAdv
  $
��File��5K      ��5L      �� *
FontSize+.@�5M      
  $��Save Graph Image��5N      ��5O      �� *
FontSize+ 
  Command 5P      �� *SaveImageCommand+
  5Q      $
��Exit��5R      ��5S      �� *
FontSize+ 5T      �� *CloseCommand+5U      5V      '   !Syncfusion.UI.Xaml.Charts.SfChart  5X   
   -   5          Name$ chart��5X      $��10,10q�6e   )   #Syncfusion.UI.Xaml.Charts.ChartBase   Header 6(   �� *ChartHeader+��6G   �� *
FontSize+   	Behaviors 5Z      4   .Syncfusion.UI.Xaml.Charts.ChartTooltipBehavior     EnableAnimation$
 True��6-      ShowDuration$
 1000��6D   4   .Syncfusion.UI.Xaml.Charts.ChartZoomPanBehavior  5[         EnableMouseWheelZooming$
 True��6-      EnableDirectionalZooming$
 True��6L      EnablePanning$
 True��6l      ZoomRelativeToCursor$
 True��6�      MaximumZoomLevel$	 5.0Q�6�      ResetOnDoubleTap$
 True��6�      PrimaryAxis 5]      -   'Syncfusion.UI.Xaml.Charts.NumericalAxis  5^      )   #Syncfusion.UI.Xaml.Charts.ChartAxis   Header$ Finish Position��6&   $��22(�6?      LabelExtent$ 50Q�6M      SecondaryAxis 5`        5a      $ Number of Times��6&   $��22(�6?   $ 50Q�6M      Series. 5c      ,   &Syncfusion.UI.Xaml.Charts.ColumnSeries  /   )Syncfusion.UI.Xaml.Charts.ChartSeriesBase    XBindingPath$  FinishPosition��6C   ,   &Syncfusion.UI.Xaml.Charts.XyDataSeries!   YBindingPath$! FinishPositionCount��6a   "   ShowTooltip$
" True��6�   #   EnableAnimation$
# True��6�   $   Spacing$	$ 0.5Q�6�   %   ItemsSource% 6!   �� *	Positions+&   TooltipTemplate& 5g      �� 5h      .R�5i      �� ��6$   �� *Item.Description+��6F   �� *	FontColor+5j      5l      5m      5n      5o      