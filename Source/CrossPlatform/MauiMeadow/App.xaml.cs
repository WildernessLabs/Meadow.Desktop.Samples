using Meadow;
using Meadow.Foundation.Displays;
using Meadow.Foundation.ICs.IOExpanders;
using Meadow.Peripherals.Displays;
using Meadow.UI;

namespace MauiMeadow
{
    public partial class App : MauiMeadowApplication<Meadow.Windows>
    {
        public App()
        {
            InitializeComponent();
            LoadMeadowOS();

            MeadowInitialize();

            MainPage = new AppShell();
        }

        protected Task MeadowInitialize()
        {
            var expander = FtdiExpanderCollection.Devices[0];

            var display = new Gc9a01
            (
                spiBus: expander.CreateSpiBus(),
                chipSelectPin: expander.Pins.C0,
                dcPin: expander.Pins.C1,
                resetPin: expander.Pins.C2
            );

            Resolver.Services.Add<IPixelDisplay>(display);

            return Task.CompletedTask;
        }
    }
}