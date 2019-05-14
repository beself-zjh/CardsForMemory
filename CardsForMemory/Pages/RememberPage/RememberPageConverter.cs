using CardsForMemory.Locator;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace CardsForMemory.RememberPageConverter {
    public class AntiVis : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return (bool)(value ?? false) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class Vis : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return (bool)(value ?? false) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class RingWidth : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            var vm = ViewModelLocator.Instance.RememberPageViewModel;
            return (double)(vm.Style % 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class ClockWidth : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            var vm = ViewModelLocator.Instance.RememberPageViewModel;
            return (double)(vm.Style / 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class RingStatus : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            var vm = ViewModelLocator.Instance.RememberPageViewModel;
            int ClockWidth = vm.Style / 100, RingWidth = vm.Style % 100, Second = vm.Time % 60;
            double circumference = (ClockWidth - RingWidth) * 3.1415926535898 / RingWidth;
            return new DoubleCollection { circumference * Second / 60, circumference };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class ClockText : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            var vm = ViewModelLocator.Instance.RememberPageViewModel;
            string second = (vm.Time % 60).ToString(), minute = (vm.Time / 60).ToString();
            if (second.Length == 1) {
                second = "0" + second;
            }
            if (minute.Length == 1) {
                minute = "0" + minute;
            }
            return minute + ":" + second;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class InColor : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            var vm = ViewModelLocator.Instance.RememberPageViewModel;
            if (vm.Time / 60 % 2 == 0) {
                return new SolidColorBrush(Windows.UI.Colors.DeepSkyBlue);
            } else {
                return new SolidColorBrush(Windows.UI.Colors.Gray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class OutColor : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            var vm = ViewModelLocator.Instance.RememberPageViewModel;
            if (vm.Time / 60 % 2 == 0) {
                return new SolidColorBrush(Windows.UI.Colors.Gray);
            } else {
                return new SolidColorBrush(Windows.UI.Colors.DeepSkyBlue);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
