using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Windows;

namespace Zadatak2.Klase
{
    public class DataDAO : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
            Console.WriteLine("Changed list");
        }

        // Citanje ucionica iz fajla
        public List<Ucionica> readUcionicaFromFile<Ucionica>()
        {
            try
            {
                using (Stream stream = File.Open("Ucionica.bin", FileMode.Open))
                {

                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    if (stream.Length > 0)
                    {
                        return (List<Ucionica>)binaryFormatter.Deserialize(stream);
                    }
                    else
                    {
                        return new List<Ucionica>();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                File.Open("Ucionica.bin", FileMode.Create);
                return new List<Ucionica>();
            }

        }

        //Pisanje ucionica u fajl
        public void writeUcionicaToFile<Ucionica>(List<Ucionica> objectToWrite, bool append = false)
        {

            using (Stream stream = File.Open("Ucionica.bin", append ? FileMode.Append : FileMode.Create))
            {

                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                binaryFormatter.Serialize(stream, objectToWrite);

            }

        }


        // Citanje Predmeta iz fajla
        public List<Predmet> readPredmetFromFile<Predmet>()
        {
            try
            {
                using (Stream stream = File.Open("Predmet.bin", FileMode.Open))
                {

                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    if (stream.Length > 0)
                    {
                        return (List<Predmet>)binaryFormatter.Deserialize(stream);
                    }
                    else
                    {
                        return (new List<Predmet>());
                    }
                }
            }
            catch (FileNotFoundException)
            {
                File.Open("Predmet.bin", FileMode.Create);
                return (new List<Predmet>());
            }

        }

        // Pisanje Predmeta u fajl
        public void writePredmetToFile<Predmet>(List<Predmet> objectToWrite, bool append = false, bool sendMessage = true)
        {

            using (Stream stream = File.Open("Predmet.bin", append ? FileMode.Append : FileMode.Create))
            {

                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                binaryFormatter.Serialize(stream, objectToWrite);
                if (sendMessage) OnSendMessage();

            }

        }


        // Citanje Smerova iz fajla
        public List<Smer> readSmerFromFile<Smer>()
        {
            try
            {
                using (Stream stream = File.Open("Smer.bin", FileMode.Open))
                {

                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    if (stream.Length > 0)
                    {
                        return (List<Smer>)binaryFormatter.Deserialize(stream);
                    }
                    else
                    {
                        return new List<Smer>();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                File.Open("Smer.bin", FileMode.Create);
                return new List<Smer>();
            }

        }

        // Upisivanje smerova u fajl
        public void writeSmerToFile<Smer>(List<Smer> objectToWrite, bool append = false)
        {

            using (Stream stream = File.Open("Smer.bin", append ? FileMode.Append : FileMode.Create))
            {

                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                binaryFormatter.Serialize(stream, objectToWrite);

            }

        }


        // Citanje softvera iz fajla
        public List<Softver> readSoftverFromFile<Softver>()
        {
            try
            {
                using (Stream stream = File.Open("Softver.bin", FileMode.Open))
                {

                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    if (stream.Length > 0)
                    {
                        return (List<Softver>)binaryFormatter.Deserialize(stream);
                    }
                    else
                    {
                        return new List<Softver>();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                File.Open("Softver.bin", FileMode.Create);
                return new List<Softver>();
            }

        }

        // Pisanje softvera u fajl
        public void writeSoftverToFile<Softver>(List<Softver> objectToWrite, bool append = false)
        {

            using (Stream stream = File.Open("Softver.bin", append ? FileMode.Append : FileMode.Create))
            {

                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                binaryFormatter.Serialize(stream, objectToWrite);

            }
        }

        // Citanje rasporeda termina iz fajla
        public List<RasporedTermina> readRasporedTerminaFromFile<RasporedTermina>()
        {
            try
            {
                using (Stream stream = File.Open("Raspored.bin", FileMode.Open))
                {

                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    if (stream.Length > 0)
                    {
                        return (List<RasporedTermina>)binaryFormatter.Deserialize(stream);
                    }
                    else
                    {
                        return new List<RasporedTermina>();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                File.Open("Raspored.bin", FileMode.Create);
                return new List<RasporedTermina>();
            }

        }

        // Pisanje rasporeda termina u fajl
        public void writeRasporedTerminaToFile<Softver>(List<RasporedTermina> objectToWrite, bool append = false)
        {
            try
            {
                using (Stream stream = File.Open("Raspored.bin", append ? FileMode.Append : FileMode.Create))
                {

                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    binaryFormatter.Serialize(stream, objectToWrite);
                }
            }
            catch (IOException) { }
        }

        public List<Termin> readTerminFromFile<Termin>()
        {
            try
            {
                using (Stream stream = File.Open("Termini.bin", FileMode.Open))
                {

                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    if (stream.Length > 0)
                    {
                        return (List<Termin>)binaryFormatter.Deserialize(stream);
                    }
                    else
                    {
                        return new List<Termin>();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                File.Open("Termini.bin", FileMode.Create);
                return new List<Termin>();
            }

        }

        // Pisanje rasporeda termina u fajl
        public void writeTerminToFile<Termin>(List<Termin> objectToWrite, bool append = false)
        {
            try
            {
                using (Stream stream = File.Open("Termini.bin", append ? FileMode.Append : FileMode.Create))
                {

                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    binaryFormatter.Serialize(stream, objectToWrite);
                }
            }
            catch (IOException) { }
        }

        public void TerminPrebacenNaMapu(Termin termin)
        {
            foreach (Termin t in DataDAO.getDataDAO().Termini)
            {
                if (t.Id == termin.Id)
                {
                    t.X = termin.X;
                    t.Y = termin.Y;
                    break;
                }
            }

            DataDAO.getDataDAO().writeTerminToFile<Termin>(DataDAO.getDataDAO().Termini);
        }

        public DataDAO()
        {
            this.ucionice = new List<Ucionica>();
            this.predmeti = new List<Predmet>();
            this.smerovi = new List<Smer>();
            this.softveri = new List<Softver>();
            this.rasporediTermina = new List<RasporedTermina>();
            this.termini = new List<Termin>();
        }

        public static DataDAO getDataDAO()
        {
            if (dataDAO == null)
            {
                dataDAO = new DataDAO();
            }
            return dataDAO;
        }

        public List<Ucionica> Ucionice
        {
            get
            {
                return ucionice;
            }

            set
            {
                if (value != ucionice)
                {
                    ucionice = value;
                    OnPropertyChanged("Ucionice");
                }
            }
        }

        public List<Predmet> Predmeti
        {
            get
            {
                return predmeti;
            }

            set
            {
                if (value != predmeti)
                {
                    predmeti = value;
                    OnPropertyChanged("Predmeti");
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

        public List<Softver> Softveri
        {
            get
            {
                return softveri;
            }

            set
            {
                if (value != softveri)
                {
                    softveri = value;
                    OnPropertyChanged("Softveri");
                }
            }
        }

        public List<RasporedTermina> RasporediTermina
        {
            get
            {
                return rasporediTermina;
            }

            set
            {
                if (value != rasporediTermina)
                {
                    rasporediTermina = value;
                    OnPropertyChanged("RasporediTermina");
                }
            }
        }

        public delegate void SendMessageToMainWindow(String notice);
        public static event SendMessageToMainWindow OnPredmetiChange;

        private static void OnSendMessage()
        {
            if (OnPredmetiChange != null)
                OnPredmetiChange("CHANGED");
        }
        private static DataDAO dataDAO = null;

        private List<RasporedTermina> rasporediTermina;
        private List<Ucionica> ucionice;
        private List<Termin> termini;
        private List<Predmet> predmeti;
        private List<Smer> smerovi;
        private List<Softver> softveri;
    }
}
