using Meadow;
using Meadow.Hardware;
using Meadow.Pinouts;
using Meadow.Foundation.Displays.Lcd;

public class MeadowApp : App<Linux<RaspberryPi>>
{
    private CharacterDisplay? display;

    public override Task Initialize()
    {
        Console.WriteLine("Creating Outputs");

        display = new CharacterDisplay
            (
                pinRS: Device.Pins.GPIO27,
                pinE:  Device.Pins.GPIO27,
                pinD4: Device.Pins.GPIO27,
                pinD5: Device.Pins.GPIO27,
                pinD6: Device.Pins.GPIO27,
                pinD7: Device.Pins.GPIO27,
                rows: 4, columns: 20
            );

        display.Write("Hello Meadow!");

        return Task.CompletedTask;
    }

    void UpdateCountdown()
    {
        var today = DateTime.Now;

        display?.WriteLine($"{today.ToString("MMMM dd, yyyy")}", 2);
        display?.WriteLine($"{today.ToString("hh:mm:ss tt")}", 3);
    }

    public override Task Run()
    {
        display?.WriteLine($"Wilderness Labs", 0);
        display?.WriteLine($"Meadow.Linux ", 1);

        while (true)
        {
            UpdateCountdown();
            Thread.Sleep(1000);
        }
    }

    public static async Task Main(string[] args)
    {
        await MeadowOS.Start(args);
    }
}