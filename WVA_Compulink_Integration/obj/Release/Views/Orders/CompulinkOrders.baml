   M S B A M L   `   `   `  ���� T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:WVA_Compulink_Integration.Views.Orders&WVA_Compulink_Integration.Views.Orders  <    6WVA_Compulink_Integration.Views.Orders.CompulinkOrders   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Flocal4clr-namespace:WVA_Compulink_Integration.Views.Orders     -     � 	Resources     ExamsButtonGroup �� 
StartPoint �� EndPoint � Color � Offset �� 
TargetType �� Property �� Value �� 
TargetType	 �Name
 ��Name �� CornerRadius 
� GradientOrigin �� RelativeTransform �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y �� Triggers P� Property P� Value �� 
TargetName  WVA_Blue_Color��%�  &	        '�� �     7   7  �� $	 0,0&�$	 1,1&�.��� $ LightSlateGray��$
 0.02Q�� $ 
WhiteSmoke��$
 0.22Q�� $ LightSlateGray��$	 .53Q��� " ��.���  � ��8   ��  � ��$ Center=���  � ��
 �������  � ��$ 
Sitka Text)���  � ��$ 25(���  � ��$ 35����  � ��$	 240����  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R�� $
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
 0.25Q����� $ 0.8,1&�$ 0.2,0&�.��� $ White��$ 1Q�� $ 	#FEABCAE5��.���  � ColumnDefinitions �� $��50 ��� $��* ��� $��50 � � RowDefinitions �� $��60 ��� $��* ��� $��80 �.+�� $��Center=�$��Stretch=�$��1�� �� $	��200 ��� $��* ��� $��60 �.+��� -   $��SearchProductsLabel��$��Search Orders��$��0��$��
Sitka Text)�$��26(��� -   $��SearchTextBox��$��5,0,5,0q�$��35��$��Stretch=�$��
Sitka Text)�$��26(�$��1��$��Center=��� -   $��RefreshButton��$��50��$��2��$��0,0,0,0q�$��40��$��Right=�.���� -    ��Name$ RefreshImage��$��Center=�$��30��$��0,0,0,0q�$��Center=�$��30��$7��1/Resources/icons8-available-updates-filled-48.png��&   System.Windows.Controls.DataGrid  -     Name$ OrdersDataGrid��$��Stretch=�$��Stretch=�$��1��$��1��   AutoGenerateColumns .     CanUserResizeRows .     CanUserAddRows .  $��DemiBold"�$��1q�   CanUserReorderColumns .      CanUserSortColumns  .  $��Stretch=�!   
IsReadOnly! . $��0,0,0,0q�
������ "   HeadersVisibility$" Column=����� *Prescription+# J� OpacityMask# 
� .��� $ Black��$ 0Q�� $ White��$ 1Q�� $ 	#FF040404��$   HorizontalGridLinesBrush$ �� % �� Color$% Transparent��&   VerticalGridLinesBrush& �� $% Transparent��'   Columns' 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn(   Width5  /System.Windows.Controls.DataGridLengthConverter$( 30 )   Header$	)   +��*   MinWidth$* 30Q�+   MaxWidth$+ 30Q�,   
IsReadOnly, .  -   CellTemplate- �� .R��� $��Center=�. d� 	IsChecked. �� / �� Path$/ 	IsChecked�0 �� UpdateSourceTrigger$0 PropertyChanged=�  $( * $) Is STP��$* 45Q�$+ 45Q�, .  - �� .R��� $��Center=�. �� $/ IsShipToPatient�$0 PropertyChanged=�0  *System.Windows.Controls.DataGridTextColumn  $( * $) Patient��$	* 110Q�1  +System.Windows.Controls.DataGridBoundColumn1   Binding1 �� $/ Patient�  $( * $
) Date��$* 70Q�1 �� $
/ Date�  $( * $) Product��$	* 100Q�1 �� $/ Product�  $( * $	) Eye��$+ 40Q�1 �� $	/ Eye�  $( * $	) Qty��$* 40Q�1 �� $/ Quantity�  $( * $) 
Base Curve��$* 70Q�1 �� $/ 	BaseCurve�  $( * $) Diameter��$* 60Q�1 �� $/ Diameter�  $( * $) Sphere��1 �� $/ Sphere�  $( * $) Cylinder��$* 55Q�1 �� $/ Cylinder�  $( * $
) Axis��$* 35Q�1 �� $
/ Axis�  $( * $	) Add��$* 35Q�1 �� $	/ Add�  $( * $) Color��1 �� $/ Color�  $( * $) 
Multifocal��$* 70Q�1 �� $/ 
Multifocal�� $��2��$��1��$��0,0,0,0q�.+��  �� $��50 ��� $��* ��� $	��250 �.+��� -   $��WvaOrdersComboBox��$��1��2 �� Text$2  ��3 �� 
IsEditable3 . 4 �� 
IsReadOnly4 .  $��Stretch=�$��0,0,0,0q�$��2��$��
Sitka Text)�$��25(�$��Right=�$��Center=�$��Center=�$��35���� -   $��AddToOrderOrderButton��$��Add To WVA Order��$��0,0,0,0q�$��2��$��Right=�