using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Zadatak2.Binders
{
    class EnumToStringConverter : IValueConverter
    {
        private Dictionary<String, String> EnumValues = 
            new Dictionary<string,string>()
            {
                {"WINDOWS", "Windows"},
                {"LINUX", "Linux"},
                {"OBOJE", "Windows i Linux"}
            };

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string EnumString;
            try
            {
                EnumString = Enum.GetName((value.GetType()), value);
                return EnumValues[EnumString];
            }
            catch
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
