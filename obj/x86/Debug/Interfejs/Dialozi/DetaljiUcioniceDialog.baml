   M S B A M L   `   `   `  ����C  ?Zadatak2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Zadatak2.BindersZadatak2.Binders  5   	   6    0Zadatak2.Interfejs.Dialozi.DetaljiUcioniceDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   1binderclr-namespace:Zadatak2.Binders     5   	     � Title$  DetaljiUcioniceDialog��5   	   $	��600��6'   $	��800��64    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�6.    � 	Resources 5      /  )Zadatak2.Binders.BooleanToStringConverter   BooleanToString,  &Zadatak2.Binders.EnumToStringConverter  EnumToString��%:   &	        &	        5
   
   6*     5   
   6'   5      .��5      � $��0q�6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0��6   $��3��6,   .+�5      �  5       �� 5!      $
��Auto �6$    5$      �� 5%      $	��128 �6'   �� 5&      $��* �6'   .+�5)      � $��0��6   $��1��6$   $��2��64   .+�5*      ��  �� Orientation$ Vertical=�6!   $��5,5,5,5q�68   .+�5+      �� ���#666$��24(�6Y   ��6$   ��  �� Path$ Ucionica.Id�6Y   �� 5,      $��22(�6    $��SemiBold"�6.   .��6D    Instaliran softver u ucionici:6d   �� 5-      $��22(�6#   
������ $��0q�6J   $��0,10,0,0q�6^   $��48��6p   ��5.      �� $  Ucionica.InstaliranSoftver���5/      �� 50   "   .R�51   &   � ��60   �� * (FrameworkElement.ActualWidth)+ �� RelativeSource �� 	 �� AncestorType$	 ScrollContentPresenterL�
 � 	ItemWidth
 53   )   �� *(ListView.View).ItemWidth+ �� $	 ListViewL���55   )   �� *	ItemWidth+ �� *Self+ � 
ItemHeight 56   )   �� *(ListView.View).ItemHeight+ �� $	 ListViewL�58   #   5:      5;      5<      5=      5>      � 5@   
   $��1��6   $��3��6   .+�5A      �� $ Vertical=�6   .+�5B      �� $��22(�6   $��SemiBold"�6&   .��6<   Opis Predmeta:6L   �� 5C      $��24(�6@   $
��Wrap=�6N   ���#333 �� Padding$ 10,10,10,10q�5D   .   $��Stretch=�6D   ��5C      �� $ Ucionica.Opis�5D   D   5E      5F      � 5H   
   $��2��6   $��0��6   $��5,5,5,5q�6,   .+�5I      �� $ 
Horizontal=�6   ���#EEE.+�5K      �� $ Vertical=�6   .+�5L      �� $��22(�6   ���#666.��6<   Broj radnih mesta:6P   �� 5M      $��24(�6    ���#333��6A   �� *Ucionica.BrRadnihMesta+5N      5O      5P      � 5R   
   $��2��6   $��1��6   $��5,5,5,5q�6,   .+�5S      �� $ 
Horizontal=�6   ���#EEE.+�5U      �� $ Vertical=�6   .+�5V      �� $��22(�6   ���#666.��6<   Operativni sistem u ucionici:6[   �� 5W      $��24(�6    ���#333��6A   �� *Ucionica.OperativniSistemU+ �� 	Converter# [ 5X      5Y      5Z      � 5\   
   $��2��6   $��3��6   $��5,5,5,5q�6,   .+�5]      �� $ 
Horizontal=�6   ���#EEE.+�5_      �� $ Vertical=�6   .+�5`      �� $��22(�6   ���#666.��6<   Ima projektor:6L   �� 5a      $��24(�6    ���#333��6A   �� *Ucionica.ImaProjektor+# [  5b      5c      5d      � 5f   
   $��3��6   $��0��6   $��5,5,5,5q�6,   .+�5g      �� $ 
Horizontal=�6   ���#EEE.+�5i      �� $ Vertical=�6   .+�5j      �� $��22(�6   ���#666.��6<   
Ima tablu:6H   �� 5k      $��24(�6    ���#333��6A   �� *Ucionica.ImaTablu+# [  5l      5m      5n      � 5p   
   $��3��6   $��1��6   $��5,5,5,5q�6,   .+�5q      �� $ 
Horizontal=�6   ���#EEE.+�5s      �� $ Vertical=�6   .+�5t      �� $��22(�6   ���#666.��6<   Ima pametnu tablu:6P   �� 5u      $��24(�6    ���#333��6A   �� *Ucionica.ImaPametnuTablu+# [  5v      5w      5x      5y      5z      