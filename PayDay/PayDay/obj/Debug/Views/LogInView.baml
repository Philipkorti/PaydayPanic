   M S B A M L   `   `   `  ����A  =PayDay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PayDay.ViewsPayDay.Views  5      5clr-namespace:PayDay.ComponentsPayDay.Components  67       PayDay.Views.LogInView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      H DBusyIndicator, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null:busyIndicator&https://github.com/Peoky/BusyIndicator  5      &localclr-namespace:PayDay.Views  5      0
componentsclr-namespace:PayDay.Components  67   ;busyIndicator1&https://github.com/Peoky/BusyIndicator  6j     � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $: 4../ResourceDictionarys/ButtonResourceDictionary.xaml?�6%   �� 5      $; 5../ResourceDictionarys/TextBoxResourceDictionary.xaml?�6%   �� 5      $= 7../ResourceDictionarys/TextBlockResourceDictionary.xaml?�6%   %    5      .��5      � .+�5   
     BusyIndicator.BusyMask  -   5          Name$ BusyIndicator��5   	      BusyContent$ Please wait...��5   	      IndicatorType$ 	Cupertino=�5   	      IsBusy 5   	   ��  �� Path$ 	IsLoading�.��5   
   �  � RowDefinitions 5      �� 5      $
��0.9* �6    �� 5      .+�5       �� 	 �� CornerRadius$	 10��6   $��2��6'   ��5!      �� 5"      .��5#      � 
 � Color$
 Bisque��6'    � Offset$	 0.0Q�66   � 5$      $
 Green��6'   $ 1Q�65   5%      5'      �� 5(      $	 10��6   $	��200��6'   $	��200��63   ��5)      �� 5*      .��5+      � $
 Bisque��6'   $	 0.3Q�66   � 5,      $
 Green��6'   $ 1Q�65   5-      ._�5/      �� $��Center=�6   .+�50      �� $��\Pictures\paydayicon.png��6   $��80��6>   �� 51      $��Payday Panic��6    $��22(�64   $��Center=�6B   52      54      �� 56      $��1��6   $��0,10,0,0q�6&   .+�57      �� $��Username��6   �� 58       �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�59      ��    SignInTextBox#��[  6   ��6E   �� $ Username� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5:      �� 5;      $	��250��6   $��2��6%   
������� J� Opacity$	 0.5Q�6C   �� 5<      $��Password��6   �� 5=      $ 
Horizontal=�6   $��Center=�66   .+�5>      +   %PayDay.Components.BindablePasswordBox  $��	Helvetica)�65   $��Light"�6L   $��22(�6�   $��Center=�6�   
�������
������ $��0q�5?   3   $	��235��6G   $	 0.5Q�6S   $��25��6a      Password 5>   _   �� $ Password�$ TwoWay=�$ PropertyChanged=�5?   a   5@      �� 5A      $	��250��6   $��2��6%   
�������$	 0.5Q�6C   �� 5B      $��Center=�6   $��
0,10,150,0q�68    d� 	IsChecked 6L   �� $ 	SaveLogIn�.��6q   
Save LogIn6}   �� 5C      $ 
Horizontal=�6   $��Center=�66   $��0,10,0,0q�6S   .+�5D      �� $��	0,20,10,0q�6    
 SignIn#��[ 60   ��6P   �� $ LogInCommand�.��6w   LogIn6~   �� 5E      #��[ 6   ��6=   �� $ RegisterCommand�.��6g   
Register6q   5F      5J      5K      5L      5M      5N      