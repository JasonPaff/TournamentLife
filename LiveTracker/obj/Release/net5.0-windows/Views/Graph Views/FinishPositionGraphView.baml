   M S B A M L   `   `   `  ���� b  ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89eGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPFSyncfusion.UI.Xaml.Charts  h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager U QSystem.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aO8clr-namespace:System.Collections;assembly=System.RuntimeSystem.Collections O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  Tournament Life;   5LiveTracker.Views.Graph_Views.FinishPositionGraphView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R	 NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml   	  @d2http://schemas.microsoft.com/expression/blend/2008   	  Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006   	  a
 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   
      _chartGclr-namespace:Syncfusion.UI.Xaml.Charts;assembly=Syncfusion.SfChart.WPF   
      msyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF   
      Vcollections8clr-namespace:System.Collections;assembly=System.Runtime   
      ?System&clr-namespace:System;assembly=mscorlib   
      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors    � 
ResizeMode$  	CanResize=� � Title$ Finish Position Graph��$	��800��$	��800��,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+0  *Syncfusion.Windows.Shared.ChromelessWindow   RightHeaderItemsSource "  System.Collections.ArrayList  �� $"��Maximum Positions To Display��3  -Syncfusion.Windows.Tools.Controls.ComboBoxAdv  $��0,0,25,0q� �� SelectedItem �� *Max+ � 	Resources  �� 
TargetType �� BasedOn��%   '��       �� " �� Z� ���� .R��� $��20�� �� Padding$ 4q����� ��  ��6����9����18����27����36����45����100����250����500����1000����2000����5000��*  $Microsoft.Xaml.Behaviors.Interaction	   Triggers	 +  %Microsoft.Xaml.Behaviors.EventTrigger  
   	EventName$
 SelectionChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *FilterCommand+   CommandParameter �� *Max+.��� .+�'	  !Syncfusion.Windows.Shared.MenuAdv	  $��0��$��0��$��2�� 	  
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
FontSize+ �� *CloseCommand+'   !Syncfusion.UI.Xaml.Charts.SfChart  -     Name$ chart��$��10,10q�)   #Syncfusion.UI.Xaml.Charts.ChartBase   Header �� *ChartHeader+���� *
FontSize+   	Behaviors 4   .Syncfusion.UI.Xaml.Charts.ChartTooltipBehavior     EnableAnimation$
 True��   ShowDuration$
 1000��4   .Syncfusion.UI.Xaml.Charts.ChartZoomPanBehavior     EnableMouseWheelZooming$
 True��   EnableDirectionalZooming$
 True��   EnablePanning$
 True��   ZoomRelativeToCursor$
 True��   MaximumZoomLevel$	 5.0Q�   ResetOnDoubleTap$
 True��   PrimaryAxis -   'Syncfusion.UI.Xaml.Charts.NumericalAxis  )   #Syncfusion.UI.Xaml.Charts.ChartAxis   Header$ Finish Position��$��22(�   LabelExtent$ 50Q�   SecondaryAxis   $ Number of Times��$��22(�$ 50Q�   Series. ,   &Syncfusion.UI.Xaml.Charts.ColumnSeries  /   )Syncfusion.UI.Xaml.Charts.ChartSeriesBase    XBindingPath$  FinishPosition��,   &Syncfusion.UI.Xaml.Charts.XyDataSeries!   YBindingPath$! FinishPositionCount��"   ShowTooltip$
" True��#   EnableAnimation$
# True��$   Spacing$	$ 0.5Q�%   ItemsSource% �� *	Positions+&   TooltipTemplate& �� .R��� ���� *Item.Description+���� *	FontColor+