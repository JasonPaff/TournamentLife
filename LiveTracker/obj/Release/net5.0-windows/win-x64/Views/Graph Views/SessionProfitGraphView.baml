   M S B A M L   `   `   `  ���� b  ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89eGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPFSyncfusion.UI.Xaml.Charts  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  Tournament Life>   8Tournament_Life.Views.Graph_Views.SessionProfitGraphView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a	 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf     	 
   _chartGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPF     	 
   msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF     	 
     � Title$  Sessions Profit Graph��$	��800��$
��1200��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ � 	Resources    yPrefix��%B   &	        �� .R��� $��Center=�$��$������ *
FontSize+.��� .+�' 
 !Syncfusion.Windows.Shared.MenuAdv  $��0��$��0��$��2��   
ExpandMode$ ExpandOnClick=�.@�+ 
 %Syncfusion.Windows.Shared.MenuItemAdv  $
��File������ *
FontSize+.@�  $��Save Graph Image������ *
FontSize+   Command �� *SaveImageCommand+  $
��Exit������ *
FontSize+ �� *CloseCommand+'   !Syncfusion.UI.Xaml.Charts.SfChart  -     Name$ chart��$��10,10q�)   #Syncfusion.UI.Xaml.Charts.ChartBase   Header �� *ChartHeader+���� *
FontSize+   Palette �� *	Palette+   	Behaviors 4   .Syncfusion.UI.Xaml.Charts.ChartTooltipBehavior  	   EnableAnimation$
	 True��
   ShowDuration$

 3500��4   .Syncfusion.UI.Xaml.Charts.ChartZoomPanBehavior     EnableMouseWheelZooming$
 True��   EnableDirectionalZooming$
 True��   EnablePanning$
 True��   ZoomRelativeToCursor$
 True��   MaximumZoomLevel$	 5.0Q�   ResetOnDoubleTap$
 True��   PrimaryAxis -   'Syncfusion.UI.Xaml.Charts.NumericalAxis  )	   #Syncfusion.UI.Xaml.Charts.ChartAxis 	  Header$ Tournaments Played��   RangePadding$ Normal=� 	  LabelExtent$ 20Q����� *
FontSize+   SecondaryAxis   $ Total Profit��$ Normal=�$ 50Q� 	  LabelFormat$ $##.00������ *
FontSize+   Series. ,
   &Syncfusion.UI.Xaml.Charts.SplineSeries
  /   )Syncfusion.UI.Xaml.Charts.ChartSeriesBase   XBindingPath$ Count��,   &Syncfusion.UI.Xaml.Charts.XyDataSeries   YBindingPath$ Profit��   ShowTooltip$
 True��   EnableAnimation$
 True�� 
  
SplineType$ Cardinal=�   ItemsSource �� *SessionList+/   )Syncfusion.UI.Xaml.Charts.AdornmentSeries   AdornmentsInfo 2   ,Syncfusion.UI.Xaml.Charts.ChartAdornmentInfo  6   0Syncfusion.UI.Xaml.Charts.ChartAdornmentInfoBase   
ShowMarker$
 True��    Symbol$  Ellipse=�!   SymbolInterior
! �����"   SymbolStroke
" �   �#   HighlightOnSelection$
# True��$   FontSize$ �� *
FontSize+%   TooltipTemplate% �� .R��� ���� *Item.Description+���� *	FontColor+