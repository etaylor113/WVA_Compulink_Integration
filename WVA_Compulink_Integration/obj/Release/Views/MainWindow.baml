   M S B A M L   `   `   `  ���� T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:WVA_Compulink_IntegrationWVA_Compulink_Integration  [2clr-namespace:WVA_Compulink_Integration.ViewModels$WVA_Compulink_Integration.ViewModels  Q-clr-namespace:WVA_Compulink_Integration.ViewsWVA_Compulink_Integration.Views  0    *WVA_Compulink_Integration.Views.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:WVA_Compulink_Integration     I
viewmodels2clr-namespace:WVA_Compulink_Integration.ViewModels     ?views-clr-namespace:WVA_Compulink_Integration.Views     -   $	��650��$
��1200��$
��1000��$	��550��  � WindowStartupLocation$  CenterScreen=� � 
ResizeMode$ 	CanResize=� � Icon$H B/WVA_Compulink_Integration;component/Resources/wva_square_logo.jpg�� � 	Resources  �� 
TargetType   	TabButton �� Property �� Value �� 
TargetType �Name	 ��Name
 �� CornerRadius 
� GradientOrigin �� RelativeTransform �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y � Offset  Fiery_Orange_Color � Color �� Triggers P� Property P� Value �� 
TargetName �� DataType:   4WVA_Compulink_Integration.ViewModels.OrdersViewModel0   *WVA_Compulink_Integration.Views.OrdersView � DataContext �� ElementName<   6WVA_Compulink_Integration.ViewModels.SettingsViewModel2   ,WVA_Compulink_Integration.Views.SettingsView��%�  &	        7  7  7  (�� �    *, +)(�� I    *, +)�� " ��.���  � ��
 �������  �@��TextAlignment$ Center=���  � ��$ 
Sitka Text)���  � ��$ 36(���  � ��$ Center=���  � ��$	 150����  � ��$ 0,0,0,0q���  � ��ButtonTemplate�� �� " ��.R�� $
 grid��.+��� $	 border��$
 3,3,3,3��
���   �$��1.6,1.9,2.9,2.9q���
� $ 0.496,1.052&� V� .~��� $	 0.6Q�$	 0.6Q�$	 1.5Q�$	 1.5Q�T� $
 0.02Q�$
 0.75Q�.��� $ 1Q�8   � $	 0.3Q�8  ._��� $��Center=�$��Center=�$��Regular"� P�  �@J�IsMouseOver . . ���  � ��
 �   ��� $ border�� � �� 
� $ 0.496,1.052&� V� .~��� $	 0.6Q�$	 0.6Q�$	 1.5Q�$	 1.5Q�T� $
 0.02Q�$
 0.75Q�.��� 8  $ 1Q�� $ White��$	 0.3Q�P�  � �� . . ���  � ��
 �   ��� $ border�� � �� 
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$ 2Q�$ 2Q�T� $
 0.02Q�$	 0.9Q�.��� $ White��$ 1Q�� $ #F14419��$	 0.3Q��� ordersViewTemplate��"  .R�   �� $ ordersViewTemplate���� settingsViewTemplate��"  .R�   �� $ settingsViewTemplate��.��� $��0,0,0,0q�.+�� $��0,0,0,0q�$	��200��$
��Left=�����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.��� $ 	#FF327EC3��$ 0.393Q�� $ White��$ 1Q� � RowDefinitions �� $��76 ��� $��20 ��� $��60 ��� $��60 ��� $��60 ��� $��60 ��� $��60 ��� $��60 ��� $��* �.+��� -    ��Name$ TabLogo��$��Center=�$	��150��$/��)/Resources/logo-plain-vector-72-white.png��$��0,0,0,0q��� 
���   �$��1��$��3��$	��Top=�$��Stretch=��� $��2��.+��� -     ��Name$  	TabOrders��$��Orders��#��[  �� $��3��.+��� -   $  TabSettings��$��Settings��#��[  �� -   $��AppVersionLabel��$�� ��$��8��$
��Left=�$��
Sitka Text)�$��13(�$��0,0,0,0q�$��24��$��Bottom=�� $��	200,0,0,0q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF327EC3��$ 0.393Q�� $ White��$ 1Q�! � ColumnDefinitions!  .+��� 
���   �$��3,0,0,0q�._��� -   $��MainContentControl��$��Stretch=�$��Stretch=�
���   ����� 