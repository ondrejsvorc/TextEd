   M S B A M L   `   `   `  ����A  =TextEd, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TextEdTextEd  5   	       TextEd.FindReplace   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   &localclr-namespace:TextEd     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          �Name$  findReplaceWindow��5   	    � Title$ Find / Replace��5   	   $	��510��5	   	   $	��300��5
   	   $��36(�5   	    � 
ResizeMode$ NoResize=�5   	    � ShowInTaskbar .   � Topmost .  � WindowStartupLocation$ CenterScreen=�5   	   .��5      � .+�5   
   o� $	��510��6   $
��Left=�5      $	��Top=�5      ���	#FFD2E3FF.@�5      m� -   5         m�Name$
 find��5      $��Segoe UI Light)�5      $��20(�5      $
��Find��5       m� 	IsChecked .  m� StaysOpenOnClick . $��0��5       m� 5!      -   5        $ replace��5!      $��Segoe UI Light)�5#      $��20(�5$      $��Replace��5%       . $��1��5)      5*      �� 5,   
   -   5        	 ��Name$	 lbFind��5,      $��10,92,360,0q�5-      $	��Top=�5.      $
��Left=�5/      $��Find:��50      $��Yu Gothic Medium)�51      $��18(�52      �� 53   
   -   5        $	 	lbReplace��53      $��10,134,360,0q�54      $	��Top=�55      $
��Left=�56      $��Replace:��57      $��Yu Gothic Medium)�58      $��18(�59      $y�	Collapsed=�5:      �� 5;   
   -   5        
 ��Name$
 
txtBoxFind��5;      $	��340��5<      $��30��5=      $��
144,96,0,0q�5>      $
��Left=�5?      $	��Top=�5@      $��25(�5A       �� TextWrapping$
 Wrap=�5C      �� 5D   
   -   5        $
 txtBoxReplaceWith��5D      $	��340��5E      $��30��5F      $��144,136,0,0q�5G      $
��Left=�5H      $	��Top=�5I      $��25(�5J      $
 Wrap=�5K      $y�	Collapsed=�5L      �� 5M   
   -   5         ��Name$ 
btnConfirm��5M      $	��104��5N      $��34��5O      $��200,220,0,0q�5P      $
��Left=�5Q      $	��Top=�5R      $
��Find��5T      $��24(�5U      $y�	Collapsed=�5V      �� 5W   
   -	   5         ��Name$ checkBoxCaseSensitive��5W      $��
10,238,0,0q�5X      $
��Left=�5Y      $	��Top=�5Z      $��Case-Sensitive��5[      $��13(�5\      5]      5^      