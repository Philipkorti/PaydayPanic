   M S B A M L   `   `   `  ����A  =PayDay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PayDay.ViewsPayDay.Views  5      5clr-namespace:PayDay.ViewModelsPayDay.ViewModels  5      clr-namespace:PayDayPayDay  5          PayDay.Views.L   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:PayDay.Views     5      1Modelclr-namespace:PayDay.ViewModels     5      *	Converterclr-namespace:PayDay     5        � 	Resources  5       �� MergedDictionaries �� Source   PayDay.PolygonConverter   PolygonConverter�� 5   
    5      �� 5      $* $../ResourceDictionarys/DataGrid.xaml?�6%   �� 5      $: 4../ResourceDictionarys/ButtonResourceDictionary.xaml?�6%   �� 5      $; 5../ResourceDictionarys/TextBoxResourceDictionary.xaml?�6%   �� 5      $; 5../ResourceDictionarys/PolygonResourceDictionary.xaml?�6%   �� 5      $> 8../ResourceDictionarys/TabControlResourceDictionary.xaml?�6%   %   &	          5      6)   5      .��5      � .+�5   
   �� .@�5      �� $��Games��6   .��5      �  � RowDefinitions 5      �� 5      $
��0.1* �6(   �� 5      .+�5      �� $
��Shop��6    $��Center=�6,   $��20(�6I   �� 5       $��1��6#   $
��Auto=�60   .��5!      �� ��6'   �� *	ShopItems+ �� UpdateSourceTrigger$ PropertyChanged=���5$      �� 5%      .R�5&   "   A� 60   5(      ��5,      �� 5-   "   .R�5.   &   �� 
���   �$��1q�6A   $��10q�6U   ._�5/   *   �  50   .   �� 51   2   �� 52   2   .+�54   .   �� .+�55   2   �� $
��Auto��6V   $
��Auto��6c   ��68   �� *
PictureURL+6s   57   /   �� 58   .   $��1��69    �� Orientation$ 
Horizontal=�6F   $��Bottom=�6_   $��Center=�6z   .+�59   2   �� 
������� �� CornerRadius$	 100��6M   $��1q�6`   ._�5:   6   ��   	BuyButton#��[ 6=   $	��Buy��6�   ��6`   ��  �� Path$  DataContext.BuyGameCommand� �� RelativeSource �� 	 �� AncestorType"	 >���6�   �� *ItemID+6
  5;   3   �� 5<   2   $��Center=�6=   .+�5=   6   �� $ 
Horizontal=�6A   .+�5>   :   �� $��Center=�6D   $��Center=�6a   Lager:6�   �� 5?   :   $��Center=�6�   $��10,0,0,0q�6�   ��6D   �� *	InStock+$ PropertyChanged=�6�   5@   7   �� 5A   6   $ 
Horizontal=�6A   .+�5B   :   �� $��Center=�6D   $��Center=�6a   Price:6�   �� 5C   :   $��Center=�6�   $��10,0,0,0q�6�   ��6D   �� *Price+$ PropertyChanged=�6�   5D   7   5E   3   �� 5G   2     
SellButton#��[ 69   $
��Sell��6�   ��6]   �� $! DataContext.SellGameCommand� �� "	 >���6�   �� *ItemID+5H   /   5J   +   5K   '   5M   #   5O      5P      5Q      5R      �� 5S      $
��Gold��6   .��5T      �  5U      �� 5V      $
��0.5* �6(   �� 5W      $
��0.5* �6(   �� 5X      $
��0.5* �6(   �� 5Y      $
��0.5* �6(   �� 5Z      $
��0.5* �6(   �� 5[      $
��0.5* �6(   �� 5\      
 � ColumnDefinitions
 5^      �� 5_      �� 5`      $
��0.1* �6+   �� 5a      $
��0.2* �6+   .+�5c      �� $��5q�6   $��0��6(   $��5��65   $��1q�6F   
������� J� SnapsToDevicePixels . ._�5d      �� }�. 
����ح�.+�5e      � 
���   �$��4��65   $��	2.5,5,0,2q�6I   
�������  5f   "    �� 
TargetType �� Property �� Value `� 	Converter��%3  '�       7   �� 5g   &   " �6,   .�5h   *   ��  �@�Points 5i   .   `� 5j   2   8   .1�5k   6   �� $ ProcessorTime.Values�6>   �� 5l   6   $ ActualWidth�6>    6Q   �� $	 CanvasL��� 5m   6   $ ActualHeight�6>    6R   �� $	 CanvasL�5n   3   5p   +   5q   '   5r   #   5s      5t      5u      �� 5v      $��1��6    $��4��60   $��25(�6=   $	��Top=�6K   $��Center=�6c   206�   �� 5w      $��1��6    $��3��60   $��25(�6=   $	��Top=�6K   $��Center=�6c   406�   �� 5x      $��1��6    $��2��60   $��25(�6=   $	��Top=�6K   $��Center=�6c   606�   �� 5y      $��1��6    $��1��60   $��25(�6=   $	��Top=�6K   $��Center=�6c   806�   �� 5z      $��1��6    $��0��60   $��25(�6=   $	��Top=�6K   $��Center=�6c   1006�   �� 5{      $��2��6!   $��2��61   $��2��6>   .+�5|      �� $��25(�6$   $��Center=�62   
Gold Price6[   �� 5}      $��30(�6$   $��Center=�6�   ��62   �� $ Game.GoldPrice� �� Mode$ TwoWay=�$ PropertyChanged=���6�   �� *Color+$ PropertyChanged=�6�   5~      �� 5      $��6��6!   $ 
Horizontal=�6.   $��Center=�6G   $��Center=�6b   $��3��6   .+�5�      �� .+�5�      �� #��[ 6%   $	��200��6H   ��6T   �� $ 
BuyCommand�.��6x   Buy6}   �� 5�      #��[ 6%   $	��200��6H   ��6T   �� $ BuyMaxCommand�.��6{   	Max Buy6�   5�      �� 5�      $��60��6!   $ 10��6-   
���   �$��1q�6S   $��Center=�6g   ._�5�      ��   Gold#��[ 6&   ��6D   �� $ 	GoldCount�$ TwoWay=�$ PropertyChanged=�6�   5�      �� 5�      .+�5�      �� #��[ 6%   $��Center=�6I   $	��200��6m   ��6y   �� $ SellCommand�.��6�   Sell6�   �� 5�      #��[ 6%   $��Center=�6I   $	��200��6m   ��6y   �� $ SellMaxCommand�.��6�   
Max Sell6�   5�      5�      5�      5�      5�      5�      5�      