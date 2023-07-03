using Meadow;
using Meadow.Devices;
using Meadow.Pinouts;

public class MeadowApp : App<Linux<RaspberryPi>>
{
    static async Task Main(string[] args)
    {
        await MeadowOS.Start(args);
    }

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        return base.Initialize();
    }

    public override Task Run()
    {
        Resolver.Log.Info("Run...");

        Resolver.Log.Info("Hello, Meadow.Linux!");

        return base.Run();
    }
}