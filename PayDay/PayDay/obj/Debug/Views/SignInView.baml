   M S B A M L   `   `   `  ����A  =PayDay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PayDay.ViewsPayDay.Views  5      5clr-namespace:PayDay.ComponentsPayDay.Components  67       PayDay.Views.SignInView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:PayDay.Views     5      6
componentsclr-namespace:PayDay.Components     67   H DBusyIndicator, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null:busyIndicator&https://github.com/Peoky/BusyIndicator  5        � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $: 4../ResourceDictionarys/ButtonResourceDictionary.xaml?�6%   �� 5      $; 5../ResourceDictionarys/TextBoxResourceDictionary.xaml?�6%   �� 5      $= 7../ResourceDictionarys/TextBlockResourceDictionary.xaml?�6%   %    5      .��5      � .+�5   
     BusyIndicator.BusyMask  -   5          Name$ BusyIndicator��5   	      BusyContent$ Please wait...��5   	      IndicatorType$ 	Cupertino=�5   	      IsBusy 5   	   ��  �� Path$ 	IsLoading�.��5      �  � RowDefinitions 5      �� 5      $
��0.9* �6    �� 5      .+�5       �� 	 �� CornerRadius$	 10��6   $��2��6'   ��5!      �� 5"      .��5#      � 
 � Color$
 Bisque��6'    � Offset$	 0.0Q�66   � 5$      $
 Green��6'   $ 1Q�65   5%      5'      �� 5(      $	 10��6   $	��200��6'   $	��200��63   ��5)      �� 5*      .��5+      � $
 Bisque��6'   $	 0.3Q�66   � 5,      $
 Green��6'   $ 1Q�65   5-      ._�5/      �� $��Center=�6   .+�50      �� $��\Pictures\paydayicon.png��6   $��80��6>   �� 51      $��Payday Panic��6    $��22(�64   $��Center=�6B   52      53      �� 55      $��1��6   $��0,20,0,0q�6&   .+�56      �� $��	Usernaame��6   �� 57       �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�58      �� $	��210��6      SignInTextBox#��[  6*   ��6R   �� $ Username� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 59      ��6   �� $ UsernameCount��� 5:      $��Center=�6   $��/��67   �� 5;      ��6   �� $ 
UserLength�5<      �� 5=      $	��280��6   $��2��6%   
������� J� Opacity$	 0.5Q�6C   �� 5>      $��	Paassword��6   �� 5?      $ 
Horizontal=�6   $��Center=�66   .+�5@      +   %PayDay.Components.BindablePasswordBox  $��	Helvetica)�65   $��Light"�6L   $��22(�6�   $��Center=�6�   
�������
������ $��0q�5A   3   $	��275��6G   $	 0.5Q�6S   $��25��6a      Password 5@   _   �� $ Password�$ TwoWay=�$ PropertyChanged=�5A   a   5B      �� 5C      $	��280��6   $��2��6%   
�������$	 0.5Q�6C   �� 5D      $��Center=�6    
 SignIn#��[ 6:   ��6Z   �� $ SignIn�.��6z   
Register6�   5E      5F      5G      5H      5I      