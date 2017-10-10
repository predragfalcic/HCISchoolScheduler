using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Zadatak2.Klase;

namespace Zadatak2.Commands
{
    public static class Commands
    {
        public static readonly ICommand DodajUcionicu = new DodajUcionicuCommand();
        public static readonly ICommand PrikaziSveUcionice = new PrikaziSveUcioniceCommand();

        public static readonly ICommand DodajPredmet = new DodajPredmetCommand();
        public static readonly ICommand PrikaziSvePredmete = new PrikaziSvePredmeteCommand();
        public static readonly ICommand IzmeniPredmet = new IzmeniPredmetCommand();
        public static readonly ICommand PrikaziDetaljePredmeta = new PrikaziDetaljePredmetaCommand();

        public static readonly ICommand DodajSmer = new DodajSmerCommand();
        public static readonly ICommand PrikaziSveSmerove = new PrikaziSveSmeroveCommand();

        public static readonly ICommand DodajSoftver = new DodajSoftverCommand();
        public static readonly ICommand PrikaziSveSoftvere = new PrikaziSveSoftvereCommand();

        

        public class PrikaziSveSmeroveCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var window = new Zadatak2.Interfejs.Dialozi.PrikaziSveSmeroveDialog();
                window.Show();
            }
        }



        public class DodajSmerCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var window = new Zadatak2.Interfejs.Dialozi.DodajSmerDialog();
                window.Show();
            }
        }





        public class DodajSoftverCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var window = new Zadatak2.Interfejs.Dialozi.DodajSoftverDialog();
                window.Show();
            }
        }



        public class PrikaziSveSoftvereCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var window = new Zadatak2.Interfejs.Dialozi.PrikaziSveSoftvereDialog();
                window.Show();
            }
        }





        public class PrikaziSveUcioniceCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var window = new Zadatak2.Interfejs.Dialozi.PrikaziSveUcioniceDialog();
                window.Show();
            }
        }


        public class DodajUcionicuCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var window = new Zadatak2.Interfejs.Dialozi.DodajUcionicuDialog();
                window.Show();
            }
        }







        public class PrikaziSvePredmeteCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var window = new Zadatak2.Interfejs.Dialozi.PrikaziSvePredmeteDialog();
                window.Show();
            }
        }

        public class PrikaziDetaljePredmetaCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var window = new Zadatak2.Interfejs.Dialozi.DetaljiPredmetaDialog(parameter as Predmet);
                window.Show();
            }
        }

        public class IzmeniPredmetCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var window = new Zadatak2.Interfejs.Dialozi.IzmeniPredmetDialog(parameter as Predmet);
                window.Show();
            }
        }

        public class DodajPredmetCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var window = new Zadatak2.Interfejs.Dialozi.DodajPredmetDialog();
                window.Show();
            }
        }
    }
}
