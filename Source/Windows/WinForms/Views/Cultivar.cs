using Meadow.Foundation.Graphics;
using Meadow.Foundation.Graphics.MicroLayout;
using System;
using System.Threading.Tasks;

namespace WinFormsMeadow.Views
{
    public class Cultivar
    {
        DisplayScreen screen;

        Image imgRed = Image.LoadFromResource("WinFormsMeadow.img-red.bmp");
        Image imgGreen = Image.LoadFromResource("WinFormsMeadow.img-green.bmp");

        protected Label TemperatureLabel { get; set; }

        protected Label HumidityLabel { get; set; }

        protected Label SoilMoistureLabel { get; set; }

        protected Picture ledLights { get; set; }

        protected Picture ledWater { get; set; }

        protected Picture ledVents { get; set; }

        protected Picture ledHeater { get; set; }

        public Cultivar(IGraphicsDisplay _display)
        {
            screen = new DisplayScreen(_display);

            screen.Controls.Add(new Box(0, 0, screen.Width, screen.Height) { ForeColor = Meadow.Foundation.Color.White });
            screen.Controls.Add(new Box(0, 27, 106, 93) { ForeColor = Meadow.Foundation.Color.FromHex("#B35E2C") });
            screen.Controls.Add(new Box(106, 27, 108, 93) { ForeColor = Meadow.Foundation.Color.FromHex("#1A80AA") });
            screen.Controls.Add(new Box(214, 27, 106, 93) { ForeColor = Meadow.Foundation.Color.FromHex("#98A645") });

            screen.Controls.Add(new Box(160, 120, 1, screen.Height) { ForeColor = Meadow.Foundation.Color.Black, Filled = false });
            screen.Controls.Add(new Box(0, 180, screen.Width, 1) { ForeColor = Meadow.Foundation.Color.Black, Filled = false });

            screen.Controls.Add(new Label(5, 32, 12, 16)
            {
                Text = "TEMP.",
                Font = new Font12x16(),
                TextColor = Meadow.Foundation.Color.White
            });
            screen.Controls.Add(new Label(77, 99, 12, 16)
            {
                Text = "°C",
                Font = new Font12x20(),
                TextColor = Meadow.Foundation.Color.White
            });

            screen.Controls.Add(new Label(111, 32, 12, 16)
            {
                Text = "HUM.",
                Font = new Font12x16(),
                TextColor = Meadow.Foundation.Color.White
            });
            screen.Controls.Add(new Label(197, 99, 12, 16)
            {
                Text = "%",
                Font = new Font12x20(),
                TextColor = Meadow.Foundation.Color.White
            });

            screen.Controls.Add(new Label(219, 32, 12, 16)
            {
                Text = "S.M.",
                Font = new Font12x16(),
                TextColor = Meadow.Foundation.Color.White
            });
            screen.Controls.Add(new Label(303, 99, 12, 16)
            {
                Text = "%",
                Font = new Font12x20(),
                TextColor = Meadow.Foundation.Color.White
            });

            TemperatureLabel = new Label(50, 70, 12, 16, ScaleFactor.X2)
            {
                Text = "31",
                Font = new Font12x16(),
                TextColor = Meadow.Foundation.Color.White,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            screen.Controls.Add(TemperatureLabel);
            HumidityLabel = new Label(155, 70, 12, 16, ScaleFactor.X2)
            {
                Text = "33",
                Font = new Font12x16(),
                TextColor = Meadow.Foundation.Color.White,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            screen.Controls.Add(HumidityLabel);
            SoilMoistureLabel = new Label(260, 70, 12, 16, ScaleFactor.X2)
            {
                Text = "23",
                Font = new Font12x16(),
                TextColor = Meadow.Foundation.Color.White,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            screen.Controls.Add(SoilMoistureLabel);


            ledLights = new Picture(8, 128, 46, 46, imgGreen)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            screen.Controls.Add(ledLights);
            screen.Controls.Add(new Label(60, 145, 12, 16, ScaleFactor.X2)
            {
                Text = "Lights",
                Font = new Font8x12(),
                TextColor = Meadow.Foundation.Color.Black
            });

            ledWater = new Picture(168, 128, 46, 46, imgGreen)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            screen.Controls.Add(ledWater);
            screen.Controls.Add(new Label(60, 205, 12, 16, ScaleFactor.X2)
            {
                Text = "Water",
                Font = new Font8x12(),
                TextColor = Meadow.Foundation.Color.Black
            });

            ledVents = new Picture(8, 188, 46, 46, imgGreen)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            screen.Controls.Add(ledVents);
            screen.Controls.Add(new Label(220, 145, 12, 16, ScaleFactor.X2)
            {
                Text = "Vents",
                Font = new Font8x12(),
                TextColor = Meadow.Foundation.Color.Black
            });

            ledHeater = new Picture(168, 188, 46, 46, imgGreen)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            screen.Controls.Add(ledHeater);
            screen.Controls.Add(new Label(220, 205, 12, 16, ScaleFactor.X2)
            {
                Text = "Heater",
                Font = new Font8x12(),
                TextColor = Meadow.Foundation.Color.Black
            });
        }

        protected void UpdateTemperature(double temp)
        {
            TemperatureLabel.Text = temp.ToString("N0");
        }

        protected void UpdateHumidity(double humidity)
        {
            HumidityLabel.Text = humidity.ToString("N0");
        }

        protected void UpdateSoilMoisture(double moisture)
        {
            SoilMoistureLabel.Text = moisture.ToString("N0");
        }

        protected void UpdateLights(bool on)
        {
            ledLights.Image = on ? imgGreen : imgRed;
        }

        protected void UpdateHeater(bool on)
        {
            ledHeater.Image = on ? imgGreen : imgRed;
        }

        protected void UpdateWater(bool on)
        {
            ledWater.Image = on ? imgGreen : imgRed;
        }

        protected void UpdateVents(bool on)
        {
            ledVents.Image = on ? imgGreen : imgRed;
        }

        public async Task Run()
        {
            while (true)
            {
                var random = new Random();

                UpdateTemperature(random.Next(20, 25));
                //UpdateHumidity(random.Next(30, 35));
                //UpdateSoilMoisture(random.Next(40, 45));

                await Task.Delay(1000);
            }
        }
    }
}