   M S B A M L   `   `   `  ����T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:WVA_Compulink_Integration.ViewsWVA_Compulink_Integration.Views  5      i9clr-namespace:WVA_Compulink_Integration.ViewModels.Orders+WVA_Compulink_Integration.ViewModels.Orders  5      _4clr-namespace:WVA_Compulink_Integration.Views.Orders&WVA_Compulink_Integration.Views.Orders  5      0    *WVA_Compulink_Integration.Views.OrdersView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:WVA_Compulink_Integration.Views     5      P
viewmodels9clr-namespace:WVA_Compulink_Integration.ViewModels.Orders     5      Fviews4clr-namespace:WVA_Compulink_Integration.Views.Orders     5        � 	Resources  5         ExamsButtonGroup �� 
StartPoint �� EndPoint � Color � Offset �� 
TargetType �� Property �� Value �� 
TargetType	 �Name
 ��Name �� CornerRadius 
� GradientOrigin �� RelativeTransform �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y �� Triggers P� Property P� Value �� 
TargetName  WVA_Blue_Color �� DataTypeJ  DWVA_Compulink_Integration.ViewModels.Orders.CompulinkOrdersViewModel<   6WVA_Compulink_Integration.Views.Orders.CompulinkOrders � DataContext �� ElementNameD  >WVA_Compulink_Integration.ViewModels.Orders.WVAOrdersViewModel6   0WVA_Compulink_Integration.Views.Orders.WVAOrdersH  BWVA_Compulink_Integration.ViewModels.Orders.OrderCreationViewModel>   8WVA_Compulink_Integration.Views.Orders.OrderCreationViewK   EWVA_Compulink_Integration.ViewModels.Orders.ViewOrderDetailsViewModel=   7WVA_Compulink_Integration.Views.Orders.ViewOrderDetails��%*  &	        '�� �     7   7  (�� �    5x   <   *, +)(�� 2	    5|   6   *, +)(�� �	    5�   :   *, +)(�� &
    5�   =   *, +)�� 5   
   $	 0,0&�67   $	 1,1&�6H   .��5      � $ LightSlateGray��6   $
 0.02Q�62   � 5      $ 
WhiteSmoke��6   $
 0.22Q�6.   � 5      $ LightSlateGray��6   $	 .53Q�62   5      �� 5   
   " ��6   .�5      ��  � ��8   6+   �� 5       � ��$ Center=�6;   �� 5       � ��
 �����6+   �� 5       � ��$ 25(�6)   �� 5       � ��$ 
Sitka Text)�6+   �� 5       � ��$	 170��6&   �� 5       � ��$ Center=�69   �� 5       � ��$ 1q�6(   �� 5       � �� 5      �� 5      " ��6&   .R�5!      � $
	 grid��6   .+�5"      �� $
 border��6%   $ 3,3,3,3��5#   %   
���   �$��1.6,1.9,2.9,2.9q�5%   %   ��5&   "   
� 5'   &   $ 0.496,1.052&�6:    5(   *   V� 5)   .   .~�5*   2   �� $	 0.5Q�6A   $	 0.5Q�5+   A   $	 1.5Q�5,   A   $	 1.5Q�5-   A   T� 5.   2   $
 0.02Q�6E   $	 0.3Q�6N   5/   /   .��51   *   � $ 1Q�67   $ 	#FF327EC3��6B   � 52   *   $	 0.3Q�67   $ 	#FF327EC3��6D   53   '   ._�55   "   �� $��Center=�63   $��Center=�56   3   $��Regular"�57   3   58   #   59      5:       5=      P� 5@       �@J�IsMouseOver . . �5A   "   ��  � ��
 �   �6?   �� 5B   "   $ border��6?    � �� 5C   &   
� 5D   *   $ 0.496,1.052&�6>    5E   .   V� 5F   2   .~�5G   6   �� $	 0.5Q�6E   $	 0.5Q�5H   E   $ 2Q�5I   E   $ 2Q�5J   E   T� 5K   6   $
 0.02Q�6I   $	 0.9Q�6R   5L   3   .��5N   .   � $ 	#FF327EC3��6;   $ 1Q�6M   � 5O   .   $ White��6;   $	 0.3Q�6I   5P   +   5R   #   5S      P� 5V       � �� . . �5W   "   ��  � ��
 �   �6?   �� 5X   "   $ border��6?    � �� 5Y   &   
� 5Z   *   $ 0.496,1.052&�6>    5[   .   V� 5\   2   .~�5]   6   �� $	 0.5Q�6E   $	 0.5Q�5^   E   $ 2Q�5_   E   $ 2Q�5`   E   T� 5a   6   $
 0.02Q�6I   $	 0.9Q�6R   5b   3   .��5d   .   � $ White��6;   $ 1Q�6I   � 5e   .   8  $	 0.3Q�6c   5f   +   5h   #   5i      P� 5l       � {� .  . �5m   "   �� $
 grid��5n   )    �@J�Opacity$
 0.25Q�5o   )   5p      5s      5u      5v      �� 5x   
   CompulinkOrdersViewTemplate��"  6<   .R�5y         6$   �� $! compulinkOrdersViewTemplate��5z      �� 5|   
   WVAOrdersViewTemplate��"  66   .R�5}         6   �� $ wvaOrdersViewTemplate��5~      �� 5�   
   OrderCreationViewTemplate��"  6:   .R�5�         6&   �� $ orderCreationViewTemplate��5�      �� 5�   
    ViewOrderDetailsViewTemplate��"  6=   .R�5�         6%   �� $" viewOrderDetailsViewTemplate��5�      5�      .��5�      � 
���   � � RowDefinitions 5�   
   �� 5�      $��50 �6   �� 5�      $��* �6   .+�5�   
   � 
�������$��50��5�      $	��Top=�5�      $��Stretch=�5�      $��0��5�       5�      �� 5�      $��45 �6    �� 5�      $��5 �6     � ColumnDefinitions 5�      �� 5�      $	��250 �6#   �� 5�      $	��250 �6#   �� 5�      $��* �6#   .+�5�      �� $��0��6   $��Center=�6&   .+�5�      �� -   5         ��Name$ CompulinkOrdersButton��5�      $��	Compulink��5�      $��Center=�5�      $��Center=�5�      5�      5�      �� 5�      -   5        $��Rect_1��5�      $��0��5�      $��1��5�      $
��Left=�5�      $��5��5�      $	��Top=�5�      $	��250��5�      ���	#FF327EC35�      �� 5�      -   5        $ ShipToPatientButton��5�      $��1��5�      $��0��5�      $��
WVA Orders��5�      $��Center=�5�      $��Center=�5�      5�      �� 5�      -   5        $��Rect_2��5�      $��1��5�      $��1��5�      $
��Left=�5�      $��5��5�      $	��Top=�5�      $	��250��5�      
�������5�      �� 5�      -   5        $��TabLabel��5�      $��Compulink Orders��5�      $��30(�5�      $��2��5�      $��0,0,10,0q�5�      $��Center=�5�      $��Right=�5�      $��Sylfaen)�5�      5�      � 5�   
   
�������$��1��5�      $��Stretch=�5�      $��Stretch=�5�      .+�5�      �� -   5        $��OrdersContentControl��5�      $��Stretch=�5�      $��Stretch=�5�      ��5�      �� 5�      5�      5�      5�      