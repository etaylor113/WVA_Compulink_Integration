   M S B A M L   `   `   `  ���� T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:WVA_Compulink_Integration.ViewsWVA_Compulink_Integration.Views  3    -WVA_Compulink_Integration.Views.MessageWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ?local-clr-namespace:WVA_Compulink_Integration.Views     -   $��GeneralMessageWindow��  � Title$   �� � 
ResizeMode$ NoResize=� � Topmost .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=�$	��325��$	��500�����	#FF327EC3 � 	Resources    ExamsButtonGroup �� 
StartPoint �� EndPoint � Color	 � Offset
 �� 
TargetType �� Property �� Value �� 
TargetType �Name ��Name �� CornerRadius 
� GradientOrigin �� RelativeTransform �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y �� Triggers P� Property P� Value �� 
TargetName  WVA_Blue_Color  ImageButtonStyle��%�  &	        '�� �     7   7  &	 �    �� $	 0,0&�$	 1,1&�.��� $ LightSlateGray��$
	 0.02Q�� $ 
WhiteSmoke��$
	 0.22Q�� $ LightSlateGray��$		 .53Q��� "
 ��.���  � ��8   ��  � ��$ Center=���  � ��
 �������  � ��$ 
Sitka Text)���  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R�� $
 grid��.+��� $ border��$ 3,3,3,3��
���   �$��1.6,1.9,2.9,2.9q���
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$	 1.5Q�$	 1.5Q�T� $
 0.02Q�$	 0.3Q�.��� $	 1Q�$ 	#FF327EC3��� $		 0.3Q�$ 	#FF327EC3��._��� $��Center=�$��Center=�$��Regular"� P�  �@J�IsMouseOver . . ���  � ��
 �   ��� $ border�� � �� 
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$ 2Q�$ 2Q�T� $
 0.02Q�$	 0.9Q�.��� $ 	#FF327EC3��$	 1Q�� $ White��$		 0.3Q�P�  � �� . . ���  � ��
 �   ��� $ border�� � �� 
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$ 2Q�$ 2Q�T� $
 0.02Q�$	 0.9Q�.��� $ White��$	 1Q�� 8  $		 0.3Q�P�  � {� .  . ��� $
 grid�� �@J�Opacity$
 0.25Q��� "
 ��.���  � �� �� " ��.R��� .��� ���� $	 1,0&�$	 1,1&�.��� $ 	#FF408DCD��� $ 	#FFF3F8FC��$	 0.994Q�� $ 	#FFF3F8FC��$	 0.236Q�.+��� -   $��MessagesTextBox��$
��Left=�$	��120��$��
50,105,0,0q�$	��Top=�$	��400��$��
Sitka Text)�$��16(�. �/� .T�J� .*�.*��� $� ���� $
��Left=�$��98��$	��Top=�$	��500��$$��/Resources/WVA_Scan_Banner.PNG���� -   $��CloseButton��#��[ .���� -    ��Name$ 
CloseImage��$��Right=�$��30��$��0,5,5,0q�$	��Top=�$��30��$ ��/Resources/CloseButton.png���� -    ��Name$ OkButton��$��OK��$��Center=�$��Center=�$��	0,210,0,0q�$��27��$	��100��$��
Sitka Text)�$��20(�$��1�� K� ZIndex$ 1 ������ $	 1,1&�$	 0,0&�.��� $ LightSlateGray��$
	 0.02Q�� $ 
WhiteSmoke��$
	 0.22Q�� $ 	#FFDFE0E2��$
	 0.53Q�