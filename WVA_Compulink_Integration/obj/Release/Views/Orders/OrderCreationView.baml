   M S B A M L   `   `   `  ���� T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:WVA_Compulink_Integration.Views.Orders&WVA_Compulink_Integration.Views.Orders  >    8WVA_Compulink_Integration.Views.Orders.OrderCreationView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Flocal4clr-namespace:WVA_Compulink_Integration.Views.Orders     -     � 	Resources     ExamsButtonGroup �� 
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
Sitka Text)���  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R�� $
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
 0.25Q�.��� ���� $	 1,1&�$
 .5,0&�.��� $ White��� $ 	#FF72A6D5��$ 1Q� � ColumnDefinitions  � RowDefinitions �� $��* ��� $	��200 �.+�� $��0�� �� $��20 ��� $��* ��� $��20 � �� $��10 ��� $
��150* ��� $��50 �.+��� $��0��$��1�� �� Orientation$ 
Horizontal=�$��Right=�&   System.Windows.Controls.DataGrid  -     Name$ OrdersDataGrid��$��Stretch=�$��Stretch=�$��1��$��2��$��1��$��1��   AutoGenerateColumns .     CanUserResizeRows .  $��DemiBold"�$��1q�   CanUserReorderColumns .     CanUserSortColumns .      CanUserAddRows  .  !   
IsReadOnly! .  ���� *Prescription+" � ContextMenu" �� -   $��WVA_OrdersContextMenu��$	��300��  *  $System.Windows.Controls.DataGridCell��%   '        �� "  # J� OpacityMask# 
� .��� $ Black��$ 0Q�� $ White��$ 1Q�� $ 	#FF040404������ $	 1,1&�$ � MappingMode$$ RelativeToBoundingBox=�$	 0,1&�.��� $ 	#FF327EC3��$ 0.994Q�� $ White��� $ 	#FF4485C0��$
 0.54Q�� $ 	#FF8FB6D9��%   HorizontalGridLinesBrush% �� & �� Color$& Black��'   Columns' 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn(   Width5  /System.Windows.Controls.DataGridLengthConverter$( * )   MinWidth$	) 110Q�*   Header$* Patient��1  +System.Windows.Controls.DataGridBoundColumn+   Binding+ �� , �� Path$, Patient�  $( * $	* Eye��$) 40Q�-   MaxWidth$- 40Q�+ �� $	, Eye�4  .System.Windows.Controls.DataGridTemplateColumn  $*  ��$) 30Q�$- 30Q�.   
IsReadOnly. . /   CellTemplate/ �� .R��� $��ProductStatusImage��$��16������ $, ProductImagePath�  $	( 200 $* Product��+ �� $, Product�  $( 40 $	* Qty��+ �� $, Quantity�  $( * $) 70Q�$* 	BaseCurve��+ �� $, 	BaseCurve�0   	CellStyle0 �� "  1 �� Triggers1 �� 2 �� Binding2 �� *BaseCurveCellColor+3 �� Value	3 Green.a���  � ��
 ������ 2 �� *BaseCurveCellColor+3 Red.a���  � �� �	#FFF03C3C�� 2 �� *BaseCurveCellColor+	3 White.a���  � ��
 �����  $( * $* Diameter��+ �� $, Diameter�0 �� "  1 �� 2 �� *DiameterCellColor+	3 Green.a���  � ��
 ������ 2 �� *DiameterCellColor+3 Red.a���  � �� �	#FFF03C3C�� 2 �� *DiameterCellColor+	3 White.a���  � ��
 �����  $( * $* Sphere��+ �� $, Sphere�0 �� "  1 �� 2 �� *SphereCellColor+	3 Green.a���  � ��
 ������ 2 �� *SphereCellColor+3 Red.a���  � �� �	#FFF03C3C�� 2 �� *SphereCellColor+	3 White.a���  � ��
 �����  $( * $* Cylinder��+ �� $, Cylinder�0 �� "  1 �� 2 �� *CylinderCellColor+	3 Green.a���  � ��
 ������ 2 �� *CylinderCellColor+3 Red.a���  � �� �	#FFF03C3C�� 2 �� *CylinderCellColor+	3 White.a���  � ��
 �����  $( * $
* Axis��+ �� $
, Axis�0 �� "  1 �� 2 �� *AxisCellColor+	3 Green.a���  � ��
 ������ 2 �� *AxisCellColor+3 Red.a���  � �� �	#FFF03C3C�� 2 �� *AxisCellColor+	3 White.a���  � ��
 �����  $( * $	* Add��+ �� $	, Add�0 �� "  1 �� 2 �� *AddCellColor+	3 Green.a���  � ��
 ������ 2 �� *AddCellColor+3 Red.a���  � �� �	#FFF03C3C�� 2 �� *AddCellColor+	3 White.a���  � ��
 �����  $( * $* Color��+ �� $, Color�0 �� "  1 �� 2 �� *ColorCellColor+	3 Green.a���  � ��
 ������ 2 �� *ColorCellColor+3 Red.a���  � �� �	#FFF03C3C�� 2 �� *ColorCellColor+	3 White.a���  � ��
 �����  $( * $* MF��+ �� $, 
Multifocal�0 �� "  1 �� 2 �� *MultifocalCellColor+	3 Green.a���  � ��
 ������ 2 �� *MultifocalCellColor+3 Red.a���  � �� �	#FFF03C3C�� 2 �� *MultifocalCellColor+	3 White.a���  � ��
 ������� $��2��$��1��$��1��$ 
Horizontal=�$��Right=�$��Center=�$��0,7,0,0q�.+��� -   $��VerifyOrderButton��$��Verify��$��Center=�$	��100��$��20,0,0,0q�$��24��$��16(�$��0���� -   $��SaveOrderButton��$
��Save��$��3��$��Right=�$	��100��$��20,0,0,0q�$��6��$��1��$��24��$��16(�$��4���� -   $��DeleteOrderButton��$��Delete��$��3��$��Right=�$��6��$	��100��$��20,0,0,0q�$��1��$��24��$��16(�$��6���� $9��3Not finding matches? Adjust match percentage below.��$��1��$��2��$	��400��$��
Sitka Text)�$��0,5,0,0q�$
��12.4(�$
��Left=��� $��0��$��1��$��2��$��0,25,0,0q�$��20��$
��Left=��� -   $��MatchPercentLabel��$��Match Percent: 0%��$��1��$��2��$��
215,25,0,0q�$	��150��$
��Left=��� -   $��MinScoreAdjustSlider��$��1��$
��Left=�$��	15,30,0,0q�$��2��4 � Maximum$	4 100Q�5 � Minimum$5 0Q�6 � Value$6 70Q�$��3��$	��Top=�$	��158��7 � SmallChange$7 1Q��� $	��100��$��1��$��2��$��
170,25,0,0q�$��30��$
��Left=�� $��1�� �� $��20 ��� $
��122* ��� $
��111* ��� $
��184* ��� $
��343* ��� $��20 � �� $��10 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��* �.+��� 
���   �$��3��$��1��$��4��$	��Top=��� 
���   �$��3��$��4��$��0��$��8��$��Stretch=�$
��Left=�$��0,0,0,0q��� -	   $��AddresseeLabel��$��	Addressee��$��1��$
��Left=�$��1��$��Center=�$��30��$��80��$��15(��� -
   $��AddresseeTextBox��$��1��$��Stretch=�$��24��$��15(�$��	84,3,11,3q�$��1��8 �� 	MaxLength$8 40��$��7��9 �� TextWrapping$
9 Wrap=�$��Center=�$��3���� -   $��AddressLabel��$��Address��$��1��$
��Left=�$��2��$��Center=�$��30��$��80��$��15(��� -   $��AddressTextBox��$��1��$��Stretch=�$��24��$��15(�$��	85,3,10,3q�$��2��$8 45��$��8��$
9 Wrap=�$��Center=�$��3���� -   $��Suite_AptLabel��$��
Suite/Apt.��$��1��$
��Left=�$��3��$��Center=�$��30��$��80��$��15(��� -   $��Suite_AptTextBox��$��1��$��Stretch=�$��24��$��15(�$��	85,3,10,3q�$��3��$8 45��$��9��$
9 Wrap=�$��Center=�$��3���� -   $��	CityLabel��$
��City��$��1��$
��Left=�$��4��$��Center=�$��80��$��30��$��15(��� -   $��CityTextBox��$��1��$��Stretch=�$��24��$��	85,3,10,3q�$��4��$��10��$8 25��$��15(�$
9 Wrap=�$��Center=�$��3���� -   $��
StateLabel��$��State��$��1��$
��Left=�$��5��$��Center=�$��30��$��80��$��15(��� -   : ��Name$: StateComboBox��$��1��; �� Text$;  ��< �� 
IsEditable< . = �� 
IsReadOnly= .  $��
Sitka Text)�$��15(�$��Stretch=�$��85,3,0,3q�$��5��$��11��$��Center=�$��24��$��2��.@��� $��Alabama - AL���� $��Alaska - AK���� $��Arizona - AZ���� $��Arkansas - AR���� $��California - CA���� $��Colorado - CO���� $��Connecticut - CT���� $��Delaware - DE���� $��Florida - FL���� $��Georgia - GA���� $��Hawaii - HI���� $��
Idaho - ID���� $��Illinois - IL���� $��Indiana - IN���� $��	Iowa - IA���� $��Kansas - KS���� $��Kentucky - KY���� $��Louisiana - LA���� $��
Maine - ME���� $��Maryland - MD���� $��Massachusetts - MA���� $��Michigan - MI���� $��Minnesota - MN���� $��Mississippi - MS���� $��Missouri - MO���� $��Montana - MT���� $��Nebraska - NE���� $��Nevada - NV���� $��New Hampshire - NH���� $��New Jersey - NJ���� $��New Mexico - NM���� $��New York - NY���� $��North Carolina - NC���� $��North Dakota - ND���� $��	Ohio - OH���� $��Oklahoma - OK���� $��Oregon - OR���� $��Pennsylvania - PA���� $��Rhode Island - RI���� $��South Carolina - SC���� $��South Dakota - SD���� $��Tennessee - TN���� $��
Texas - TX���� $��	Utah - UT���� $��Vermont - VT���� $��Virginia - VA���� $��Washington - WA���� $��West Virginia - WV���� $��Wisconsin - WI���� $��Wyoming - WY���� -   $��ZipLabel��$	��ZIP��$��3��$
��Left=�$��5��$��Center=�$��30��$��30��$��10,0,0,0q�$��15(��� -   $��
ZipTextBox��$��3��$��Stretch=�$��24��$��	40,3,10,3q�$��5��$��15(�$��12��$8 12��$
9 Wrap=�$��Center=��� -   $��
PhoneLabel��$��Phone��$��1��$
��Left=�$��6��$��Center=�$��30��$��12��$��80��$��15(��� -   $��PhoneTextBox��$��1��$��Stretch=�$��24��$��15(�$��13��$��85,3,0,3q�$��6��$
9 Wrap=�$��Center=�$��2���� -   $��DoBLabel��$	��DoB��$��3��$
��Left=�$��6��$��Center=�$y�Hidden=�$��30��$��50��$��15(��� -   $��
DoBTextBox��$��3��$��Stretch=�$��24��$��	40,3,10,3q�$��6��$��15(�$��14��$
9 Wrap=�$y�Hidden=�$��Center=��� -   $��OrderNameLabel��$��Order Name:��$��4��$
��Left=�$��1��$��15(�$��Center=�$��30��$	��100��$��5,0,0,0q��� -   > ��Name$> OrderNameTextBox��$��4��? �� 
IsReadOnly? . $��Stretch=�$��24��$��	100,3,0,3q�$��1��$��15(�$��15��$9 NoWrap=�$��Center=��� -   $��AccountIDLabel��$��Account ID:��$��4��$
��Left=�$��2��$��15(�$��Center=�$��30��$��90��$��5,0,0,0q��� -   $> AccountIDTextBox��$��4��$��Stretch=�$��24��? . $��	100,3,0,3q�$��2��$8 40��$��15(�$��16��$9 NoWrap=�$��Center=��� -   $��OrderedByLabel��$��Ordered By:��$��4��$
��Left=�$��3��$��Center=�$��30��$��95��$��15(�$��5,0,0,0q��� -   $��OrderedByTextBox��$��4��$��Stretch=�$��24��? . $��	100,3,0,3q�$��3��$��15(�$9 NoWrap=�$��17��$��Center=��� -   $��PoNumberLabel��$��
PO Number:��$��4��$
��Left=�$��4��$��Center=�$��30��$��95��$��15(�$��5,0,0,0q��� -    $��PoNumberTextBox��$��4��$��Stretch=�$��24��$��	100,3,0,3q�$��4��$��15(�$��19��$9 NoWrap=�$��Center=��� $��
Ship Type:��$��4��$
��Left=�$��5��$��Center=�$��15(�$��30��$��90��$��5,0,0,0q��� -!   $��ShippingTypeComboBox��$��4��$��Stretch=�$��5��$��15(�@ �� SelectedIndex$@ 0��$��
Sitka Text)�$��Center=�$��24��$��20��$��	100,3,0,3q�< . = . .@��� .��
Standard�� .��Ground�� .��2nd Day Air�� .��Next Day Air�� -"   $��SubmitOrderButton��$��Submit��$��4��$��Right=�$	��100��$��20,0,0,0q�$��6��$��1��$��24��$��16(�$��5��