   M S B A M L   `   `   `  ����C  ?Checkers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Checkers.MVVM.ViewCheckers.MVVM.View  5      "    Checkers.MVVM.View.AboutView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:Checkers.MVVM.View     5      $
��1200��5      $	��900��5	      .��5      �� .+�5   
   �� $&�� /Assets/Images/checkers-home.jpg��6   $��UniformToFill=�6:   ��5      �� *Width+  �� RelativeSource  �� *FindAncestor+ �� AncestorType" ���5      �� *Height+  �� *FindAncestor+" ��� 5   
   $!��/Assets/Images/Checkers.png��6    �� Left$	 480��5      
 �� Top$	 103��5      $	��250��5      $
��Left=�5      $��Center=�5       J� InputBindings 5      d� 5       d� Gesture$ 	LeftClicka�6    �� Command 64   �� *!DataContext.NavigateHomeCommand+  �� "   Checkers.MVVM.View.AboutView"  5      �� 5   
   $��/Assets/Images/text.png��6   $	 580��5      $	 172��5      $��40��5      $
��Left=�5       $	��Top=�5!       5"      d� 5#      $ 	LeftClicka�6    64   �� *!DataContext.NavigateHomeCommand+  �� "  5%      �� 5&   
   $��Teulea Ioan-Octavian��6   $��36(�60   $
��Bold"�6>   $	 427��6P   $	 270��6b   �� 5)   
   $��Grupa 10LF323��6   $��24(�6)   $��Medium"�67   $��Center=�6K   $	��Top=�6h   $	 520��6�   $	 323��6�   �� 5*   
   $���� Checkers este un joc clasic de strategie pentru doi jucători. Fiecare jucător își plasează piesele pe tabla de joc și încearcă să captureze piesele adversarului, să împiedice mișcarea acestora sau să le blocheze. Obiectivul este să rămâi cu mai multe piese sau să îți împiedici adversarul in a avea mutari posibile.��6   $��20(�5+      $��
Montserrat)�5,      $
��Bold"�5-      
���   �
������ $
��Left=�50      $	��Top=�6-   $	��700��51      $	��400��6    �� 
IsReadOnly . $��0q�53      
������  �� TextWrapping$
 Wrap=�56      $	 255��6&   $	 426��68   58      59      