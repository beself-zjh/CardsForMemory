using System;
using Windows.UI.Xaml.Data;

namespace CardsForMemory.QueryNewOldConverter {
    public class NewMaxConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "新卡数量: " + (value as string) ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class OldMaxConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return "旧卡数量: " + (value as string) ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
