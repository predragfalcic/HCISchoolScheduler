   M S B A M L   `   `   `  ����C  ?Zadatak2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Zadatak2.KlaseZadatak2.Klase  5   	   8    2Zadatak2.Interfejs.Dialozi.PrikaziSveSmeroveDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   *lclr-namespace:Zadatak2.Klase     5   	     � Title$  Smerovi��5   	   $	��600��6   $	��800��6&    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�6.    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5	   
   -   5           Command$ ApplicationCommands.Help��5	      6<    � 	Resources 5         View �� 	UriSource  Edit 
 Delete �� 
TargetType  HeaderButton �� Property	 �� Value
 �� Path �� UpdateSourceTrigger  DataGridCellStyle*  $System.Windows.Controls.DataGridCell �� 
TargetType��%X  &	        &	 -     &	 Z     &	 �     &	 h    �� 5   
   $ /Slike/ViewItem.png?�6#   �� 5   
   $ /Slike/EditItem.png?�6$   �� 5   
   $ /Slike/DeleteItem.png?�6&   �� 5   
   " ��6   .�5      ��  � ��$
	 10,5q�6'   �� 5       � ��$	 30��6*   �� 5       � ��$	 4q�6(   �� 5       � {�	 6*   �� $
 ButtonEnabled�$ PropertyChanged=�5      �� 5   
   "  5   	   .�5      ��  � ��	 5      �� 5      "  6&   .R�5      � #��z��6   .+�5      �� $��Center=�6/   5      5       5"      5#      5$      .��5&      �  � RowDefinitions 5'   
   �� 5(      $
��Auto �6   �� 5)      $
��Auto �6   �� 5*       � ColumnDefinitions 5,   
   �� 5-      .+�51   
   � ���#F5F5F5 52      �� 53      $
��Auto �6    �� 54      $
��Auto �6    �� 55      $
��Auto �6    �� 56      $
��Auto �6    �� 57      $
��Auto �6     5:      �� 5;      $��1* �6#   �� 5<      $��1* �6#   �� 5=      $��1* �6#   .+�5@      �� ���#BBB$��1q�6(   $��2��6<   $��4,4,4,4q�6M   ._�5A      � $��4,4,4,4q�6   .+�5B      �� $��22(�6   .��6*   Oznaka Smera:69   �� 5C      -   5        $��IdSmeraPolje��5C   ,   $��24(�6   $��37��6@   "   Zadatak2.Klase.PomocniSistem   HelpKey$ PretragaSmera��5E         HelpSection$ id��6F   $��
0,36,0,-10q�6g   ��5D      �� $
 SmerId�$ PropertyChanged=�5E   g   5F      5G      �� 5J      $��1��6   $��3��6%   ���#BBB$��1q�6I   $��4,4,4,41q�6]   $��79��6o   ._�5K      � $��4,4,4,4q�6    5L      �� 5M      $
��Auto �6(   �� 5N      $	��26* �6(   .+�5P      �� $��22(�6   .��6*   Naziv Smera:68   �� 5Q      -   5        $��NazivSmeraPolje��5Q   9   $��24(�6   $��1��6,   $ PretragaSmera��5S      $ naziv��6F   $��34��6j   $��0,0,0,-9q�6v   ��5R      �� $
 	SmerNaziv�$ PropertyChanged=�5S   v   5T      5U      �� 5X      $��2��6   $��2��6"   ���#BBB$��1q�6F   $��4,4,4,4q�6Z   ._�5Y      � $��4,4,4,4q�6    5Z      �� 5[      $
��Auto �6(   �� 5\      $��26 �6(   .+�5_      �� $��22(�6   $��0��6*   $��0��67   .��6G   Datum Uvodjenja:6Y   � 5`      $��1��6   $��0��6(    5a      �� 5b      $
��Auto �6/   �� 5c      $��* �6/   �� 5d      $
��Auto �6/   �� 5e      $��* �6/   .+�5h      �� $��22(�6    $��0,0,0,-9q�6.   .��6@   Od6D   (  "System.Windows.Controls.DatePicker  5i      $��24(�6%   $��1��63   $ PretragaSmera��5j   %   $ datum_uvodjenja��6M   $��0,1,0,-9q�6{      SelectedDate 5i   C   �� $
 SmerBeg�$ PropertyChanged=�5j   {   �� 5k      $��22(�6    $��2��6.   $��0,0,0,-9q�6>   .��6P   do6T     5l      $��24(�6%   $��3��63   $ PretragaSmera��5m   %   $ datum_uvodjenja��6M   $��	0,1,0,-10q�6{    5l   C   �� $
 SmerEnd�$ PropertyChanged=�5m   {   5n      5o      5p      �� 5r      -   5        $��Search��5r   #   $��24(�6   $��2��61   $��20,10,20,18q�6A   $��32��6V   $��36��6z   .��6�   
Pretrazi5t      5v      � 5y   
   $��1��6   ���#DDDDDD.+�5z      �  5{      �� 5|       5~      �� 5      �� 5�      �� 5�      .+�5�      �� -   5        $��PogledaSmer��5�   7   $��22(�6   $��0��6'    � ToolTip$- 'Pogledaj informacije o odabranom smeru.��6J   #��[ 6|   .��5�      ��  �� Orientation$ 
Horizontal=�6!   .+�5�      �� #��[  6    �� 5�      $��Center=�6$   $��6,0,2,0q�6?   $��37��6P   Vidi detalja6j   5�      5�      �� 5�      -   5        $��
IzmeniSmer��5�   7   $��22(�6   $��1��6'   $ Izmeni odabrani smer.��6I   #��[ 6i   .��5�      �� $ 
Horizontal=�6!   .+�5�      �� #��[ 6    �� 5�      $��Center=�6$   $��6,0,2,0q�6?   $��37��6P   Izmeni6d   5�      5�      �� 5�      -   5        $��
ObrisiSmer��5�   7   $��22(�6   $��2��6'   $ Obrisi odabrani smer��6I   #��[ 6h   .��5�      �� $ 
Horizontal=�6!   .+�5�      �� #��[ 6    �� 5�      $��Center=�6$   $��6,0,2,0q�6?   $��35��6P   Obrisi6d   5�      5�      5�      5�      &   System.Windows.Controls.DataGrid  5�   
   -   5          Name$ TabelaSmerova��5�   .   $��24(�6   $��2��6!      AutoGenerateColumns .     	CellStyle# [ 5�         
IsReadOnly . ��5�      �� $
 Smerovi� �� SelectedItem 5�      �� $
 IzabranSmer�$ PropertyChanged=�   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Oznaka Smera��6%      MinWidth$	 128Q�6;      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6J   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6T   �� *Id+  5�      $ Naziv Smera��6%   $	 128Q�6;   $ * 6J    6T   �� *Naziv+  5�      $ 
Opis Smera��6%   $	 256Q�6;   $ * 6J    6T   �� *Opis+5�      5�      5�      