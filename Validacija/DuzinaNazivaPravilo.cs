using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Zadatak2.Validacija
{

    // Proveravamo da li je naziv presao predvidjen max broj karaktera
    public class DuzinaNazivaPravilo : ValidationRule
    {
        private static int MAX_NAME_LENGTH = 80;

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
}
