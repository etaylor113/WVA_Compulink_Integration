   M S B A M L   `   `   `  ���� T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:WVA_Compulink_Integration.Views.Login%WVA_Compulink_Integration.Views.Login  @    :WVA_Compulink_Integration.Views.Login.ChangePasswordWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Elocal3clr-namespace:WVA_Compulink_Integration.Views.Login       � Title$  Change Password��$	��330��$	��500�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�
������� � Topmost .  � 	Resources    ExamsButtonGroup �� 
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
 ��.���  � �� �� " ��.R��� .��� ���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF327EC3��$	 1Q�� $ White��$	 0.191Q�� $ 	#FF98BEE1��$	 0.698Q�.+��� $$��/Resources/WVA_Scan_Banner.PNG��$��0,0,0,0q�$	��Top=��� -   $��CloseButton��#��[ $��0��$��2��$��25��$��25��$��460,0,0,360q�.���� $ ��/Resources/CloseButton.png��$��0,0,0,0q��� $	��Top=�$��Center=�$��0,95,0,0q�.+��� -   $��PasswordLabel��$��New Password��$��
Sitka Text)�$��22(�$y�Visible=�$
��Left=�$��35��$��0,0,0,0q�G� -   $��PasswordTextBox��$��30��$	��250��$��
Sitka Text)�$��20(�$y�Visible=��� -   $��PasswordConfLabel��$��Confirm Password��$��
Sitka Text)�$��22(�$y�Visible=�$
��Left=�$��35��$��0,0,00,0q�G� -   $��PasswordConfTextBox��$��30��$	��250��$��
Sitka Text)�$��20(�$y�Visible=��� -    ��Name$ SubmitCodeButton��$��Submit��$��Center=�$��0,10,0,0q�$	��100��$��20(�$��33��$y�Visible=��� -   $��MessageLabel��$�� ��$��
Sitka Text)�$
��Wrap=� �� TextAlignment$ Center=�$��18(�$	��400��$y�Visible=�$��Center=�$��Bottom=�$��70��$��0,0,0,5q�