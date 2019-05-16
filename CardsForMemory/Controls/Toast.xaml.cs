using CardsForMemory.Services;
using CardsForMemoryLibrary;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Timer = System.Timers.Timer;

namespace CardsForMemory.Controls {
    public sealed partial class Toast : UserControl {
        public Toast(string content, int showTime = 2) {
            RequestedTheme = App.RootTheme;
            InitializeComponent();
            var popup = new Popup { Child = this };
            Width = Window.Current.Bounds.Width;
            Height = Window.Current.Bounds.Height;
            Loaded += (sender, e) => {
                if (content.Contains("`")) {
                    ToastText.Text = content.Split("`")[1];
                } else {
                    ToastText.Text = content;
                }
                fadeOut.BeginTime = TimeSpan.FromSeconds(showTime);
                fadeOut.Begin();
                fadeOut.Completed += (a, b) => { popup.IsOpen = false; };
                Window.Current.SizeChanged += (a, b) => {
                    Width = b.Size.Width;
                    Height = b.Size.Height;
                };
            };
            Unloaded += (sender, e) => {
                Window.Current.SizeChanged -= (a, b) => {
                    Width = b.Size.Width;
                    Height = b.Size.Height;
                };
            };
            popup.IsOpen = true;
            if (new SettingService()["sound"] as string=="False") {
                return;
            }
            var thread = new Thread(() => {
                if (!content.Contains("`")) {
                    return;
                }
                var texts = content.Split("`");
                var api = $"https://fanyi.baidu.com/gettts?lan={texts[0]}&text={texts[1]}&spd=5&source=web";
                var tmp = new DirectoryInfo(Environment.GetEnvironmentVariable("TEMP"));
                if (!tmp.Exists) {
                    tmp.Create();
                }

                var md5 = new MD5CryptoServiceProvider();
                var filename = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(content)), 4, 8);
                filename = filename.Replace("-", "");

                var path = tmp + $"\\{filename}.mp3";
                Download(api, path);
                var player = new MediaPlayer {
                    Source = MediaSource.CreateFromUri(new Uri(path)),
                    AutoPlay = true,
                    IsLoopingEnabled = false
                };

                var timer = new Timer(showTime * 2000) { AutoReset = false };
                timer.Elapsed += (a, b) => {
                    player?.Dispose();
                };
                timer.Enabled = true;
            });
            thread.Start();
        }


        public bool Download(string url, string localfile) {
            if (File.Exists(localfile)) {
                return true;
            }
            FileStream writeStream = new FileStream(localfile, FileMode.Create);
            try {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                var readStream = myRequest.GetResponse().GetResponseStream();
                var btArray = new byte[512];
                var contentSize = readStream.Read(btArray, 0, btArray.Length);
                while (contentSize > 0) {
                    writeStream.Write(btArray, 0, contentSize);
                    contentSize = readStream.Read(btArray, 0, btArray.Length);
                }
                writeStream.Close();
                readStream.Close();
                return true;
            } catch (Exception) {
                writeStream.Close();
                return false;
            }
        }
    }
}
