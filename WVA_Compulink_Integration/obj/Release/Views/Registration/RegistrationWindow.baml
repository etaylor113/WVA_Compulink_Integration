   M S B A M L   `   `   `  ���� T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:WVA_Compulink_Integration.Views.Registration,WVA_Compulink_Integration.Views.Registration  E    ?WVA_Compulink_Integration.Views.Registration.RegistrationWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Llocal:clr-namespace:WVA_Compulink_Integration.Views.Registration       � Title$  CreateAccountWindow��$	��360��$	��500�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � 	Resources    ExamsButtonGroup �� 
StartPoint �� EndPoint � Color � Offset	 �� 
TargetType
 �� Property �� Value �� 
TargetType �Name ��Name �� CornerRadius 
� GradientOrigin �� RelativeTransform �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y �� Triggers P� Property P� Value �� 
TargetName  WVA_Blue_Color��%�  &	        '�� �     7   7  �� $	 0,0&�$	 1,1&�.��� $ LightSlateGray��$
 0.02Q�� $ 
WhiteSmoke��$
 0.22Q�� $ LightSlateGray��$	 .53Q��� "	 ��.��� 
 � ��8   �� 
 � ��$ Center=��� 
 � ��
 ������� 
 � ��$ 
Sitka Text)��� 
 � ��$ Center=��� 
 � ��$ 1q��� 
 � �� �� " ��.R�� $
 grid��.+��� $ border��$ 3,3,3,3��
���   �$��1.6,1.9,2.9,2.9q���
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$	 1.5Q�$	 1.5Q�T� $
 0.02Q�$	 0.3Q�.��� $ 1Q�$ 	#FF327EC3��� $	 0.3Q�$ 	#FF327EC3��._��� $��Center=�$��Center=�$��Regular"� P�  �@J�IsMouseOver . . ��� 
 � ��
 �   ��� $ border��
 � �� 
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$ 2Q�$ 2Q�T� $
 0.02Q�$	 0.9Q�.��� $ 	#FF327EC3��$ 1Q�� $ White��$	 0.3Q�P�  � �� . . ��� 
 � ��
 �   ��� $ border��
 � �� 
� $ 0.496,1.052&� V� .~��� $	 0.5Q�$	 0.5Q�$ 2Q�$ 2Q�T� $
 0.02Q�$	 0.9Q�.��� $ White��$ 1Q�� 8  $	 0.3Q�P�  � {� .  . ��� $
 grid��
 �@J�Opacity$
 0.25Q�.��� ���� $ 0.5,1&�$ 0.5,0&�.��� $ White��� $ 	#FF327EC3��$ 1Q�� $ 	#FF9ABFE1��$ 0.514Q� � ColumnDefinitions  � RowDefinitions �� $��50 ��� $��60 ��� $��60 ��� $��60 ��� $��60 ��� $��60 ��� $��* �.+��� $��Register��$��Center=�$��0��$��DemiBold"�$��0,0,0,0q�$	��Top=�$��24(��� 
���   �$��0��$��90��$��1��$��Center=�$��Bottom=�$��0,0,0,18q��� $��Email��$��Center=�$��1��$��	0,0,135,0q�$	��Top=�$	��100��$��18(��� -    ��Name$ EmailTextBox�� �� 	MaxLength$ 25��$��Center=�$��28��$��1��$��0,0,0,0q�  �� TextWrapping$
  Wrap=�$��Bottom=�$	��233��$��16(��� $��Username��$��Center=�$��2��$��	0,0,140,0q�$	��Top=�$��18(��� -   $ UserNameTextBox��$ 20��$��Center=�$��28��$��2��$��0,0,0,0q�$
  Wrap=�$��Bottom=�$	��233��$��16(��� $��Password��$��Center=�$��	0,0,150,0q�$	��Top=�$��3��$��18(�G� -   $��PasswordTextBox��! G� 	MaxLength$! 20��$��Center=�$��0,0,0,0q�$��Bottom=�$��3��$��28��$	��233���� $��Confirm Password��$��Center=�$��0,0,85,0q�$��4��$	��Top=�$��18(�G� -   " G�Name$" ConfirmPasswordTextBox��$! 20��$��Center=�$��0,0,0,0q�$��4��$��Bottom=�$��28��$	��233���� -   # ��Name$# SubmitButton��$��Submit��$��Center=�$��0,20,0,0q�$	��Top=�$��5��$��95��$��30��$��18(��� -   $ ��Name$$ BackToLoginLink��$��0,0,5,5q�$��14(�$��5��$��Right=�$��Bottom=�|�.  �� ���	#FF062B7C.�.��� $�Back to login���� -   $��NotifyLabel��$��Center=�$�� ��$��-0,0,0,0q�$��7��$��Bottom=�$��15(�$y�Hidden=�$��60��$
��Wrap=�$	��300��