using System;
using Windows.UI.Xaml.Data;

namespace CardsForMemory.EditPackagePageConverter {
    public class DateTimeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if ((value as DateTime?).Value == DateTime.MinValue) {
                return "尚未学习";
            }
            return (value as DateTime?).Value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
