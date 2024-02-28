using Meadow;
using Meadow.Foundation.Graphics;
using Meadow.Peripherals.Displays;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiMeadow.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private MicroGraphics graphics;

        private IPixelDisplay _display;

        public ICommand CountCommand { get; set; }

        public int Counter { get; set; } = -1;

        public MainPageViewModel()
        {
            CountCommand = new Command(UpdateCounter);

            _ = Task.Run(WaitForDisplay);
        }

        private async Task WaitForDisplay()
        {
            while (_display == null)
            {
                _display = Resolver.Services.Get<IPixelDisplay>();
                await Task.Delay(100);
            }

            graphics = new MicroGraphics(_display)
            {
                CurrentFont = new Font12x16(),
                Stroke = 2,
                Rotation = RotationType._180Degrees
            };

            UpdateCounter();
        }

        public void UpdateCounter()
        {
            Counter++;

            graphics.DrawRectangle(
                x: 0,
                y: 0,
                width: graphics.Width,
                height: graphics.Height,
                color: Meadow.Color.FromHex("10485E"),
                filled: true);

            graphics.DrawText(
                x: graphics.Width / 2,
                y: graphics.Height / 4 + 10,
                text: $"Clicked",
                scaleFactor: ScaleFactor.X2,
                alignmentH: Meadow.Foundation.Graphics.HorizontalAlignment.Center,
                alignmentV: Meadow.Foundation.Graphics.VerticalAlignment.Center);

            graphics.DrawText(
                x: graphics.Width / 2,
                y: graphics.Height / 2,
                text: $"{Counter}",
                scaleFactor: ScaleFactor.X3,
                alignmentH: Meadow.Foundation.Graphics.HorizontalAlignment.Center,
                alignmentV: Meadow.Foundation.Graphics.VerticalAlignment.Center);

            graphics.DrawText(
                x: graphics.Width / 2,
                y: graphics.Height * 3 / 4 - 10,
                text: $"Times!",
                scaleFactor: ScaleFactor.X2,
                alignmentH: Meadow.Foundation.Graphics.HorizontalAlignment.Center,
                alignmentV: Meadow.Foundation.Graphics.VerticalAlignment.Center);

            graphics.Show();
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}