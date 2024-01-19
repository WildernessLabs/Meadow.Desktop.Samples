using Meadow;
using Meadow.Cloud;
using Meadow.Logging;
using Meadow.Pinouts;

public class MyCommand : IMeadowCommand
{
    public bool Enabled { get; set; }
    public string? DisplayText { get; set; }
}

public class MeadowApp : App<Linux<WSL2>>
{
    public static async Task Main(string[] args)
    {
        await MeadowOS.Start(args);
    }

    private DateTimeOffset _startTime;

    public override Task Initialize()
    {
        _startTime = DateTimeOffset.UtcNow;

        Resolver.Log.AddProvider(new DebugLogProvider());

        Resolver.CommandService?.Subscribe<MyCommand>(HandleMyCommandReceived);

        return base.Initialize();
    }

    private void OnCloudServiceError(object? sender, string e)
    {
        Resolver.Log.Info($"CLOUD ERROR: {e}");
    }

    private void HandleMyCommandReceived(MyCommand command)
    {
        TickEnabled = command.Enabled;

        if (command.DisplayText != null && command.DisplayText.Length > 0)
        {
            Resolver.Log.Info($"{Environment.NewLine}{command.DisplayText}");
            Tick = 1;
        }
    }

    private int Tick { get; set; }
    private bool TickEnabled { get; set; } = true;

    public override async Task Run()
    {
        Tick = 1;

        while (true)
        {
            await Task.Delay(1000);

            if (TickEnabled)
            {
                Console.Write(".");

                if (Tick++ % 10 == 0)
                {
                    Console.WriteLine("");
                }
            }
        }
    }
}