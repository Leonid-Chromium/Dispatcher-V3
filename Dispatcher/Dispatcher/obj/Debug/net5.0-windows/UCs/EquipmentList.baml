   M S B A M L   `   `   `  ����E  ADispatcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Dispatcher.UCsDispatcher.UCs  5      "    Dispatcher.UCs.EquipmentList   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Dispatcher.UCs     5      -   5        .��5
      �   � RowDefinitions  5   
   �� 5      $	��130 �6   �� 5      .+�5   
   �� $��1q�6   
�������$
��0 10q�68   ._�5      �  � ColumnDefinitions 5      �� 5      �� 5      �� 5      �� 5      �� 5        5      �� 5      $��30 �6$   �� 5      $��50 �6$   �� 5      .+�5      �� $��0��6   $��0��6(   $0��*Сортировка по названию��65   $��Stretch=�5      $��Center=�6%   $
��Left=�5       $��Center=�6)   �� 5!      $��1��6
   $��0��6   $.��(Фильтрация по участку��6'   $��Stretch=�5"      $��Center=�6%   $
��Left=�5#      $��Center=�6)   �� 5$      $��2��6
   $��0��6   $.��(Фильтрация по статусу��6'   $��Stretch=�5%      $��Center=�6%   $
��Left=�5&      $��Center=�6)   �� 5'      $��3��6
   $��0��6   $��
Поиск��6'   $	��150��67   $��Stretch=�5(      $��Center=�6%   $
��Left=�5)      $��Center=�6)   �� 5+      -   5         ��Name$ SortName��5+      $��0��5,      $��1��6   $��Stretch=�5-      $��Center=�6%   $
��Left=�5.      $��Center=�6)   $��5q�5/      .@�51      �� 
 � Tag$
 None��6   $��Нет��6    �� 
IsSelected$
 True��6+   �� 52      $	 ASC��6   $!��По возрастанию��6   �� 53      $
 DESC��6   $��По убыванию��6   54      �� 56      -   5        $ FilterDistrict��56      $��1��57      $��1��6   $��Stretch=�58      $��Center=�6%   $
��Left=�59      $��Center=�6)   $��5q�5:      .@�5<      �� $��Нет��6   $
 True��6    5=      �� 5?      -   5        $ FilterStatus��5?      $��2��5@      $��1��6   $��Stretch=�5A      $��Center=�6%   $
��Left=�5B      $��Center=�6)   $��5q�5C      5D      �� 5F      -   5         ��Name$ SearchTB��5F      $��3��5G      $��1��6   $��Stretch=�5H      $��Center=�6#   $
��Left=�5I      $��Center=�6'   $��5q�5J      5K      �� 5M      -   5         ��Name$ RefreshButton��5M      $��Перезагрузка��60   $��2��5N      $��4��6   $��Center=�5O      $��Center=�6"   $��Center=�5P      $��Center=�6)   5Q      5R      5S      �� 5T   
   -   5         ��Name$ ListSV��5T      $��1��5U      $	��500��5V      $
��Auto=�5W      .��5X      �� -   5         ��Name$ ListSP��5X      5Y      5Z      5[      