using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Zadatak2.Validacija
{
    class DobarLinkPravilo : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;

                if (text.Length == 0)
                {
                    return new ValidationResult(false, "Ovo polje je obavezno.");
                }

                if (text.Length > 100)
                {
                    return new ValidationResult(false, "Maksimalan broj karaktera je 100.");
                }

                Regex r = new Regex(@"^(((ht|f)tp(s?))\://)?(www.|[a-zA-Z].)[a-zA-Z0-9\-\.]+\.(com|edu|gov|mil|net|org|biz|info|name|museum|us|ca|uk)(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\;\?\'\\\+&amp;%\$#\=~_\-]+))*$", RegexOptions.IgnoreCase);

                Match m = r.Match(text);

                if (!m.Success)
                {
                    return new ValidationResult(false, "Molimo vas unesite uspravan url. Npr. www.google.com");
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
