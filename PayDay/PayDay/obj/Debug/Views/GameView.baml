   M S B A M L   `   `   `  ����A  =PayDay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PayDay.ViewsPayDay.Views  5          PayDay.Views.GameView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:PayDay.Views     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $: 4../ResourceDictionarys/ButtonResourceDictionary.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.1* �6   �� 5      .+�5   
   �  � ColumnDefinitions 5      �� 5      $
��0.5* �6#   �� 5      �� 5      �� 5      �� 5      .+�5      ��    
HomeButton#��[  6   
������ 
������ $��0q�6�   ��69   ��  �� Path$ BackMapCommand�.��5      �� $��\Pictures\home.png��6   5      �� 5       $��1��6   $��Center=�6(   $��Center=�6E   $��20(�6`   ��6n   �� $ 	TimerText� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5!      $��2��6    �� Orientation$ 
Horizontal=�6)   .+�5"      �� $��20(�6   $��Center=�6*   $��Center=�6G   $��0,0,20,0q�6b   Gold:6{   �� 5#      $��Center=�6   $��Center=�69   $��20(�6T   ��6b   �� $ 	Game.Gold�$ TwoWay=�$ PropertyChanged=�5$      �� 5%      $��3��6   $��Center=�6)   $ 
Horizontal=�6D   $��Right=�6]   .+�5&      �� $��/Pictures/coins.png��6   �� 5'      $��Right=�6   	 �� TextAlignment$	 Center=�68   $��Center=�6O   $��20(�6j   ��6x   �� $ 
Game.Money�$ TwoWay=�$ PropertyChanged=�5(      5)      �� 5*      $��Center=�6   $��4��65   $��Center=�6E   $��20(�6`   ��6n   �� $ Game.Username�$ TwoWay=�$ PropertyChanged=�5+      �� 5,   
   $��1��6   ��6&   �� $ CurrentView�5-      5.      