using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Zadatak2.Validacija
{
    class CenaSoftveraPravilo : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {   
                var text = value as string;
                if (text.Length == 0)
                {
                    return new ValidationResult(false, "Ovo polje ne sme ostati prazno.");
                }
                if (text.Contains(","))
                {
                    return new ValidationResult(false, "Molimo vas umesto zareza unesite . (Tacku). Npr. 13.99");
                }

                double cena = Double.Parse(text);

                if (cena < 0)
                {
                    return new ValidationResult(false, "Cena Softvera mora biti pozitivan broj.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            catch
            {
                return new ValidationResult(false, "Polje mora sadrzati samo brojeve.");
            }
        }
    }
}
