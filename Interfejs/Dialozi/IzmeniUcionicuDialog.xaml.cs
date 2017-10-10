using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zadatak2.Klase;

namespace Zadatak2.Interfejs.Dialozi
{
    /// <summary>
    /// Interaction logic for IzmeniUcionicuDialog.xaml
    /// </summary>
    public partial class IzmeniUcionicuDialog : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public Ucionica Ucionica { get; set; }
            public List<Softver> Softveri { get; set; }
            public List<CheckBox> SviSoftveri { get; set; }
            public string stariIdUcinice;
        }

        public IzmeniUcionicuDialog(Ucionica ucionica)
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Ucionica = ucionica;
            vm.stariIdUcinice = ucionica.Id;
            vm.Softveri = DataDAO.getDataDAO().Softveri;
            vm.SviSoftveri = new List<CheckBox>();
            this.DataContext = vm;

            int brojacSo = 0;
            foreach (Softver softver in vm.Softveri)
            {
                if (brojacSo % 6 == 0)
                {
                    ListaInstaliranogSoftvera.RowDefinitions.Add(new RowDefinition());
                }

                // Define StackPanel to CheckBox
                StackPanel sp = new StackPanel();
                sp.MinHeight = 56;
                sp.MaxHeight = 56;
                sp.Margin = new System.Windows.Thickness(5, 2, 5, 2);

                // Define tag which is CheckVox
                CheckBox cb = new CheckBox();
                cb.FontSize = 24;
                cb.Margin = new System.Windows.Thickness(5, 5, 5, 5);
                cb.Content = softver.Naziv;

                foreach (Softver softverOznacen in vm.Ucionica.InstaliranSoftver)
                {
                    if (softverOznacen.Id == softver.Id)
                    {
                        cb.IsChecked = true;
                        break;
                    }
                }


                vm.SviSoftveri.Add(cb);

                sp.Children.Add(cb);
                Grid.SetColumn(sp, brojacSo % 6);
                Grid.SetRow(sp, brojacSo / 6);
                ListaInstaliranogSoftvera.Children.Add(sp);
                ++brojacSo;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovu ucionicu. Predmet ce ostati nepromenjen. Da li ste sigurni?", "Odustani od unosa", System.Windows.MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            vm.Ucionica.InstaliranSoftver.Clear();
            for (int i = 0; i < vm.SviSoftveri.Count; ++i)
            {
                if (vm.SviSoftveri[i].IsChecked == true)
                {
                    vm.Ucionica.InstaliranSoftver.Add(vm.Softveri[i]);
                }
            }

            List<Ucionica> ucionice = new List<Ucionica>();
            foreach (Ucionica ucionica in DataDAO.getDataDAO().Ucionice)
            {
                if (ucionica.Id == vm.stariIdUcinice)
                {
                    ucionice.Add(vm.Ucionica);
                }
                else
                {
                    ucionice.Add(ucionica);
                }
            }
            DataDAO.getDataDAO().Ucionice = ucionice;
            DataDAO.getDataDAO().writeUcionicaToFile<Ucionica>(ucionice);
            this.Close();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                IInputElement focusedControl = FocusManager.GetFocusedElement(this);

                if (focusedControl is DependencyObject)
                {
                    string str = PomocniSistem.GetHelpKey((DependencyObject)focusedControl);
                    string sec = PomocniSistem.GetHelpSection((DependencyObject)focusedControl);

                    if (str == "index")
                    {
                        PomocniSistem.ShowHelp("IzmenaUcionica", "#", this);
                        return;
                    }

                    PomocniSistem.ShowHelp(str, sec, this);
                }
                else
                {
                    PomocniSistem.ShowHelp("IzmenaUcionica", "#", this);
                }
            }
            catch
            {
                PomocniSistem.ShowHelp("IzmenaUcionica", "#", this);
            }
        }
    }
}
