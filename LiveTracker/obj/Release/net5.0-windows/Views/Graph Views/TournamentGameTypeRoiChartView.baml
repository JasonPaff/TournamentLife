   M S B A M L   `   `   `  ���� U  QSystem.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aO8clr-namespace:System.Collections;assembly=System.RuntimeSystem.Collections  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem b ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89eGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPFSyncfusion.UI.Xaml.Charts h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager  Tournament LifeF   @Tournament_Life.Views.Graph_Views.TournamentGameTypeRoiChartView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R	 NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml   	  @d2http://schemas.microsoft.com/expression/blend/2008   	  Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006   	  a
 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf  
      Vcollections8clr-namespace:System.Collections;assembly=System.Runtime  
      ?System&clr-namespace:System;assembly=mscorlib  
      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  SchartGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPF  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    � 
ResizeMode$  	CanResize=� � Title$ Game Type Roi Graph��$	��800��$	��800��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+0  *Syncfusion.Windows.Shared.ChromelessWindow   RightHeaderItemsSource "   System.Collections.ArrayList  �� $.��(Minimum Tournaments Required For Display��3  -Syncfusion.Windows.Tools.Controls.ComboBoxAdv  $��0,0,25,0q� �� SelectedItem �� *MinimumTournaments+ � 	Resources  �� 
TargetType �� BasedOn��%   '��       �� " �� Z� ���� .R��� $��20�� �� Padding$ 4q����� ��  ��1����10����25����50����100����250����500����1000����2000��*  $Microsoft.Xaml.Behaviors.Interaction	   Triggers	 +  %Microsoft.Xaml.Behaviors.EventTrigger  
   	EventName$
 SelectionChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *FilterCommand+   CommandParameter �� *MinimumTournaments+.��� .+�'	  !Syncfusion.Windows.Shared.MenuAdv	  $��0��$��0��$��2�� 	  
ExpandMode$ ExpandOnClick=�.@�+
  %Syncfusion.Windows.Shared.MenuItemAdv
  $
��File������ *
FontSize+.@�
  $��Save Graph Image������ *
FontSize+ 
  Command �� *SaveImageCommand+
  $
��Exit������ *
FontSize+ �� *CloseCommand+'  !Syncfusion.UI.Xaml.Charts.SfChart  -     Name$ chart��$��10,10q�)  #Syncfusion.UI.Xaml.Charts.ChartBase   Header �� *ChartHeader+���� *
FontSize+   	Behaviors 4  .Syncfusion.UI.Xaml.Charts.ChartTooltipBehavior     EnableAnimation$
 True��   ShowDuration$
 1000��4  .Syncfusion.UI.Xaml.Charts.ChartZoomPanBehavior     EnableMouseWheelZooming$
 True��   EnableDirectionalZooming$
 True��   EnablePanning$
 True��   ZoomRelativeToCursor$
 True��   MaximumZoomLevel$	 5.0Q�   ResetOnDoubleTap$
 True��   PrimaryAxis ,  &Syncfusion.UI.Xaml.Charts.CategoryAxis  )  #Syncfusion.UI.Xaml.Charts.ChartAxis   Header$ Tournament Game Type��$��22(�   LabelRotationAngle$ 90Q�   LabelExtent$ 50Q�   ShowGridLines$ False��    SecondaryAxis  -  'Syncfusion.UI.Xaml.Charts.NumericalAxis  $ Return on Investment��$��22(�$ 50Q�!   LabelFormat! �� *YAxisLabelFormat+" �� UpdateSourceTrigger$" PropertyChanged=�#   Series.# ,  &Syncfusion.UI.Xaml.Charts.ColumnSeries  /  )Syncfusion.UI.Xaml.Charts.ChartSeriesBase$   XBindingPath$$ GameType��,  &Syncfusion.UI.Xaml.Charts.XyDataSeries%   YBindingPath$	% Roi��&   EnableAnimation$
& True��'   ShowTooltip$
' True��(   Spacing$( 0Q�)   SegmentSpacing$) 0Q�*   ItemsSource* �� *Tournaments+/  )Syncfusion.UI.Xaml.Charts.AdornmentSeries+   AdornmentsInfo+ 2  ,Syncfusion.UI.Xaml.Charts.ChartAdornmentInfo  6  0Syncfusion.UI.Xaml.Charts.ChartAdornmentInfoBase,   
ShowMarker$
, True��-   Symbol$- Ellipse=�.   SymbolInterior
. �����/   SymbolStroke
/ �   �0   HighlightOnSelection$
0 True��1   FontSize1 �� *
FontSize+2   TooltipTemplate2 �� .R��� ���� *Item.Description+���� *	FontColor+