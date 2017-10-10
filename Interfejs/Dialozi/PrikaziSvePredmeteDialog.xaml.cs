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
    /// Interaction logic for PrikaziSvePredmeteDialog.xaml
    /// </summary>
    public partial class PrikaziSvePredmeteDialog : Window
    {

        public ObservableCollection<Predmet> Predmeti { get; set; }
        public List<Predmet> SviPredmeti { get; set; }
        public Predmet IzabranPredmet { get; set; }
        public Boolean ButtonEnabled { get; set; }

        public List<Softver> Softveri { get; set; }
        public List<CheckBox> SviSoftveri { get; set; }

        public List<Smer> Smerovi { get; set; }
        public List<CheckBox> SviSmerovi { get; set; }

        public int VelicinaGrupeMin { get; set; }
        public int VelicinaGrupeMax { get; set; }
        public string PredmetId { get; set; }
        public string PredmetNaziv { get; set; }
        public int MinimalnaDuzinaTerminaMin { get; set; }
        public int MinimalnaDuzinaTerminaMax { get; set; }
        public int BrojTerminaPredmetaMin { get; set; }
        public int BrojTerminaPredmetaMax { get; set; }

        public bool Windows { get; set; }
        public bool Linux { get; set; }
        public bool Oba { get; set; }

        public bool Projektor { get; set; }
        public bool Tabla { get; set; }
        public bool PametnaTabla { get; set; }

        public PrikaziSvePredmeteDialog()
        {
            InitializeComponent();

            IzabranPredmet = null;

            this.DataContext = this;

            Predmeti = new ObservableCollection<Predmet>();
            SviPredmeti = new List<Predmet>();
            Softveri = DataDAO.getDataDAO().Softveri;
            SviSoftveri = new List<CheckBox>();
            Smerovi = DataDAO.getDataDAO().Smerovi;
            SviSmerovi = new List<CheckBox>();

            foreach (Predmet p in DataDAO.getDataDAO().Predmeti)
            {
                Predmeti.Add(p);
                SviPredmeti.Add(p);
            }

            int brojacSo = 0;
            foreach (Softver softver in Softveri)
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
                cb.FontSize = 24;
                cb.Margin = new System.Windows.Thickness(5, 5, 5, 5);
                cb.Content = softver.Naziv;
                SviSoftveri.Add(cb);

                sp.Children.Add(cb);
                Grid.SetColumn(sp, brojacSo % 6);
                Grid.SetRow(sp, brojacSo / 6);
                ListaSoftvera.Children.Add(sp);
                ++brojacSo;
            }

            int brojacS = 0;
            foreach (Smer smer in Smerovi)
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
                cb.FontSize = 24;
                cb.Margin = new System.Windows.Thickness(5, 5, 5, 5);
                cb.Content = smer.Naziv;
                SviSmerovi.Add(cb);

                sp.Children.Add(cb);
                Grid.SetColumn(sp, brojacS % 6);
                Grid.SetRow(sp, brojacS / 6);
                ListaSmerova.Children.Add(sp);
                ++brojacS;
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

        private void TabelaPredmeta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnabled = IzabranPredmet != null;

            IzmeniPredmet.IsEnabled = ButtonEnabled;
            ObrisiPredmet.IsEnabled = ButtonEnabled;
            PogledajPredmet.IsEnabled = ButtonEnabled;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Predmeti.Clear();

            foreach (Predmet p in SviPredmeti)
            {
                if (PronadjiPredmet(p))
                {
                    Predmeti.Add(p);
                }
            }
        }

        private bool pronadjiPredmetPoSoftveruSmeru(Predmet p)
        {
            bool sviPostoje = false;
            bool postojiPoSoftveru = false;
            bool postojiPoSmeru = false;
            for (int i = 0; i < SviSoftveri.Count; ++i)
            {
                if (SviSoftveri[i].IsChecked == true)
                {
                    bool pronadjen = false;
                    foreach (Softver s in p.NeophodanSoftver)
                    {
                        if (s.Naziv.Equals(SviSoftveri[i].Content))
                        {
                            pronadjen = true;
                        }
                    }
                    if (pronadjen)
                    {
                        postojiPoSoftveru = true;
                    }
                    else
                    {
                        postojiPoSoftveru = false;
                        return postojiPoSoftveru;
                    }
                }
            }

            for (int i = 0; i < SviSmerovi.Count; ++i)
            {
                if (SviSmerovi[i].IsChecked == true)
                {
                    bool pronadjen = false;
                    foreach (Smer s in p.Smerovi)
                    {
                        if (s.Naziv.Equals(SviSmerovi[i].Content))
                        {
                            pronadjen = true;
                        }
                    }
                    if (pronadjen)
                    {
                        postojiPoSmeru = true;
                    }
                    else
                    {
                        postojiPoSmeru = false;
                        return postojiPoSmeru;
                    }
                }
            }
            if (postojiPoSmeru && postojiPoSoftveru)
            {
                sviPostoje = true;
            }
            else
            {
                sviPostoje = false;
            }

            if (sviPostoje)
            {
                return true;
            }
            return false;
        }

        private bool pronadjiPredmetPoSoftveru(Predmet p)
        {
            bool sviPostoje = false;
            for (int i = 0; i < SviSoftveri.Count; ++i)
            {
                if (SviSoftveri[i].IsChecked == true)
                {
                    bool pronadjen = false;
                    foreach (Softver s in p.NeophodanSoftver)
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

        private bool pronadjiPredmetPoSmeru(Predmet p)
        {
            bool sviPostoje = false;
            for (int i = 0; i < SviSmerovi.Count; ++i)
            {
                if (SviSmerovi[i].IsChecked == true)
                {
                    bool pronadjen = false;
                    foreach (Smer s in p.Smerovi)
                    {
                        if (s.Naziv.Equals(SviSmerovi[i].Content))
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

        private bool PronadjiPredmet(Predmet p)
        {
            if (PredmetId != null && PredmetId != "" && !p.Id.ToLower().Contains(PredmetId.ToLower()))
                return false;

            if (PredmetNaziv != null & PredmetNaziv != "" && !p.Naziv.ToLower().Contains(PredmetNaziv.ToLower()))
                return false;

            if (VelicinaGrupeMin > 0 && VelicinaGrupeMin > p.VelicinaGrupe)
                return false;

            if (VelicinaGrupeMax > 0 && VelicinaGrupeMax < p.VelicinaGrupe)
                return false;

            if (MinimalnaDuzinaTerminaMin > 0 && MinimalnaDuzinaTerminaMin > p.MinimalnaDuzinaTermina)
                return false;

            if (MinimalnaDuzinaTerminaMax > 0 && MinimalnaDuzinaTerminaMax < p.MinimalnaDuzinaTermina)
                return false;

            if (BrojTerminaPredmetaMin > 0 && BrojTerminaPredmetaMin > p.BrojTerminaPredmeta)
                return false;

            if (BrojTerminaPredmetaMax > 0 && BrojTerminaPredmetaMax < p.BrojTerminaPredmeta)
                return false;


            if (!((Windows && p.OperativniSistemP == OperativniSistemP.WINDOWS)
                || (Linux && p.OperativniSistemP == OperativniSistemP.LINUX)
                || (Oba && p.OperativniSistemP == OperativniSistemP.OBOJE))
                && (Windows || Linux || Oba))
                return false;

            if (Projektor && !p.PotrebanProjektor)
                return false;

            if (Tabla && !p.PotrebnaTabla)
                return false;

            if (PametnaTabla && !p.PotrebnaPametnaTabla)
                return false;

            if (proveriCekiraneBoxoveSoftvera() && proveriCekiraneBoxoveSmerova())
            {
                // Trazimo predmet po softveru i smeru koje stikliran
                bool pronadjiPredmet = pronadjiPredmetPoSoftveruSmeru(p);

                if (pronadjiPredmet == false)
                    return false;
            }
            else if(proveriCekiraneBoxoveSoftvera())
            {
                // Trazimo predmet po softveru koje stikliran
                bool pronadjiPredmet = pronadjiPredmetPoSoftveru(p);

                if (pronadjiPredmet == false)
                    return false;
            }
            else if (proveriCekiraneBoxoveSmerova())
            {
                // Trazimo predmet po smeru koje stikliran
                bool pronadjiPredmet = pronadjiPredmetPoSmeru(p);

                if (pronadjiPredmet == false)
                    return false;
            }
            return true;
        }

        private bool proveriCekiraneBoxoveSoftvera()
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

        private bool proveriCekiraneBoxoveSmerova()
        {
            for (int i = 0; i < SviSmerovi.Count; i++)
            {
                if (SviSmerovi[i].IsChecked == true)
                {
                    return true;
                }
            }
            return false;
        }

        private void ObrisiPredmet_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show(
                "Da li ste sigurni da zelite obrisati smer ("
                + IzabranPredmet.Id + " " + IzabranPredmet.Naziv + ")?",
                "Potvrda brisanja", System.Windows.MessageBoxButton.YesNo);

            if (messageBox == MessageBoxResult.Yes)
            {
                Predmeti.Remove(IzabranPredmet);

                List<Predmet> predmeti = new List<Predmet>();
                foreach (Predmet p in Predmeti)
                {
                    predmeti.Add(p);
                }
                DataDAO.getDataDAO().Predmeti = predmeti;
                DataDAO.getDataDAO().writePredmetToFile<Predmet>(predmeti);
            }
        }

        private void PogledajPredmet_Click(object sender, RoutedEventArgs e)
        {
            var DetaljiDialog = new Zadatak2.Interfejs.Dialozi.DetaljiPredmetaDialog(IzabranPredmet);
            DetaljiDialog.Show();
        }

        private void IzmeniPredmet_Click(object sender, RoutedEventArgs e)
        {
            var IzmeniPredmetDialog = new Zadatak2.Interfejs.Dialozi.IzmeniPredmetDialog(IzabranPredmet);
            IzmeniPredmetDialog.Show();
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
                        PomocniSistem.ShowHelp("PretragaPredmeta", "#", this);
                        return;
                    }

                    PomocniSistem.ShowHelp(str, sec, this);
                }
                else
                {
                    PomocniSistem.ShowHelp("PretragaPredmeta", "#", this);
                }
            }
            catch
            {
                PomocniSistem.ShowHelp("PretragaPredmeta", "#", this);
            }
        }
    }
}
