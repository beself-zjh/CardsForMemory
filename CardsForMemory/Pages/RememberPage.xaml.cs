using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CardsForMemory.Pages
{
    public sealed partial class RememberPage : Page
    {
        public string Text { get; set; }
        private DispatcherTimer timer;//定义定时器
        private int second = 0;
        private int minute = 0;
        private const double PI = 3.1415926535898;
        private double circleLength;
        private List<Card> now = null;
        private int x = 0;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                RememberPageShieldingLayer.Width = 750;
                return;
            }
            else
            {
                RememberPageShieldingLayer.Width = 0;
            }
            int val = (int)e.Parameter;
            now = CardHolder.CardHolders[val].Cards;
            if (now.Count == 0) return;
            Problem.Text = now[x++].a;
            Items.Text = now[x++].b;
        }

        public RememberPage()
        {
            InitializeComponent();
            if (RememberPageInnerClock.StrokeThickness != RememberPageOuterClock.StrokeThickness)
            {
                throw new Exception("Clock Thickness is not equal!!");
            }
            double strokeThickness = RememberPageOuterClock.StrokeThickness;
            circleLength = (RememberPageClock.Width - strokeThickness) * PI / strokeThickness;
        }

        private void RememberPageLoadedHandler(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
            // 每秒触发这个事件，刷新UI
            timer.Tick += (a, s) =>
            {
                ++second;
                if (second == 60)
                {
                    second = 0;
                    minute++;

                    var t = RememberPageInnerClock.Stroke;
                    RememberPageInnerClock.Stroke = RememberPageOuterClock.Stroke;
                    RememberPageOuterClock.Stroke = t;

                    RememberPageInnerClock.StrokeDashArray = new DoubleCollection { circleLength * second / 60, circleLength };
                }
                string smin = minute.ToString(), ssec = second.ToString();
                if (minute < 10)
                {
                    smin = "0" + smin;
                }
                if (second < 10)
                {
                    ssec = "0" + ssec;
                }
                RememberPageClockText.Text = String.Format("{0}:{1}", smin, ssec);
            };
            timer.Start();
        }

        private void RememberPageBtn1(object sender, RoutedEventArgs e)
        {
            Problem.Text = now[x++].a;
            Items.Text = now[x++].b;
        }
    }
}
