   M S B A M L   `   `   `  ����b  ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89eGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPFSyncfusion.UI.Xaml.Charts  5      h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager 5	       Tournament Life>   8Tournament_Life.Views.Graph_Views.SessionProfitGraphView   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      a ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k
 gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf    	 
    5      _chartGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPF    	 
    5      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    	 
    5	        � Title$  Sessions Profit Graph��5      $	��800��5      $
��1200��5      ,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle 5
      �� *Theme+ � 	Resources 5         yPrefix��%q   &	        �� 5   
   .R�5      �� $��Center=�66   $��$��6Q   ��6   �� *
FontSize+6Q   5      5      .��5      � .+�5   
   '  !Syncfusion.Windows.Shared.MenuAdv  $��0��5      $��0��5      $��2��5         
ExpandMode$ ExpandOnClick=�5      .@�5      +  %Syncfusion.Windows.Shared.MenuItemAdv  $
��File��5       ��5!      �� *
FontSize+.@�5"        $��
Save Image��5#       � ToolTip$( "Save an image of the graph to disk��5$      ��5%      �� *
FontSize+   Command 5&      �� *SaveImageCommand+1  +Syncfusion.Windows.Shared.MenuItemSeparator  5'      $	��140��6/   $
��Left=�6;     5(      $
��Exit��5)      $ Close the graph window��5*      ��5+      �� *
FontSize+ 5,      �� *CloseCommand+5-      5.      '   !Syncfusion.UI.Xaml.Charts.SfChart  50   
   -   5          Name$ chart��50      $��10,10q�6e   )   #Syncfusion.UI.Xaml.Charts.ChartBase   Header 6(   �� *ChartHeader+��6G   �� *
FontSize+   Palette 6t   �� *	Palette+	   	Behaviors	 52      4   .Syncfusion.UI.Xaml.Charts.ChartTooltipBehavior  
   EnableAnimation$

 True��6-      ShowDuration$
 3500��6D   4   .Syncfusion.UI.Xaml.Charts.ChartZoomPanBehavior  53         EnableMouseWheelZooming$
 True��6-      EnableDirectionalZooming$
 True��6L      EnablePanning$
 True��6l      ZoomRelativeToCursor$
 True��6�      MaximumZoomLevel$	 5.0Q�6�      ResetOnDoubleTap$
 True��6�      PrimaryAxis 55      -	   'Syncfusion.UI.Xaml.Charts.NumericalAxis	  56      )
   #Syncfusion.UI.Xaml.Charts.ChartAxis 
  Header$ Tournaments Played��6&    	  RangePadding$ Normal=�6`    
  LabelExtent$ 20Q�6w    	  StartRangeFromZero$
 True��6�   ��6B   �� *
FontSize+6�      SecondaryAxis 58      	  59      $ Total Profit��6&   $ Normal=�6Z   $ 50Q�6p   ��6<   �� *
FontSize+ 
  LabelFormat 6�   �� *YAxisLabelFormat+ �� UpdateSourceTrigger$ PropertyChanged=�   Series. 5;      *   $Syncfusion.UI.Xaml.Charts.LineSeries  /   )Syncfusion.UI.Xaml.Charts.ChartSeriesBase   XBindingPath$ Count��6C   ,   &Syncfusion.UI.Xaml.Charts.XyDataSeries   YBindingPath$ Profit��6X      ShowTooltip$
 True��6n      EnableAnimation$
 True��6�      ItemsSource 6   �� *SessionList+/   )Syncfusion.UI.Xaml.Charts.AdornmentSeries    AdornmentsInfo  5<      2   ,Syncfusion.UI.Xaml.Charts.ChartAdornmentInfo  5=      6   0Syncfusion.UI.Xaml.Charts.ChartAdornmentInfoBase!   
ShowMarker$
! True��60   "   Symbol$" Ellipse=�6B   #   SymbolInterior
# �����$   SymbolStroke
$ �   �%   HighlightOnSelection$
% True��6�   &   FontSize& 6   �� *
FontSize+6�   '   TooltipTemplate' 5?      �� 5@      .R�5A      �� ��6$   �� *Item.Description+��6F   �� *	FontColor+5B      5D      5E      5F      5G      