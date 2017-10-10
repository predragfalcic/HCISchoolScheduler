using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Zadatak2.Validacija
{
    class DuzinaOpisaPravilo : ValidationRule
    {
        private static int MAX_NAME_LENGTH = 300;

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (0 == text.Length)
                {
                    return new ValidationResult(false, "Ovo polje je obavezno.");
                }
                else if (text.Length > MAX_NAME_LENGTH)
                {
                    return new ValidationResult(false, "Maksimalna duzina polja je " + MAX_NAME_LENGTH + " karaktera.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            catch
            {
                return new ValidationResult(false, "Desila se nepredvidjena greska.");
            }
        }
    }

    class BrojRadnihMestaUcionice : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                try
                {
                    if (text.Length == 0)
                    {
                        return new ValidationResult(false, "Ovo polje ne sme ostati prazno.");
                    }
                    int vr = Int32.Parse(text);

                    if (vr < 0)
                    {
                        return new ValidationResult(false, "Vrednost mora biti pozitivan broj.");
                    }
                    else if (vr < 1)
                    {
                        return new ValidationResult(false, "Vrednost mora veca od 0.");
                    }
                    else if (vr > 30)
                    {
                        return new ValidationResult(false, "Vrednost ne sme biti veca od 30.");
                    }
                    else
                    {
                        return new ValidationResult(true, null);
                    }
                }
                catch (FormatException e)
                {
                    return new ValidationResult(false, "Vrednost mora biti ceo broj.");
                }
            }
            catch
            {
                return new ValidationResult(false, "Polje mora sadrzati samo brojeve.");
            }
        }
    }
}
