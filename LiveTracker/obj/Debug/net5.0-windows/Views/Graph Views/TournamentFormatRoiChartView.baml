   M S B A M L   `   `   `  ����U  QSystem.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aO8clr-namespace:System.Collections;assembly=System.RuntimeSystem.Collections  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89eGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPFSyncfusion.UI.Xaml.Charts 5
      h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager 5       Tournament LifeD   >Tournament_Life.Views.Graph_Views.TournamentFormatRoiChartView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R	 NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml   	  5      @d2http://schemas.microsoft.com/expression/blend/2008   	  5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006   	  5      a
 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf  
      5      Vcollections8clr-namespace:System.Collections;assembly=System.Runtime  
      5      ?System&clr-namespace:System;assembly=mscorlib  
      5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5	      SchartGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPF  5
      asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF  5        � 
ResizeMode$  	CanResize=�5       � Title$ Format Roi Graph��5      $	��800��5      $	��800��5      ,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5      �� *Theme+0  *Syncfusion.Windows.Shared.ChromelessWindow   RightHeaderItemsSource 5      "   System.Collections.ArrayList  5   
   �� 5      $.��(Minimum Tournaments Required For Display��6   3  -Syncfusion.Windows.Tools.Controls.ComboBoxAdv  5      $��0,0,25,0q�5       �� SelectedItem 5      �� *MinimumTournaments+ � 	Resources 5       �� 
TargetType �� BasedOn��%5   '��       �� 5      " ��6/    6   Z� 6/   5      ��5      �� 5      .R�5      �� $��20��5        �� Padding$ 4q�5!      ��5"      �� 5#      ��5%        5&      ��5'      1��6+   ��5(      10��6,   ��5)      25��6,   ��5*      50��6,   ��5+      100��6-   ��5,      250��6-   ��5-      500��6-   ��5.      1000��6.   ��5/      2000��6.   50      *  $Microsoft.Xaml.Behaviors.Interaction	   Triggers	 52      +  %Microsoft.Xaml.Behaviors.EventTrigger  53      
   	EventName$
 SelectionChanged��6%   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 54      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 55      �� *FilterCommand+   CommandParameter 56      �� *MinimumTournaments+57      59      5:      .��5=      � .+�5?   
   '	  !Syncfusion.Windows.Shared.MenuAdv	  $��0��5@      $��0��5A      $��2��5B       	  
ExpandMode$ ExpandOnClick=�5C      .@�5F      +
  %Syncfusion.Windows.Shared.MenuItemAdv
  $
��File��5G      ��5H      �� *
FontSize+.@�5I      
  $��Save Graph Image��5J      ��5K      �� *
FontSize+ 
  Command 5L      �� *SaveImageCommand+
  5M      $
��Exit��5N      ��5O      �� *
FontSize+ 5P      �� *CloseCommand+5Q      5R      '  !Syncfusion.UI.Xaml.Charts.SfChart  5T   
   -   5          Name$ chart��5T      $��10,10q�6e   )  #Syncfusion.UI.Xaml.Charts.ChartBase   Header 6(   �� *ChartHeader+��6G   �� *
FontSize+   	Behaviors 5V      4  .Syncfusion.UI.Xaml.Charts.ChartTooltipBehavior     EnableAnimation$
 True��6-      ShowDuration$
 5000��6D   4  .Syncfusion.UI.Xaml.Charts.ChartZoomPanBehavior  5W         EnableMouseWheelZooming$
 True��6-      EnableDirectionalZooming$
 True��6L      EnablePanning$
 True��6l      ZoomRelativeToCursor$
 True��6�      MaximumZoomLevel$	 5.0Q�6�      ResetOnDoubleTap$
 True��6�      PrimaryAxis 5Y      ,  &Syncfusion.UI.Xaml.Charts.CategoryAxis  5Z      )  #Syncfusion.UI.Xaml.Charts.ChartAxis   Header$ Tournament Format��6%   $��22(�6@      LabelRotationAngle$ 90Q�6N      LabelExtent$ 50Q�6f      ShowGridLines$ False��6w       SecondaryAxis  5\      -  'Syncfusion.UI.Xaml.Charts.NumericalAxis  5]      $ Return on Investment��6&   $��22(�6D   $ 50Q�6�   !   LabelFormat! 6R   �� *YAxisLabelFormat+" �� UpdateSourceTrigger$" PropertyChanged=�6�   #   Series.# 5`      ,  &Syncfusion.UI.Xaml.Charts.ColumnSeries  /  )Syncfusion.UI.Xaml.Charts.ChartSeriesBase$   XBindingPath$$ Format��6E   ,  &Syncfusion.UI.Xaml.Charts.XyDataSeries%   YBindingPath$	% Roi��6[   &   EnableAnimation$
& True��6n   '   ShowTooltip$
' True��6�   (   Spacing$( 0Q�6�   )   SegmentSpacing$) 0Q�6�   *   ItemsSource* 6!   �� *Tournaments+/  )Syncfusion.UI.Xaml.Charts.AdornmentSeries+   AdornmentsInfo+ 5a      2  ,Syncfusion.UI.Xaml.Charts.ChartAdornmentInfo  5b      6  0Syncfusion.UI.Xaml.Charts.ChartAdornmentInfoBase,   
ShowMarker$
, True��60   -   Symbol$- Ellipse=�6B   .   SymbolInterior
. �����/   SymbolStroke
/ �   �0   HighlightOnSelection$
0 True��6�   1   FontSize1 6�   �� *
FontSize+6�   2   TooltipTemplate2 5d      �� 5e      .R�5f      �� ��6$   �� *Item.Description+��6F   �� *	FontColor+5g      5i      5j      5k      5l      