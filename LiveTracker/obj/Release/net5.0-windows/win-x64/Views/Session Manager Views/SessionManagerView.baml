   M S B A M L   `   `   `  ���� J  FTournament Life, Version=1.0.1.2, Culture=neutral, PublicKeyToken=null?$clr-namespace:LiveTracker.ViewModelsLiveTracker.ViewModels  _4clr-namespace:LiveTracker.ViewModels.Menu_ViewModels&LiveTracker.ViewModels.Menu_ViewModels  g8clr-namespace:LiveTracker.ViewModels.Datagrid_ViewModels*LiveTracker.ViewModels.Datagrid_ViewModels  K*clr-namespace:LiveTracker.Views.Menu_ViewsLiveTracker.Views.Menu_Views  ;"clr-namespace:LiveTracker.CommandsLiveTracker.Commands  ?$clr-namespace:LiveTracker.ConvertersLiveTracker.Converters  U QSystem.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aO8clr-namespace:System.Collections;assembly=System.RuntimeSystem.Collections h dSyncfusion.SfSkinManager.WPF, Version=19.1500.0.56, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89iLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPFSyncfusion.SfSkinManager @    :LiveTracker.Views.Session_Manager_Views.SessionManagerView   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     :	viewmodel$clr-namespace:LiveTracker.ViewModels     Omenuviewmodels4clr-namespace:LiveTracker.ViewModels.Menu_ViewModels     WdatagridViewModels8clr-namespace:LiveTracker.ViewModels.Datagrid_ViewModels     @	menuviews*clr-namespace:LiveTracker.Views.Menu_Views     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ` \Syncfusion.Tools.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b	 ^Syncfusion.SfChart.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a
 ]Syncfusion.SfGrid.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89b ^Syncfusion.SfInput.Wpf, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89k gSyncfusion.SfRichTextBoxAdv.WPF, Version=19.1500.0.55, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89>
syncfusion!http://schemas.syncfusion.com/wpf   	 
    =commands"clr-namespace:LiveTracker.Commands   	 
    c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  .command http://www.galasoft.ch/mvvmlight  5
converters$clr-namespace:LiveTracker.Converters  Jcollections8clr-namespace:System.Collections;assembly=System.Runtime  _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  asyncfusionskinLclr-namespace:Syncfusion.SfSkinManager;assembly=Syncfusion.SfSkinManager.WPF    � WindowStartupLocation$  CenterScreen=� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�,  &Syncfusion.SfSkinManager.SfSkinManager   VisualStyle �� *Theme+ +� FocusedElement ��  �� ElementName$ SessionName�� � Title �� *Title+.���  � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto � � RowDefinitions �� $��5 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��1��$��0,37,0,0q�$��1��.+��� $	��Add��$��26��$	��5,5q�$	��5,5q�$��Add Tournament(s)��	 � ToolTip$-	 'Add selected template(s) to the session������ *
FontSize+���� 1   +LiveTracker.Commands.SessionManagerCommands
   MenuItemCommand �� Source# Z
 �� $��Remove��$��26��$	��5,5q�$	��5,5q�$��Remove Tournament(s)��$2	 ,Remove selected template(s) from the session������ *
FontSize+���� # Z
 �� $��Clear��$��26��$	��5,5q�$	��5,5q�$��Clear Selected��$4	 .Deselect all the selected tournament templates������ *
FontSize+���� # Z
 �� $��1q��� $	��New��$��26��$	��5,5q�$	��5,5q�$��New Session��$	 Start a new blank session������ *
FontSize+���� # Z
  �� $ SessionName���� $
��Edit��$��26��$	��5,5q�$	��5,5q�$��Edit Session��$	 Edit an existing session������ *
FontSize+���� # Z
 �� $
��Copy��$��26��$	��5,5q�$	��5,5q�$��Copy Session��$(	 "Make a copy of an existing session������ *
FontSize+���� # Z
 �� $��Delete��$��26��$	��5,5q�$	��5,5q�$��Delete Session��$ 	 Delete an existing session������ *
FontSize+���� # Z
 �� $��1q��� $
��Save��$��26��$	��5,5q�$	��5,5q�$
��Save��$	 Save the session������ *
FontSize+���� # Z
 �� $��Close��$��26��$	��5,5q�$	��5,5q�$��Close��$	 Close window������ *
FontSize+���� # Z
 �� $��1��$��2��$��0��.+��� $��Tournament Templates��$��Center=�$��0,4,0,0q����� *
FontSize+�� $	��300��$	��800��$	��5,5q�$	��5,5q�$��1q� �� SelectionMode$ Multiple=����� *TemplateList+ �� NotifyOnSourceUpdated$
 True�� �� Mode$ TwoWay=����� *
FontSize+���� .R��� ���� *DisplayString+����  �� 
TargetType" ��.���  �� Property �@��
IsSelected �� Value �� $ TwoWay=� �� Path$ 
IsSelected���  �@�ToolTip �� *Description+�� $��1��$��2��$��2��.+���  �� Orientation$ 
Horizontal=�.+��� $��Session Name��$��0,4,0,0q�$	 Session name������ *
FontSize+�� -    ��Name$ SessionName��$	��200��$��Center=�$	 Enter your session's name������ *SessionName+���� *
FontSize+�� $	��300��$	��800��$	��5,5q�$	��5,5q�$��1q�$ Multiple=����� *SessionList+$
 True��$ TwoWay=����� *
FontSize+���� .R��� ���� *DisplayString+���� " ��.���  �@��
IsSelected �� $ TwoWay=�$ 
IsSelected���  �@�ToolTip �� *Description+