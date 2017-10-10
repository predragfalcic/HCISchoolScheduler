using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Zadatak2.Klase
{
    [Serializable]
    public class Smer : INotifyPropertyChanged
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


        public DateTime DatumUvodjenjaSmera
        {
            get
            {
                return datumUvodjenjaSmera;
            }

            set
            {
                if (value != datumUvodjenjaSmera)
                {
                    datumUvodjenjaSmera = value;
                    OnPropertyChanged("DatumUvodjenjaSmera");
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

        public override string ToString()
        {
            return naziv;
        }

        private string id;
        private string naziv;
        private DateTime datumUvodjenjaSmera;
        private string opis;

    }
}
