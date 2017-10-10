using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using Zadatak2.Klase;

namespace Zadatak2.Validacija
{
    // Proveravamo da li uneta vrednost odgovara nasim ogranicenjima
    public class MinMaxDuzinaIdPravilo : ValidationRule
    {
        private static int MIN_LENGTH = 5;
        private static int MAX_LENGTH = 30;

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (0 == text.Length)
                {
                    return new ValidationResult(false, "Ovo polje je obavezno");
                }
                else if (text.Length < MIN_LENGTH)
                {
                    return new ValidationResult(false, "Minimalna dužina polja je " + MIN_LENGTH + " karaktera.");
                }
                else if (text.Length > MAX_LENGTH)
                {
                    return new ValidationResult(false, "Maksimalna dužina polja je " + MAX_LENGTH + " karaktera.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredviđena greška.");
            }
        }
    }

    // Pravilo kojim provjeravamo sadrzaj koji je unesen u polje
    public class SadrzajIdPravilo : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;

                Regex r = new Regex(@"[^0-9a-zA-Z]+", RegexOptions.IgnoreCase);

                Match m = r.Match(text);

                if (m.Success)
                {
                    return new ValidationResult(false, "Polje moze sadrzati samo slova i brojeve.");
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredvidjena greska.");
            }
        }
    }

    // Proveramo da li vec postoji Softver sa unesenim id-em
    public class JedinstvenIdSoftverPravilo : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;

                foreach (Softver s in DataDAO.getDataDAO().Softveri)
                {
                    if (s.Id.ToLower() == text.ToLower())
                    {
                        return new ValidationResult(false, "Softver sa tim id-em vec postoji. Polje mora biti jedinstveno.");
                    }
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredvidjena greska.");
            }
        }
    }

    // Proveramo da li vec postoji Smer sa unesenim id-em
    public class JedinstvenIdSmerPravilo : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;

                foreach (Smer s in DataDAO.getDataDAO().Smerovi)
                {
                    if (s.Id == text)
                    {
                        return new ValidationResult(false, "Smer sa tim id-em vec postoji. Polje mora biti jedinstveno.");
                    }
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredvidjena greska.");
            }
        }
    }

    // Proveramo da li vec postoji Predmet sa unesenim id-em
    public class JedinstvenIdPredmetPravilo : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;

                foreach (Predmet s in DataDAO.getDataDAO().Predmeti)
                {
                    if (s.Id == text)
                    {
                        return new ValidationResult(false, "Predmet sa tim id-em vec postoji. Polje mora biti jedinstveno.");
                    }
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredvidjena greska.");
            }
        }
    }

    // Proveramo da li vec postoji Ucionica sa unesenim id-em
    public class JedinstvenIdUcionicaPravilo : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;

                foreach (Ucionica s in DataDAO.getDataDAO().Ucionice)
                {
                    if (s.Id == text)
                    {
                        return new ValidationResult(false, "Ucionica sa tim id-em vec postoji. Polje mora biti jedinstveno.");
                    }
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredvidjena greska.");
            }
        }
    }
}
