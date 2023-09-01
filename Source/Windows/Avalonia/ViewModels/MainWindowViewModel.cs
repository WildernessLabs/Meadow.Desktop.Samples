using Meadow;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using ReactiveUI;
using System.Drawing;
using System.Reactive;
using System.Threading.Tasks;

namespace AvaloniaMeadow.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private MicroGraphics graphics;

        private Gc9a01? _display;

        private string _buttonText;

        public ReactiveCommand<Unit, Unit> LedCommand { get; }

        public string ButtonText
        {
            get => _buttonText;
            set => this.RaiseAndSetIfChanged(ref _buttonText, value);
        }

        public MainWindowViewModel()
        {
            ButtonText = "Initializing...";
            //LedCommand = ReactiveCommand.Create(ToggleLed);

            // since Avalonia and Meadow are both starting at the same time, we must wait
            // for MeadowInitialize to complete before the output port is ready
            _ = Task.Run(WaitForDisplay);
        }

        private async Task WaitForDisplay()
        {
            while (_display == null)
            {
                _display = Resolver.Services.Get<Gc9a01>();
                await Task.Delay(100);
            }

            graphics = new MicroGraphics(_display)
            {
                CurrentFont = new Font12x16(),
                Stroke = 2,
                Rotation = RotationType._180Degrees
            };

            graphics.Clear(Meadow.Foundation.Color.Red);

            graphics.Show();

            ButtonText = "Turn LED On";
        }

        //private void ToggleLed()
        //{
        //    if (_led == null) return;
        //    _led.State = !_led.State;
        //    if (_led.State)
        //    {
        //        ButtonText = "Turn LED Off";
        //    }
        //    else
        //    {
        //        ButtonText = "Turn LED On";
        //    }
        //}
    }
}