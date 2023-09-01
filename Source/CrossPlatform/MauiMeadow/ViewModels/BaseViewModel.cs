using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.ICs.IOExpanders;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiMeadow.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private MicroGraphics graphics;

        public int Counter { get; set; } = -1;

        public BaseViewModel()
        {
            var expander = new Ft232h();

            var display = new Gc9a01
            (
                spiBus: expander.CreateSpiBus(),
                chipSelectPin: expander.Pins.C0,
                dcPin: expander.Pins.C1,
                resetPin: expander.Pins.C2
            );

            graphics = new MicroGraphics(display)
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
                color: Meadow.Foundation.Color.FromHex("10485E"),
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