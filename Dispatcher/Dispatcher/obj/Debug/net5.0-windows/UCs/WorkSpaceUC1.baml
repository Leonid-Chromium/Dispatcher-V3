   M S B A M L   `   `   `  ����E  ADispatcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Dispatcher.UCsDispatcher.UCs  5      !    Dispatcher.UCs.WorkSpaceUC1   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Dispatcher.UCs     5      -   5        .��5
      �   � ColumnDefinitions  5      �� 5      $
��auto �6   �� 5      .+�5      � -   5         �Name$ Sidebar��5   	   $	��300��5      $	��150��6   $��0��5       � RowDefinitions 5      �� 5      $��50 �6   �� 5      $	��200��6   �� 5      �� 5      $	��100 �6   .+�5      �� -   5         ��Name$ HideSidebarButton��5      $��Скрыть панель��6'   $��0��5      $��10q�5      5         Dispatcher.UCs.CalendarUC  5      -   5          Name$ MyCalendarUC��5      $��1��6,   � 5      
����� �$��3��6     5      �� 5      �� 5       .+�5"      �� -   5        $ 
BtnNewAuth��5"      $��0��6-   $��70��5#      $��90��6%   .��5$   2   Авторизация5&      �� 5'      -   5        $ SettingsButton��5'      $��1��6%   $��70��5(      ��6   ��  �� ElementName$ SettingsButton�� �� Path$ ActualHeight�.��5)   $   Настройки5+      5,      5-      �� 5.      -   5        $ ShowSidebarButton��5.      $��>>��6&   $��2��5/       K� ZIndex$ 2��50      $��Center=�51      $
��Left=�6    $y�	Collapsed=�52      53      �� 54      -   5         ��Name$ MyTabControle��54      $��1��55      	 �� TabStripPlacement$		 Top=�56      .@�57      �� -	   5        
 ��Name$
 SuperTab��57      $8��2Вкладка супер-пользователя��6   $��0��6C   .��58          Dispatcher.UCs.SuperUserUC  $��Stretch=�6$   $��Stretch=�6B   59      �� 5:      -
   5        $
 EquipmentTab��5:      $��Оборудование��6#   .��5;          Dispatcher.UCs.EquipmentUC  $��Center=�6   $��Stretch=�65   5<      �� 5=      -   5        $
 
BatchesTab��5=      $��Партии��6!   .��5>         Dispatcher.UCs.BatchesUC  $��Center=�6"   $	��Top=�6?   5?      �� 5@      -   5        $
 SimpleBatchesTab��5@      $!��Простые партии��6'   .��5A      '   !Dispatcher.UCs.SimpleBatchsViewer  $��Center=�6+   $��Stretch=�6H   5B      �� 5C      -   5        $
 SimpleBatchesTab2��5C      $��Вывод партий��61   .��5D      (   "Dispatcher.UCs.SimpleBatchsViewer2  $��Center=�6,   $��Stretch=�6I   5E      �� 5F      -   5        $
 SimpleScannerTab��5F      $��Сканер��60   .��5G      "   Dispatcher.UCs.SimpleScanner  $��Center=�6&   $	��Top=�6C   5H      �� 5I      -   5        $
 SimpleAddTechMap��5I      $,��&Добавление тех.карты��6'   .��5J         Dispatcher.UCs.AddTKKK  $��Center=�6    $	��Top=�6=   5K      �� 5L      -   5        $$
 MonitorNextOperationOnDistrict��5L      $)��#Близлежащие партии��65   .��5M      5	   /Dispatcher.UCs.MonitorNextOperationOnDistrictUC	  5N      �� 5O      -   5        $
 BatchsMonitor��5O      $!��Монитор партий��6$   .��5P      "
   Dispatcher.UCs.BatchsMonitor
  5Q      �� 5R      -   5        $
 
TimeSpanTI��5R      $)��#Промежуток времени��6!   .��5S         Dispatcher.UCs.TimeSpanUC  5T      5U      5V      5W      