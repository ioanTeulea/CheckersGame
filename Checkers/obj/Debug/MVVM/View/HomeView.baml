   M S B A M L   `   `   `  ����C  ?Checkers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Checkers.MVVM.ViewCheckers.MVVM.View  5      !    Checkers.MVVM.View.HomeView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:Checkers.MVVM.View     5      .��5	      �� .+�5   
   �� $&�� /Assets/Images/checkers-home.jpg��6   $��UniformToFill=�6:   ��5   
   �� *Width+  �� RelativeSource  �� *FindAncestor+ �� AncestorType" ���5   
   �� *Height+  �� *FindAncestor+" ��� 5   
   $!��/Assets/Images/Checkers.png��6    �� Left$	 480��65   
 �� Top$	 103��6G   $	��250��6X   $
��Left=�6d   $��Center=�6   �� 5   
   $��/Assets/Images/text.png��6   $	 580��61   $	 172��6C   $��40��6T   $
��Left=�6_   $	��Top=�6z   �� 5   
   $��New Game��6   $	 480��6I   $	 317��6[   $
��Left=�6m   $	��Top=�6�   ��6$   �� *OpenPopupCommand+6�   �� 5   
   $	 485��6   $	 414��6#   $
��Left=�5   	   $	��Top=�6$   $��	Load Game��5   	   ��5   	   �� *DataContext.LoadGameCommand+  �� !   Checkers.MVVM.View.HomeView"  5   	   �� 5   
   $��About��6   $	 485��6J   $	 524��6\   $
��Left=�6n   $	��Top=�6�   ��6!   �� *NavigateAboutCommand+6�   � 5   
   -   5        $��OptionsPopup��5      $��Center=�6y   ��6$   �� *IsPopupOpen+ � PlacementTarget 6C   ��  �� ElementName$ NewGameButton��.��5      �� 
���   �$��2q�6)   ���	#CCFFFFFF$	��300��6T   $	��150��6`   ._�5      �� $��10q�6   $��Center=�6)   .+�5      �� $5��/Selectați opțiunea pentru sărituri multiple:��6    $
��Bold"�6T   $��Center=�6f   �� 5      -   5        $��JumpOptionsCheckBox��5      $��Sărituri multiple��6:   $��Center=�6�   $��Center=�6�    d� 	IsChecked 6V   �� *IsMultipleJumpsSelected+6�   �� 5       $��OK��6   $��Center=�6H   $��0,10,0,0q�6e   ��6*   �� *	OkCommand+6e   5!      5"      5#      5$      5&      