using Meadow.Foundation.Graphics;
using SimpleJpegDecoder;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace WinFormsMeadow.Views
{
    public class WiFiWeather
    {
        MicroGraphics graphics;
        int x_padding = 5;

        Meadow.Foundation.Color backgroundColor = Meadow.Foundation.Color.FromHex("#F3F7FA");
        Meadow.Foundation.Color foregroundColor = Meadow.Foundation.Color.Black;

        Font12x20 font12X20 = new Font12x20();
        Font8x16 font8X16 = new Font8x16();

        public WiFiWeather(IGraphicsDisplay display) 
        {
            graphics = new MicroGraphics(display)
            {
                Stroke = 1,
                CurrentFont = font12X20
            };

            x_padding = 20;

            graphics.Clear(backgroundColor);
        }

        private static string GetOrdinalSuffix(int num)
        {
            string number = num.ToString();
            if (number.EndsWith("11")) return "th";
            if (number.EndsWith("12")) return "th";
            if (number.EndsWith("13")) return "th";
            if (number.EndsWith("1")) return "st";
            if (number.EndsWith("2")) return "nd";
            if (number.EndsWith("3")) return "rd";
            return "th";
        }

        private void DisplayJPG(string weatherIcon, int xOffset, int yOffset)
        {
            var jpgData = LoadResource(weatherIcon);
            var decoder = new JpegDecoder();
            var jpg = decoder.DecodeJpeg(jpgData);

            int x = 0;
            int y = 0;
            byte r, g, b;

            for (int i = 0; i < jpg.Length; i += 3)
            {
                r = jpg[i];
                g = jpg[i + 1];
                b = jpg[i + 2];

                graphics.DrawPixel(x + xOffset, y + yOffset, Meadow.Foundation.Color.FromRgb(r, g, b));

                x++;
                if (x % decoder.Width == 0)
                {
                    y++;
                    x = 0;
                }
            }
        }

        private byte[] LoadResource(string weatherIcon)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(weatherIcon))
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        public void UpdateDateTime()
        {
            var today = DateTime.Now;

            graphics.DrawRectangle(graphics.Width / 2, 24, graphics.Width, 82, backgroundColor, true);

            graphics.CurrentFont = font12X20;
            graphics.DrawText(graphics.Width - x_padding, 25, $"{today.DayOfWeek},{today.Day}{GetOrdinalSuffix(today.Day)}", foregroundColor, alignmentH: HorizontalAlignment.Right);
            graphics.DrawText(graphics.Width - x_padding, 50, today.ToString("MMM"), foregroundColor, scaleFactor: ScaleFactor.X2, alignmentH: HorizontalAlignment.Right);
            graphics.DrawText(graphics.Width - x_padding, 95, today.ToString("yyyy"), foregroundColor, alignmentH: HorizontalAlignment.Right);

            graphics.DrawRectangle(0, 135, graphics.Width, 35, backgroundColor, true);
            graphics.DrawText(graphics.Width / 2, 135, today.ToString("hh:mm:ss tt"), foregroundColor, ScaleFactor.X2, alignmentH: HorizontalAlignment.Center);

            graphics.Show();
        }

        public void UpdateDisplay(string weatherIcon, string temperature, string humidity, string pressure, string feelsLike, string windDirection, string windSpeed)
        {
            int spacing = 95;
            int valueSpacing = 30;
            int y = 200;

            graphics.Clear(backgroundColor);

            DisplayJPG(weatherIcon, x_padding, 15);

            graphics.CurrentFont = font12X20;
            graphics.DrawText(x_padding, y, "Temperature", foregroundColor);
            graphics.DrawText(x_padding, y + spacing, "Humidity", foregroundColor);
            graphics.DrawText(x_padding, y + spacing * 2, "Pressure", foregroundColor);
            graphics.DrawText(graphics.Width - x_padding, y, "Feels like", foregroundColor, alignmentH: HorizontalAlignment.Right);
            graphics.DrawText(graphics.Width - x_padding, y + spacing, "Wind Dir", foregroundColor, alignmentH: HorizontalAlignment.Right);
            graphics.DrawText(graphics.Width - x_padding, y + spacing * 2, "Wind Spd", foregroundColor, alignmentH: HorizontalAlignment.Right);

            graphics.DrawText(x_padding, y + valueSpacing, $"{temperature}", foregroundColor, ScaleFactor.X2);
            graphics.DrawText(graphics.Width - x_padding, y + valueSpacing, $"{feelsLike}", foregroundColor, ScaleFactor.X2, alignmentH: HorizontalAlignment.Right);
            graphics.DrawText(x_padding, y + valueSpacing + spacing, $"{humidity}", foregroundColor, ScaleFactor.X2);
            graphics.DrawText(graphics.Width - x_padding, y + valueSpacing + spacing, $"{windDirection}", foregroundColor, ScaleFactor.X2, alignmentH: HorizontalAlignment.Right);

            graphics.CurrentFont = font8X16;
            graphics.DrawText(graphics.Width - x_padding, y + valueSpacing + spacing * 2, $"{windSpeed}", foregroundColor, ScaleFactor.X2, alignmentH: HorizontalAlignment.Right);
            graphics.DrawText(x_padding, y + valueSpacing + spacing * 2, $"{pressure}", foregroundColor, ScaleFactor.X2);

            graphics.Show();
        }

        public async Task Run()
        {
            UpdateDisplay(
                weatherIcon: $"WinFormsMeadow.w_clear.jpg",
                temperature: $"23°C",
                humidity: $"93%",
                pressure: $"1102hPa",
                feelsLike: $"26°C",
                windDirection: $"178°",
                windSpeed: $"19m/s");

            while (true)
            {
                UpdateDateTime();
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}