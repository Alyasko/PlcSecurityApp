using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using PlcSecurityApp.Views.UC;

namespace PlcSecurityApp.Views.Converters
{
    class SensorStateToFillConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SensorState state;

            if(Enum.TryParse(value.ToString(), out state) == false)
                throw new FormatException();

            return state == SensorState.Ok ? Brushes.Green : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
