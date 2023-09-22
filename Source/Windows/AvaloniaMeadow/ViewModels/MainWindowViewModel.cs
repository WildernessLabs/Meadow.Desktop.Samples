using Meadow;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Sensors.Atmospheric;
using Meadow.Peripherals.Leds;
using Meadow.Peripherals.Sensors;
using Meadow.Units;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaMeadow.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ILed _led;
        private Bme680 _bme680;

        private string _temperatureValue;
        public string TemperatureValue
        {
            get => _temperatureValue;
            set => this.RaiseAndSetIfChanged(ref _temperatureValue, value);
        }

        private string _humidityValue;
        public string HumidityValue
        {
            get => _humidityValue;
            set => this.RaiseAndSetIfChanged(ref _humidityValue, value);
        }

        private string _pressureValue;
        public string PressureValue
        {
            get => _pressureValue;
            set => this.RaiseAndSetIfChanged(ref _pressureValue, value);
        }

        public MainWindowViewModel()
        {
            TemperatureValue = "0°C";
            HumidityValue = "0%";
            PressureValue = "0atm";

            // since Avalonia and Meadow are both starting at the same time, we must wait
            // for MeadowInitialize to complete before the output port is ready
            //_ = Task.Run(WaitForHardware);

            Simulate();
        }

        private async Task WaitForHardware()
        {
            while (_led == null || _bme680 == null)
            {
                _bme680 = Resolver.Services.Get<Bme680>();
                _led = Resolver.Services.Get<ILed>();
                await Task.Delay(100);
            }

            _bme680.Updated += Bme680Updated;
            _bme680.StartUpdating();
        }

        void Simulate() 
        {
            Task.Run(() =>
            {
                var random = new Random();

                while (true)
                {
                    //_led.IsOn = true;

                    Thread.Sleep(1000);

                    TemperatureValue = $"{random.Next(25, 27)}°C";
                    HumidityValue = $"{random.Next(92, 95)}%";
                    PressureValue = $"1.{random.Next(10, 14)}atm";

                    //_led.IsOn = false;
                }
            });
        }

        private void Bme680Updated(object? sender, IChangeResult<(Temperature? Temperature, RelativeHumidity? Humidity, Pressure? Pressure, Resistance? GasResistance)> e)
        {
            _led.IsOn = true;

            Console.WriteLine($"Temperature: {e.New.Temperature.Value.Celsius}, Humidity: {e.New.Humidity.Value.Percent}");

            _led.IsOn = false;
        }
    }
}