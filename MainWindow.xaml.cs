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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zadatak2.Klase;
using Zadatak2.Interfejs.Dialozi;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace Zadatak2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel vm;

        Point start = new Point();

        private const string FROM_SIDEBAR = "PredmetDraggedFromSidebar";
        private const string FROM_MAP = "PredmetDraggedFromMap";
        

        public class ViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<Predmet> Predmeti { get; set; }
            public ObservableCollection<Ucionica> Ucionice { get; set; }
            public Termin ClickedTermin { get; set; }
            public Predmet ClickedPredmet { get; set; }
            public ObservableCollection<Predmet> DroppedPredmeti { get; set; }
            public List<RasporedTermina> RasporediTermina { get; set; }
            public RasporedTermina RasporedTermina { get; set; }
            public ObservableCollection<Termin> Termini { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
            public DateTime DatumRasporeda
            {
                get
                {
                    return datumRasporeda;
                }

                set
                {
                    if (value != datumRasporeda)
                    {
                        datumRasporeda = value;
                        OnPropertyChanged("DatumRasporeda");
                    }
                }
            }
            protected virtual void OnPropertyChanged(string name)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
                Console.WriteLine("Changed" + name);
            }

            private DateTime datumRasporeda;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataDAO.getDataDAO().Smerovi = DataDAO.getDataDAO().readSmerFromFile<Smer>();
            DataDAO.getDataDAO().Softveri = DataDAO.getDataDAO().readSoftverFromFile<Softver>();
            DataDAO.getDataDAO().Predmeti = DataDAO.getDataDAO().readPredmetFromFile<Predmet>();
            DataDAO.getDataDAO().Ucionice = DataDAO.getDataDAO().readUcionicaFromFile<Ucionica>();
            DataDAO.getDataDAO().RasporediTermina = DataDAO.getDataDAO().readRasporedTerminaFromFile<RasporedTermina>();
            DataDAO.getDataDAO().Termini = DataDAO.getDataDAO().readTerminFromFile<Termin>();
            

            vm = new ViewModel();
            vm.Predmeti = new ObservableCollection<Predmet>();
            vm.Ucionice = new ObservableCollection<Ucionica>();
            vm.DroppedPredmeti = new ObservableCollection<Predmet>();
            vm.RasporedTermina = new RasporedTermina();
            vm.Termini = new ObservableCollection<Termin>();
            vm.RasporediTermina = new List<RasporedTermina>();
            vm.DatumRasporeda = DateTime.Now.Date;

            foreach (RasporedTermina r in DataDAO.getDataDAO().RasporediTermina)
            {
                vm.RasporediTermina.Add(r);
            }

            if (vm.DatumRasporeda.Date < DateTime.Now.Date && vm.DatumRasporeda.DayOfWeek == DayOfWeek.Sunday)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Visible;
                lblErrorDatum.Visibility = Visibility.Visible;

                SatnicaMap.IsEnabled = false;
                cmbListaUcionica.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else if (vm.DatumRasporeda.Date < DateTime.Now.Date)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Visible;
                lblErrorDatum.Visibility = Visibility.Hidden;

                cmbListaUcionica.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else if (vm.DatumRasporeda.DayOfWeek == DayOfWeek.Sunday)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Hidden;
                lblErrorDatum.Visibility = Visibility.Visible;

                cmbListaUcionica.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Hidden;
                lblErrorDatum.Visibility = Visibility.Hidden;

                cmbListaUcionica.IsEnabled = true;
                SatnicaMap.IsEnabled = true;
                PredmetiItems.IsEnabled = true;
            }

            foreach (Termin t in DataDAO.getDataDAO().Termini)
            {
                vm.Termini.Add(t);
            }

            foreach (Ucionica ucionica in DataDAO.getDataDAO().Ucionice)
            {
                vm.Ucionice.Add(ucionica);
            }

            foreach (Predmet predmet in DataDAO.getDataDAO().Predmeti)
            {
                vm.Predmeti.Add(predmet);
            }

            if (vm.Ucionice.Count > 0)
            {
                cmbListaUcionica.ItemsSource = vm.Ucionice;
                cmbListaUcionica.SelectedIndex = 0;
                cmbListaUcionica.SelectedValue = vm.Ucionice[0].Id;
            }

            if (cmbListaUcionica.SelectedValue == null)
            {
                PredmetiItems.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                lblErrorUcionica.Visibility = Visibility.Visible;
            }
            else
            {
                PredmetiItems.IsEnabled = true;
                SatnicaMap.IsEnabled = true;
                lblErrorUcionica.Visibility = Visibility.Hidden;

                lblOsUcioniceText.Visibility = Visibility.Visible;
                lblOsUcionice.Visibility = Visibility.Visible;
                Ucionica u = pronadjiUcionicu(cmbListaUcionica.SelectedValue.ToString());
                lblOsUcionice.Content = u.OperativniSistemU.ToString();

                if (u.InstaliranSoftver.Count > 0)
                {
                    lblSoftverUcioniceText.Visibility = Visibility.Visible;
                    lblSoftverUcionice.Visibility = Visibility.Visible;
                    lblSoftverUcionice.Content = string.Join("\n", u.InstaliranSoftver);
                }
            }

            RasporedTermina rt = null;

            try
            {
                rt = proveriPostojiLiRaspored(vm.DatumRasporeda.ToString() + cmbListaUcionica.SelectedValue.ToString());
            }
            catch (NullReferenceException) { }

            if (rt != null)
            {
                vm.RasporedTermina = rt;

                foreach (Termin termin in vm.RasporedTermina.Termini)
                {
                    Canvas canvas = SatnicaMap;

                    int krajTermin = 63 * termin.PredmetTermina.MinimalnaDuzinaTermina + termin.Y;
                    if (krajTermin > 715)
                    {
                        int VisinaTerminaKojiOstaje = 715 - termin.Y;
                        int VisinaTerminaKojiSePrebacuje = krajTermin - 715;

                        //PredmetIcon.ToolTip = GetTooltip(manifestation);

                        Label lbl1 = new Label();
                        lbl1.Name = termin.Id;
                        lbl1.Uid = termin.Id;
                        lbl1.Content = termin.PredmetTermina.Naziv;
                        lbl1.FontSize = 18;
                        lbl1.Background = Brushes.Aqua;
                        lbl1.Width = 385;
                        lbl1.Height = VisinaTerminaKojiOstaje;
                        //PredmetIcon.ToolTip = GetTooltip(manifestation);

                        canvas.Children.Add(lbl1);

                        Canvas.SetLeft(lbl1, termin.X);
                        Canvas.SetTop(lbl1, termin.Y);

                        Label lbl2 = new Label();
                        lbl2.Name = termin.Id;
                        lbl2.Uid = termin.Id;
                        lbl2.FontSize = 18;
                        lbl2.Content = termin.PredmetTermina.Naziv;
                        lbl2.Background = Brushes.Aqua;
                        lbl2.Width = 385;
                        lbl2.Height = VisinaTerminaKojiSePrebacuje;
                        //PredmetIcon.ToolTip = GetTooltip(manifestation);

                        canvas.Children.Add(lbl2);

                        Canvas.SetLeft(lbl2, 595);
                        Canvas.SetTop(lbl2, 11);
                    }
                    else
                    {
                        Label lbl = new Label();
                        lbl.Name = termin.Id;
                        lbl.Uid = termin.Id;
                        lbl.FontSize = 18;
                        lbl.Content = termin.PredmetTermina.Naziv;
                        lbl.Background = Brushes.Aqua;
                        lbl.Width = 385;
                        lbl.Height = 63 * termin.PredmetTermina.MinimalnaDuzinaTermina;
                        //PredmetIcon.ToolTip = GetTooltip(manifestation);

                        canvas.Children.Add(lbl);

                        Canvas.SetLeft(lbl, termin.X);
                        Canvas.SetTop(lbl, termin.Y);
                    }
                }
            }
            else
            {
                try
                {
                    vm.RasporedTermina = new RasporedTermina();
                    vm.RasporedTermina.Id = vm.DatumRasporeda.ToString() + cmbListaUcionica.SelectedValue.ToString();
                    vm.RasporedTermina.UcionicaRasporeda = pronadjiUcionicu(cmbListaUcionica.SelectedValue.ToString());
                    vm.RasporedTermina.DatumRasporeda = vm.DatumRasporeda;
                    DataDAO.getDataDAO().RasporediTermina.Add(vm.RasporedTermina);
                    DataDAO.getDataDAO().writeRasporedTerminaToFile<RasporedTermina>(DataDAO.getDataDAO().RasporediTermina);
                }
                catch (NullReferenceException) { }
            }

            this.DataContext = vm;
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        private RasporedTermina proveriPostojiLiRaspored(string id)
        {
            foreach (RasporedTermina rt in DataDAO.getDataDAO().RasporediTermina)
            {
                if (rt.Id.Equals(id))
                {
                    return rt;
                }
            }
            return null;
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                Demo_Mod_Clicked(null, null);
            }
            else if (e.Key == Key.F1)
            {
                IInputElement focusedControl = FocusManager.GetFocusedElement(this);
                PomocniSistem.ShowHelp("Index", "#", this);
            }
        }

        private void Demo_Mod_Clicked(object sender, RoutedEventArgs e)
        {
            DodajPredmetDialog dpd = new DodajPredmetDialog();
            dpd.Show();
            dpd.PrikaziDemoMod();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e){
            DataDAO.getDataDAO().writeRasporedTerminaToFile<RasporedTermina>(DataDAO.getDataDAO().RasporediTermina);
        }

        private Termin nadjiTerminZaOvajRaspored(string id)
        {
            foreach (Termin termin in vm.RasporedTermina.Termini)
            {
                if (id.Equals(termin.Id))
                {
                    return termin;
                }
            }
            return null;
        }

        // klik levog tastera misa na mapi
        private void SatnicaMap_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(null);
            
            Canvas map = sender as Canvas;
            Termin dataObject = null;

            string idTermina = null;
            if (e.Source is Label)
            {
                idTermina = ((Label)e.Source).Name;
            }
            else { return; }

            dataObject = nadjiTerminZaOvajRaspored(idTermina);
            // Initialize the drag & drop operation
            if (dataObject != null)
            {
                DataObject data = new DataObject(FROM_MAP, dataObject);
                DragDrop.DoDragDrop(map, data, DragDropEffects.Move);
            }
        }

        private void SatnicaMap_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void SatnicaMap_DragEnter(object sender, DragEventArgs e)
        {
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        } 

        private void SatnicaMap_Drop(object sender, DragEventArgs e)
        {
            Point dropPosition = e.GetPosition(SatnicaMap);

            if ((int)dropPosition.Y > 715)
            {
                return;
            }

            if ((int)dropPosition.Y < 11)
            {
                dropPosition.Y = 11;
            }

            if ((int)dropPosition.X < 92)
            {
                return;
            }
            if ((int)dropPosition.X >= 92 && (int)dropPosition.X <= 480)
            {
                dropPosition.X = 94;
            }
            else if ((int)dropPosition.X >= 595)
            {
                dropPosition.X = 595;
            }
            else if ((int)dropPosition.X > 480 && (int)dropPosition.X < 595)
            {
                dropPosition.X = 595;
            }
            if ((int)dropPosition.X >= 595)
            {
                if (dropPosition.Y > 674)
                {
                    return;
                }
            }

            if ((int)dropPosition.Y > 715)
            {
                return;
            }

            //Proveri da li postoji predmet na toj lokaciji
            if (e.Source is Label)
            {
                return;
            }

            if (e.Data.GetDataPresent(FROM_SIDEBAR))
            {
                try
                {
                    Predmet predmet = e.Data.GetData(FROM_SIDEBAR) as Predmet;


                    int weekOfYear = GetIso8601WeekOfYear(vm.DatumRasporeda);

                    //Proveravamo da li je predmet nadmasio broj broj termina
                    int brojac = 0;
                    foreach (RasporedTermina rt in DataDAO.getDataDAO().RasporediTermina)
                    {
                        int currentWeek = GetIso8601WeekOfYear(rt.DatumRasporeda);
                        if (currentWeek == weekOfYear)
                        {
                            foreach (Termin t in rt.Termini)
                            {
                                if (t.PredmetTermina.Id.Equals(predmet.Id))
                                {
                                    brojac++;
                                }
                            }
                        }
                    }

                    int ukupnoTerminaPredmeta = 0;

                    ukupnoTerminaPredmeta = brojac * predmet.MinimalnaDuzinaTermina;

                    if (ukupnoTerminaPredmeta >= predmet.BrojTerminaPredmeta)
                    {
                        MessageBox.Show("Broj termina za izabran predmet za ovu nedelju je nadmasen.");
                        return;
                    }

                    if (cmbListaUcionica.SelectedValue.ToString() != null)
                    {
                        Ucionica izabranaUcionica = pronadjiUcionicu(cmbListaUcionica.SelectedValue.ToString());
                        foreach (Softver neophodanSoftver in predmet.NeophodanSoftver)
                        {
                            Softver s = pronadjiNeophodanSoftverUUcionici(neophodanSoftver.Id, izabranaUcionica);
                            if (s == null)
                            {
                                MessageBox.Show("Nazalost izabrana ucionica nema instaliran potreban softver za vas predmet.");
                                return;
                            }
                        }
                        bool valid = proveriOpremuUcionice(izabranaUcionica, predmet);
                        if (!valid)
                        {
                            MessageBox.Show("Nazalost izabrana ucionica nema svu potrebnu opremu za izabran predmet.");
                            return;
                        }
                    }

                    int krajPredmeta = 63 * predmet.MinimalnaDuzinaTermina + (int)dropPosition.Y;

                    if (krajPredmeta > 674 && (int)dropPosition.X >= 595)
                    {
                        return;
                    }

                    if (krajPredmeta > 715)
                    {
                        Termin termin = new Termin();
                        termin.PredmetTermina = predmet;

                        int VisinaPredmetaKojiOstaje = 715 - (int)dropPosition.Y;
                        int VisinaPredmetaKojiSePrebacuje = krajPredmeta - 715;
                        // Da li postoji preklapanje
                        if (proveriPreklapanje(termin, krajPredmeta, (int)dropPosition.X, (int)dropPosition.Y))
                        {
                            MessageBox.Show("Ucionica je za izabran termin zauzeta.");
                            return;
                        }

                        if (proveriPreklapanjePriPrebacivanju(termin, 11 + VisinaPredmetaKojiSePrebacuje, 595, 11))
                        {
                            MessageBox.Show("Ucionica je za izabran termin zauzeta.");
                            return;
                        }
                        termin.X = (int)dropPosition.X;
                        termin.Y = (int)dropPosition.Y;

                        DataDAO.getDataDAO().TerminPrebacenNaMapu(termin);
                        vm.RasporedTermina.Termini.Add(termin);

                        Canvas canvas = this.SatnicaMap;

                        //PredmetIcon.ToolTip = GetTooltip(manifestation);

                        Label lbl1 = new Label();
                        lbl1.Name = termin.Id;
                        lbl1.Uid = termin.Id;
                        lbl1.FontSize = 18;
                        lbl1.Content = termin.PredmetTermina.Naziv;
                        lbl1.Background = Brushes.Aqua;
                        lbl1.Width = 385;
                        lbl1.Height = VisinaPredmetaKojiOstaje;
                        //PredmetIcon.ToolTip = GetTooltip(manifestation);

                        canvas.Children.Add(lbl1);

                        Canvas.SetLeft(lbl1, termin.X);
                        Canvas.SetTop(lbl1, termin.Y);

                        Label lbl2 = new Label();
                        lbl2.Name = termin.Id + termin.Id;
                        lbl2.Uid = termin.Id;
                        lbl2.FontSize = 18;
                        lbl2.Content = termin.PredmetTermina.Naziv;
                        lbl2.Background = Brushes.Aqua;
                        lbl2.Width = 385;
                        lbl2.Height = VisinaPredmetaKojiSePrebacuje;
                        //PredmetIcon.ToolTip = GetTooltip(manifestation);

                        canvas.Children.Add(lbl2);

                        Canvas.SetLeft(lbl2, 595);
                        Canvas.SetTop(lbl2, 11);
                    }
                    else
                    {
                        Termin termin = new Termin();
                        termin.PredmetTermina = predmet;
                        // Da li postoji preklapanje
                        if (proveriPreklapanje(termin, krajPredmeta, (int)dropPosition.X, (int)dropPosition.Y))
                        {
                            MessageBox.Show("Ucionica je za izabran termin zauzeta.");
                            return;
                        }

                        //vm.Predmeti.Remove(predmet);
                        termin.X = (int)dropPosition.X;
                        termin.Y = (int)dropPosition.Y;

                        DataDAO.getDataDAO().TerminPrebacenNaMapu(termin);

                        vm.RasporedTermina.Termini.Add(termin);

                        Canvas canvas = this.SatnicaMap;

                        Label lbl = new Label();
                        lbl.Name = termin.Id;
                        lbl.Uid = termin.Id;
                        lbl.FontSize = 18;
                        lbl.Content = predmet.Naziv;
                        lbl.Background = Brushes.Aqua;
                        lbl.Width = 385;
                        lbl.Height = 63 * predmet.MinimalnaDuzinaTermina;
                        //PredmetIcon.ToolTip = GetTooltip(manifestation);

                        canvas.Children.Add(lbl);

                        Canvas.SetLeft(lbl, termin.X);
                        Canvas.SetTop(lbl, termin.Y);
                    }
                }
                catch (NullReferenceException) { }
                return;
            }

            if (e.Data.GetDataPresent(FROM_MAP))
            {
                Termin termin = e.Data.GetData(FROM_MAP) as Termin;

                int krajPredmeta = 63 * termin.PredmetTermina.MinimalnaDuzinaTermina + (int)dropPosition.Y;
                if (krajPredmeta > 674 && (int)dropPosition.X >= 595)
                {
                    return;
                }
                if (krajPredmeta > 715)
                {
                    int VisinaPredmetaKojiOstaje = 715 - (int)dropPosition.Y;
                    int VisinaPredmetaKojiSePrebacuje = krajPredmeta - 715;
                    // Da li postoji preklapanje
                    if (proveriPreklapanje(termin, krajPredmeta, (int)dropPosition.X, (int)dropPosition.Y))
                    {
                        MessageBox.Show("Ucionica je za izabran termin zauzeta.");
                        return;
                    }

                    if (proveriPreklapanjePriPrebacivanju(termin, 11 + VisinaPredmetaKojiSePrebacuje, 595, 11))
                    {
                        MessageBox.Show("Ucionica je za izabran termin zauzeta.");
                        return;
                    }

                    vm.RasporedTermina.Termini.Remove(termin);
                    termin.X = (int)dropPosition.X;
                    termin.Y = (int)dropPosition.Y;

                    DataDAO.getDataDAO().TerminPrebacenNaMapu(termin);
                    vm.RasporedTermina.Termini.Add(termin);
                    Canvas canvas = this.SatnicaMap;

                    List<UIElement> remove = new List<UIElement>();
                    foreach (UIElement elem in canvas.Children)
                    {
                        if (elem.Uid == termin.Id)
                        {
                            remove.Add(elem);
                        }
                    }
                    foreach (UIElement element in remove)
                    {
                        canvas.Children.Remove(element);
                    }

                    //PredmetIcon.ToolTip = GetTooltip(manifestation);

                    Label lbl1 = new Label();
                    lbl1.Name = termin.Id;
                    lbl1.Content = termin.PredmetTermina.Naziv;
                    lbl1.FontSize = 18;
                    lbl1.Uid = termin.Id;
                    lbl1.Background = Brushes.Aqua;
                    lbl1.Width = 385;
                    lbl1.Height = VisinaPredmetaKojiOstaje;
                    //PredmetIcon.ToolTip = GetTooltip(manifestation);

                    canvas.Children.Add(lbl1);

                    Canvas.SetLeft(lbl1, termin.X);
                    Canvas.SetTop(lbl1, termin.Y);

                    Label lbl2 = new Label();
                    lbl2.Name = termin.Id + termin.Id;
                    lbl2.Uid = termin.Id;
                    lbl2.FontSize = 18;
                    lbl2.Content = termin.PredmetTermina.Naziv;
                    lbl2.Background = Brushes.Aqua;
                    lbl2.Width = 385;
                    lbl2.Height = VisinaPredmetaKojiSePrebacuje+11;
                    //PredmetIcon.ToolTip = GetTooltip(manifestation);

                    canvas.Children.Add(lbl2);

                    Canvas.SetLeft(lbl2, 595);
                    Canvas.SetTop(lbl2, 11);
                }
                else
                {
                    // Da li postoji preklapanje
                    if (proveriPreklapanje(termin, krajPredmeta, (int)dropPosition.X, (int)dropPosition.Y))
                    {
                        MessageBox.Show("Ucionica je za izabran termin zauzeta.");
                        return;
                    }

                    vm.RasporedTermina.Termini.Remove(termin);
                    termin.X = (int)dropPosition.X;
                    termin.Y = (int)dropPosition.Y;

                    DataDAO.getDataDAO().TerminPrebacenNaMapu(termin);
                    vm.RasporedTermina.Termini.Add(termin);

                    Canvas canvas = this.SatnicaMap;

                    List<UIElement> remove = new List<UIElement>();
                    foreach (UIElement elem in canvas.Children)
                    {
                        if (elem.Uid == termin.Id)
                        {
                            remove.Add(elem);
                        }
                    }
                    foreach (UIElement element in remove)
                    {
                        canvas.Children.Remove(element);
                    }

                    //PredmetIcon.ToolTip = GetTooltip(manifestation);

                    Label lbl = new Label();
                    lbl.Name = termin.Id;
                    lbl.Uid = termin.Id;
                    lbl.FontSize = 18;
                    lbl.Content = termin.PredmetTermina.Naziv;
                    lbl.Background = Brushes.Aqua;
                    lbl.Width = 385;
                    lbl.Height = 63 * termin.PredmetTermina.MinimalnaDuzinaTermina;
                    //PredmetIcon.ToolTip = GetTooltip(manifestation);

                    canvas.Children.Add(lbl);

                    Canvas.SetLeft(lbl, termin.X);
                    Canvas.SetTop(lbl, termin.Y);

                }
                return;
            }
        }

        private bool proveriOpremuUcionice(Ucionica ucionica, Predmet predmet)
        {
            if (!(ucionica.OperativniSistemU.ToString().Equals("OBOJE")))
            {
                if (!(ucionica.OperativniSistemU.ToString().Equals(predmet.OperativniSistemP.ToString())))
                {
                    return false;
                }
            }
            else if ((predmet.OperativniSistemP.ToString().Equals("OBOJE")))
            {
                if (!(predmet.OperativniSistemP.ToString().Equals(ucionica.OperativniSistemU.ToString())))
                {
                    return false;
                }
            }

            if (predmet.VelicinaGrupe > ucionica.BrRadnihMesta)
            {
                return false;
            }
            if (predmet.PotrebanProjektor)
            {
                if (!(ucionica.ImaProjektor))
                {
                    return false;
                }
            }
            if (predmet.PotrebnaTabla)
            {
                if (!(ucionica.ImaTablu))
                {
                    return false;
                }
            }
            if (predmet.PotrebnaPametnaTabla)
            {
                if (!(ucionica.ImaPametnuTablu))
                {
                    return false;
                }
            }
            return true;
        }

        private Softver pronadjiNeophodanSoftverUUcionici(string idSoftveraPredmeta, Ucionica ucionica)
        {
            foreach (Softver instaliranSoftver in ucionica.InstaliranSoftver)
            {
                if (instaliranSoftver.Id.Equals(idSoftveraPredmeta))
                {
                    return instaliranSoftver;
                }
            }
            return null;
        }


        private bool proveriPreklapanjePriPrebacivanju(Termin p, int krajPredmeta, int X, int Y)
        {
            foreach (Termin termin in vm.RasporedTermina.Termini)
            {
                if (Y <= termin.Y)
                {
                    if (termin.X >= 595)
                    {
                        if (krajPredmeta > termin.Y)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool proveriPreklapanje(Termin p, int krajPredmeta, int X, int Y)
        {
            foreach (Termin termin in vm.RasporedTermina.Termini)
            {
                if (!termin.Id.Equals(p.Id))
                {
                    if (Y < termin.Y)
                    {
                        if (termin.X > 90 && termin.X < 595)
                        {
                            if (p.X > 90 && p.X < 595)
                            {
                                if (X > 90 && X < 595)
                                {
                                    if (krajPredmeta > termin.Y)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                        if (termin.X >= 595)
                        {
                            if (p.X >= 595)
                            {
                                if (X >= 595)
                                {
                                    if (krajPredmeta > termin.Y)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                        if (termin.X > 90 && termin.X < 595)
                        {
                            if (p.X >= 595)
                            {
                                if (X > 90 && X < 595)
                                {
                                    if (krajPredmeta > termin.Y)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                        if (termin.X >= 595)
                        {
                            if (p.X > 90 && p.X < 595)
                            {
                                if (X >= 595)
                                {
                                    if (krajPredmeta > termin.Y)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }

                        // Ako je predmet ubacen sa sidebara
                        if (termin.X > 90 && termin.X < 595)
                        {
                            if (X > 90 && X < 595)
                            {
                                if (krajPredmeta > termin.Y)
                                {
                                    return true;
                                }
                            }
                        }
                        if (termin.X >= 595)
                        {
                            if (X >= 595)
                            {
                                if (krajPredmeta > termin.Y)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void SatnicaMap_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Label)
            {
                vm.ClickedPredmet = nadjiPredmetZaKliknutTermin(((Label)e.Source).Name);
                vm.ClickedTermin = nadjiIzabranTermin(((Label)e.Source).Name);
            }
            else
            {
                return;
            }
            ContextMenu cm = PredmetContextMenu;

            if (vm.ClickedPredmet != null)
            {
                cm.PlacementTarget = sender as Canvas;
                cm.IsOpen = true;
            }
        }

        private Predmet nadjiPredmetZaKliknutTermin(string id)
        {
            foreach (Termin t in vm.RasporedTermina.Termini)
            {
                if (t.Id.Equals(id))
                {
                    return t.PredmetTermina;
                }
            }
            return null;
        }

        // Drag and drog iz liste predmta, sidebar
        private void PredmetiItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(null);

            StackPanel predmetPanel = sender as StackPanel;
            Predmet dataObject = null;

            foreach (Predmet predmet in vm.Predmeti)
            {
                if ((string)predmetPanel.Tag == predmet.Id)
                {
                    dataObject = predmet;
                    break;
                }
            }

            // Inicijalizujemo drag and drop operaciju
            DataObject data = new DataObject(FROM_SIDEBAR, dataObject);
            DragDrop.DoDragDrop(predmetPanel, data, DragDropEffects.Move);
        }

        private void PredmetiItem_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private Termin nadjiIzabranTermin(string id)
        {
            foreach (Termin t in vm.RasporedTermina.Termini)
            {
                if (t.Id.Equals(id))
                {
                    return t;
                }
            }
            return null;
        }

        // Click on Remove in ContextMenu
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (vm.ClickedTermin == null)
            {
                return;
            }
            vm.RasporedTermina.Termini.Remove(vm.ClickedTermin);

            vm.ClickedTermin.X = -1;
            vm.ClickedTermin.Y = -1;

            //vm.Predmeti.Add(vm.ClickedPredmet);

            DataDAO.getDataDAO().TerminPrebacenNaMapu(vm.ClickedTermin);

            Canvas canvas = SatnicaMap;
            UIElement remove = null;
            foreach (UIElement elem in canvas.Children)
            {
                if (elem.Uid == vm.ClickedTermin.Id)
                {
                    remove = elem;
                    break;
                }
            }
            canvas.Children.Remove(remove);
        }

        private void GetNotification(String s)
        {
            if (s == "CHANGED")
            {
                if (vm.DatumRasporeda.Date < DateTime.Now.Date && vm.DatumRasporeda.DayOfWeek == DayOfWeek.Sunday)
                {
                    lblErrorDatumPreDanasnjeg.Visibility = Visibility.Visible;
                    lblErrorDatum.Visibility = Visibility.Visible;

                    SatnicaMap.IsEnabled = false;
                    cmbListaUcionica.IsEnabled = false;
                    PredmetiItems.IsEnabled = false;
                }
                else if (vm.DatumRasporeda.Date < DateTime.Now.Date)
                {
                    lblErrorDatumPreDanasnjeg.Visibility = Visibility.Visible;
                    lblErrorDatum.Visibility = Visibility.Hidden;

                    cmbListaUcionica.IsEnabled = false;
                    SatnicaMap.IsEnabled = false;
                    PredmetiItems.IsEnabled = false;
                }
                else if (vm.DatumRasporeda.DayOfWeek == DayOfWeek.Sunday)
                {
                    lblErrorDatumPreDanasnjeg.Visibility = Visibility.Hidden;
                    lblErrorDatum.Visibility = Visibility.Visible;

                    cmbListaUcionica.IsEnabled = false;
                    SatnicaMap.IsEnabled = false;
                    PredmetiItems.IsEnabled = false;
                }
                else
                {
                    lblErrorDatumPreDanasnjeg.Visibility = Visibility.Hidden;
                    lblErrorDatum.Visibility = Visibility.Hidden;

                    cmbListaUcionica.IsEnabled = true;
                    SatnicaMap.IsEnabled = true;
                    PredmetiItems.IsEnabled = true;
                }

                if (cmbListaUcionica.SelectedValue == null)
                {
                    PredmetiItems.IsEnabled = false;
                    SatnicaMap.IsEnabled = false;
                    lblErrorUcionica.Visibility = Visibility.Visible;
                }
                else
                {
                    PredmetiItems.IsEnabled = true;
                    SatnicaMap.IsEnabled = true;
                    lblErrorUcionica.Visibility = Visibility.Hidden;

                    lblOsUcioniceText.Visibility = Visibility.Visible;
                    lblOsUcionice.Visibility = Visibility.Visible;
                    Ucionica u = pronadjiUcionicu(cmbListaUcionica.SelectedValue.ToString());
                    lblOsUcionice.Content = u.OperativniSistemU.ToString();

                    if (u.InstaliranSoftver.Count > 0)
                    {
                        lblSoftverUcioniceText.Visibility = Visibility.Visible;
                        lblSoftverUcionice.Visibility = Visibility.Visible;
                        lblSoftverUcionice.Content = string.Join("\n", u.InstaliranSoftver);
                    }
                }

                vm.Predmeti.Clear();

                Canvas canvas = SatnicaMap;

                canvas.Children.Clear();

                RasporedTermina rt = null;
                try
                {
                    rt = proveriPostojiLiRaspored(vm.DatumRasporeda.ToString() + cmbListaUcionica.SelectedValue.ToString());
                }
                catch (NullReferenceException) { }

                List<Termin> terminiZaBrisanje = new List<Termin>();

                if (rt != null)
                {
                    vm.RasporedTermina = rt;

                    foreach (Termin termin in vm.RasporedTermina.Termini)
                    {
                        Predmet p = proveriPredmetObrisanIzGlavneListe(termin.PredmetTermina);
                        if (p != null)
                        {
                            int krajPredmeta = 63 * termin.PredmetTermina.MinimalnaDuzinaTermina + termin.Y;
                            if (krajPredmeta > 715)
                            {
                                int VisinaPredmetaKojiOstaje = 715 - termin.Y;
                                int VisinaPredmetaKojiSePrebacuje = krajPredmeta - 715;

                                //PredmetIcon.ToolTip = GetTooltip(manifestation);

                                Label lbl1 = new Label();
                                lbl1.Name = termin.Id;
                                lbl1.Uid = termin.Id;
                                lbl1.FontSize = 18;
                                lbl1.Content = termin.PredmetTermina.Naziv;
                                lbl1.Background = Brushes.Aqua;
                                lbl1.Width = 385;
                                lbl1.Height = VisinaPredmetaKojiOstaje;
                                //PredmetIcon.ToolTip = GetTooltip(manifestation);

                                canvas.Children.Add(lbl1);

                                Canvas.SetLeft(lbl1, termin.X);
                                Canvas.SetTop(lbl1, termin.Y);

                                Label lbl2 = new Label();
                                lbl2.Name = termin.Id + termin.Id;
                                lbl2.Uid = termin.Id;
                                lbl2.FontSize = 18;
                                lbl2.Content = termin.PredmetTermina.Naziv;
                                lbl2.Background = Brushes.Aqua;
                                lbl2.Width = 385;
                                lbl2.Height = VisinaPredmetaKojiSePrebacuje;
                                //PredmetIcon.ToolTip = GetTooltip(manifestation);

                                canvas.Children.Add(lbl2);

                                Canvas.SetLeft(lbl2, 595);
                                Canvas.SetTop(lbl2, 11);
                            }
                            else
                            {
                                Label lbl = new Label();
                                lbl.Name = termin.Id;
                                lbl.Uid = termin.Id;
                                lbl.FontSize = 18;
                                lbl.Content = termin.PredmetTermina.Naziv;
                                lbl.Background = Brushes.Aqua;
                                lbl.Width = 385;
                                lbl.Height = 63 * termin.PredmetTermina.MinimalnaDuzinaTermina;
                                //PredmetIcon.ToolTip = GetTooltip(manifestation);

                                canvas.Children.Add(lbl);

                                Canvas.SetLeft(lbl, termin.X);
                                Canvas.SetTop(lbl, termin.Y);
                            }
                        }
                        else
                        {
                            terminiZaBrisanje.Add(termin);
                        }
                    }
                }
                else
                {
                    try
                    {
                        vm.RasporedTermina = new RasporedTermina();
                        vm.RasporedTermina.Id = vm.DatumRasporeda.ToString() + cmbListaUcionica.SelectedValue.ToString();
                        vm.RasporedTermina.UcionicaRasporeda = pronadjiUcionicu(cmbListaUcionica.SelectedValue.ToString());
                        vm.RasporedTermina.DatumRasporeda = vm.DatumRasporeda;
                        DataDAO.getDataDAO().RasporediTermina.Add(vm.RasporedTermina);
                        DataDAO.getDataDAO().writeRasporedTerminaToFile<RasporedTermina>(DataDAO.getDataDAO().RasporediTermina);
                    }
                    catch (NullReferenceException) { }
                }

                foreach (Predmet predmet in DataDAO.getDataDAO().Predmeti)
                {
                    vm.Predmeti.Add(predmet);
                }

                foreach (Termin termin in terminiZaBrisanje)
                {
                    vm.RasporedTermina.Termini.Remove(termin);
                }
            }
        }
        private Predmet proveriPredmetObrisanIzGlavneListe(Predmet predmetZaBrisanje)
        {
            foreach (Predmet predmet in DataDAO.getDataDAO().Predmeti)
            {
                if (predmet.Id.Equals(predmetZaBrisanje.Id))
                {
                    return predmet;
                }
            }
            return null;
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            if (cmbListaUcionica.SelectedValue == null)
            {
                PredmetiItems.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                lblErrorUcionica.Visibility = Visibility.Visible;
                lblOsUcioniceText.Visibility = Visibility.Hidden;
                lblOsUcionice.Visibility = Visibility.Hidden;
                lblSoftverUcioniceText.Visibility = Visibility.Hidden;
                lblSoftverUcionice.Visibility = Visibility.Hidden;
            }
            else
            {
                PredmetiItems.IsEnabled = true;
                SatnicaMap.IsEnabled = true;
                lblErrorUcionica.Visibility = Visibility.Hidden;

                lblOsUcioniceText.Visibility = Visibility.Visible;
                lblOsUcionice.Visibility = Visibility.Visible;
                Ucionica u = pronadjiUcionicu(cmbListaUcionica.SelectedValue.ToString());
                lblOsUcionice.Content = u.OperativniSistemU.ToString();

                if (u.InstaliranSoftver.Count > 0)
                {
                    lblSoftverUcioniceText.Visibility = Visibility.Visible;
                    lblSoftverUcionice.Visibility = Visibility.Visible;
                    lblSoftverUcionice.Content = string.Join("\n", u.InstaliranSoftver);
                }
            }

            if (vm.DatumRasporeda.Date < DateTime.Now.Date && vm.DatumRasporeda.DayOfWeek == DayOfWeek.Sunday)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Visible;
                lblErrorDatum.Visibility = Visibility.Visible;

                SatnicaMap.IsEnabled = false;
                cmbListaUcionica.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else if (vm.DatumRasporeda.Date < DateTime.Now.Date)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Visible;
                lblErrorDatum.Visibility = Visibility.Hidden;

                cmbListaUcionica.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else if (vm.DatumRasporeda.DayOfWeek == DayOfWeek.Sunday)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Hidden;
                lblErrorDatum.Visibility = Visibility.Visible;

                cmbListaUcionica.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Hidden;
                lblErrorDatum.Visibility = Visibility.Hidden;

                cmbListaUcionica.IsEnabled = true;
                SatnicaMap.IsEnabled = true;
                PredmetiItems.IsEnabled = true;
            }
            DataDAO.OnPredmetiChange += new DataDAO.SendMessageToMainWindow(GetNotification);
        }

        

        private void cmbListaUcionica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm.DatumRasporeda.Date < DateTime.Now.Date && vm.DatumRasporeda.DayOfWeek == DayOfWeek.Sunday)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Visible;
                lblErrorDatum.Visibility = Visibility.Visible;

                SatnicaMap.IsEnabled = false;
                cmbListaUcionica.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else if (vm.DatumRasporeda.Date < DateTime.Now.Date)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Visible;
                lblErrorDatum.Visibility = Visibility.Hidden;

                cmbListaUcionica.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else if (vm.DatumRasporeda.DayOfWeek == DayOfWeek.Sunday)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Hidden;
                lblErrorDatum.Visibility = Visibility.Visible;

                cmbListaUcionica.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Hidden;
                lblErrorDatum.Visibility = Visibility.Hidden;

                cmbListaUcionica.IsEnabled = true;
                SatnicaMap.IsEnabled = true;
                PredmetiItems.IsEnabled = true;
            }

            if (cmbListaUcionica.SelectedValue == null)
            {
                PredmetiItems.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                lblErrorUcionica.Visibility = Visibility.Visible;
                lblOsUcioniceText.Visibility = Visibility.Hidden;
                lblOsUcionice.Visibility = Visibility.Hidden;
                lblSoftverUcioniceText.Visibility = Visibility.Hidden;
                lblSoftverUcionice.Visibility = Visibility.Hidden;
            }
            else
            {
                PredmetiItems.IsEnabled = true;
                SatnicaMap.IsEnabled = true;
                lblErrorUcionica.Visibility = Visibility.Hidden;

                lblOsUcioniceText.Visibility = Visibility.Visible;
                lblOsUcionice.Visibility = Visibility.Visible;
                Ucionica u = pronadjiUcionicu(cmbListaUcionica.SelectedValue.ToString());
                lblOsUcionice.Content = u.OperativniSistemU.ToString();

                if (u.InstaliranSoftver.Count > 0)
                {
                    lblSoftverUcioniceText.Visibility = Visibility.Visible;
                    lblSoftverUcionice.Visibility = Visibility.Visible;
                    lblSoftverUcionice.Content = string.Join("\n", u.InstaliranSoftver);
                }
            }

            vm.Predmeti.Clear();
            vm.DroppedPredmeti.Clear();

            Canvas canvas = SatnicaMap;

            RasporedTermina rt = null;

            canvas.Children.Clear();

            try
            {
                rt = proveriPostojiLiRaspored(vm.DatumRasporeda.ToString() + cmbListaUcionica.SelectedValue.ToString());
            }
            catch (NullReferenceException) { }

            List<Termin> terminiZaBrisanje = new List<Termin>();

            if (rt != null)
            {
                vm.RasporedTermina = rt;

                foreach (Termin termin in vm.RasporedTermina.Termini)
                {
                    Predmet p = proveriPredmetObrisanIzGlavneListe(termin.PredmetTermina);
                    if (p != null)
                    {
                        int krajPredmeta = 63 * termin.PredmetTermina.MinimalnaDuzinaTermina + termin.Y;
                        if (krajPredmeta > 715)
                        {
                            int VisinaPredmetaKojiOstaje = 715 - termin.Y;
                            int VisinaPredmetaKojiSePrebacuje = krajPredmeta - 715;

                            //PredmetIcon.ToolTip = GetTooltip(manifestation);

                            Label lbl1 = new Label();
                            lbl1.Name = termin.Id;
                            lbl1.Uid = termin.Id;
                            lbl1.FontSize = 18;
                            lbl1.Content = termin.PredmetTermina.Naziv;
                            lbl1.Background = Brushes.Aqua;
                            lbl1.Width = 385;
                            lbl1.Height = VisinaPredmetaKojiOstaje;
                            //PredmetIcon.ToolTip = GetTooltip(manifestation);

                            canvas.Children.Add(lbl1);

                            Canvas.SetLeft(lbl1, termin.X);
                            Canvas.SetTop(lbl1, termin.Y);

                            Label lbl2 = new Label();
                            lbl2.Name = termin.Id + termin.Id;
                            lbl2.Uid = termin.Id;
                            lbl2.FontSize = 18;
                            lbl2.Content = termin.PredmetTermina.Naziv;
                            lbl2.Background = Brushes.Aqua;
                            lbl2.Width = 385;
                            lbl2.Height = VisinaPredmetaKojiSePrebacuje;
                            //PredmetIcon.ToolTip = GetTooltip(manifestation);

                            canvas.Children.Add(lbl2);

                            Canvas.SetLeft(lbl2, 595);
                            Canvas.SetTop(lbl2, 11);
                        }
                        else
                        {
                            Label lbl = new Label();
                            lbl.Name = termin.Id;
                            lbl.Uid = termin.Id;
                            lbl.FontSize = 18;
                            lbl.Content = termin.PredmetTermina.Naziv;
                            lbl.Background = Brushes.Aqua;
                            lbl.Width = 385;
                            lbl.Height = 63 * termin.PredmetTermina.MinimalnaDuzinaTermina;
                            //PredmetIcon.ToolTip = GetTooltip(manifestation);

                            canvas.Children.Add(lbl);

                            Canvas.SetLeft(lbl, termin.X);
                            Canvas.SetTop(lbl, termin.Y);
                        }
                    }
                    else
                    {
                        terminiZaBrisanje.Add(termin);
                    }
                }
            }
            else
            {
                try
                {
                    vm.RasporedTermina = new RasporedTermina();
                    vm.RasporedTermina.Id = vm.DatumRasporeda.ToString() + cmbListaUcionica.SelectedValue.ToString();
                    vm.RasporedTermina.UcionicaRasporeda = pronadjiUcionicu(cmbListaUcionica.SelectedValue.ToString());
                    vm.RasporedTermina.DatumRasporeda = vm.DatumRasporeda;
                    DataDAO.getDataDAO().RasporediTermina.Add(vm.RasporedTermina);
                    DataDAO.getDataDAO().writeRasporedTerminaToFile<RasporedTermina>(DataDAO.getDataDAO().RasporediTermina);
                }
                catch (NullReferenceException) { }
            }

            foreach (Predmet predmet in DataDAO.getDataDAO().Predmeti)
            {
                vm.Predmeti.Add(predmet);
            }

            foreach (Termin termin in terminiZaBrisanje)
            {
                vm.RasporedTermina.Termini.Remove(termin);
            }
        }

        private void KreiranjeTerminaDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm.DatumRasporeda.Date < DateTime.Now.Date && vm.DatumRasporeda.DayOfWeek == DayOfWeek.Sunday)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Visible;
                lblErrorDatum.Visibility = Visibility.Visible;

                SatnicaMap.IsEnabled = false;
                cmbListaUcionica.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else if (vm.DatumRasporeda.Date < DateTime.Now.Date)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Visible;
                lblErrorDatum.Visibility = Visibility.Hidden;

                cmbListaUcionica.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else if (vm.DatumRasporeda.DayOfWeek == DayOfWeek.Sunday)
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Hidden;
                lblErrorDatum.Visibility = Visibility.Visible;

                cmbListaUcionica.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                PredmetiItems.IsEnabled = false;
            }
            else
            {
                lblErrorDatumPreDanasnjeg.Visibility = Visibility.Hidden;
                lblErrorDatum.Visibility = Visibility.Hidden;

                cmbListaUcionica.IsEnabled = true;
                SatnicaMap.IsEnabled = true;
                PredmetiItems.IsEnabled = true;
            }


            if (cmbListaUcionica.SelectedValue == null)
            {
                PredmetiItems.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                lblErrorUcionica.Visibility = Visibility.Visible;
                lblOsUcioniceText.Visibility = Visibility.Hidden;
                lblOsUcionice.Visibility = Visibility.Hidden;
                lblSoftverUcioniceText.Visibility = Visibility.Hidden;
                lblSoftverUcionice.Visibility = Visibility.Hidden;

            }
            else
            {
                PredmetiItems.IsEnabled = true;
                SatnicaMap.IsEnabled = true;
                lblErrorUcionica.Visibility = Visibility.Hidden;
            }

            vm.Predmeti.Clear();
            vm.DroppedPredmeti.Clear();

            Canvas canvas = SatnicaMap;

            canvas.Children.Clear();

            RasporedTermina rt = null;

            try
            {
                rt = proveriPostojiLiRaspored(vm.DatumRasporeda.ToString() + cmbListaUcionica.SelectedValue.ToString());
            }
            catch (NullReferenceException) { }


            List<Termin> terminiZaBrisanje = new List<Termin>();

            if (rt != null)
            {
                vm.RasporedTermina = rt;

                foreach (Termin termin in vm.RasporedTermina.Termini)
                {
                    Predmet p = proveriPredmetObrisanIzGlavneListe(termin.PredmetTermina);
                    if (p != null)
                    {
                        int krajPredmeta = 63 * termin.PredmetTermina.MinimalnaDuzinaTermina + termin.Y;
                        if (krajPredmeta > 715)
                        {
                            int VisinaPredmetaKojiOstaje = 715 - termin.Y;
                            int VisinaPredmetaKojiSePrebacuje = krajPredmeta - 715;

                            //PredmetIcon.ToolTip = GetTooltip(manifestation);

                            Label lbl1 = new Label();
                            lbl1.Name = termin.Id;
                            lbl1.Uid = termin.Id;
                            lbl1.FontSize = 18;
                            lbl1.Content = termin.PredmetTermina.Naziv;
                            lbl1.Background = Brushes.Aqua;
                            lbl1.Width = 385;
                            lbl1.Height = VisinaPredmetaKojiOstaje;
                            //PredmetIcon.ToolTip = GetTooltip(manifestation);

                            canvas.Children.Add(lbl1);

                            Canvas.SetLeft(lbl1, termin.X);
                            Canvas.SetTop(lbl1, termin.Y);

                            Label lbl2 = new Label();
                            lbl2.Name = termin.Id + termin.Id;
                            lbl2.Uid = termin.Id;
                            lbl2.FontSize = 18;
                            lbl2.Content = termin.PredmetTermina.Naziv;
                            lbl2.Background = Brushes.Aqua;
                            lbl2.Width = 385;
                            lbl2.Height = VisinaPredmetaKojiSePrebacuje;
                            //PredmetIcon.ToolTip = GetTooltip(manifestation);

                            canvas.Children.Add(lbl2);

                            Canvas.SetLeft(lbl2, 595);
                            Canvas.SetTop(lbl2, 11);
                        }
                        else
                        {
                            Label lbl = new Label();
                            lbl.Name = termin.Id;
                            lbl.Uid = termin.Id;
                            lbl.FontSize = 18;
                            lbl.Content = termin.PredmetTermina.Naziv;
                            lbl.Background = Brushes.Aqua;
                            lbl.Width = 385;
                            lbl.Height = 63 * termin.PredmetTermina.MinimalnaDuzinaTermina;
                            //PredmetIcon.ToolTip = GetTooltip(manifestation);

                            canvas.Children.Add(lbl);

                            Canvas.SetLeft(lbl, termin.X);
                            Canvas.SetTop(lbl, termin.Y);
                        }
                    }
                    else
                    {
                        terminiZaBrisanje.Add(termin);
                    }
                }
            }
            else
            {
                try
                {
                    vm.RasporedTermina = new RasporedTermina();
                    vm.RasporedTermina.Id = vm.DatumRasporeda.ToString() + cmbListaUcionica.SelectedValue.ToString();
                    vm.RasporedTermina.UcionicaRasporeda = pronadjiUcionicu(cmbListaUcionica.SelectedValue.ToString());
                    vm.RasporedTermina.DatumRasporeda = vm.DatumRasporeda;
                    DataDAO.getDataDAO().RasporediTermina.Add(vm.RasporedTermina);
                    DataDAO.getDataDAO().writeRasporedTerminaToFile<RasporedTermina>(DataDAO.getDataDAO().RasporediTermina);
                }
                catch (NullReferenceException) { }
            }

            foreach (Predmet predmet in DataDAO.getDataDAO().Predmeti)
            {
                vm.Predmeti.Add(predmet);
            }

            foreach (Termin termin in terminiZaBrisanje)
            {
                vm.RasporedTermina.Termini.Remove(termin);
            }

            UcioniceUlisti.IsChecked = false;
        }

        private Ucionica pronadjiUcionicu(string id)
        {
            foreach (Ucionica ucionica in DataDAO.getDataDAO().Ucionice)
            {
                if (ucionica.Id.Equals(id))
                {
                    return ucionica;
                }
            }
            return null;
        }

        private void UcioniceUlisti_Checked(object sender, RoutedEventArgs e)
        {
            prikaziSlobodneUcionice();
            if (cmbListaUcionica.SelectedValue == null)
            {
                PredmetiItems.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                lblErrorUcionica.Visibility = Visibility.Visible;
            }
            else
            {
                PredmetiItems.IsEnabled = true;
                SatnicaMap.IsEnabled = true;
                lblErrorUcionica.Visibility = Visibility.Hidden;
            }
        }

        private void UcioniceUlisti_Unchecked(object sender, RoutedEventArgs e)
        {
            vm.Ucionice.Clear();
            foreach (Ucionica ucionica in DataDAO.getDataDAO().Ucionice)
            {
                vm.Ucionice.Add(ucionica);
            }
            if (cmbListaUcionica.SelectedValue == null)
            {
                PredmetiItems.IsEnabled = false;
                SatnicaMap.IsEnabled = false;
                lblErrorUcionica.Visibility = Visibility.Visible;
            }
            else
            {
                PredmetiItems.IsEnabled = true;
                SatnicaMap.IsEnabled = true;
                lblErrorUcionica.Visibility = Visibility.Hidden;
            }
        }

        private void prikaziSlobodneUcionice()
        {
            vm.Ucionice.Clear();

            List<Ucionica> ucioniceSaTerminima = new List<Ucionica>();

            foreach (Ucionica ucionica in DataDAO.getDataDAO().Ucionice)
            {
                RasporedTermina rt = pronadjiTerminZaUcionicu(ucionica.Id);
                if (rt != null)
                {
                    ucioniceSaTerminima.Add(ucionica);
                }
                else
                {
                    vm.Ucionice.Add(ucionica);
                }
            }

            // Za ucionice koje imaju raspored proveri da li ima slobodnih mesta
            foreach (Ucionica ucionica in ucioniceSaTerminima)
            {
                int ukupnaVisinaPredmetaNaCanvasu = 0;
                RasporedTermina rt = pronadjiTerminZaUcionicu(ucionica.Id);
                if (rt != null)
                {
                    foreach (Termin termin in rt.Termini)
                    {
                        ukupnaVisinaPredmetaNaCanvasu += (63 * termin.PredmetTermina.MinimalnaDuzinaTermina);
                    }
                    if (ukupnaVisinaPredmetaNaCanvasu < 1380)
                    {
                        vm.Ucionice.Add(ucionica);
                    }
                }
            }
        }

        private RasporedTermina pronadjiTerminZaUcionicu(string idUcionice)
        {
            foreach (RasporedTermina rt in DataDAO.getDataDAO().RasporediTermina)
            {
                if (rt.UcionicaRasporeda.ToString().Equals(idUcionice.ToString()) && DateTime.Compare(vm.DatumRasporeda, rt.DatumRasporeda) == 0)
                {
                    return rt;
                }
            }
            return null;
        }

        private void DodajUcionicu_Click(object sender, RoutedEventArgs e)
        {
            DodajUcionicuDialog duw = new DodajUcionicuDialog();
            duw.Check += popup_Check;
            duw.Show();
        }

        private void PrikaziSveUcionice_Click(object sender, RoutedEventArgs e)
        {
            PrikaziSveUcioniceDialog duw = new PrikaziSveUcioniceDialog();
            duw.Check += osveziListu;
            duw.Show();
        }

        void osveziListu(List<Ucionica> ucionice)
        {
            vm.Ucionice.Clear();
            foreach (Ucionica u in ucionice)
            {
                vm.Ucionice.Add(u);
            }
            cmbListaUcionica.ItemsSource = vm.Ucionice;
        }

        void popup_Check(Ucionica obj)
        {
            vm.Ucionice.Add(obj);
            cmbListaUcionica.ItemsSource = vm.Ucionice;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            PomocniSistem.ShowHelp("Index", "#", this);
        }
    }
}
