using Meadow;
using Meadow.Foundation.Displays.Lcd;
using Meadow.Foundation.ICs.IOExpanders;

public class MeadowApp : App<Windows>
{
    private Ft232h? _expander;
    private CharacterDisplay? display;

    public override Task Initialize()
    {
        Console.WriteLine("Creating Outputs");

        _expander = new Ft232h();

        display = new CharacterDisplay
            (
                pinRS: _expander.Pins.C5,
                pinE: _expander.Pins.C4,
                pinD4: _expander.Pins.C3,
                pinD5: _expander.Pins.C2,
                pinD6: _expander.Pins.C1,
                pinD7: _expander.Pins.C0,
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
        display?.WriteLine($"Meadow.Windows ", 1);

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