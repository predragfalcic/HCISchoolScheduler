   M S B A M L   `   `   `  ����C  ?Zadatak2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Zadatak2.BindersZadatak2.Binders  5   	   3    -Zadatak2.Interfejs.Dialozi.DetaljiSmeraDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   1binderclr-namespace:Zadatak2.Binders     5   	     � Title$  DetaljiSmeraDialog��5   	   $	��600��6$   $	��800��61    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�6.    � 	Resources 5      /  )Zadatak2.Binders.BooleanToStringConverter   BooleanToString,  &Zadatak2.Binders.EnumToStringConverter  EnumToString��%:   &	        &	        5
   
   6*     5   
   6'   5      .��5      � $��0q�6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0��6   $��3��6,   .+�5      �  5      �� 5      $
��Auto �6$    5!      �� 5"      $	��128 �6'   �� 5#      $��* �6'   .+�5&      � $��0��6   $��1��6$   $��2��64   .+�5'      ��  �� Orientation$ Vertical=�6!   $��5,5,5,5q�68   .+�5(      �� ���#666$��24(�6U   ��6$   ��  �� Path$ Smer.Id�6U   �� 5)      ���#333$��24(�6X   $
��Bold"�6f   ��6$   �� $ 
Smer.Naziv�6f   5*      5+      5,      5-      � 5/   
   $��1��6   $��3��6   .+�50      �� $ Vertical=�6   .+�51      �� $��22(�6   $��SemiBold"�6&   .��6<   Opis Smera:6I   �� 52      $��24(�6<   $
��Wrap=�6J   ���#333 �� Padding$ 10,10,10,10q�53   .   $��Stretch=�6D   ��52      �� $ 	Smer.Opis�53   D   54      55      � 57   
   $��2��6   $��0��6   $��5,5,5,5q�6,   .+�58      �� $ 
Horizontal=�6   ���#EEE.+�5:      �� $ Vertical=�6   .+�5;      �� $��22(�6   ���#666.��6<   Datum Uvodjenja:6N   �� 5<      $��24(�6    ���#333��6A   �� *Smer.DatumUvodjenjaSmera+	 �� StringFormat$	 {0:dd. MM. yyyy.}��5=      5>      5?      5@      5A      