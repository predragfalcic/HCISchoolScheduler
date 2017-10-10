using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Zadatak2.Validacija
{
    class DatumSmerValidacijaPravilo : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            DateTime datumUvodjenjaSmera = (DateTime)value;

            return new ValidationResult(datumUvodjenjaSmera.Date >= DateTime.Now.Date, "Datum ne sme biti pre danasnjeg.");
        }
    }
}
