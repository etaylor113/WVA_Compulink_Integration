   M S B A M L   `   `   `  ���� T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:WVA_Compulink_Integration.Views.Orders&WVA_Compulink_Integration.Views.Orders  =    7WVA_Compulink_Integration.Views.Orders.ViewOrderDetails   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Flocal4clr-namespace:WVA_Compulink_Integration.Views.Orders     -     � 	Resources     ExamsButtonGroup �� 
StartPoint �� EndPoint � Color � Offset �� 
TargetType �� Property �� Value �� 
TargetType	 �Name
 � ColumnDefinitions ��Name �� CornerRadius 
� GradientOrigin �� RelativeTransform �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y �� Triggers P� Property P� Value �� 
TargetName  WVA_Blue_Color��%  &	        '�� �     7   7  �� $	 0,0&�$	 1,1&�.��� $ LightSlateGray��$
 0.02Q�� $ 
WhiteSmoke��$
 0.22Q�� $ LightSlateGray��$	 .53Q��� " ��.���  � ��8   ��  � ��$ Center=���  � ��
 �������  � ��$ 
Sitka Text)���  � ��$ 27(���  � ��$ 40����  � ��$	 150����  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R�� $
	 grid��
 �� $��3* ��� $��2* �.+��� $ border��$ 3,3,3,3��
���   �$��1.6,1.9,2.9,2.9q�$��2����
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$	 1.5Q�$	 1.5Q�T� $
 0.02Q�$	 0.3Q�.��� $ 1Q�$ 	#FF327EC3��� $	 0.3Q�$ 	#FF327EC3��._��� $��Center=�$��Center=�$��Regular"� P�  �@J�IsMouseOver . . ���  � ��
 �   ��� $ border�� � �� 
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$ 2Q�$ 2Q�T� $
 0.02Q�$	 0.9Q�.��� $ 	#FF327EC3��$ 1Q�� $ White��$	 0.3Q�P�  � �� . . ���  � ��
 �   ��� $ border�� � �� 
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$ 2Q�$ 2Q�T� $
 0.02Q�$	 0.9Q�.��� $ White��$ 1Q�� 8  $	 0.3Q�P�  � {� .  . ��� $
 grid�� �@J�Opacity$
 0.25Q����� $ 0.8,1&�$ 0.2,0&�.��� $ White��$ 1Q�� $ 	#FEABCAE5��.��� 
 �� $��50 ��� $��* ��� $��50 � � RowDefinitions �� $��50 ��� $
��auto �$	��140���� $��* �.+��� $��0��$��1�� �� Orientation$ 
Horizontal=�$��Center=�$��Center=�.+��� -   $��OrderNameLabel��$��
Sitka Text)�$��20(�$��0,0,0,0q�$��	OrderName���� 
���   �$��25��$��1��$��1��$��Center=��� -   $��OrderIDLabel��$��
Sitka Text)�$��20(�$��
WvaStoreID��$��Center=�$	��Top=�$��0,0,0,0q�$��30���� 
���   �$��25��$��1��$��1��$��Center=��� -   $��OrderedByLabel��$��
Sitka Text)�$��20(�$��0,0,0,0q�$��	OrderedBy���� $��
Sitka Text)�$��20(�$��0,0,0,0q�$��-���� -   $��AccountIDLabel��$��
Sitka Text)�$��20(�$��0,0,0,0q�$��
Account ID���� 
���   �$��3��$��1��$��1��$	��Top=�� $��1��$��1��.+��� $ 
Horizontal=�$��Center=�$��	0,10,0,10q�.+��� -   $��LeftInnerStackPanel���� -   $��RightInnerStackPanel��$��10,0,0,0q��� 
���   �$��3��$��2��$��1��$	��Top=�&   System.Windows.Controls.DataGrid  -   $��ReviewOrderDataGrid��$��Stretch=�$��Stretch=�$��2��$��2��$��1��$��1��$��	0,50,0,30q�   AutoGenerateColumns .     CanUserResizeRows .  $��DemiBold"�$��1q�   CanUserReorderColumns .     CanUserSortColumns .     CanUserAddRows .      
IsReadOnly  . ���� *Prescription+! J� OpacityMask! 
� .��� $ Black��$ 0Q�� $ White��$ 1Q�� $ 	#FF040404������ $	 1,1&�" � MappingMode$" RelativeToBoundingBox=�$	 0,1&�.��� $ 	#FF327EC3��$ 0.994Q�� $ White��� $ 	#FF4485C0��$
 0.54Q�� $ 	#FF8FB6D9��#   HorizontalGridLinesBrush# �� $ �� Color$$ Black��% � ContextMenu% �� -	   $��WVA_OrdersContextMenu��$	��300��&   Columns& 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn'   Width5  /System.Windows.Controls.DataGridLengthConverter$' * (   Header$( Patient��)   MinWidth$	) 100Q�1  +System.Windows.Controls.DataGridBoundColumn*   Binding* �� + �� Path$+ Patient�  $' * $	( Eye��$) 40Q�,   MaxWidth$, 40Q�* �� $	+ Eye�  $	' 200 $( Product��* �� $+ Product�  $' 40 $	( Qty��$) 30Q�* �� $+ Quantity�  $' * $( 	BaseCurve��$) 70Q�* �� $+ 	BaseCurve�  $' * $( Diameter��$) 60Q�* �� $+ Diameter�  $' * $( Sphere��$) 40Q�* �� $+ Sphere�  $' * $( Cylinder��$) 60Q�* �� $+ Cylinder�  $' * $
( Axis��* �� $
+ Axis�  $' * $	( Add��* �� $	+ Add�  $' * $( Color��* �� $+ Color�  $' * $( MF��* �� $+ 
Multifocal�