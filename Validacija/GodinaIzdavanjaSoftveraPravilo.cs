using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Zadatak2.Validacija
{
    class GodinaIzdavanjaSoftveraPravilo : ValidationRule
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
                    int godina = Int32.Parse(text);

                    if (godina < 0)
                    {
                        return new ValidationResult(false, "Godina izdavanja mora biti pozitivan broj.");
                    }
                    else if (godina < 1950)
                    {
                        return new ValidationResult(false, "Godina izdavanja mora veca od 1949.");
                    }
                    else if (godina > DateTime.Now.Year)
                    {
                        return new ValidationResult(false, "Godina izdavanja mora manja od trenutne godine.");
                    }
                    else
                    {
                        return new ValidationResult(true, null);
                    }
                }
                catch (FormatException e)
                {
                    return new ValidationResult(false, "Godina izdavanja mora biti ceo broj.");
                }
            }
            catch
            {
                return new ValidationResult(false, "Polje mora sadrzati samo brojeve.");
            }
        }
    }

    // Proveravamo da li je broj pozitivan, ceo i ne veci od 4
    class MinimalanBrojCasovaPredmetaPravilo : ValidationRule
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
                    else if (vr > 4)
                    {
                        return new ValidationResult(false, "Vrednost ne sme biti veca od 4.");
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

    // Proveravamo da li je vrednost veca od 0 i manja od 20
    class BrojTerminaPredmetaPravilo : ValidationRule
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
                    else if (vr > 20)
                    {
                        return new ValidationResult(false, "Vrednost ne sme biti veca od 20.");
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

    // Proveravamo da li je vrednost veca od 0 i manja od 20
    class VelicinaGrupePredmetPravilo : ValidationRule
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

