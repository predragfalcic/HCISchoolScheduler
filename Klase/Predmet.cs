using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace Zadatak2.Klase
{
    [Serializable]
    public enum OperativniSistemP
    {
        WINDOWS,
        LINUX,
        OBOJE
    }

    [Serializable]
    public class Predmet : INotifyPropertyChanged
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

        public List<Smer> Smerovi
        {
            get
            {
                return smerovi;
            }

            set
            {
                if (value != smerovi)
                {
                    smerovi = value;
                    OnPropertyChanged("Smerovi");
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

        public int VelicinaGrupe
        {
            get
            {
                return velicinaGrupe;
            }

            set
            {
                if (value != velicinaGrupe)
                {
                    velicinaGrupe = value;
                    OnPropertyChanged("VelicinaGrupe");
                }
            }
        }

        public int MinimalnaDuzinaTermina
        {
            get
            {
                return minimalnaDuzinaTermina;
            }

            set
            {
                if (value != minimalnaDuzinaTermina)
                {
                    minimalnaDuzinaTermina = value;
                    OnPropertyChanged("MinimalnaDuzinaTermina");
                }
            }
        }

        public int BrojTerminaPredmeta
        {
            get
            {
                return brojTerminaPredmeta;
            }

            set
            {
                if (value != brojTerminaPredmeta)
                {
                    brojTerminaPredmeta = value;
                    OnPropertyChanged("BrojTerminaPredmeta");
                }
            }
        }

        public bool PotrebanProjektor
        {
            get
            {
                return potrebanProjektor;
            }

            set
            {
                if (value != potrebanProjektor)
                {
                    potrebanProjektor = value;
                    OnPropertyChanged("PotrebanProjektor");
                }
            }
        }

        public bool PotrebnaTabla
        {
            get
            {
                return potrebnaTabla;
            }

            set
            {
                if (value != potrebnaTabla)
                {
                    potrebnaTabla = value;
                    OnPropertyChanged("PotrebnaTabla");
                }
            }
        }

        public bool PotrebnaPametnaTabla
        {
            get
            {
                return potrebnaPametnaTabla;
            }

            set
            {
                if (value != potrebnaPametnaTabla)
                {
                    potrebnaPametnaTabla = value;
                    OnPropertyChanged("PotrebnaPametnaTabla");
                }
            }
        }

        public OperativniSistemP OperativniSistemP
        {
            get
            {
                return operativniSistemP;
            }

            set
            {
                if (value != operativniSistemP)
                {
                    operativniSistemP = value;
                    OnPropertyChanged("OperativniSistemP");
                }
            }
        }

        public List<Softver> NeophodanSoftver
        {
            get
            {
                return neophodanSoftver;
            }

            set
            {
                if (value != neophodanSoftver)
                {
                    neophodanSoftver = value;
                    OnPropertyChanged("NeophodanSoftver");
                }
            }
        }

        public string IdUcionice
        {
            get
            {
                return idUcionice;
            }

            set
            {
                if (value != idUcionice)
                {
                    idUcionice = value;
                    OnPropertyChanged("IdUcionice");
                }
            }
        }

        public Predmet() {
            this.neophodanSoftver = new List<Softver>();
            this.smerovi = new List<Smer>();
            this.brojTerminaPredmeta = 1;
            this.velicinaGrupe = 1;
            this.minimalnaDuzinaTermina = 1;
        }

        private string idUcionice;
        private string id;
        private string naziv;
        private List<Smer> smerovi;
        private string opis;
        private int velicinaGrupe;
        private int minimalnaDuzinaTermina;
        private int brojTerminaPredmeta;
        private bool potrebanProjektor;
        private bool potrebnaTabla;
        private bool potrebnaPametnaTabla;
        private OperativniSistemP operativniSistemP;
        private List<Softver> neophodanSoftver;
    }
}
