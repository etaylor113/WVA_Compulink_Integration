   M S B A M L   `   `   `  ���� T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:WVA_Compulink_Integration.Views.Orders&WVA_Compulink_Integration.Views.Orders  6    0WVA_Compulink_Integration.Views.Orders.WVAOrders   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Flocal4clr-namespace:WVA_Compulink_Integration.Views.Orders     -     � 	Resources     ExamsButtonGroup �� 
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
 �� $��75 ��� $��* ��� $��75 � � RowDefinitions �� $��70 ��� $��40 ��� $��10 ��� $��* ��� $	��100 �.+�� $��1��$��0��$��Bottom=�$��Stretch=�$��0,0,0,0q�
 �� $��* ��� $	��200 ��� $��50 �.+��� -   $��SearchTextBox��$��0,0,10,0q�$��35��$��Stretch=�$��
Sitka Text)�$��26(�$��Center=��� -   $��SearchFilterComboBox��$��Stretch=�$��35��$��Center=�$��18(�$��0,0,10,0q�$��1�� �� SelectedIndex$ 0��.@��� .��WVA Store ID�� .��
Order Name�� .��Created Date�� .��Ship To Patient�� .��	PO Number�� .��
Ordered By�� .��Status�� -   $��RefreshButton��$��50��$��2��$��0,0,0,0q�$��Right=�.���� -    ��Name$ RefreshImage��$��Right=�$��30��$��0,0,0,0q�$��Center=�$��30��$7��1/Resources/icons8-available-updates-filled-48.png���� -   $��ListBoxHeaderLabel��$��26(�$��1��$��1��$��Center=�$��
WVA Orders���� $��1��$��2��$��3��
���   �$��0,0,0,0q�&   System.Windows.Controls.DataGrid  -   $��WvaOrdersDataGrid��$��Stretch=�$��Stretch=�$��3��$��1��$��DemiBold"�
������ $��1q�$��0,5,0,0q�   
IsReadOnly .    CanUserResizeRows .     CanUserAddRows .     CanUserReorderColumns .      CanUserSortColumns  .  !   HeadersVisibility$! Column=����� *Order+"   Columns" 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn#   Width5  /System.Windows.Controls.DataGridLengthConverter$# 85 $   Header$$ WVA Store ID��1  +System.Windows.Controls.DataGridBoundColumn%   Binding% �� & �� Path$& 
WvaStoreID�  $# * $$ 
Order Name��'   MinWidth$	' 200Q�% �� $& 	OrderName�  $	# 140 $$ Created Date��$	' 120Q�% �� $& CreatedDate�  $# 35 $	$ STP��% �� $& ShipToPatient�  $# 75 $$ 	PO Number��$' 75Q�% �� $& PoNumber�  $# 80 $$ 
Ordered By��% �� $& 	OrderedBy�  $# 35 $	$ Qty��% �� $& Quantity�  $# 80 $$ Status��% �� $& Status��� $��4��$��1��$��0,0,0,0q�( �� Orientation$( 
Horizontal=�$��Center=�.+��� -   $��EditOrderButton��$
��Edit��$��	10,0,10,0q�$��0��$��Center=��� -	   $��DeleteOrderButton��$��Delete��$��	10,0,10,0q�$��1��$��Center=�