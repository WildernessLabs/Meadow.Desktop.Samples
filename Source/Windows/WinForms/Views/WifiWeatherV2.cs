using Meadow.Foundation.Graphics;
using Meadow.Foundation.Graphics.MicroLayout;
using System;
using System.Threading.Tasks;

namespace WinFormsMeadow.Views
{
    internal class WifiWeatherV2
    {
        private Meadow.Foundation.Color backgroundColor = Meadow.Foundation.Color.FromHex("10485E");
        private Meadow.Foundation.Color selectedColor = Meadow.Foundation.Color.FromHex("C9DB31");
        private Meadow.Foundation.Color accentColor = Meadow.Foundation.Color.FromHex("EF7D3B");
        private Meadow.Foundation.Color ForegroundColor = Meadow.Foundation.Color.FromHex("EEEEEE");
        private Font12x20 font12X20 = new Font12x20();
        private Font8x12 font8x12 = new Font8x12();
        private Font8x16 font8x16 = new Font8x16();
        private Font6x8 font6x8 = new Font6x8();
        private int margin = 5;
        public LineChartSeries LineChartSeries { get; set; }
        protected DisplayScreen DisplayScreen { get; set; }
        protected AbsoluteLayout SplashLayout { get; set; }
        protected AbsoluteLayout DataLayout { get; set; }
        protected LineChart LineChart { get; set; }
        protected Picture WifiStatus { get; set; }
        protected Picture SyncStatus { get; set; }
        protected Picture WeatherIcon { get; set; }
        protected Label Status { get; set; }

        protected Box TemperatureBox { get; set; }
        protected Label TemperatureLabel { get; set; }
        protected Label TemperatureValue { get; set; }

        protected Box PressureBox { get; set; }
        protected Label PressureLabel { get; set; }
        protected Label PressureValue { get; set; }

        protected Box HumidityBox { get; set; }
        protected Label HumidityLabel { get; set; }
        protected Label HumidityValue { get; set; }

        protected Label FeelsLike { get; set; }
        protected Label Sunrise { get; set; }
        protected Label Sunset { get; set; }

        public WifiWeatherV2(IGraphicsDisplay display)
        {
            DisplayScreen = new DisplayScreen(display)
            {
                BackgroundColor = backgroundColor
            };

            LoadSplashLayout();

            DisplayScreen.Controls.Add(SplashLayout);

            LoadDataLayout();

            DisplayScreen.Controls.Add(DataLayout);
        }

        private void LoadSplashLayout()
        {
            SplashLayout = new AbsoluteLayout(DisplayScreen, 0, 0, DisplayScreen.Width, DisplayScreen.Height)
            {
                Visible = false
            };

            var image = Image.LoadFromResource("WinFormsMeadow.Resources.img_meadow.bmp");
            var displayImage = new Picture(0, 0, DisplayScreen.Width, DisplayScreen.Height, image)
            {
                BackColor = Meadow.Foundation.Color.FromHex("#14607F"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            SplashLayout.Controls.Add(displayImage);
        }

        private void LoadDataLayout()
        {
            DataLayout = new AbsoluteLayout(DisplayScreen, 0, 0, DisplayScreen.Width, DisplayScreen.Height)
            {
                BackgroundColor = backgroundColor,
                Visible = false
            };

            DataLayout.Controls.Add(new Label(
                margin,
                margin,
                DisplayScreen.Width / 2,
                font8x16.Height)
            {
                Text = $"Project Lab v3",
                TextColor = Meadow.Foundation.Color.White,
                Font = font8x16
            });

            var wifiImage = Image.LoadFromResource("WinFormsMeadow.Resources.img_wifi_connecting.bmp");
            WifiStatus = new Picture(
                DisplayScreen.Width - wifiImage.Width - margin,
                margin,
                wifiImage.Width,
                font8x16.Height,
                wifiImage)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            DataLayout.Controls.Add(WifiStatus);

            var syncImage = Image.LoadFromResource("WinFormsMeadow.Resources.img_refreshed.bmp");
            SyncStatus = new Picture(
                DisplayScreen.Width - syncImage.Width - wifiImage.Width - margin * 2,
                margin,
                syncImage.Width,
                font8x16.Height,
                syncImage)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            DataLayout.Controls.Add(SyncStatus);
        }

        public void ShowSplashScreen()
        {
            DataLayout.Visible = false;
            SplashLayout.Visible = true;
        }

        public void ShowDataScreen()
        {
            SplashLayout.Visible = false;
            DataLayout.Visible = true;
        }

        public void UpdateStatus(string status)
        {
            Status.Text = status;
        }

        public void UpdateWiFiStatus(bool isConnected)
        {
            var imageWiFi = isConnected
                ? Image.LoadFromResource("WinFormsMeadow.Resources.img_wifi_connected.bmp")
                : Image.LoadFromResource("WinFormsMeadow.Resources.img_wifi_connecting.bmp");
            WifiStatus.Image = imageWiFi;
        }

        public void UpdateSyncStatus(bool isSyncing)
        {
            var imageSync = isSyncing
                ? Image.LoadFromResource("WinFormsMeadow.Resources.img_refreshing.bmp")
                : Image.LoadFromResource("WinFormsMeadow.Resources.img_refreshed.bmp");
            SyncStatus.Image = imageSync;
        }

        protected void UpdateReadings(double temperature, double pressure, double humidity)
        {
            DisplayScreen.BeginUpdate();

            TemperatureValue.Text = $"{temperature:N1}C";
            PressureValue.Text = $"{pressure:N1}atm";
            HumidityValue.Text = $"{humidity:N1}%";

            DisplayScreen.EndUpdate();
        }
        public async Task Run()
        {
            //ShowSplashScreen();
            //Thread.Sleep(3000);
            ShowDataScreen();

            var random = new Random();

            int x = 0;

            while (true)
            {


                await Task.Delay(1000);
            }
        }
    }
}