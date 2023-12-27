using Meadow;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Graphics.MicroLayout;
using System;
using System.Threading.Tasks;

namespace WinFormsMeadow.Views
{
    public class HomeWidget
    {
        Image _weatherIcon = Image.LoadFromResource("WinFormsMeadow.Resources.w_clear.bmp");

        int x_padding = 10;

        protected DisplayScreen DisplayScreen { get; set; }

        protected Label DayOfWeek { get; set; }

        protected Label Month { get; set; }

        protected Label Year { get; set; }

        protected Label Time { get; set; }

        protected Picture Weather { get; set; }

        protected Label Temperature { get; set; }

        protected Label Humidity { get; set; }

        protected Label Pressure { get; set; }

        protected Label FeelsLike { get; set; }

        protected Label WindDirection { get; set; }

        protected Label WindSpeed { get; set; }

        Meadow.Color backgroundColor = Meadow.Color.FromHex("#F3F7FA");
        Meadow.Color foregroundColor = Meadow.Color.Black;

        Font12x20 font12X20 = new Font12x20();
        Font12x16 font12X16 = new Font12x16();
        Font8x16 font8X16 = new Font8x16();

        public HomeWidget(IGraphicsDisplay display)
        {
            DisplayScreen = new DisplayScreen(display)
            {
                BackgroundColor = backgroundColor
            };

            Weather = new Picture(x_padding, 10, 100, 100, _weatherIcon);
            DisplayScreen.Controls.Add(Weather);

            DisplayScreen.Controls.Add(new Label(120, 10, 170, font12X20.Height)
            {
                Text = $"2023 December",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right
            });

            DisplayScreen.Controls.Add(new Label(120, 40, 170, font12X20.Height)
            {
                Text = $"Monday 25th",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right
            });

            DisplayScreen.Controls.Add(new Label(120, 70, 170, font12X20.Height * 2)
            {
                Text = $"12:00",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                ScaleFactor = ScaleFactor.X2,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right
            });

            DisplayScreen.Controls.Add(new Label(x_padding, 120, 135, font12X20.Height)
            {
                Text = $"TEMPERATURE",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            });

            DisplayScreen.Controls.Add(new Label(155, 120, 135, font12X20.Height)
            {
                Text = $"HUMIDITY",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right
            });

            DisplayScreen.Controls.Add(new Label(x_padding, 150, 135, font12X20.Height * 2)
            {
                Text = $"25.4C",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                ScaleFactor = ScaleFactor.X2,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center
            });

            DisplayScreen.Controls.Add(new Label(155, 150, 135, font12X20.Height * 2)
            {
                Text = $"75%",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                ScaleFactor = ScaleFactor.X2,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right
            });

            DisplayScreen.Controls.Add(new Box(10, 204, 280, 1)
            {
                ForeColor = Color.Black
            });

            DisplayScreen.Controls.Add(new Label(x_padding, 220, DisplayScreen.Width / 2, font12X20.Height)
            {
                Text = $"UPCOMING WEEK (#2):",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            });

            DisplayScreen.Controls.Add(new Label(x_padding, 250, DisplayScreen.Width / 2, font12X20.Height)
            {
                Text = $"- Japan Curry + Rice",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            });

            DisplayScreen.Controls.Add(new Label(x_padding, 280, DisplayScreen.Width / 2, font12X20.Height)
            {
                Text = $"- Baked Pasta",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            });

            DisplayScreen.Controls.Add(new Label(x_padding, 310, DisplayScreen.Width / 2, font12X20.Height)
            {
                Text = $"WEEK AFTER (#3):",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            });

            DisplayScreen.Controls.Add(new Label(x_padding, 340, DisplayScreen.Width / 2, font12X20.Height)
            {
                Text = $"- Japan Curry + Rice",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            });

            DisplayScreen.Controls.Add(new Label(x_padding, 370, DisplayScreen.Width / 2, font12X20.Height)
            {
                Text = $"- Baked Pasta",
                TextColor = foregroundColor,
                BackColor = backgroundColor,
                Font = font12X20,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            });
        }

        private static string GetOrdinalSuffix(int num)
        {
            string number = num.ToString();
            if (number.EndsWith("1")) return "st";
            if (number.EndsWith("2")) return "nd";
            if (number.EndsWith("3")) return "rd";
            if (number.EndsWith("11")) return "th";
            if (number.EndsWith("12")) return "th";
            if (number.EndsWith("13")) return "th";
            return "th";
        }

        public void UpdateDisplay(string weatherIcon, string temperature, string humidity, string pressure, string feelsLike, string windDirection, string windSpeed)
        {
            _weatherIcon = Image.LoadFromResource(weatherIcon);
            Weather.Image = _weatherIcon;

            //Temperature.Text = temperature;
            //Humidity.Text = humidity;
            //Pressure.Text = pressure;
            //FeelsLike.Text = feelsLike;
            //WindDirection.Text = windDirection;
            //WindSpeed.Text = windSpeed;
        }

        public async Task Run()
        {
            UpdateDisplay(
                weatherIcon: $"WinFormsMeadow.Resources.w_drizzle.bmp",
                temperature: $"23°C",
                humidity: $"93%",
                pressure: $"1102hPa",
                feelsLike: $"26°C",
                windDirection: $"178°",
                windSpeed: $"19m/s");

            while (true)
            {
                //var today = DateTime.Now;

                //DayOfWeek.Text = $"{today.DayOfWeek},{today.Day}{GetOrdinalSuffix(today.Day)}";
                //Month.Text = $"{today.ToString("MMMM").Substring(0, today.ToString("MMMM").Length > 6 ? 7 : today.ToString("MMMM").Length)}";
                //Year.Text = $"{today.ToString("yyyy")}";
                //Time.Text = DateTime.Now.ToString("hh:mm:ss tt");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}