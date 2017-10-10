using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Zadatak2.Klase
{
    [Serializable]
    public class Termin : INotifyPropertyChanged
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

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                if (value != x)
                {
                    x = value;
                    OnPropertyChanged("X");
                }
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                if (value != y)
                {
                    y = value;
                    OnPropertyChanged("Y");
                }
            }
        }

        public Predmet PredmetTermina
        {
            get
            {
                return predmetTermina;
            }

            set
            {
                if (value != predmetTermina)
                {
                    predmetTermina = value;
                    OnPropertyChanged("PredmetTermina");
                }
            }
        }

        public Termin()
        {
            this.id = RandomString(10);
            this.x = -1;
            this.y = -1;
        }

        private string id;
        private Predmet predmetTermina;
        private int x;
        private int y;

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuioplkjhgfdsazxcvbnm";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
