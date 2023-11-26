using Meadow.Foundation.Graphics;
using Meadow.Foundation.Graphics.MicroLayout;
using System;
using System.Threading.Tasks;

namespace WinFormsMeadow.Views
{
    internal class ProjectLabDemoView
    {
        //private readonly int rowHeight = 40;
        private readonly int graphHeight = 135;
        //private readonly int margin = 15;

        protected DisplayScreen DisplayScreen { get; set; }

        protected AbsoluteLayout SplashLayout { get; set; }

        protected AbsoluteLayout DataLayout { get; set; }

        public LineChartSeries LineChartSeries { get; set; }

        protected LineChart LineChart { get; set; }

        protected Picture WifiStatus { get; set; }

        protected Picture SyncStatus { get; set; }

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

        protected Box LuminanceBox { get; set; }
        protected Label LuminanceLabel { get; set; }
        protected Label LuminanceValue { get; set; }

        protected Label Date { get; set; }
        protected Label Time { get; set; }

        protected Box Up { get; set; }
        protected Box Down { get; set; }
        protected Box Left { get; set; }
        protected Box Right { get; set; }

        protected Label ConnectionErrorLabel { get; set; }

        private Meadow.Foundation.Color backgroundColor = Meadow.Foundation.Color.FromHex("10485E");
        private Meadow.Foundation.Color selectedColor = Meadow.Foundation.Color.FromHex("C9DB31");
        private Meadow.Foundation.Color accentColor = Meadow.Foundation.Color.FromHex("EF7D3B");
        private Meadow.Foundation.Color ForegroundColor = Meadow.Foundation.Color.FromHex("EEEEEE");
        private Font12x20 font12X20 = new Font12x20();
        private Font8x12 font8x12 = new Font8x12();
        private Font6x8 font6x8 = new Font6x8();

        public ProjectLabDemoView(IGraphicsDisplay display)
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

        int margin = 5;
        int smallMargin = 3;


        int measureBoxWidth = 82;

        int column1x = 92;
        int column2x = 206;
        int columnWidth = 109;

        int rowHeight = 30;
        int row1x = 135;
        int row2x = 170;
        int row3x = 205;

        private void LoadDataLayout()
        {
            DataLayout = new AbsoluteLayout(DisplayScreen, 0, 0, DisplayScreen.Width, DisplayScreen.Height)
            {
                BackgroundColor = backgroundColor,
                Visible = false
            };

            DataLayout.Controls.Add(new Label(
                margin,
                7,
                166,
                16)
            {
                Text = $"Project Lab v3",
                TextColor = Meadow.Foundation.Color.White,
                Font = new Font8x16()
            });

            var wifiImage = Image.LoadFromResource("WinFormsMeadow.Resources.img_wifi_connecting.bmp");
            WifiStatus = new Picture(DisplayScreen.Width - wifiImage.Width - margin, 5, wifiImage.Width, 16, wifiImage)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            DataLayout.Controls.Add(WifiStatus);

            var syncImage = Image.LoadFromResource("WinFormsMeadow.Resources.img_refreshed.bmp");
            SyncStatus = new Picture(DisplayScreen.Width - syncImage.Width - wifiImage.Width - margin * 2, 5, syncImage.Width, 16, syncImage)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            DataLayout.Controls.Add(SyncStatus);

            LineChart = new LineChart(
                margin,
                24,
                310,
                106)
            {
                BackgroundColor = Meadow.Foundation.Color.FromHex("082936"),
                AxisColor = ForegroundColor,
                ShowYAxisLabels = true,
                Visible = false,
                AlwaysShowYOrigin = false,
            };
            LineChartSeries = new LineChartSeries()
            {
                LineColor = selectedColor,
                PointColor = selectedColor,
                LineStroke = 1,
                PointSize = 2,
                ShowLines = true,
                ShowPoints = true,
            };
            LineChart.Series.Add(LineChartSeries);
            DataLayout.Controls.Add(LineChart);

            #region TEMPERATURE
            TemperatureBox = new Box(margin, row1x, measureBoxWidth, rowHeight)
            {
                ForeColor = selectedColor
            };
            DataLayout.Controls.Add(TemperatureBox);
            TemperatureLabel = new Label(
                margin + smallMargin,
                row1x + smallMargin,
                measureBoxWidth - smallMargin * 2,
                font6x8.Height)
            {
                Text = $"TEMPERATURE",
                TextColor = backgroundColor,
                Font = font6x8
            };
            DataLayout.Controls.Add(TemperatureLabel);
            TemperatureValue = new Label(
                margin + smallMargin,
                row1x + font6x8.Height + smallMargin * 2,
                measureBoxWidth - smallMargin * 2,
                font6x8.Height * 2)
            {
                Text = $"-.-C",
                TextColor = backgroundColor,
                Font = font6x8,
                ScaleFactor = ScaleFactor.X2
            };
            DataLayout.Controls.Add(TemperatureValue);
            #endregion

            #region PRESSURE
            PressureBox = new Box(
                margin,
                row2x,
                measureBoxWidth,
                rowHeight)
            {
                ForeColor = backgroundColor
            };
            DataLayout.Controls.Add(PressureBox);
            PressureLabel = new Label(
                margin + smallMargin,
                row2x + smallMargin,
                measureBoxWidth - smallMargin * 2,
                font6x8.Height)
            {
                Text = $"PRESSURE",
                TextColor = ForegroundColor,
                Font = font6x8
            };
            DataLayout.Controls.Add(PressureLabel);
            PressureValue = new Label(
                margin + smallMargin,
                row2x + font6x8.Height + smallMargin * 2,
                measureBoxWidth - smallMargin * 2,
                font6x8.Height * 2)
            {
                Text = $"-.-atm",
                TextColor = ForegroundColor,
                Font = font6x8,
                ScaleFactor = ScaleFactor.X2
            };
            DataLayout.Controls.Add(PressureValue);
            #endregion

            #region HUMIDITY
            HumidityBox = new Box(
                margin,
                row3x,
                measureBoxWidth,
                rowHeight)
            {
                ForeColor = backgroundColor
            };
            DataLayout.Controls.Add(HumidityBox);
            HumidityLabel = new Label(
                margin + smallMargin,
                row3x + smallMargin,
                measureBoxWidth - smallMargin * 2,
                font6x8.Height)
            {
                Text = $"HUMIDITY",
                TextColor = ForegroundColor,
                Font = font6x8
            };
            DataLayout.Controls.Add(HumidityLabel);
            HumidityValue = new Label(
                margin + smallMargin,
                row3x + font6x8.Height + smallMargin * 2,
                columnWidth - smallMargin * 2,
                font6x8.Height * 2)
            {
                Text = $"-.-%",
                TextColor = ForegroundColor,
                Font = font6x8,
                ScaleFactor = ScaleFactor.X2
            };
            DataLayout.Controls.Add(HumidityValue);
            #endregion

            #region LUMINANCE
            LuminanceBox = new Box(
                column1x,
                row1x,
                columnWidth,
                rowHeight)
            {
                ForeColor = backgroundColor
            };
            DataLayout.Controls.Add(LuminanceBox);
            LuminanceLabel = new Label(
                column1x + smallMargin,
                row1x + smallMargin,
                columnWidth - smallMargin * 2,
                font6x8.Height)
            {
                Text = $"LUMINANCE",
                TextColor = ForegroundColor,
                Font = font6x8
            };
            DataLayout.Controls.Add(LuminanceLabel);
            LuminanceValue = new Label(
                column1x + smallMargin,
                row1x + font6x8.Height + smallMargin * 2,
                columnWidth - smallMargin * 2,
                font6x8.Height * 2)
            {
                Text = $"0Lux",
                TextColor = ForegroundColor,
                Font = font6x8,
                ScaleFactor = ScaleFactor.X2
            };
            DataLayout.Controls.Add(LuminanceValue);
            #endregion

            #region ACCELEROMETER
            DataLayout.Controls.Add(new Label(
                column1x + smallMargin,
                row2x + smallMargin,
                columnWidth - smallMargin * 2,
                font6x8.Height)
            {
                Text = $"ACCELEROMETER (g)",
                TextColor = Meadow.Foundation.Color.White,
                Font = font6x8
            });
            DataLayout.Controls.Add(new Label(
                column1x + smallMargin,
                184,
                font6x8.Width * 2,
                font6x8.Height * 2)
            {
                Text = $"X",
                TextColor = Meadow.Foundation.Color.White,
                Font = font6x8,
                ScaleFactor = ScaleFactor.X2
            });
            DataLayout.Controls.Add(new Label(
                column1x + smallMargin,
                202,
                font6x8.Width * 2,
                font6x8.Height * 2)
            {
                Text = $"Y",
                TextColor = Meadow.Foundation.Color.White,
                Font = font6x8,
                ScaleFactor = ScaleFactor.X2
            });
            DataLayout.Controls.Add(new Label(
                column1x + smallMargin,
                220,
                font6x8.Width * 2,
                font6x8.Height * 2)
            {
                Text = $"Z",
                TextColor = Meadow.Foundation.Color.White,
                Font = font6x8,
                ScaleFactor = ScaleFactor.X2
            });
            DataLayout.Controls.Add(new Box(
                108,
                184,
                92,
                14)
            {
                ForeColor = Meadow.Foundation.Color.FromHex("98A645")
            });
            DataLayout.Controls.Add(new Box(
                108,
                202,
                92,
                14)
            {
                ForeColor = Meadow.Foundation.Color.FromHex("C9DB31")
            });
            DataLayout.Controls.Add(new Box(
                108,
                220,
                92,
                14)
            {
                ForeColor = Meadow.Foundation.Color.FromHex("E1EB8B")
            });
            #endregion

            #region GYROSCOPE
            DataLayout.Controls.Add(new Label(
                column2x + smallMargin,
                row2x + smallMargin,
                columnWidth - smallMargin * 2,
                font6x8.Height)
            {
                Text = $"GYROSCOPE (rpm)",
                TextColor = Meadow.Foundation.Color.White,
                Font = font6x8
            });
            DataLayout.Controls.Add(new Label(
                column2x + smallMargin,
                184,
                font6x8.Width * 2,
                font6x8.Height * 2)
            {
                Text = $"X",
                TextColor = Meadow.Foundation.Color.White,
                Font = font6x8,
                ScaleFactor = ScaleFactor.X2
            });
            DataLayout.Controls.Add(new Label(
                column2x + smallMargin,
                202,
                font6x8.Width * 2,
                font6x8.Height * 2)
            {
                Text = $"Y",
                TextColor = Meadow.Foundation.Color.White,
                Font = font6x8,
                ScaleFactor = ScaleFactor.X2
            });
            DataLayout.Controls.Add(new Label(
                column2x + smallMargin,
                220,
                font6x8.Width * 2,
                font6x8.Height * 2)
            {
                Text = $"Z",
                TextColor = Meadow.Foundation.Color.White,
                Font = font6x8,
                ScaleFactor = ScaleFactor.X2
            });
            DataLayout.Controls.Add(new Box(
                222,
                184,
                92,
                14)
            {
                ForeColor = Meadow.Foundation.Color.FromHex("98A645")
            });
            DataLayout.Controls.Add(new Box(
                222,
                202,
                92,
                14)
            {
                ForeColor = Meadow.Foundation.Color.FromHex("C9DB31")
            });
            DataLayout.Controls.Add(new Box(
                222,
                220,
                92,
                14)
            {
                ForeColor = Meadow.Foundation.Color.FromHex("E1EB8B")
            });
            #endregion

            #region CLOCK
            DataLayout.Controls.Add(new Box(
                244,
                row1x,
                71,
                rowHeight)
            {
                ForeColor = ForegroundColor
            });
            Date = new Label(
                244,
                row1x + smallMargin,
                71,
                font6x8.Height)
            {
                Text = $"2023/11/20",
                TextColor = backgroundColor,
                HorizontalAlignment = HorizontalAlignment.Center,
                Font = font6x8
            };
            DataLayout.Controls.Add(Date);
            Time = new Label(
                244,
                row1x + font6x8.Height + smallMargin * 2,
                71,
                font6x8.Height * 2)
            {
                Text = $"12:12",
                TextColor = backgroundColor,
                HorizontalAlignment = HorizontalAlignment.Center,
                Font = font6x8,
                ScaleFactor = ScaleFactor.X2
            };
            DataLayout.Controls.Add(Time);
            #endregion

            #region D-PAD
            Up = new Box(
                218,
                136,
                9,
                9)
            {
                ForeColor = ForegroundColor
            };
            DataLayout.Controls.Add(Up);
            Down = new Box(
                218,
                156,
                9,
                9)
            {
                ForeColor = ForegroundColor
            };
            DataLayout.Controls.Add(Down);
            Left = new Box(
                208,
                146,
                9,
                9)
            {
                ForeColor = ForegroundColor
            };
            DataLayout.Controls.Add(Left);
            Right = new Box(
                228,
                146,
                9,
                9)
            {
                ForeColor = ForegroundColor
            };
            DataLayout.Controls.Add(Right);

            #endregion
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

        protected void UpdateReadings(double temperature, double pressure, double humidity, double luminance)
        {
            DisplayScreen.BeginUpdate();

            TemperatureValue.Text = $"{temperature:N1}C";
            PressureValue.Text = $"{pressure:N1}atm";
            HumidityValue.Text = $"{humidity:N1}%";
            LuminanceValue.Text = $"{luminance:N0}Lx";

            DisplayScreen.EndUpdate();
        }

        protected void UpdateDateTime()
        {
            DisplayScreen.BeginUpdate();

            var today = DateTime.Now;
            Date.Text = today.ToString("yyyy/MM/dd");
            Time.Text = today.ToString("HH:mm");

            DisplayScreen.EndUpdate();
        }

        protected void UpdateDirectionalPad(int direction)
        {
            DisplayScreen.BeginUpdate();

            Up.ForeColor = Down.ForeColor = Left.ForeColor = Right.ForeColor = ForegroundColor;

            switch (direction)
            {
                case 0: Up.ForeColor = accentColor; break;
                case 1: Down.ForeColor = accentColor; break;
                case 2: Left.ForeColor = accentColor; break;
                case 3: Right.ForeColor = accentColor; break;
            }

            DisplayScreen.EndUpdate();
        }

        protected void UpdateSelectReading(int reading)
        {
            TemperatureBox.ForeColor = PressureBox.ForeColor = HumidityBox.ForeColor = LuminanceBox.ForeColor = backgroundColor;
            TemperatureLabel.TextColor = PressureLabel.TextColor = HumidityLabel.TextColor = LuminanceLabel.TextColor = ForegroundColor;
            TemperatureValue.TextColor = PressureValue.TextColor = HumidityValue.TextColor = LuminanceValue.TextColor = ForegroundColor;

            switch (reading)
            {
                case 0:
                    TemperatureBox.ForeColor = selectedColor;
                    TemperatureLabel.TextColor = backgroundColor;
                    TemperatureValue.TextColor = backgroundColor;
                    break;
                case 1:
                    PressureBox.ForeColor = selectedColor;
                    PressureLabel.TextColor = backgroundColor;
                    PressureValue.TextColor = backgroundColor;
                    break;
                case 2:
                    HumidityBox.ForeColor = selectedColor;
                    HumidityLabel.TextColor = backgroundColor;
                    HumidityValue.TextColor = backgroundColor;
                    break;
                case 3:
                    LuminanceBox.ForeColor = selectedColor;
                    LuminanceLabel.TextColor = backgroundColor;
                    LuminanceValue.TextColor = backgroundColor;
                    break;
            }
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
                UpdateDateTime();

                UpdateReadings(
                    random.Next(25, 35),
                    random.Next(0, 2),
                    random.Next(75, 90),
                    random.Next(100, 4000)
                );

                UpdateDirectionalPad(random.Next(0, 5));

                UpdateSelectReading(random.Next(0, 4));

                this.LineChartSeries.Points.Add(x, random.Next(2, 8));
                x++;

                await Task.Delay(1000);
            }
        }
    }
}