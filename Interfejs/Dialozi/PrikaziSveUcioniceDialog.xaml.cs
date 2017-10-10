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
using System.Collections.ObjectModel;

namespace Zadatak2.Interfejs.Dialozi
{
    /// <summary>
    /// Interaction logic for PrikaziSveUcioniceDialog.xaml
    /// </summary>
    public partial class PrikaziSveUcioniceDialog : Window
    {

        public ObservableCollection<Ucionica> Ucionice { get; set; }
        public List<Ucionica> SveUcionice { get; set; }
        public Ucionica IzabranaUcionica { get; set; }
        public Boolean ButtonEnabled { get; set; }

        public List<Softver> Softveri { get; set; }
        public List<CheckBox> SviSoftveri { get; set; }

        public int BrojRadnihMestaMin { get; set; }
        public int BrojRadnihMestaMax { get; set; }
        public string UcionicaId { get; set; }
        public string UcionicaOpis { get; set; }

        public bool Windows { get; set; }
        public bool Linux { get; set; }
        public bool Oba { get; set; }

        public bool Projektor { get; set; }
        public bool Tabla { get; set; }
        public bool PametnaTabla { get; set; }

        public PrikaziSveUcioniceDialog()
        {
            InitializeComponent();
            IzabranaUcionica = null;

            this.DataContext = this;

            Ucionice = new ObservableCollection<Ucionica>();
            SveUcionice = new List<Ucionica>();
            Softveri = DataDAO.getDataDAO().Softveri;
            SviSoftveri = new List<CheckBox>();

            foreach (Ucionica u in DataDAO.getDataDAO().Ucionice)
            {
                Ucionice.Add(u);
                SveUcionice.Add(u);
            }

            int brojacSo = 0;
            foreach (Softver softver in Softveri)
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
                SviSoftveri.Add(cb);

                sp.Children.Add(cb);
                Grid.SetColumn(sp, brojacSo % 6);
                Grid.SetRow(sp, brojacSo / 6);
                ListaInstaliranogSoftvera.Children.Add(sp);
                ++brojacSo;
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
                        PomocniSistem.ShowHelp("PretragaUcionica", "#", this);
                        return;
                    }

                    PomocniSistem.ShowHelp(str, sec, this);
                }
                else
                {
                    PomocniSistem.ShowHelp("PretragaUcionica", "#", this);
                }
            }
            catch
            {
                PomocniSistem.ShowHelp("PretragaUcionica", "#", this);
            }
        }

        private void ShowMore_Click(object sender, RoutedEventArgs e)
        {
            AdvancedSearch.Visibility = (AdvancedSearch.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
            if (AdvancedSearch.Visibility == Visibility.Visible)
            {
                ShowMore.Content = "Sakrij opcije";
            }
            else
            {
                ShowMore.Content = "Vise opcija";
            }
        }

        private void TabelaUcionica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnabled = IzabranaUcionica != null;

            IzmeniUcionicu.IsEnabled = ButtonEnabled;
            ObrisiUcionicu.IsEnabled = ButtonEnabled;
            PogledajUcionicu.IsEnabled = ButtonEnabled;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Ucionice.Clear();

            foreach (Ucionica p in SveUcionice)
            {
                if (PronadjiUcionicu(p))
                {
                    Ucionice.Add(p);
                }
            }
        }

        private bool pronadjiUcionicuPoSoftveru(Ucionica p)
        {
            bool sviPostoje = false;
            for (int i = 0; i < SviSoftveri.Count; ++i)
            {
                if (SviSoftveri[i].IsChecked == true)
                {
                    bool pronadjen = false;
                    foreach (Softver s in p.InstaliranSoftver)
                    {
                        if (s.Naziv.Equals(SviSoftveri[i].Content))
                        {
                            pronadjen = true;
                        }
                    }
                    if (pronadjen)
                    {
                        sviPostoje = true;
                    }
                    else
                    {
                        sviPostoje = false;
                        return sviPostoje;
                    }
                }
            }
            if (sviPostoje)
            {
                return true;
            }
            return false;
        }

        private bool PronadjiUcionicu(Ucionica p)
        {
            if (UcionicaId != null && UcionicaId != "" && !p.Id.ToLower().Contains(UcionicaId.ToLower()))
                return false;

            if (UcionicaOpis != null & UcionicaOpis != "" && !p.Opis.ToLower().Contains(UcionicaOpis.ToLower()))
                return false;

            if (BrojRadnihMestaMin > 0 && BrojRadnihMestaMin > p.BrRadnihMesta)
                return false;

            if (BrojRadnihMestaMax > 0 && BrojRadnihMestaMax < p.BrRadnihMesta)
                return false;


            if (!((Windows && p.OperativniSistemU == OperativniSistemU.WINDOWS)
                || (Linux && p.OperativniSistemU == OperativniSistemU.LINUX)
                || (Oba && p.OperativniSistemU == OperativniSistemU.OBOJE))
                && (Windows || Linux || Oba))
                return false;

            if (Projektor && !p.ImaProjektor)
                return false;

            if (Tabla && !p.ImaTablu)
                return false;

            if (PametnaTabla && !p.ImaPametnuTablu)
                return false;

            if (proveriCekiraneBoxove())
            {
                // Trazimo predmet po softveru koje stikliran
                bool pronadjiPredmet = pronadjiUcionicuPoSoftveru(p);

                if (pronadjiPredmet == false)
                    return false;
            }
            return true;
        }

        private bool proveriCekiraneBoxove()
        {
            for (int i = 0; i < SviSoftveri.Count; i++)
            {
                if (SviSoftveri[i].IsChecked == true)
                {
                    return true;
                }
            }
            return false;
        }

        private void ObrisiUcionicu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show(
                "Da li ste sigurni da zelite obrisati smer ("
                + IzabranaUcionica.Id + " " + ")?",
                "Potvrda brisanja", System.Windows.MessageBoxButton.YesNo);

            if (messageBox == MessageBoxResult.Yes)
            {
                Ucionice.Remove(IzabranaUcionica);

                List<Ucionica> ucionice = new List<Ucionica>();
                foreach (Ucionica p in Ucionice)
                {
                    ucionice.Add(p);
                }
                DataDAO.getDataDAO().Ucionice = ucionice;
                DataDAO.getDataDAO().writeUcionicaToFile<Ucionica>(ucionice);
            }
        }

        private void IzmeniUcionicu_Click(object sender, RoutedEventArgs e)
        {
            var IzmeniUcionicaDialog = new Zadatak2.Interfejs.Dialozi.IzmeniUcionicuDialog(IzabranaUcionica);
            IzmeniUcionicaDialog.Show();
        }

        private void PogledajUcionicu_Click(object sender, RoutedEventArgs e)
        {
            var DetaljiDialog = new Zadatak2.Interfejs.Dialozi.DetaljiUcioniceDialog(IzabranaUcionica);
            DetaljiDialog.Show();
        }

        public event Action<List<Ucionica>> Check;

        private void Window_Closed(object sender, EventArgs e)
        {
            List<Ucionica> ucionice = new List<Ucionica>();
            foreach (Ucionica p in Ucionice)
            {
                ucionice.Add(p);
            }

            if (Check != null)
                Check(ucionice);
        }
    }
}
