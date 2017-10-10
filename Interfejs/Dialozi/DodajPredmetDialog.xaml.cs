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
using System.Threading;

namespace Zadatak2.Interfejs.Dialozi
{
    /// <summary>
    /// Interaction logic for DodajPredmetDialog.xaml
    /// </summary>
    public partial class DodajPredmetDialog : Window
    {

        private ViewModel vm;

        private Thread thread;
        private int DemoPokrenut;

        private static int SLEEP_TIME = 150;

        // Sve za demo mod
        private List<String> fields =
            new List<String>()
            {
                "OznakaPredmeta_+01",
                "Naziv Predmeta maksimalno 80 karaktera",
                "Ovde unosimo opis predmeta koji moze sadrzati maksimalno 300 karaktera.",
                "+-2++35++fd++20",
                "+-4++6+fd++2",
                "+-2++15++ds++2"
            };

        private List<TextBox> boxes = new List<TextBox>();

        public class ViewModel
        {
            public Predmet Predmet { get; set; }
            public List<Smer> Smerovi { get; set; }
            public List<Softver> Softveri { get; set; }
            public List<CheckBox> SviSmerovi { get; set; }
            public List<CheckBox> SviSoftveri { get; set; }
            public bool ProzorJeDemoMod { get; set; }
        }

        public DodajPredmetDialog()
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Predmet = new Predmet();
            vm.Smerovi = DataDAO.getDataDAO().Smerovi;
            vm.Softveri = DataDAO.getDataDAO().Softveri;
            vm.SviSmerovi = new List<CheckBox>();
            vm.SviSoftveri = new List<CheckBox>();
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
                cb.FontSize = 24;
                cb.Margin = new System.Windows.Thickness(5, 5, 5, 5);
                cb.Content = smer.Naziv;
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
                cb.FontSize = 24;
                cb.Margin = new System.Windows.Thickness(5, 5, 5, 5);
                cb.Content = softver.Naziv;
                vm.SviSoftveri.Add(cb);

                sp.Children.Add(cb);
                Grid.SetColumn(sp, brojacSo % 6);
                Grid.SetRow(sp, brojacSo / 6);
                ListaSoftvera.Children.Add(sp);
                ++brojacSo;
            }

            DemoPokrenut = 1;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (!vm.ProzorJeDemoMod)
            {
                MessageBoxResult messageBox = System.Windows.MessageBox.Show("Odustajanjem gubite sve unesene podatke za ovaj predmet. Da li ste sigurni?", "Odustani od unosa", System.Windows.MessageBoxButton.YesNo);
                if (messageBox == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                MessageBoxResult messageBox = System.Windows.MessageBox.Show("Da li ste sigurni da zelite izaci iz demo moda?", "Napusti demo mod", System.Windows.MessageBoxButton.YesNo);
                if (messageBox == MessageBoxResult.Yes)
                {
                    this.Close();
                }
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
                        PomocniSistem.ShowHelp("Predmet", "#", this);
                        return;
                    }

                    PomocniSistem.ShowHelp(str, sec, this);
                }
                else
                {
                    PomocniSistem.ShowHelp("Predmet", "#", this);
                }
            }
            catch
            {
                PomocniSistem.ShowHelp("Predmet", "#", this);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!vm.ProzorJeDemoMod)
            {
                for (int i = 0; i < vm.SviSmerovi.Count; ++i)
                {
                    if (vm.SviSmerovi[i].IsChecked == true)
                    {
                        vm.Predmet.Smerovi.Add(vm.Smerovi[i]);
                    }
                }

                for (int i = 0; i < vm.SviSoftveri.Count; ++i)
                {
                    if (vm.SviSoftveri[i].IsChecked == true)
                    {
                        vm.Predmet.NeophodanSoftver.Add(vm.Softveri[i]);
                    }
                }

                DataDAO.getDataDAO().Predmeti.Add(vm.Predmet);
                DataDAO.getDataDAO().writePredmetToFile<Predmet>(DataDAO.getDataDAO().Predmeti);
                MessageBox.Show("Predmet je uspesno kreiran.");
                this.Close();
            }
            else
            {
                MessageBoxResult messageBox = System.Windows.MessageBox.Show("Nalazite se u demo modu, uneseni podaci nece biti sacuvani. Da li ste sugurni da zelite izaci iz demo moda?", "Napusti demo mod", System.Windows.MessageBoxButton.YesNo);
                if (messageBox == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }

        public void PrikaziDemoMod()
        {
            vm.ProzorJeDemoMod = true;

            boxes.Add(IdPredmeta);
            boxes.Add(NazivPredmeta);
            boxes.Add(OpisPredmeta);
            boxes.Add(VelicinaGrupePredrmeta);
            boxes.Add(MinimalnaDuzinaPredmeta);
            boxes.Add(BrojTerminaPredmeta);

            List<RadioButton> radiobuttons = new List<RadioButton>();
            List<CheckBox> checkboxes = new List<CheckBox>();

            checkboxes.Add(ProjektorStatus);
            checkboxes.Add(TablaStatus);

            radiobuttons.Add(rdbWindows);
            radiobuttons.Add(rdbLinux);

            thread = new Thread(() => TypingThread(boxes, fields, radiobuttons, checkboxes, vm.SviSoftveri, vm.SviSmerovi));
            thread.Start();
        }

        public delegate void UpdateTextCallback(TextBox textbox, string message);
        public delegate void RadioButtonChecker(RadioButton rb);
        public delegate void CheckBoxChecker(CheckBox cb);


        private void TypingThread(List<TextBox> boxes, List<String> messages,
            List<RadioButton> radiobuttons, List<CheckBox> checkboxes, List<CheckBox> softveri, List<CheckBox> smerovi)
        {
            while (true)
            {
                for (int i = 0; i < boxes.Count; ++i)
                {
                    String message = messages[i];
                    TextBox textBox = boxes[i];

                    foreach (char s in message)
                    {
                        Thread.Sleep(SLEEP_TIME);

                        if (s == '+')
                        {
                            Thread.Sleep(8 * SLEEP_TIME);
                            textBox.Dispatcher.Invoke(
                                new UpdateTextCallback(this.RemoveLastChar),
                                new object[] { textBox, "" }
                                );
                        }

                        if (s != '+')
                        {
                            textBox.Dispatcher.Invoke(
                                new UpdateTextCallback(this.UpdateText),
                                new object[] { textBox, s.ToString() }
                                );
                        }
                    }
                }

                foreach (RadioButton rb in radiobuttons)
                {
                    Thread.Sleep(9 * SLEEP_TIME);
                    rb.Dispatcher.Invoke(
                        new RadioButtonChecker(this.CheckRadioButton),
                        new object[] { rb }
                        );
                }

                foreach (CheckBox cb in checkboxes)
                {
                    Thread.Sleep(9 * SLEEP_TIME);
                    cb.Dispatcher.Invoke(
                        new CheckBoxChecker(this.CheckCheckBox),
                        new object[] { cb }
                        );
                }

                for (int i = 0; i < smerovi.Count; ++i)
                {
                    if (i % 3 == 1)
                    {
                        Thread.Sleep(9 * SLEEP_TIME);
                        smerovi[i].Dispatcher.Invoke(
                            new CheckBoxChecker(this.CheckCheckBox),
                            new object[] { smerovi[i] }
                            );
                    }
                }
                Thread.Sleep(18 * SLEEP_TIME);

                for (int i = 0; i < softveri.Count; ++i)
                {
                    if (i % 3 == 1)
                    {
                        Thread.Sleep(9 * SLEEP_TIME);
                        softveri[i].Dispatcher.Invoke(
                            new CheckBoxChecker(this.CheckCheckBox),
                            new object[] { softveri[i] }
                            );
                    }
                }
                Thread.Sleep(18 * SLEEP_TIME);

                foreach (TextBox t in boxes)
                {
                    t.Dispatcher.Invoke(
                        new UpdateTextCallback(this.ClearTextBox),
                               new object[] { t, "" }
                               );
                }

                for (int i = 0; i < smerovi.Count; ++i)
                {
                    if (i % 3 == 1)
                    {
                        Thread.Sleep(9 * SLEEP_TIME);
                        smerovi[i].Dispatcher.Invoke(
                            new CheckBoxChecker(this.CheckCheckBox),
                            new object[] { smerovi[i] }
                            );
                    }
                }

                for (int i = 0; i < softveri.Count; ++i)
                {
                    if (i % 3 == 1)
                    {
                        Thread.Sleep(9 * SLEEP_TIME);
                        softveri[i].Dispatcher.Invoke(
                            new CheckBoxChecker(this.CheckCheckBox),
                            new object[] { softveri[i] }
                            );
                    }
                }

                checkboxes[0].Dispatcher.Invoke(
                            new CheckBoxChecker(this.CheckCheckBox),
                            new object[] { checkboxes[0] }
                            );

                checkboxes[1].Dispatcher.Invoke(
                            new CheckBoxChecker(this.CheckCheckBox),
                            new object[] { checkboxes[1] }
                            );
            }
        }

        private void UpdateText(TextBox textbox, string message)
        {
            textbox.AppendText(message);
        }

        private void ClearTextBox(TextBox textbox, string message)
        {
            textbox.Text = "";
        }

        private void RemoveLastChar(TextBox textbox, string message)
        {
            if(textbox.Text.Length > 0)
                textbox.Text = textbox.Text.Substring(0, textbox.Text.Count() - 1);
        }

        private void CheckRadioButton(RadioButton btn)
        {
            btn.IsChecked = true;
        }

        private void CheckCheckBox(CheckBox cb)
        {
            cb.IsChecked = !cb.IsChecked;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5 && vm.ProzorJeDemoMod)
            {
                if (DemoPokrenut == 1)
                {
                    thread.Suspend();
                }
                else
                {
                    thread.Resume();

                }

                DemoPokrenut = (DemoPokrenut + 1) % 2;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (vm.ProzorJeDemoMod) thread.Abort();
        }
    }
}
