   M S B A M L   `   `   `  ����E  ADispatcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Dispatcher.UCsDispatcher.UCs  5      %    Dispatcher.UCs.ConfigurationUC2   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Dispatcher.UCs     5      -   5        .��5
      �   � ColumnDefinitions  5      �� 5      $	��150 �6   �� 5       � RowDefinitions 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5      �� $��Конфигурация��6
   $��0��5      $��0��6   �� 5      -   5         ��Name$ ConfigurationCB��5      $��0��5       $��1��6    $��5q�60    �� 
IsEditable$
 True��5#      �� 5%      $��SQLConnectionString��6
   $��1��5&      $��0��6   �� 5'      -   5         ��Name$ SQLConnectionStringTB��5'      $��1��5(      $��1��6   $��5q�6/   �� 5*      $��	Server IP��6
   $��2��5+      $��0��6   �� 5,      -   5        $ 
serverIpTB��5,      $��2��5-      $��1��6   $��5q�6/   �� 5/      $��Server port��6
   $��3��50      $��0��6   �� 51      -   5        $ serverPortTB��51      $��3��52      $��1��6   $��5q�6/   �� 54      $��Net path��6
   $��4��55      $��0��6   �� 56      -   5        $ 	netPathTB��56      $��4��57      $��1��6   $��5q�6/   �� 59      $��District��6
   $��5��5:      $��0��6   �� 5;      -   5        $ 
districtTB��5;      $��5��5<      $��1��6   $��5q�6/   �� 5>      $
��Room��6
   $��6��5?      $��0��6   �� 5@      -   5        $ roomTB��5@      $��6��5A      $��1��6   $��5q�6/   �� 5C      $��
Theme path��6
   $��7��5D      $��0��6   �� 5E      -	   5        $ themePathTB��5E      $��7��5F      $��1��6   $��5q�6/   �� 5H      $��Theme��6
   $��8��5I      $��0��6   �� 5J      -
   5        $ themeTB��5J      $��8��5K      $��1��6   $��5q�6/   �� 5M      $��Assets path��6
   $��9��5N      $��0��6   �� 5O      -   5        $ assetsPathTB��5O      $��9��5P      $��1��6   $��5q�6/   �� 5R      $��Log path��6
   $��10��5S      $��0��6   �� 5T      -   5        $ 	logPathTB��5T      $��10��5U      $��1��6    $��5q�60   � 5W      $��11��6	   $��2��6     5X      �� 5Y      �� 5Z      �� 5[      .+�5]      �� -   5         ��Name$ delConfButton��5]      $.��(Удалить  конфигурацию��6#   $��0��6_   $��5q�6o   �� 5^      -   5        $ saveConfButton��5^      $1��+Сохранить конфигурацию��6$   $��1��6b   $��5q�6r   �� 5_      -   5        $ addConfButton��5_      $:��4Добавить новую конфигурацию��6#   $��2��6e   $��5q�6u   5`      5a      5b      