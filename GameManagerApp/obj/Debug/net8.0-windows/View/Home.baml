   M S B A M L   `   `   `  ����I  EGameManagerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:GameManagerApp.ViewModelsGameManagerApp.ViewModels  5      ?$clr-namespace:GameManagerApp.ConvertGameManagerApp.Convert  5          GameManagerApp.View.Home   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6vm'clr-namespace:GameManagerApp.ViewModels     5      6local$clr-namespace:GameManagerApp.Convert     5        � 	Resources  5
      7   1GameManagerApp.Convert.IconToImageSourceConverter   IconConverter��%   &	          5   
   6+   5       � DataContext 5      &   GameManagerApp.ViewModels.HomeVM  5   
   .��5      � .+�5   
   ��  �� CornerRadius$ 	0,20,20,0��6   ��5      �� 5       �� ImageSource$ /Images/home_page.png��6    j� Stretch$
 None=�5      5      � 5   
    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5!      �� 
������ $	��100��6s   $��0q�5"      ��5!      �� *Games+ �� SelectedItem 64   �� *SelectedGame+��5#      �� 5$      .R�5%      �� ��6!   �� *DataContext.GameInfoCommand+ �� RelativeSource ��  �� AncestorType" ����5&      �� .��5'      �� 	 �� Orientation$	 
Horizontal=�6)   .+�5(   "   �� $��50��6(   $��50��63   ��6?   �� *
GameIcon+
 �� 	Converter#
 [  5*      5+      5,      5.      � 53      $��1��6   .+�55      �� $��1��6!   ��61   �� *CurrentView+56      57      59      � 5:   
   .+�5;      �� $��50��6   $	��100��5<      $
��Left=�5=      $��Center=�5>      $��1��5@      ��5?      �� *AddGameCommand+.��5A      �� $��AddGame��6   5B      �� 5C      $��50��6   $	��100��5D      $
��Left=�5E      $	��Top=�5F      $��	0,325,0,0q�5G   9   ��6   �� *OpenGameCommand+��5H      �� *SelectedGame+.��5I      �� $��OpenGame��6   5J      5L      5M      5N      