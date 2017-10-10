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
    /// Interaction logic for IzmeniSmer.xaml
    /// </summary>
    public partial class IzmeniSmer : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public Smer Smer { get; set; }
            public string StariIdSmera { get; set; }
        }

        public IzmeniSmer(Smer smer)
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Smer = smer;
            vm.StariIdSmera = smer.Id;
            this.DataContext = vm;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovaj smer. Predmet ce ostati nepromenjen. Da li ste sigurni?", "Odustani od unosa", System.Windows.MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<Smer> smerovi = new List<Smer>();
            foreach (Smer smer in DataDAO.getDataDAO().Smerovi)
            {
                if (smer.Id == vm.StariIdSmera)
                {
                    smerovi.Add(vm.Smer);
                }
                else
                {
                    smerovi.Add(smer);
                }
            }
            DataDAO.getDataDAO().Smerovi = smerovi;
            DataDAO.getDataDAO().writeSmerToFile<Smer>(smerovi);
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
                        PomocniSistem.ShowHelp("IzmenaSmera", "#", this);
                        return;
                    }

                    PomocniSistem.ShowHelp(str, sec, this);
                }
                else
                {
                    PomocniSistem.ShowHelp("IzmenaSmera", "#", this);
                }
            }
            catch
            {
                PomocniSistem.ShowHelp("IzmenaSmera", "#", this);
            }
        }
    }
}
