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
using Xceed.Wpf.Toolkit;
using Zadatak2.Klase;
using System.Collections.ObjectModel;

namespace Zadatak2.Interfejs.Dialozi
{
    /// <summary>
    /// Interaction logic for PrikaziSveSoftvereDialog.xaml
    /// </summary>
    public partial class PrikaziSveSoftvereDialog : Window
    {

        public ObservableCollection<Softver> Softveri { get; set; }
        public List<Softver> SviSoftveri { get; set; }
        public Softver IzabranSoftver { get; set; }
        public Boolean ButtonEnabled { get; set; }

        public int GodinaIzdavanjaMin { get; set; }
        public int GodinaIzdavanjaMax { get; set; }
        public string SoftverId { get; set; }
        public string SoftverNaziv { get; set; }
        public double CenaSoftveraMin { get; set; }
        public double CenaSoftveraMax { get; set; }

        public bool Windows { get; set; }
        public bool Linux { get; set; }
        public bool Oba { get; set; }

        public PrikaziSveSoftvereDialog()
        {
            InitializeComponent();

            IzabranSoftver = null;

            this.DataContext = this;

            Softveri = new ObservableCollection<Softver>();
            SviSoftveri = new List<Softver>();

            foreach (Softver s in DataDAO.getDataDAO().Softveri)
            {
                Softveri.Add(s);
                SviSoftveri.Add(s);
            }

            GodinaIzdavanjaMin = 2000;
            GodinaIzdavanjaMax = 2000;
        }

        private void ButtonSpinnerGodMin_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
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
                        PomocniSistem.ShowHelp("PretragaSoftvera", "#", this);
                        return;
                    }

                    PomocniSistem.ShowHelp(str, sec, this);
                }
                else
                {
                    PomocniSistem.ShowHelp("PretragaSoftvera", "#", this);
                }
            }
            catch
            {
                PomocniSistem.ShowHelp("PretragaSoftvera", "#", this);
            }
        }

        private void ButtonSpinnerGodMax_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
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

        private void TabelaSoftvera_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnabled = IzabranSoftver != null;

            IzmeniSoftver.IsEnabled = ButtonEnabled;
            ObrisiSoftver.IsEnabled = ButtonEnabled;
            PogledajDetaljeSoftvera.IsEnabled = ButtonEnabled;
        }


        private bool PronadjiSoftver(Softver s)
        {
            if (SoftverId != null && SoftverId != "" && !s.Id.ToLower().Contains(SoftverId.ToLower()))
                return false;

            if (SoftverNaziv != null & SoftverNaziv != "" && !s.Naziv.ToLower().Contains(SoftverNaziv.ToLower()))
                return false;

            if (GodinaIzdavanjaMin > 0 && GodinaIzdavanjaMin > s.GodinaIzdavanja)
                return false;

            if (GodinaIzdavanjaMax > 0 && GodinaIzdavanjaMax < s.GodinaIzdavanja)
                return false;

            if (CenaSoftveraMin > 0 && CenaSoftveraMin > s.Cena)
                return false;

            if (CenaSoftveraMax > 0 && CenaSoftveraMax < s.Cena)
                return false;

            if (!((Windows && s.Os == OperativniSistemS.WINDOWS)
                || (Linux && s.Os == OperativniSistemS.LINUX)
                || (Oba && s.Os == OperativniSistemS.OBOJE))
                && (Windows || Linux || Oba))
                return false;

            return true;
        }

        private void ObrisiSoftver_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show(
                "Da li ste sigurni da zelite obrisati smer ("
                + IzabranSoftver.Id + " " + IzabranSoftver.Naziv + ")?",
                "Potvrda brisanja", System.Windows.MessageBoxButton.YesNo);

            if (messageBox == MessageBoxResult.Yes)
            {
                Softveri.Remove(IzabranSoftver);

                List<Softver> softveri = new List<Softver>();
                foreach (Softver s in Softveri)
                {
                    softveri.Add(s);
                }
                DataDAO.getDataDAO().Softveri = softveri;
                DataDAO.getDataDAO().writeSoftverToFile<Softver>(softveri);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Softveri.Clear();

            foreach (Softver s in SviSoftveri)
            {
                if (PronadjiSoftver(s))
                {
                    Softveri.Add(s);
                }
            }
        }

        private void IzmeniSoftver_Click(object sender, RoutedEventArgs e)
        {
            var IzmeniSoftvertDialog = new Zadatak2.Interfejs.Dialozi.IzmeniSoftverDialog(IzabranSoftver);
            IzmeniSoftvertDialog.Show();
        }

        private void PogledajDetaljeSoftvera_Click(object sender, RoutedEventArgs e)
        {
            var DetaljiDialog = new Zadatak2.Interfejs.Dialozi.DetaljiSoftveraDialog(IzabranSoftver);
            DetaljiDialog.Show();
        }
    }
}
