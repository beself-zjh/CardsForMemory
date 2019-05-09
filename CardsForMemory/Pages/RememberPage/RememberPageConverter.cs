namespace CardsForMemory.RememberPageConverter {
    public class RingWidth : Windows.UI.Xaml.Data.IValueConverter {
        public object Convert(object value, System.Type targetType, object parameter, string language) {
            var vm = Locator.ViewModelLocator.Instance.RememberPageViewModel;
            return (double)(vm.Style % 100);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language) {
            throw new System.NotImplementedException();
        }
    }

    public class ClockWidth : Windows.UI.Xaml.Data.IValueConverter {
        public object Convert(object value, System.Type targetType, object parameter, string language) {
            var vm = Locator.ViewModelLocator.Instance.RememberPageViewModel;
            return (double)(vm.Style / 100);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language) {
            throw new System.NotImplementedException();
        }
    }

    public class RingStatus : Windows.UI.Xaml.Data.IValueConverter {
        public object Convert(object value, System.Type targetType, object parameter, string language) {
            var vm = Locator.ViewModelLocator.Instance.RememberPageViewModel;
            int ClockWidth = vm.Style / 100, RingWidth = vm.Style % 100, Second = vm.Time % 60;
            double circumference = (ClockWidth - RingWidth) * 3.1415926535898 / RingWidth;
            return new Windows.UI.Xaml.Media.DoubleCollection { circumference * Second / 60, circumference };
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language) {
            throw new System.NotImplementedException();
        }
    }

    public class ClockText : Windows.UI.Xaml.Data.IValueConverter {
        public object Convert(object value, System.Type targetType, object parameter, string language) {
            var vm = Locator.ViewModelLocator.Instance.RememberPageViewModel;
            string second = (vm.Time % 60).ToString(), minute = (vm.Time / 60).ToString();
            if (second.Length == 1) {
                second = "0" + second;
            }
            if (minute.Length == 1) {
                minute = "0" + minute;
            }
            return minute + ":" + second;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language) {
            throw new System.NotImplementedException();
        }
    }

    public class InColor : Windows.UI.Xaml.Data.IValueConverter {
        public object Convert(object value, System.Type targetType, object parameter, string language) {
            var vm = Locator.ViewModelLocator.Instance.RememberPageViewModel;
            if (vm.Time / 60 % 2 == 0) {
                return new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.DeepSkyBlue);
            } else {
                return new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Gray);
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language) {
            throw new System.NotImplementedException();
        }
    }

    public class OutColor : Windows.UI.Xaml.Data.IValueConverter {
        public object Convert(object value, System.Type targetType, object parameter, string language) {
            var vm = Locator.ViewModelLocator.Instance.RememberPageViewModel;
            if (vm.Time / 60 % 2 == 0) {
                return new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Gray);
            } else {
                return new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.DeepSkyBlue);
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, string language) {
            throw new System.NotImplementedException();
        }
    }
}
