   M S B A M L   `   `   `  ����D  @SnakeGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SnakeGame	SnakeGame  5   	       SnakeGame.SnakeGameWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:SnakeGame     5   	   -   5          � Title$  SnakeGameWindow��5   	    � SizeToContent$ WidthAndHeight=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $��15 �6   6,   �� 5      $
��Auto �6   6.   �� 5      $��15 �6   6,    � RowDefinitions 5   
   �� 5      $��15 �6   6*   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6-   �� 5      $��15 �6   6*   .+�5   
   �� 
 �� Top$ 5��6    �� Left$ 5��6    $��5q�5      
���/kU�$��1��5      $��2��6%   ._�5      �� -   5         ��Name$ 	GameField��5      $
}�True��5       $	��400��5!      $	��400��6   5#      5$      �� 5'   
   $��1��6   $��1��6   $��40��6.   .+�5(      �� $��1��6   $��1��6&   .+�5)      �� $��Score: ��6   �� 5*      $��Difficulty: ��6   5+      5,      5/      50      