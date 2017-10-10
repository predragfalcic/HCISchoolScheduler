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
using Xceed.Wpf.Toolkit;

namespace Zadatak2.Interfejs.Dialozi
{
    /// <summary>
    /// Interaction logic for IzmeniSoftverDialog.xaml
    /// </summary>
    public partial class IzmeniSoftverDialog : Window
    {

        private ViewModel vm;

        public class ViewModel
        {
            public Softver Softver { get; set; }
            public string StariIdSoftvera;
        }

        public IzmeniSoftverDialog(Softver s)
        {
            InitializeComponent();
            vm = new ViewModel();

            vm.Softver = s;
            vm.StariIdSoftvera = s.Id;

            this.DataContext = vm;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovaj softver. Predmet ce ostati nepromenjen. Da li ste sigurni?", "Odustani od unosa", System.Windows.MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            List<Softver> softveri = new List<Softver>();
            foreach (Softver softver in DataDAO.getDataDAO().Softveri)
            {
                if (softver.Id == vm.StariIdSoftvera)
                {
                    softveri.Add(vm.Softver);
                }
                else
                {
                    softveri.Add(softver);
                }
            }
            DataDAO.getDataDAO().Softveri = softveri;
            DataDAO.getDataDAO().writeSoftverToFile<Softver>(softveri);
            this.Close();
        }

        private void ButtonSpinner_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
            ButtonSpinner spinner = (ButtonSpinner)sender;
            TextBox txtBox = (TextBox)spinner.Content;
            try
            {
                int value = String.IsNullOrEmpty(txtBox.Text) ? 0 : Convert.ToInt32(txtBox.Text);
                if (e.Direction == Xceed.Wpf.Toolkit.SpinDirection.Increase)
                    value++;
                else
                    value--;
                txtBox.Text = value.ToString();
            }
            catch (FormatException)
            {
                txtBox.Text = "2000";
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
                        PomocniSistem.ShowHelp("IzmenaSoftvera", "#", this);
                        return;
                    }

                    PomocniSistem.ShowHelp(str, sec, this);
                }
                else
                {
                    PomocniSistem.ShowHelp("IzmenaSoftvera", "#", this);
                }
            }
            catch
            {
                PomocniSistem.ShowHelp("IzmenaSoftvera", "#", this);
            }
        }
    }
}
