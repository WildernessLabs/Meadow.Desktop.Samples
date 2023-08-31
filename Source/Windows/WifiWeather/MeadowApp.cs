using Meadow;
using Meadow.Foundation.Displays;
using Meadow.Foundation.ICs.IOExpanders;
using WifiWeather.Services;
using WifiWeather.ViewModels;
using WifiWeather.Views;

public class MeadowApp : App<Windows>
{
    private DisplayView _displayController;

    public override Task Initialize()
    {
        Console.WriteLine("Creating Outputs");

        var expander = new Ft232h();

        var display = new Ili9488
        (
            spiBus: expander.CreateSpiBus(),
            chipSelectPin: expander.Pins.C0,
            dcPin: expander.Pins.C2,
            resetPin: expander.Pins.C1
        );

        _displayController = new DisplayView();
        _displayController.Initialize(display);

        return Task.CompletedTask;
    }

    async Task GetTemperature()
    {
        // Get outdoor conditions
        var outdoorConditions = await WeatherService.GetWeatherForecast();

        // Format indoor/outdoor conditions data
        var model = new WeatherViewModel(outdoorConditions);

        // Send formatted data to display to render
        _displayController.UpdateDisplay(model);
    }

    public override async Task Run()
    {
        await GetTemperature();

        while (true)
        {
            if (DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)
            {
                await GetTemperature();
            }

            _displayController.UpdateDateTime();
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    public static async Task Main(string[] args)
    {
        await MeadowOS.Start(args);
    }
}