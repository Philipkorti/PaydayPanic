   M S B A M L   `   `   `  ����A  =PayDay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PayDay.ViewsPayDay.Views  5          PayDay.Views.CasinoView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:PayDay.Views     5      U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5        � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $: 4../ResourceDictionarys/ButtonResourceDictionary.xaml?�6%   %    5      .��5      � ��5   
   �� 5       �� ImageSource$ \Pictures\casinospiel.jpg��6    � ColumnDefinitions 5   
   �� 5      $
��0.8* �6   �� 5      $
��0.1* �6   �� 5      $
��0.7* �6   �� 5      $
��0.2* �6   �� 5      $
��0.7* �6   �� 5      $
��0.2* �6   �� 5      $
��0.7* �6   �� 5      $��0.27* �6   �� 5      $
��0.7* �6    � RowDefinitions 5    
   �� 5!      $
��0.6* �6   �� 5"      �� 5#      $
��0.3* �6   .+�5%   
   �� $��6��6   $��30(�6v    �� TextAlignment$ Right=�6�   
������ $
��Auto��6�   $��40��6�   $	��Top=�6�   ��6$   ��  �� Path$ WinMoney� �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=���6�   �� *Color+6�   �� 5&   
   $��Bottom=�6   $
��Left=�6,   $��9��6G   ��6W   �� $ 
RoleCasino��� 5'   
   $��1��6   $��2��6   $��10,30q�6-   "  WpfAnimatedGif.ImageBehavior
   AnimatedSource
 5(      �� $ ButtonImgSourceone�$ TwoWay=�$	 PropertyChanged=��� 5)   
   $��1��6   $��4��6   $��10,30q�6-   
 5*      �� $ ButtonImgSourcetwo�$ TwoWay=�$	 PropertyChanged=��� 5+   
   $��1��6   $��6��6   $��10,30q�6-   
 5,      �� $ ButtonImgSourcethre�$ TwoWay=�$	 PropertyChanged=��� 5-   
   $��2��6   $��2��6"    �� Orientation$ 
Horizontal=�62   .+�5.      �� $��Rolls:��6   $��25(�6&   $��Center=�64   
�������6O   �� 5/      $��25(�6;   $	��200��6I   $��10,0,0,0q�6U   $��40��6g   {�6   �� $ 	CasinoRun���6s   �� $ CountRounds�$ TwoWay=�$	 PropertyChanged=�50      �� 52   
   $��2��6   $��6��6"   $ 
Horizontal=�62   .+�53      �� $��25(�6   $
��Bet:��6&   $��Center=�62   
�������6M   �� 54      $��25(�6;   $	��100��6I   $��10,0,0,0q�6U   $��40��6g   {�6   �� $ 	CasinoRun���6s   �� $ Stake�$ TwoWay=�$	 PropertyChanged=�55      56      58      