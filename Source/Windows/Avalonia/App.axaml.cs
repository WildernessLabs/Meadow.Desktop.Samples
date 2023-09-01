using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaMeadow.ViewModels;
using AvaloniaMeadow.Views;
using Meadow;
using Meadow.Foundation.Displays;
using Meadow.Foundation.ICs.IOExpanders;
using Meadow.UI;
using System.Threading.Tasks;

namespace AvaloniaMeadow
{
    public partial class App : AvaloniaMeadowApplication<Windows>
    {
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            LoadMeadowOS();
        }

        public override Task MeadowInitialize()
        {
            var expander = new Ft232h();

            var display = new Gc9a01
            (
                spiBus: expander.CreateSpiBus(),
                chipSelectPin: expander.Pins.C0,
                dcPin: expander.Pins.C1,
                resetPin: expander.Pins.C2
            );

            Resolver.Services.Add(display);

            return Task.CompletedTask;
        }
    }
}