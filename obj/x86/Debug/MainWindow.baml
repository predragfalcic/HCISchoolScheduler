   M S B A M L   `   `   `  ����C  ?Zadatak2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Zadatak2.CommandsZadatak2.Commands  5   	   9!clr-namespace:Zadatak2.ValidacijaZadatak2.Validacija  5   	       Zadatak2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   4commandsclr-namespace:Zadatak2.Commands     5   	   8
validacija!clr-namespace:Zadatak2.Validacija     5   	   -   5          � Title$  Racunarski Centar��5   	   $	��799��6#   $
��1631��60    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�6.    J� InputBindings 5
      �� 5   
    �� 	Modifiers$ Controlf�6   
 �� Key$ U��6)    �� Command$% commands:Commands.DodajUcionicu��5      �� 5   
   $ 
Ctrl+Shiftf�6   $ U��6,   $* $commands:Commands.PrikaziSveUcionice��5      �� 5   
   $ Controlf�6   $ P��6)   $$ commands:Commands.DodajPredmet��5      �� 5   
   $ 
Ctrl+Shiftf�6   $ P��6,   $* $commands:Commands.PrikaziSvePredmete��5      �� 5   
   $ Controlf�6   $ S��6)   $! commands:Commands.DodajSmer��5      �� 5   
   $ 
Ctrl+Shiftf�6   $ S��6,   $) #commands:Commands.PrikaziSveSmerove��5      �� 5   
   $ Controlf�6   $ F��6)   $$ commands:Commands.DodajSoftver��5      �� 5   
   $ 
Ctrl+Shiftf�6   $ F��6,   $* $commands:Commands.PrikaziSveSoftvere��5       � 	Resources 5          Add �� 	UriSource 
 Delete  Edit  View  DemoMode  Help	 �� 
TargetType  ToolbarButton
 �� Property �� Value  	ErrorIcon��%�  &	        &	 ,     &	 [     &	 �     &	 �     &	 �     &	     &	 {    �� 5"   
   $ /Slike/AddItem.png?�6"   �� 5#   
   $ /Slike/DeleteItem.png?�6%   �� 5$   
   $ /Slike/EditItem.png?�6#   �� 5%   
   $ /Slike/ViewItem.png?�6#   �� 5&   
   $ /Slike/DemoMode.jpg?�6'   �� 5'   
   $ /Slike/Help.png?�6#   �� 5)   
   "	 ��6   .�5*      �� 
 � ��$ 1,2,1,2q�6'   �� 5+      
 � ��$ 4q�6(   5,      �� 5-   
   $ Slike/error.png?�6(   5.      .��5/      ]� .+�50   
   o� $	��Top=�6   ���#ddd$��22(�67   ��51      �� 53      .R�55      ]� $
��Left=�6    6=   57      .@�59      m� $��	_Ucionica��6   .@�5:      m� -   5         m� InputGestureText$ Ctrl+U��5:      $��_Dodaj Ucionicu��65    m� Icon 5;      �� 5<      $��20��6    $��20��6,   #��[  67   5>      m� 5?      -   5        $ Ctrl+Shift+U��5?      $��_Prikazi Ucionice��6;    5A      �� 5B      $��20��6    $��20��6,   #��[ 67   5D      5E      m� 5F      $��_Predmet��6   .@�5G      m� $ Ctrl+P��6   $��_Dodaj Predmet��65    m� Command$$ commands:Commands.DodajPredmet��5H       5I      �� 5J      $��20��6    $��20��6,   #��[  67   5L      m� 5M      $ Ctrl+Shift+P��6   $��_Prikaži Predmete��6;   $* $commands:Commands.PrikaziSvePredmete��5N       5O      �� 5P      $��20��6    $��20��6,   #��[ 67   5R      5S      m� 5T      $��_Smer��6   .@�5U      m� $ Ctrl+S��6   $��_Dodaj Smer��65   $! commands:Commands.DodajSmer��5V       5W      �� 5X      $��20��6    $��20��6,   #��[  67   5Z      m� 5[      $ Ctrl+Shift+S��6   $��_Prikaži Smerove��6;   $) #commands:Commands.PrikaziSveSmerove��5\       5]      �� 5^      $��20��6    $��20��6,   #��[ 67   5`      5a      m� 5b      $��_Softver��6   .@�5c      m� $ Ctrl+F��6   $��_Dodaj Softver��65   $$ commands:Commands.DodajSoftver��5d       5e      �� 5f      $��20��6    $��20��6,   #��[  67   5h      m� 5i      $ Ctrl+Shift+F��6   $��_Prikaži Softvere��6;   $* $commands:Commands.PrikaziSveSoftvere��5j       5k      �� 5l      $��20��6    $��20��6,   #��[ 67   5n      5o      m� 5p      $��_Pomoc��6   .@�5q      m� -   5        $ F5��5q      $��Demo mod��61    5r      �� 5s      $��20��6    $��20��6,   #��[ 68   5u      m� 5v      -   5        $ F1��5v      $��Online pomoc��61    5w      �� 5x      $��20��6    $��20��6,   #��[ 67   5z      5{      5|      ]� 5}   
   $��Right=�6   5      ]� 5�   
   $	��Top=�6   .+�5�      � $��Bottom=�6   .+�5�      � -   5        $��MainWindowGrid��5�      
����İ� � ColumnDefinitions 5�      �� 5�      $
��Auto �6+   �� 5�      $��* �6+   �� 5�      $
��Auto �6+    � RowDefinitions 5�      �� 5�      $��* �6(   .+�5�      �� -   5         ��Name$ PredmetiItems��5�   #   $��0��6:   ��6J   ��  �� Path$ Predmeti���5�      �� 5�      .R�5�   "   ��  �� Orientation$ Vertical=�6-   ���#34495E6D   5�      ��5�      �� 5�      .R�5�   "   �� -   5        $ 
Horizontal=�5�   -   ���#EEEEEE$��1,1,1,0q�6[    � Cursor$
 Hand��6l   
 � Tag 6z   �� $ Id�.+�5�   &   �� $ Vertical=�61   $��0,4,2,4q�6H   .+�5�   *   �� $��20(�64   $
��Bold"�6[   ��6B   �� $ Id�6[   �� 5�   *   $��20(�64   ��6B   �� $ Naziv��� 5�   *   $��20(�64   �� 5�   .   $�Broj casova predmeta: ��62    �� 5�   .   �62   �� $ MinimalnaDuzinaTermina�5�   +   5�   '   5�   #   5�      5�      �� 5�      -	   5        $��
SatnicaMap��5�      $��1��6/    J� 	AllowDrop .  �� 	IsEnabled .   5�      ��%�   '_�       �� 5�      "	 _�6=   .�5�   "   �� 
 � �� �#EEEEEE6?   �� 5�   "   
 � �� �#6666666@   �� 5�   "   
 �@_�HasDropShadow .  6B   5�      5�      ��5�      �� 5�       �� ImageSource$ Slike\Satnica.png��6)    j� 
AlignmentX$
 Left=�6I    j� 
AlignmentY$	 Top=�6[    j� Stretch$
 None=�6l    � ContextMenu 5�      �� 5�      -
   5        $��PredmetContextMenu��5�   *   .@�5�   "   m� $��20(�6+   $��Prikazi detalje predmeta��69   $. (commands:Commands.PrikaziDetaljePredmeta��5�   +    m� CommandParameter 5�   +   �� $ ClickedPredmet� 5�   &   �� 5�   *   $��20��60   $��20��6<   #��[ 6G   5�   #   m� 5�   "   $��20(�6+   $��Izmeni predmet��69   $% commands:Commands.IzmeniPredmet��5�   +    5�   +   �� $ ClickedPredmet� 5�   &   �� 5�   *   $��20��60   $��20��6<   #��[ 6G   5�   #   m� 5�   "   -   5        $��20(�5�   +   $!��Ukloni predmet iz rasporeda��69    5�   &   �� 5�   *   $��20��60   $��20��6<   #��[ 6G   5�   #   5�      5�      � 5�      $��2��6   
������� 5�      �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   .+�5�      �� $��0��6    $
��Left=�6-   .��5�      �� $��20(�6(   �� 5�   "   $&� Datum za koji kreirate raspored:��6&    �� 5�   "   $�*��6&   ���#D500006/   5�      5�   '   (  "System.Windows.Controls.DatePicker  5�   &   -   5        $��KreiranjeTerminaDatum��5�   L   $��22(�61   $��1��6?   |�.   5�   *   :  4System.Windows.Controls.Primitives.DatePickerTextBox��%�   '        �� 5�   .   "	  64   .�5�   2   �� 
 � ��$% Datum za koji kreirate raspored��6I   �� 5�   2   
 �@J�IsHitTestVisible .  6U   5�   /   5�   +      SelectedDate 5�   *   �� 5�   .   $ DatumRasporeda�66    �� UpdateSourceTrigger$ PropertyChanged=�6L   5�   /   5�   '   �� 5�   &   -   5        $��lblErrorDatum��5�   ,   $y�Hidden=�6A   $��2��6U   
���  ��$��20(�6s   .��6�   )'Za nedelju ne mozete kreirati raspored.6�   �� 5�   &   -   5        $��lblErrorDatumPreDanasnjeg��5�   ,   $y�Hidden=�6M   $��3��6a   
���  ��$��20(�6   .��6�   *(Datum ne moze biti stariji od danasnjeg.6�   �� 5�   &   $��4��6,   $
��Left=�69   .��5�   *   �� $��20(�64   �� 5�   .   $(�"Ucionica za koju pravite raspored:��62    �� 5�   .   $�*��62   ���#D500006;   5�   +   5�   '   �� 5�   &   -   5        $��cmbListaUcionica��5�   <   $��5��6/   $��22(�6T   ��6b   �� *
Ucionice+6�   �� 5�   &   -   5        $��lblErrorUcionica��5�   ,   $y�Hidden=�6D   $��6��6X   
���  ��$��20(�6v   .��6�    Molimo vas izaberite ucionicu.6�   �� 5�   &   -   5        $��UcioniceUlisti��5�   /   $��7��6E     d� 	IsChecked$  FalseY�6R   $��22(�6�   .��6�   Prikaz slobodnih ucionica6�   �� 5�   &   -   5        $��lblOsUcioniceText��5�   ,   $y�Hidden=�6E   $��8��6Y   $��22(�6f   .��6t   Operativni sistem u ucionici:6�   �� 5�   &   -   5        $��lblOsUcionice��5�   C   
����ح�$y�Hidden=�6X   $��9��6l   $��24(�6y   6�   �� 5�   &   -   5        $��lblSoftverUcioniceText��5�   ,   $��10��6J   $��22(�6X   .��6f   Softver u ucionici:6{   �� 5�   *   -   5        $��scrViewSoftver��5�   7   $��11��6M   $	��150��6[   .��5   *   � $��12��6/    5  .   �� 5  2   $
��Auto �6@   6P   .+�5  .   �� -   5        $��lblSoftverUcionice��5  4   
����ح�$y�Hidden=�6e   $��24(�6y   6�   5  +   5  '   5  #   5     5	     5
     5     5     