using Meadow;
using Meadow.Cloud;
using Meadow.Logging;
using Meadow.Pinouts;

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
        Resolver.MeadowCloudService.ServiceError += OnCloudServiceError;

        return base.Initialize();
    }

    private void OnCloudServiceError(object? sender, string e)
    {
        Console.WriteLine($"CLOUD ERROR: {e}");
    }

    public override async Task Run()
    {
        var eventNumber = 20;

        while (true)
        {
            var @event = new CloudEvent
            {
                Measurements = new()
                 {
                     { "uptime", DateTime.UtcNow - _startTime },
                     { "eventNumber", eventNumber++ },
                     { "text", "foo bar" }
                 }
            };

            try
            {
                Console.WriteLine($"Sending event to Meadow.Cloud...");
                await Resolver.MeadowCloudService.SendEvent(@event);
            }
            catch (Exception ex)
            {
                Resolver.Log.Error($"ERROR: {ex.Message}");
            }

            await Task.Delay(10000);
        }
    }
}