using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Zadatak2.Klase
{
    [Serializable]
    public enum OperativniSistemS
    {
        WINDOWS,
        LINUX,
        OBOJE
    }

    [Serializable]
    public class Softver : INotifyPropertyChanged
    {
        [field: NonSerializedAttribute()] 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
            Console.WriteLine("Changed" + name);
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Naziv
        {
            get
            {
                return naziv;
            }

            set
            {
                if (value != naziv)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }


        public int GodinaIzdavanja
        {
            get
            {
                return godinaIzdavanja;
            }

            set
            {
                if (value != godinaIzdavanja)
                {
                    godinaIzdavanja = value;
                    OnPropertyChanged("GodinaIzdavanja");
                }
            }
        }

        public string Opis
        {
            get
            {
                return opis;
            }

            set
            {
                if (value != opis)
                {
                    opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }

        public OperativniSistemS Os
        {
            get
            {
                return os;
            }

            set
            {
                if (value != os)
                {
                    os = value;
                    OnPropertyChanged("Os");
                }
            }
        }

        public string Proizvodjac
        {
            get
            {
                return proizvodjac;
            }

            set
            {
                if (value != proizvodjac)
                {
                    proizvodjac = value;
                    OnPropertyChanged("Proizvodjac");
                }
            }
        }

        public string Sajt
        {
            get
            {
                return sajt;
            }

            set
            {
                if (value != sajt)
                {
                    sajt = value;
                    OnPropertyChanged("Sajt");
                }
            }
        }

        public double Cena
        {
            get
            {
                return cena;
            }

            set
            {
                if (value != cena)
                {
                    cena = value;
                    OnPropertyChanged("Cena");
                }
            }
        }

        public Softver()
        {
            this.godinaIzdavanja = 2000;

        }

        public override string ToString()
        {
            return naziv;
        }

        private string id;
        private string naziv;
        private OperativniSistemS os;
        private string proizvodjac;
        private string sajt;
        private int godinaIzdavanja;
        private double cena;
        private string opis;

    }
}
