using System;
using System.Globalization;
using Xamarin.Forms;

namespace UpcommingMovies.UI.Converters
{
    // As describled at https://prismlibrary.readthedocs.io/en/latest/Xamarin-Forms/6-EventToCommandBehavior/
    public class ItemVisibilityEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemVisibilityEventArgs = value as ItemVisibilityEventArgs;

            if (itemVisibilityEventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type ItemVisibilityEventArgs", nameof(value));
            }

            return itemVisibilityEventArgs.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
