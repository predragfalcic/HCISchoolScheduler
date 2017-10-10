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
    /// Interaction logic for DodajSmerDialog.xaml
    /// </summary>
    public partial class DodajSmerDialog : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public Smer Smer { get; set; }
        }

        public DodajSmerDialog()
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Smer = new Smer();
            vm.Smer.DatumUvodjenjaSmera = DateTime.Now;
            this.DataContext = vm;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DataDAO.getDataDAO().Smerovi.Add(vm.Smer);
            DataDAO.getDataDAO().writeSmerToFile<Smer>(DataDAO.getDataDAO().Smerovi);
            MessageBox.Show("Smer je uspesno kreiran.");
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovaj smer. Da li ste sigurni?", "Odustani od unosa", System.Windows.MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
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
                        PomocniSistem.ShowHelp("Smer", "#", this);
                        return;
                    }

                    PomocniSistem.ShowHelp(str, sec, this);
                }
                else
                {
                    PomocniSistem.ShowHelp("Smer", "#", this);
                }
            }
            catch
            {
                PomocniSistem.ShowHelp("Smer", "#", this);
            }
        }
    }
}
