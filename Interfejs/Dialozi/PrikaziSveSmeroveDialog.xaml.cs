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
    /// Interaction logic for PrikaziSveSmeroveDialog.xaml
    /// </summary>
    public partial class PrikaziSveSmeroveDialog : Window
    {
        public ObservableCollection<Smer> Smerovi { get; set; }
        public List<Smer> SviSmerovi { get; set; }
        public Smer IzabranSmer { get; set; }
        public Boolean ButtonEnabled { get; set; }

        public DateTime SmerBeg { get; set; }
        public DateTime SmerEnd { get; set; }
        public string SmerId { get; set; }
        public string SmerNaziv { get; set; }

        public PrikaziSveSmeroveDialog()
        {
            InitializeComponent();

            IzabranSmer = null;

            this.DataContext = this;

            Smerovi = new ObservableCollection<Smer>();
            SviSmerovi = new List<Smer>();
            foreach (Smer s in DataDAO.getDataDAO().Smerovi)
            {
                Smerovi.Add(s);
                SviSmerovi.Add(s);
            }

            SmerBeg = DateTime.Now;
            SmerEnd = DateTime.Now;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Smerovi.Clear();

            foreach (Smer s in SviSmerovi)
            {
                if (PronadjiSmer(s))
                {
                    Smerovi.Add(s);
                }
            }
        }

        private void TabelaSmerova_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnabled = IzabranSmer != null;

            IzmeniSmer.IsEnabled = ButtonEnabled;
            ObrisiSmer.IsEnabled = ButtonEnabled;
            PogledaSmer.IsEnabled = ButtonEnabled;
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
                        PomocniSistem.ShowHelp("PretragaSmera", "#", this);
                        return;
                    }

                    PomocniSistem.ShowHelp(str, sec, this);
                }
                else
                {
                    PomocniSistem.ShowHelp("PretragaSmera", "#", this);
                }
            }
            catch
            {
                PomocniSistem.ShowHelp("PretragaSmera", "#", this);
            }
        }


        private bool PronadjiSmer(Smer s)
        {
            if (SmerId != null && SmerId != "" && !s.Id.ToLower().Contains(SmerId.ToLower()))
                return false;

            if (SmerNaziv != null & SmerNaziv != "" && !s.Naziv.ToLower().Contains(SmerNaziv.ToLower()))
                return false;

            if (SmerBeg.CompareTo(SmerEnd) < 0 && SmerBeg.CompareTo(s.DatumUvodjenjaSmera) > 0)
                return false;

            if (SmerBeg.CompareTo(SmerEnd) < 0 && SmerEnd.CompareTo(s.DatumUvodjenjaSmera) < 0)
                return false;

            return true;
        }

        private void ObrisiSmer_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBox = System.Windows.MessageBox.Show(
                "Da li ste sigurni da zelite obrisati smer ("
                + IzabranSmer.Id + " " + IzabranSmer.Naziv + ")?",
                "Potvrda brisanja", System.Windows.MessageBoxButton.YesNo);

            if (messageBox == MessageBoxResult.Yes)
            {
                Smerovi.Remove(IzabranSmer);

                List<Smer> smerovi = new List<Smer>();
                foreach (Smer s in Smerovi)
                {
                    smerovi.Add(s);
                }
                DataDAO.getDataDAO().Smerovi = smerovi;
                DataDAO.getDataDAO().writeSmerToFile<Smer>(smerovi);
            }
        }

        private void IzmeniSmer_Click(object sender, RoutedEventArgs e)
        {
            var IzmeniSmerDialog = new Zadatak2.Interfejs.Dialozi.IzmeniSmer(IzabranSmer);
            IzmeniSmerDialog.Show();
        }

        private void PogledaSmer_Click(object sender, RoutedEventArgs e)
        {
            var DetaljiDialog = new Zadatak2.Interfejs.Dialozi.DetaljiSmeraDialog(IzabranSmer);
            DetaljiDialog.Show();
        }
    }
}
