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
    /// Interaction logic for DetaljiSmeraDialog.xaml
    /// </summary>
    public partial class DetaljiSmeraDialog : Window
    {

        private ViewModel vm;

        public class ViewModel
        {
            public Smer Smer { get; set; }
        }

        public DetaljiSmeraDialog(Smer smer)
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Smer = smer;

            this.DataContext = vm;

        }
    }
}
