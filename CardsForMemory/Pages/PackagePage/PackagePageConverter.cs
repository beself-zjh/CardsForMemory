using CardsForMemoryLibrary.Models;
using System;
using Windows.UI.Xaml.Data;

namespace CardsForMemory.PackagePageConverter {
    public class NameConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "名字: " + (value as Package)?.Name ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class AuthorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "作者: " + (value as Package)?.Author ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class CreateTimeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "创建时间: " + (value as Package)?.CreateTime ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class UpdateTimeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "更改时间: " + (value as Package)?.UpdateTime ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class DescriptionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "描述: " + (value as Package)?.Description ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
