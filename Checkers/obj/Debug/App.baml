   M S B A M L   `   `   `  ����C  ?Checkers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:CheckersCheckers  5      A%clr-namespace:Checkers.MVVM.ViewModelCheckers.MVVM.ViewModel  5      7 clr-namespace:Checkers.MVVM.ViewCheckers.MVVM.View  6F       Checkers.App   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      (localclr-namespace:Checkers     5      ;	viewModel%clr-namespace:Checkers.MVVM.ViewModel     5      1view clr-namespace:Checkers.MVVM.View     6F     �� 	Resources  5       �� DataType+   %Checkers.MVVM.ViewModel.HomeViewModel!   Checkers.MVVM.View.HomeView+   %Checkers.MVVM.ViewModel.GameViewModel!   Checkers.MVVM.View.GameView,   &Checkers.MVVM.ViewModel.AboutViewModel"   Checkers.MVVM.View.AboutView �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius ��Name3  -System.Windows.Media.Effects.DropShadowEffect	   ShadowDepth
   
BlurRadius   Color �� Triggers P� Property P� Value �� Triggers �� 
TargetName��%�  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)'�� �     �� 5   
   "  6   .R�5	        5
      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   " ��6   .�5      ��  � ��
 ���� 6+   �� 5       � ��$ 0q�60   �� 5       � ��$ 5q�6'   �� 5       � ��$	 200��6&   �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 
Montserrat)�6+   �� 5       � ��$ 30(�6)   �� 5       � ��$ 15q�6(   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ border��6!   #��z��61   #��z��5       #��z��5!      $ 3��5"      ._�5#      � .+�5$   "   �� $ 	textBlock��6,   #��z��6?   $��Center=�5%       $��Center=�6=   ��5&   &   �� 5'   *   " ��60   .�5(   .   ��  �@J�Effect 5)   2     5*   6   $	 0Q�6G   $
 5Q�6W   $ Black��6f   5,   /    5-   .   P� 5.   2    �@J�IsMouseOver . . �5/   6   ��  �@J�Effect 50   :     51   >   $	 0Q�6O   $
 5Q�6_   $
 Gray��6n   53   7   54   3   56   +   58   #   59      5:       5;      P� 5=       �@J�IsMouseOver . . �5>   "   �� $ border��6)    � ��
 �����6S   5?      P� 5@       �@J�IsMouseOver .  . �5A   "   �� $ border��6)    � ��
 ���� 6S   5B      5D      5F      5G      5H      5I      