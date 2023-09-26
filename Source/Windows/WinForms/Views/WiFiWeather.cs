using Meadow.Foundation.Graphics;
using SimpleJpegDecoder;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MF = Meadow.Foundation;

namespace WinFormsMeadow.Views
{
    public class WiFiWeather
    {
        MicroGraphics graphics;
        int x_padding = 5;

        MF.Color backgroundColor = MF.Color.FromHex("#F3F7FA");
        MF.Color foregroundColor = MF.Color.Black;

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

        public async Task Run()
        {
            var model = new WeatherViewModel()
            {
                WeatherCode = WeatherConstants.CLOUDS_CLEAR,
                OutdoorTemperature = 26,
                FeelsLikeTemperature = 28,
                Pressure = 1011,
                Humidity = 32,
                WindSpeed = 8,
                WindDirection = 300,
            };

            UpdateDisplay(model);

            while (true)
            {
                UpdateDateTime();
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public void UpdateDateTime()
        {
            var today = DateTime.Now;

            graphics.DrawRectangle(graphics.Width / 2, 24, graphics.Width, 82, backgroundColor, true);

            graphics.CurrentFont = font12X20;
            graphics.DrawText(
                x: graphics.Width - x_padding,
                y: 25,
                text: $"{today.DayOfWeek},{today.Day}{GetOrdinalSuffix(today.Day)}",
                color: foregroundColor,
                alignmentH: MF.Graphics.HorizontalAlignment.Right);
            graphics.DrawText(
                x: graphics.Width - x_padding,
                y: 50,
                text: today.ToString("MMM"),
                color: foregroundColor,
                scaleFactor: ScaleFactor.X2,
                alignmentH: MF.Graphics.HorizontalAlignment.Right);
            graphics.DrawText(
                x: graphics.Width - x_padding,
                y: 95,
                text: today.ToString("yyyy"),
                color: foregroundColor,
                alignmentH: MF.Graphics.HorizontalAlignment.Right);

            graphics.DrawRectangle(0, 135, graphics.Width, 35, backgroundColor, true);
            graphics.DrawText(
                x: graphics.Width / 2,
                y: 135,
                text: today.ToString("hh:mm:ss tt"),
                color: foregroundColor, ScaleFactor.X2,
                alignmentH: MF.Graphics.HorizontalAlignment.Center);

            graphics.Show();
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

        public void UpdateDisplay(WeatherViewModel model)
        {
            int spacing = 95;
            int valueSpacing = 30;
            int y = 200;

            graphics.Clear(backgroundColor);

            DisplayJPG(model.WeatherCode, x_padding, 15);

            graphics.CurrentFont = font12X20;
            graphics.DrawText(x_padding, y, "Temperature", foregroundColor);
            graphics.DrawText(x_padding, y + spacing, "Humidity", foregroundColor);
            graphics.DrawText(x_padding, y + spacing * 2, "Pressure", foregroundColor);
            graphics.DrawText(graphics.Width - x_padding, y, "Feels like", foregroundColor, alignmentH: MF.Graphics.HorizontalAlignment.Right);
            graphics.DrawText(graphics.Width - x_padding, y + spacing, "Wind Dir", foregroundColor, alignmentH: MF.Graphics.HorizontalAlignment.Right);
            graphics.DrawText(graphics.Width - x_padding, y + spacing * 2, "Wind Spd", foregroundColor, alignmentH: MF.Graphics.HorizontalAlignment.Right);

            graphics.DrawText(x_padding, y + valueSpacing, $"{model.OutdoorTemperature}°C", foregroundColor, ScaleFactor.X2);
            graphics.DrawText(graphics.Width - x_padding, y + valueSpacing, $"{model.FeelsLikeTemperature + 2}°C", foregroundColor, ScaleFactor.X2, alignmentH: MF.Graphics.HorizontalAlignment.Right);
            graphics.DrawText(x_padding, y + valueSpacing + spacing, $"{model.Humidity}%", foregroundColor, ScaleFactor.X2);
            graphics.DrawText(graphics.Width - x_padding, y + valueSpacing + spacing, $"{model.WindDirection}°", foregroundColor, ScaleFactor.X2, alignmentH: MF.Graphics.HorizontalAlignment.Right);

            graphics.CurrentFont = font8X16;
            graphics.DrawText(graphics.Width - x_padding, y + valueSpacing + spacing * 2, $"{model.WindSpeed}m/s", foregroundColor, ScaleFactor.X2, alignmentH: MF.Graphics.HorizontalAlignment.Right);
            graphics.DrawText(x_padding, y + valueSpacing + spacing * 2, $"{model.Pressure}hPa", foregroundColor, ScaleFactor.X2);

            graphics.Show();
        }

        void DisplayJPG(int weatherCode, int xOffset, int yOffset)
        {
            var jpgData = LoadResource(weatherCode);
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

                graphics.DrawPixel(x + xOffset, y + yOffset, MF.Color.FromRgb(r, g, b));

                x++;
                if (x % decoder.Width == 0)
                {
                    y++;
                    x = 0;
                }
            }
        }
        byte[] LoadResource(int weatherCode)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName;

            switch (weatherCode)
            {
                case int n when (n >= WeatherConstants.THUNDERSTORM_LIGHT_RAIN && n <= WeatherConstants.THUNDERSTORM_HEAVY_DRIZZLE):
                    resourceName = $"WinFormsMeadow.w_storm.jpg";
                    break;
                case int n when (n >= WeatherConstants.DRIZZLE_LIGHT && n <= WeatherConstants.DRIZZLE_SHOWER):
                    resourceName = $"WinFormsMeadow.w_drizzle.jpg";
                    break;
                case int n when (n >= WeatherConstants.RAIN_LIGHT && n <= WeatherConstants.RAIN_SHOWER_RAGGED):
                    resourceName = $"WinFormsMeadow.w_rain.jpg";
                    break;
                case int n when (n >= WeatherConstants.SNOW_LIGHT && n <= WeatherConstants.SNOW_SHOWER_HEAVY):
                    resourceName = $"WinFormsMeadow.w_snow.jpg";
                    break;
                case WeatherConstants.CLOUDS_CLEAR:
                    resourceName = $"WinFormsMeadow.w_clear.jpg";
                    break;
                case int n when (n >= WeatherConstants.CLOUDS_FEW && n <= WeatherConstants.CLOUDS_OVERCAST):
                    resourceName = $"WinFormsMeadow.w_cloudy.jpg";
                    break;
                default:
                    resourceName = $"WinFormsMeadow.w_misc.jpg";
                    break;
            }

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }
    }

    public class WeatherViewModel
    {
        public int WeatherCode { get; set; }

        public int OutdoorTemperature { get; set; }

        public int FeelsLikeTemperature { get; set; }

        public int Pressure { get; set; }

        public int Humidity { get; set; }

        public decimal WindSpeed { get; set; }

        public int WindDirection { get; set; }
    }

    public sealed class WeatherConstants
    {
        public const int THUNDERSTORM_LIGHT_RAIN = 200;
        public const int THUNDERSTORM_RAIN = 201;
        public const int THUNDERSTORM_HEAVY_RAIN = 202;
        public const int THUNDERSTORM_LIGHT = 210;
        public const int THUNDERSTORM = 211;
        public const int THUNDERSTORM_HEAVY = 212;
        public const int THUNDERSTORM_RAGGED = 221;
        public const int THUNDERSTORM_LIGHT_DRIZZLE = 230;
        public const int THUNDERSTORM_DRIZZLE = 231;
        public const int THUNDERSTORM_HEAVY_DRIZZLE = 232;

        public const int DRIZZLE_LIGHT = 300;
        public const int DRIZZLE = 301;
        public const int DRIZZLE_HEAVY = 302;
        public const int DRIZZLE_LIGHT_RAIN = 310;
        public const int DRIZZLE_RAIN = 311;
        public const int DRIZZLE_HEAVY_RAIN = 312;
        public const int DRIZZLE_SHOWER_RAIN = 313;
        public const int DRIZZLE_SHOWER_HEAVY = 314;
        public const int DRIZZLE_SHOWER = 321;

        public const int RAIN_LIGHT = 500;
        public const int RAIN_MODERATE = 501;
        public const int RAIN_HEAVY = 502;
        public const int RAIN_VERY_HEAVY = 503;
        public const int RAIN_EXTREME = 504;
        public const int RAIN_FREEZING = 511;
        public const int RAIN_SHOWER_LIGHT = 520;
        public const int RAIN_SHOWER = 521;
        public const int RAIN_SHOWER_HEAVY = 522;
        public const int RAIN_SHOWER_RAGGED = 531;

        public const int SNOW_LIGHT = 600;
        public const int SNOW = 601;
        public const int SNOW_HEAVY = 602;
        public const int SLEET = 611;
        public const int SNOW_SHOWER_SLEET_LIGHT = 612;
        public const int SNOW_SHOWER_SLEET = 613;
        public const int SNOW_RAIN_LIGHT = 615;
        public const int SNOW_RAIN = 621;
        public const int SNOW_SHOWER_LIGHT = 622;
        public const int SNOW_SHOWER = 631;
        public const int SNOW_SHOWER_HEAVY = 631;

        public const int MIST = 701;
        public const int SMOKE = 711;
        public const int HAZE = 721;
        public const int DUST_SAND = 731;
        public const int FOG = 741;
        public const int SAND = 751;
        public const int DUST = 761;
        public const int ASH = 762;
        public const int SQUALL = 771;
        public const int TORNADO = 781;

        public const int CLOUDS_CLEAR = 800;
        public const int CLOUDS_FEW = 801;
        public const int CLOUDS_SCATTERED = 802;
        public const int CLOUDS_BROKEN = 803;
        public const int CLOUDS_OVERCAST = 804;

        private static readonly WeatherConstants instance = new WeatherConstants();

        static WeatherConstants() { }

        public static WeatherConstants Instance
        {
            get
            {
                return instance;
            }
        }
    }
}