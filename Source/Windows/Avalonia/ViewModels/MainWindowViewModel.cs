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

        private IGraphicsDisplay _display;

        private string _buttonText;

        public int Counter { get; set; } = -1;

        public ReactiveCommand<Unit, Unit> LedCommand { get; }

        public string ButtonText
        {
            get => _buttonText;
            set => this.RaiseAndSetIfChanged(ref _buttonText, value);
        }

        public MainWindowViewModel()
        {
            ButtonText = "Initializing...";
            LedCommand = ReactiveCommand.Create(UpdateCounter);

            // since Avalonia and Meadow are both starting at the same time, we must wait
            // for MeadowInitialize to complete before the output port is ready
            _ = Task.Run(WaitForDisplay);
        }

        private async Task WaitForDisplay()
        {
            while (_display == null)
            {
                _display = Resolver.Services.Get<IGraphicsDisplay>();
                await Task.Delay(100);
            }

            graphics = new MicroGraphics(_display)
            {
                CurrentFont = new Font12x16(),
                Stroke = 2,
                Rotation = RotationType._180Degrees
            };

            UpdateCounter();

            ButtonText = "Turn LED On";
        }

        public void UpdateCounter()
        {
            Counter++;

            graphics.DrawRectangle(
                x: 0,
                y: 0,
                width: graphics.Width,
                height: graphics.Height,
                color: Meadow.Foundation.Color.FromHex("10485E"),
                filled: true);

            graphics.DrawText(
                x: graphics.Width / 2,
                y: graphics.Height / 4 + 10,
                text: $"Clicked",
                scaleFactor: ScaleFactor.X2,
                alignmentH: HorizontalAlignment.Center,
                alignmentV: VerticalAlignment.Center);

            graphics.DrawText(
                x: graphics.Width / 2,
                y: graphics.Height / 2,
                text: $"{Counter}",
                scaleFactor: ScaleFactor.X3,
                alignmentH: HorizontalAlignment.Center,
                alignmentV: VerticalAlignment.Center);

            graphics.DrawText(
                x: graphics.Width / 2,
                y: graphics.Height * 3 / 4 - 10,
                text: $"Times!",
                scaleFactor: ScaleFactor.X2,
                alignmentH: HorizontalAlignment.Center,
                alignmentV: VerticalAlignment.Center);

            graphics.Show();
        }
    }
}