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
    /// Interaction logic for DetaljiPredmetaDialog.xaml
    /// </summary>
    public partial class DetaljiPredmetaDialog : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public Predmet Predmet { get; set; }
        }

        public DetaljiPredmetaDialog(Predmet predmet)
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Predmet = predmet;

            this.DataContext = vm;
        }
    }
}
