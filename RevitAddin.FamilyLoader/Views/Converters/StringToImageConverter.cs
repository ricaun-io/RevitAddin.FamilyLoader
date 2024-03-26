using ricaun.Revit.UI;
using System;
using System.Globalization;
using System.Windows.Data;

namespace RevitAddin.FamilyLoader.Views.Converters
{
    internal class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string valueString)
            {
                return valueString.GetBitmapSource();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
