   M S B A M L   `   `   `  ����T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:WVA_Compulink_IntegrationWVA_Compulink_Integration  5   	   [2clr-namespace:WVA_Compulink_Integration.ViewModels$WVA_Compulink_Integration.ViewModels  5   	   Q-clr-namespace:WVA_Compulink_Integration.ViewsWVA_Compulink_Integration.Views  5   	   0    *WVA_Compulink_Integration.Views.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:WVA_Compulink_Integration     5   	   I
viewmodels2clr-namespace:WVA_Compulink_Integration.ViewModels     5   	   ?views-clr-namespace:WVA_Compulink_Integration.Views     5   	   -   5        $	��650��5
   	   $
��1200��5   	   $
��1000��5   	   $	��550��5   	     � WindowStartupLocation$  CenterScreen=�5   	    � 
ResizeMode$ 	CanResize=�5   	    � Icon$H B/WVA_Compulink_Integration;component/Resources/wva_square_logo.jpg��5   	    � 	Resources 5       �� 
TargetType   	TabButton �� Property �� Value �� 
TargetType �Name	 ��Name
 �� CornerRadius 
� GradientOrigin �� RelativeTransform �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y � Offset  Fiery_Orange_Color � Color �� Triggers P� Property P� Value �� 
TargetName �� DataType:   4WVA_Compulink_Integration.ViewModels.OrdersViewModel0   *WVA_Compulink_Integration.Views.OrdersView � DataContext �� ElementName<   6WVA_Compulink_Integration.ViewModels.SettingsViewModel2   ,WVA_Compulink_Integration.Views.SettingsView��%�  &	        7  7  7  (�� [    5s   3   *, +)(�� �    5w   5   *, +)�� 5   
   " ��6   .�5      ��  � ��
 �����6+   �� 5       �@��TextAlignment$ Center=�68   �� 5       � ��$ 
Sitka Text)�6+   �� 5       � ��$ 36(�6)   �� 5       � ��$ Center=�64   �� 5       � ��$	 150��6&   �� 5       � ��$ 0,0,0,0q�6'   �� 5       � ��ButtonTemplate�� 5       �� 5!      " ��6&   .R�5#      � $
 grid��6   .+�5$      �� $	 border��6%   $
 3,3,3,3��5%   %   
���   �$��1.6,1.9,2.9,2.9q�5'   %   ��5(   "   
� 5)   &   $ 0.496,1.052&�6:    5*   *   V� 5+   .   .~�5,   2   �� $	 0.6Q�6A   $	 0.6Q�5-   A   $	 1.5Q�5.   A   $	 1.5Q�5/   A   T� 50   2   $
 0.02Q�6E   $
 0.75Q�6N   51   /   .��53   *   � $ 1Q�67   8   6B   � 54   *   $	 0.3Q�67   8  6D   55   '   ._�57   "   �� $��Center=�63   $��Center=�58   3   $��Regular"�59   3   5:   #   5;      5<       5?      P� 5B       �@J�IsMouseOver . . �5C   "   ��  � ��
 �   �6?   �� 5D   "   $ border��6?    � �� 5E   &   
� 5F   *   $ 0.496,1.052&�6>    5G   .   V� 5H   2   .~�5I   6   �� $	 0.6Q�6E   $	 0.6Q�5J   A   $	 1.5Q�5K   A   $	 1.5Q�5L   A   T� 5M   6   $
 0.02Q�6I   $
 0.75Q�6R   5N   3   .��5P   .   � 8  $ 1Q�6g   � 5Q   .   $ White��6;   $	 0.3Q�6I   5R   +   5T   #   5U      P� 5X       � �� . . �5Y   "   ��  � ��
 �   �6?   �� 5Z   "   $ border��6?    � �� 5[   &   
� 5\   *   $ 0.496,1.052&�6>    5]   .   V� 5^   2   .~�5_   6   �� $	 0.5Q�6E   $	 0.5Q�5`   E   $ 2Q�5a   E   $ 2Q�5b   E   T� 5c   6   $
 0.02Q�6I   $	 0.9Q�6R   5d   3   .��5f   .   � $ White��6;   $ 1Q�6I   � 5g   .   $ #F14419��6;   $	 0.3Q�6K   5h   +   5j   #   5k      5m      5o      5p      �� 5s   
   ordersViewTemplate��"  63   .R�5t         6   �� $ ordersViewTemplate��5u      �� 5w   
   settingsViewTemplate��"  65   .R�5x         6!   �� $ settingsViewTemplate��5y      5{      .��5}      � $��0,0,0,0q�6   .+�5�   
   � $��0,0,0,0q�6   $	��200��5�      $
��Left=�5�      ��5�      �� 5�       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5�      � $ 	#FF327EC3��6#   $ 0.393Q�65   � 5�      $ White��6#   $ 1Q�61   5�       � RowDefinitions 5�      �� 5�      $��76 �6    �� 5�      $��20 �6    �� 5�      $��60 �6    �� 5�      $��60 �6    �� 5�      $��60 �6    �� 5�      $��60 �6    �� 5�      $��60 �6    �� 5�      $��60 �6    �� 5�      $��* �6    .+�5�      �� -   5         ��Name$ TabLogo��5�      $��Center=�5�      $	��150��5�      $/��)/Resources/logo-plain-vector-72-white.png��5�      $��0,0,0,0q�5�      �� 5�      
���   �$��1��5�      $��3��5�      $	��Top=�5�      $��Stretch=�5�      5�      �� 5�      $��2��6   .+�5�      �� -   5          ��Name$  	TabOrders��5�      $��Orders��5�      #��[  5�      5�      5�      �� 5�      $��3��6   .+�5�      �� -   5        $  TabSettings��5�      $��Settings��5�      #��[  5�      5�      5�      �� 5�      -   5        $��AppVersionLabel��5�      $�� ��5�      $��8��5�      $
��Left=�5�      $��
Sitka Text)�5�      $��13(�5�      $��0,0,0,0q�5�      $��24��5�      $��Bottom=�5�      5�      � 5�   
   $��	200,0,0,0q�6   ��5�      �� 5�      $ 0.5,1&�6&   $ 0.5,0&�67   .��5�      � $ 	#FF327EC3��6#   $ 0.393Q�65   � 5�      $ White��6#   $ 1Q�61   5�      ! � ColumnDefinitions! 5�       5�      .+�5�      �� 
���   �$��3,0,0,0q�5�      ._�5�      �� -   5        $��MainContentControl��5�   !   $��Stretch=�5�   !   $��Stretch=�5�   !   
���   ���5�   !   �� 5�   !   5�      5�      5�      5�      