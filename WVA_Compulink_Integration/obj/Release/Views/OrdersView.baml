   M S B A M L   `   `   `  ���� T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:WVA_Compulink_Integration.ViewsWVA_Compulink_Integration.Views  i9clr-namespace:WVA_Compulink_Integration.ViewModels.Orders+WVA_Compulink_Integration.ViewModels.Orders  _4clr-namespace:WVA_Compulink_Integration.Views.Orders&WVA_Compulink_Integration.Views.Orders  0    *WVA_Compulink_Integration.Views.OrdersView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ?local-clr-namespace:WVA_Compulink_Integration.Views     P
viewmodels9clr-namespace:WVA_Compulink_Integration.ViewModels.Orders     Fviews4clr-namespace:WVA_Compulink_Integration.Views.Orders       � 	Resources     ExamsButtonGroup �� 
StartPoint �� EndPoint � Color � Offset �� 
TargetType �� Property �� Value �� 
TargetType	 �Name
 ��Name �� CornerRadius 
� GradientOrigin �� RelativeTransform �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y �� Triggers P� Property P� Value �� 
TargetName  WVA_Blue_Color �� DataTypeJ  DWVA_Compulink_Integration.ViewModels.Orders.CompulinkOrdersViewModel<   6WVA_Compulink_Integration.Views.Orders.CompulinkOrders � DataContext �� ElementNameD  >WVA_Compulink_Integration.ViewModels.Orders.WVAOrdersViewModel6   0WVA_Compulink_Integration.Views.Orders.WVAOrdersH  BWVA_Compulink_Integration.ViewModels.Orders.OrderCreationViewModel>   8WVA_Compulink_Integration.Views.Orders.OrderCreationViewK   EWVA_Compulink_Integration.ViewModels.Orders.ViewOrderDetailsViewModel=   7WVA_Compulink_Integration.Views.Orders.ViewOrderDetails��%�  &	        '�� �     7   7  (�� �    *, +)(��      *, +)(�� q    *, +)(�� �    *, +)�� $	 0,0&�$	 1,1&�.��� $ LightSlateGray��$
 0.02Q�� $ 
WhiteSmoke��$
 0.22Q�� $ LightSlateGray��$	 .53Q��� " ��.���  � ��8   ��  � ��$ Center=���  � ��
 �������  � ��$ 25(���  � ��$ 
Sitka Text)���  � ��$	 170����  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R�� $
	 grid��.+��� $
 border��$ 3,3,3,3��
���   �$��1.6,1.9,2.9,2.9q���
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$	 1.5Q�$	 1.5Q�T� $
 0.02Q�$	 0.3Q�.��� $ 1Q�$ 	#FF327EC3��� $	 0.3Q�$ 	#FF327EC3��._��� $��Center=�$��Center=�$��Regular"� P�  �@J�IsMouseOver . . ���  � ��
 �   ��� $ border�� � �� 
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$ 2Q�$ 2Q�T� $
 0.02Q�$	 0.9Q�.��� $ 	#FF327EC3��$ 1Q�� $ White��$	 0.3Q�P�  � �� . . ���  � ��
 �   ��� $ border�� � �� 
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$ 2Q�$ 2Q�T� $
 0.02Q�$	 0.9Q�.��� $ White��$ 1Q�� 8  $	 0.3Q�P�  � {� .  . ��� $
 grid�� �@J�Opacity$
 0.25Q��� CompulinkOrdersViewTemplate��"  .R�   �� $! compulinkOrdersViewTemplate���� WVAOrdersViewTemplate��"  .R�   �� $ wvaOrdersViewTemplate���� OrderCreationViewTemplate��"  .R�   �� $ orderCreationViewTemplate����  ViewOrderDetailsViewTemplate��"  .R�   �� $" viewOrderDetailsViewTemplate��.��� 
���   � � RowDefinitions �� $��50 ��� $��* �.+�� 
�������$��50��$	��Top=�$��Stretch=�$��0�� �� $��45 ��� $��5 � � ColumnDefinitions �� $	��250 ��� $	��250 ��� $��* �.+��� $��0��$��Center=�.+��� -    ��Name$ CompulinkOrdersButton��$��	Compulink��$��Center=�$��Center=��� -   $��Rect_1��$��0��$��1��$
��Left=�$��5��$	��Top=�$	��250�����	#FF327EC3�� -   $ ShipToPatientButton��$��1��$��0��$��
WVA Orders��$��Center=�$��Center=��� -   $��Rect_2��$��1��$��1��$
��Left=�$��5��$	��Top=�$	��250��
��������� -   $��TabLabel��$��Compulink Orders��$��30(�$��2��$��0,0,10,0q�$��Center=�$��Right=�$��Sylfaen)�� 
�������$��1��$��Stretch=�$��Stretch=�.+��� -   $��OrdersContentControl��$��Stretch=�$��Stretch=����� 