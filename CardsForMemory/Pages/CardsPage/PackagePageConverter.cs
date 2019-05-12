using System;
using Windows.UI.Xaml.Data;

namespace CardsForMemory.PackagePageConverter {
    public class NameConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "Name: " + value as string;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class AuthorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "Author: " + value as string;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class CreateTimeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "CreateTime: " + value as string;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class UpdateTimeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "UpdateTime: " + value as string;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class DescriptionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "Description: " + value as string;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
