   M S B A M L   `   `   `  ����I  EGameManagerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GameManagerApp.ViewModelsGameManagerApp.ViewModels  5      ?$clr-namespace:GameManagerApp.ConvertGameManagerApp.Convert  5      "    GameManagerApp.View.GameInfo   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6vm'clr-namespace:GameManagerApp.ViewModels     5      6local$clr-namespace:GameManagerApp.Convert     5         
Page_Style#��[  5
        � 	Resources  5      1   +GameManagerApp.Convert.RunningTimeConverter  RunningTimeConverter��%   &	         5   
   6%   5      .��5      � .+�5   
   ��  �� CornerRadius$ 	0,20,20,0��6   ��5      �� 5       �� ImageSource$ 	/back.png��6    j� Stretch$
 Fill=�5      5      ]� 5   
   .+�5      �� $
��Left=�6   $��60��6/   ���#2E333A5      � 5!      .+�5#      ]� $	��Top=�6   $��60��61   $	��Top=�5$      ���#23272A5'      � 5)      $��0,60,0,0q�6   .+�5*      �� $
��Left=�6!   $��Center=�6<   $��49,0,0,0q�6W   .+�5,      �� $��RunningTime��6$   
�������$��14(�6J   $��60,60,60,10q�6X   �� 5-      
���   �$��16(�6T   $��59��6b   $	��124��6n   ��6$   �� *RunningTime+6n   5.      �� 52      $
��Left=�6!   $	��Top=�6<   $��212,147,0,0q�6T   $	��340��6i   .+�54      �� $��运行时段��6$   
�������$��14(�6C   $��60,60,60,10q�6Q   �� 55      $��Running Time:��6$   
�������$��14(�6L   $��60,60,60,10q�6Z   �� 56      
���   �$��16(�67   $��52��6E   $	��198��6Q   ��57      `� 58   "    `� 	Converter# [ 6/   .1�59   &   ��  �� Path$ 	StartTime�6.   �� 5:   &   $ EndTime�6.   5;   #   5=      5>      5?      5A      5B      5D      5E      