   M S B A M L   `   `   `  ����T  PWVA_Compulink_Integration, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:WVA_Compulink_Integration.Views.Search&WVA_Compulink_Integration.Views.Search  5   	   :    4WVA_Compulink_Integration.Views.Search.LoadingWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Flocal4clr-namespace:WVA_Compulink_Integration.Views.Search     5   	     � Title$  LoadingWindow��5   	   $��243.478��5	   	   $��426.087��5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � Topmost .  � 	Resources 5         CircleChase g� 
SpeedRatio g� RepeatBehavior �� TargetProperty	 �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame
 O� KeyTime O� Value��%t  &	        �� 5   
   $
 0.75Q�6)   $ Forever��6;   .��5      U� $ (UIElement.Opacity)�6,   $	 _2��6\   .\�5        $
 0��6'   $ 0.875Q�63     5      $
 0:0:0.1��6'   $ 1Q�69     5      $
 0:0:0.2��6'   $
 0.13Q�69     5      $
 0:0:0.3��6'   $
 0.25Q�69     5      $
 0:0:0.4��6'   $
 0.38Q�69     5      $
 0:0:0.5��6'   $	 0.5Q�69     5      $
 0:0:0.6��6'   $
 0.63Q�69     5      $
 0:0:0.7��6'   $
 0.75Q�69   5      U� 5      $ (UIElement.Opacity)�6,   $	 _3��6\   .\�5        $
 0��6'   $
 0.75Q�63     5      $
 0:0:0.1��6'   $ 0.875Q�69     5      $
 0:0:0.2��6'   $ 1Q�69     5       $
 0:0:0.3��6'   $
 0.13Q�69     5!      $
 0:0:0.4��6'   $
 0.25Q�69     5"      $
 0:0:0.5��6'   $
 0.38Q�69     5#      $
 0:0:0.6��6'   $	 0.5Q�69     5$      $
 0:0:0.7��6'   $
 0.63Q�69   5%      U� 5&      $ (UIElement.Opacity)�6,   $	 _4��6\   .\�5'        $
 0��6'   $
 0.63Q�63     5(      $
 0:0:0.1��6'   $
 0.75Q�69     5)      $
 0:0:0.2��6'   $
 0.88Q�69     5*      $
 0:0:0.3��6'   $ 1Q�69     5+      $
 0:0:0.4��6'   $
 0.13Q�69     5,      $
 0:0:0.5��6'   $
 0.25Q�69     5-      $
 0:0:0.6��6'   $
 0.38Q�69     5.      $
 0:0:0.7��6'   $	 0.5Q�69   5/      U� 50      $ (UIElement.Opacity)�6,   $	 _5��6\   .\�51        $
 0��6'   $	 0.5Q�63     52      $
 0:0:0.1��6'   $
 0.63Q�69     53      $
 0:0:0.2��6'   $
 0.75Q�69     54      $
 0:0:0.3��6'   $
 0.88Q�69     55      $
 0:0:0.4��6'   $ 1Q�69     56      $
 0:0:0.5��6'   $
 0.13Q�69     57      $
 0:0:0.6��6'   $
 0.25Q�69     58      $
 0:0:0.7��6'   $
 0.38Q�69   59      U� 5:      $ (UIElement.Opacity)�6,   $	 _6��6\   .\�5;        $
 0��6'   $ 0.375Q�63     5<      $
 0:0:0.1��6'   $	 0.5Q�69     5=      $
 0:0:0.2��6'   $
 0.63Q�69     5>      $
 0:0:0.3��6'   $
 0.75Q�69     5?      $
 0:0:0.4��6'   $
 0.88Q�69     5@      $
 0:0:0.5��6'   $ 1Q�69     5A      $
 0:0:0.6��6'   $
 0.13Q�69     5B      $
 0:0:0.7��6'   $ 0.255Q�69   5C      U� 5D      $ (UIElement.Opacity)�6,   $	 _7��6\   .\�5E        $
 0��6'   $
 0.25Q�63     5F      $
 0:0:0.1��6'   $
 0.38Q�69     5G      $
 0:0:0.2��6'   $	 0.5Q�69     5H      $
 0:0:0.3��6'   $
 0.63Q�69     5I      $
 0:0:0.4��6'   $
 0.75Q�69     5J      $
 0:0:0.5��6'   $
 0.88Q�69     5K      $
 0:0:0.6��6'   $ 1Q�69     5L      $
 0:0:0.7��6'   $
 0.13Q�69   5M      U� 5N      $ (UIElement.Opacity)�6,   $	 _8��6\   .\�5O        $
 0��6'   $ 0.125Q�63     5P      $
 0:0:0.1��6'   $
 0.25Q�69     5Q      $
 0:0:0.2��6'   $
 0.38Q�69     5R      $
 0:0:0.3��6'   $	 0.5Q�69     5S      $
 0:0:0.4��6'   $
 0.63Q�69     5T      $
 0:0:0.5��6'   $
 0.75Q�69     5U      $
 0:0:0.6��6'   $
 0.88Q�69     5V      $
 0:0:0.7��6'   $ 1Q�69   5W      U� 5X      $ (UIElement.Opacity)�6,   $	 _1��6\   .\�5Y        $
 0��6'   $ 1Q�63     5Z      $
 0:0:0.1��6'   $
 0.13Q�69     5[      $
 0:0:0.2��6'   $
 0.25Q�69     5\      $
 0:0:0.3��6'   $
 0.38Q�69     5]      $
 0:0:0.4��6'   $	 0.5Q�69     5^      $
 0:0:0.5��6'   $
 0.63Q�69     5_      $
 0:0:0.6��6'   $
 0.75Q�69     5`      $
 0:0:0.7��6'   $
 0.88Q�69   5a      5b      5c      ��5d      �� 5e   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5f      �  � Color$ White��6   � 5g      $ 	#FFB8D2EA��6    � Offset$ 0.813Q�6-   � 5h      $ 	#FF88B3DC��6   $ 1Q�6-   � 5i      $ 	#FF96BCE0��6   $ 0.952Q�6-   � 5j      $ 	#FFCCDEF0��6   $ 0.581Q�6-   5k       � Triggers 5m      :� 5n   
    :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5o      �� #��[  6   5p      .��5s      � .+�5t   
   A� -   5         A�Name$ _1��5t      ���	#FF327EC3$
��Left=�5v      $��14��5w      $��199,118,0,0q�5x      ���	#FF327EC3$	��Top=�5z      $��19��5{       J� RenderTransformOrigin$ 0.5,0.5&�5|      z�5}      V� 5~      .~�5      �� �� 5�      �� 5�      T� 5�      5�      5�      A� 5�   
   -   5        $ _2��5�      ���	#FF327EC3$
��Left=�5�      $��14��5�      $��227,118,0,0q�5�      ���	#FF327EC3$	��Top=�5�      $��19��5�      $ 0.5,0.5&�5�       J� Opacity$ 0.875Q�5�      z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�      T� 5�      5�      5�      A� 5�   
   -   5        $ _3��5�      ���	#FF327EC3$
��Left=�5�      $��15��5�      $��244,135,0,0q�5�      ���	#FF327EC3$	��Top=�5�      $��19��5�      $ 0.5,0.5&�5�      $
 0.75Q�5�      z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�      T� 5�      5�      5�      A� 5�   
   -   5        $ _4��5�      ���	#FF327EC3$
��Left=�5�      $��14��5�      $��244,156,0,0q�5�      ���	#FF327EC3$	��Top=�5�      $��19��5�      $ 0.5,0.5&�5�      $ 0.675Q�5�      z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�      T� 5�      5�      5�      A� 5�   
   -   5        $ _5��5�      ���	#FF327EC3$
��Left=�5�      $��13��5�      $��227,173,0,0q�5�      ���	#FF327EC3$	��Top=�5�      $��19��5�      $ 0.5,0.5&�5�      $	 0.5Q�5�      z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�      T� 5�      5�      5�      A� 5�   
   -   5        $ _6��5�      ���	#FF327EC3$
��Left=�5�      $��13��5�      $��199,173,0,0q�5�      ���	#FF327EC3$	��Top=�5�      $��19��5�      $ 0.5,0.5&�5�      $ 0.375Q�5�      z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�      T� 5�      5�      5�      A� 5�   
   -   5        $ _7��5�      ���	#FF327EC3$
��Left=�5�      $��14��5�      $��180,155,0,0q�5�      ���	#FF327EC3$	��Top=�5�      $��19��5�      $ 0.5,0.5&�5�      $
 0.25Q�5�      z�5�      V� 5�      .~�5�      �� �� 5�      �� 5      T� 5     5     5     A� 5  
   -   5        $ _8��5     ���	#FF327EC3$
��Left=�5	     $��14��5
     $��180,134,0,0q�5     ���	#FF327EC3$	��Top=�5     $��19��5     $ 0.5,0.5&�5     $ 0.125Q�5     z�5     V� 5     .~�5     �� �� 5     �� 5     T� 5     5     5     �� 5  
   $)��#/Resources/logo-plain-vector-72.png��6   $
��Left=�5     $	��109��5     $��
105,10,0,0q�5     $	��Top=�5      $	��236��5!     �� 5#  
   $��Loading Data...��6   $
��Left=�5$     $��133,195,0,0q�5%     $	��Top=�5&     $��36��5'     $	��181��5(     $��24(�5)     $��
Sitka Text)�5*     5,     5.     