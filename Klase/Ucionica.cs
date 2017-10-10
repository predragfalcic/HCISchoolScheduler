using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Zadatak2.Klase
{
    [Serializable]
    public enum OperativniSistemU
    {
        WINDOWS,
        LINUX,
        OBOJE
    }

    [Serializable]
    public class Ucionica : INotifyPropertyChanged
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

        public List<Softver> InstaliranSoftver
        {
            get
            {
                return instaliranSoftver;
            }

            set
            {
                if (value != instaliranSoftver)
                {
                    instaliranSoftver = value;
                    OnPropertyChanged("InstaliranSoftver");
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

        public OperativniSistemU OperativniSistemU
        {
            get
            {
                return operativniSistemU;
            }

            set
            {
                if (value != operativniSistemU)
                {
                    operativniSistemU = value;
                    OnPropertyChanged("OperativniSistemU");
                }
            }
        }

        public int BrRadnihMesta
        {
            get
            {
                return brRadnihMesta;
            }

            set
            {
                if (value != brRadnihMesta)
                {
                    brRadnihMesta = value;
                    OnPropertyChanged("BrRadnihMesta");
                }
            }
        }

        public bool ImaProjektor
        {
            get
            {
                return imaProjektor;
            }

            set
            {
                if (value != imaProjektor)
                {
                    imaProjektor = value;
                    OnPropertyChanged("ImaProjektor");
                }
            }
        }

        public bool ImaTablu
        {
            get
            {
                return imaTablu;
            }

            set
            {
                if (value != imaTablu)
                {
                    imaTablu = value;
                    OnPropertyChanged("ImaTablu");
                }
            }
        }

        public bool ImaPametnuTablu
        {
            get
            {
                return imaPametnuTablu;
            }

            set
            {
                if (value != imaPametnuTablu)
                {
                    imaPametnuTablu = value;
                    OnPropertyChanged("ImaPametnuTablu");
                }
            }
        }

        public Ucionica()
        {
            this.instaliranSoftver = new List<Softver>();
            this.brRadnihMesta = 1;
        }

        public override string ToString()
        {
            return Id;
        }

        private string id;
        private string opis;
        private int brRadnihMesta;
        private bool imaProjektor;
        private bool imaTablu;
        private bool imaPametnuTablu;
        private OperativniSistemU operativniSistemU;
        private List<Softver> instaliranSoftver;

        
    }
}
