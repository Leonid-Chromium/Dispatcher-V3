   M S B A M L   `   `   `  ����E  ADispatcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Dispatcher.UCsDispatcher.UCs  5          Dispatcher.UCs.RoleSUC   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Dispatcher.UCs     5      -   5        .��5
      � .+�5      �� $��0��6   $
��Auto=�6   .��5      �� .+�5      �� $	��300��6   $��Роли��6   $��10q�6-   .��5      �   � RowDefinitions  5      �� 5   	   �� 5   	   �� 5   	   �� 5   	   �� 5   	   �� 5   	    � ColumnDefinitions 5      �� 5   	   $	��200 �6   �� 5   	   .+�5      �� $4��.Выбор среди существующих��6   $��0��5   
   $��0��6   $
��Left=�5   
   $��Center=�6%   �� 5      -   5         ��Name$ RoleCB��5      $��0��5    
   $��1��6   $��Stretch=�5!   
   $��Center=�6(   $
��Left=�5"   
   $��Center=�6,   $��5q�5#   
   5$   
   �� 5&      $)��#Идентификатор роли��6   $��1��5'   
   $��0��6   $
��Left=�5(   
   $��Center=�6%   �� 5)      -   5         ��Name$ RoleIdTB��5)      $��1��5*   	   $��1��6   $��Stretch=�5+   	   $��Center=�6'   $
��Left=�5,   	   $��Center=�6+   $��5q�5-   	    �� 
IsReadOnly$
 True��6   �� 5/      $��Имя роли��6   $��2��50   
   $��0��6   $
��Left=�51   
   $��Center=�6%   �� 52      -   5        $ 
RoleNameTB��52      $��2��53      $��1��6   $��Stretch=�54      $��Center=�6&   $
��Left=�55      $��Center=�6*   $��5q�56      �� 58      $��Пароль роли��6   $��3��59   
   $��0��6   $
��Left=�5:   
   $��Center=�6%   �� 5;      -   5        $ RolePasswordTB��5;      $��3��5<      $��1��6   $��Stretch=�5=      $��Center=�6&   $
��Left=�5>      $��Center=�6*   $��5q�5?      5@   	   �� 5B      $��Хэш роли��6   $��4��5C   
   $��0��6   $
��Left=�5D   
   $��Center=�6%   �� 5E      -   5        $ 
RoleHashTB��5E      $��4��5F      $��1��6   $��Stretch=�5G      $��Center=�6&   $
��Left=�5H      $��Center=�6*   $��5q�5I      � 5K      $��5��6   $��2��6    5L   	   �� 5M   
   �� 5N   
   �� 5O   
   .+�5R   	   �� -   5         ��Name$ 	AddButton��5R      $!��Добавить новую��6#   $��0��5S   	   $��5q�5T   	   5U   	   �� 5W   	   -   5        $ 
EditButton��5W      $'��!Изменить Выбраную��6$   $��1��5X   	   $��5q�5Y   	   5Z   	   �� 5\   	   -	   5        $ DeleteButton��5\      $%��Удалить выбраную��6&   $��2��5]   	   $��5q�5^   	   5_   	   5`   	   5a      5b      5c      5d      5e      5f      