using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Zadatak2.Klase
{
    [Serializable]
    public class RasporedTermina : INotifyPropertyChanged
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

        public List<Termin> Termini
        {
            get
            {
                return termini;
            }

            set
            {
                if (value != termini)
                {
                    termini = value;
                    OnPropertyChanged("Termini");
                }
            }
        }

        public Ucionica UcionicaRasporeda
        {
            get
            {
                return ucionicaRasporeda;
            }

            set
            {
                if (value != ucionicaRasporeda)
                {
                    ucionicaRasporeda = value;
                    OnPropertyChanged("UcionicaRasporeda");
                }
            }
        }

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

        public RasporedTermina()
        {
            this.termini = new List<Termin>();
            this.datumRasporeda = DateTime.Now.Date;
        }

        private DateTime datumRasporeda;
        private Ucionica ucionicaRasporeda;
        private List<Termin> termini;
        private string id;
    }
}
