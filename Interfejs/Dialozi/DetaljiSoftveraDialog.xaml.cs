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
    /// Interaction logic for DetaljiSoftveraDialog.xaml
    /// </summary>
    public partial class DetaljiSoftveraDialog : Window
    {

        private ViewModel vm;

        public class ViewModel
        {
            public Softver Softver { get; set; }
        }

        public DetaljiSoftveraDialog(Softver softver)
        {
            InitializeComponent();

            vm = new ViewModel();

            vm.Softver = softver;

            this.DataContext = vm;

        }
    }
}
