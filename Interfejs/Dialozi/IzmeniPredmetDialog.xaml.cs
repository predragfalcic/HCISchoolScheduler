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
    /// Interaction logic for IzmeniPredmetDialog.xaml
    /// </summary>
    public partial class IzmeniPredmetDialog : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public Predmet Predmet { get; set; }
            public List<Smer> Smerovi { get; set; }
            public List<Softver> Softveri { get; set; }
            public List<CheckBox> SviSmerovi { get; set; }
            public List<CheckBox> SviSoftveri { get; set; }
            public string StariIdPredmeta { get; set; }
        }

        public IzmeniPredmetDialog(Predmet predmet)
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Predmet = predmet;
            vm.Predmet.Naziv = predmet.Naziv;
            vm.Predmet.Opis = predmet.Opis;
            vm.Predmet.OperativniSistemP = predmet.OperativniSistemP;
            vm.Predmet.PotrebanProjektor = predmet.PotrebanProjektor;
            vm.Predmet.PotrebnaPametnaTabla = predmet.PotrebnaPametnaTabla;
            vm.Predmet.PotrebnaTabla = predmet.PotrebnaTabla;
            vm.Predmet.VelicinaGrupe = predmet.VelicinaGrupe;
            vm.Predmet.MinimalnaDuzinaTermina = predmet.MinimalnaDuzinaTermina;
            vm.Predmet.BrojTerminaPredmeta = predmet.BrojTerminaPredmeta;

            vm.Smerovi = DataDAO.getDataDAO().Smerovi;
            vm.Softveri = DataDAO.getDataDAO().Softveri;
            vm.SviSmerovi = new List<CheckBox>();
            vm.SviSoftveri = new List<CheckBox>();
            vm.StariIdPredmeta = predmet.Id;
            this.DataContext = vm;

            int brojacS = 0;
            foreach (Smer smer in vm.Smerovi)
            {
                if (brojacS % 6 == 0)
                {
                    ListaSmerova.RowDefinitions.Add(new RowDefinition());
                }

                // Define StackPanel to CheckBox
                StackPanel sp = new StackPanel();
                sp.MinHeight = 56;
                sp.MaxHeight = 56;
                sp.Margin = new System.Windows.Thickness(5, 2, 5, 2);

                // Define tag which is CheckVox
                CheckBox cb = new CheckBox();
                cb.Margin = new System.Windows.Thickness(5, 5, 5, 5);
                cb.Content = smer.Naziv;
                cb.FontSize = 24;

                foreach (Smer smerOznacen in vm.Predmet.Smerovi)
                {
                    if (smerOznacen.Id == smer.Id)
                    {
                        cb.IsChecked = true;
                        break;
                    }
                }

                vm.SviSmerovi.Add(cb);

                sp.Children.Add(cb);
                Grid.SetColumn(sp, brojacS % 6);
                Grid.SetRow(sp, brojacS / 6);
                ListaSmerova.Children.Add(sp);
                ++brojacS;
            }

            int brojacSo = 0;
            foreach (Softver softver in vm.Softveri)
            {
                if (brojacSo % 6 == 0)
                {
                    ListaSoftvera.RowDefinitions.Add(new RowDefinition());
                }

                // Define StackPanel to CheckBox
                StackPanel sp = new StackPanel();
                sp.MinHeight = 56;
                sp.MaxHeight = 56;
                sp.Margin = new System.Windows.Thickness(5, 2, 5, 2);

                // Define tag which is CheckVox
                CheckBox cb = new CheckBox();
                cb.Margin = new System.Windows.Thickness(5, 5, 5, 5);
                cb.Content = softver.Naziv;
                cb.FontSize = 24;
                foreach (Softver softverOznacen in vm.Predmet.NeophodanSoftver)
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
                ListaSoftvera.Children.Add(sp);
                ++brojacSo;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovaj predmet. Predmet ce ostati nepromenjen. Da li ste sigurni?", "Odustani od unosa", System.Windows.MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            vm.Predmet.Smerovi.Clear();
            for (int i = 0; i < vm.SviSmerovi.Count; ++i)
            {
                if (vm.SviSmerovi[i].IsChecked == true)
                {
                    vm.Predmet.Smerovi.Add(vm.Smerovi[i]);
                }
            }

            vm.Predmet.NeophodanSoftver.Clear();
            for (int i = 0; i < vm.SviSoftveri.Count; ++i)
            {
                if (vm.SviSoftveri[i].IsChecked == true)
                {
                    vm.Predmet.NeophodanSoftver.Add(vm.Softveri[i]);
                }
            }

            List<Predmet> predmeti = new List<Predmet>();
            foreach (Predmet predmet in DataDAO.getDataDAO().Predmeti)
            {
                if (predmet.Id == vm.StariIdPredmeta)
                {
                    predmeti.Add(vm.Predmet);
                }
                else
                {
                    predmeti.Add(predmet);
                }
            }
            DataDAO.getDataDAO().Predmeti = predmeti;
            DataDAO.getDataDAO().writePredmetToFile<Predmet>(predmeti);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            return;
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
                        PomocniSistem.ShowHelp("IzmenaPredmeta", "#", this);
                        return;
                    }

                    PomocniSistem.ShowHelp(str, sec, this);
                }
                else
                {
                    PomocniSistem.ShowHelp("IzmenaPredmeta", "#", this);
                }
            }
            catch
            {
                PomocniSistem.ShowHelp("IzmenaPredmeta", "#", this);
            }
        }
    }
}
