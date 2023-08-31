using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.ICs.IOExpanders;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiMeadow.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private Ft232h _expander;
        private MicroGraphics graphics;

        public BaseViewModel()
        {
            _expander = new Ft232h();

            var display = new Epd4in2bV2(
                spiBus: _expander.CreateSpiBus(),
                chipSelectPin: _expander.Pins.C0,
                dcPin: _expander.Pins.C1,
                resetPin: _expander.Pins.C2,
                busyPin: _expander.Pins.C3);

            graphics = new MicroGraphics(display)
            {
                CurrentFont = new Font12x16(),
                Stroke = 2,
                Rotation = RotationType._180Degrees
            };

            graphics.Clear();

            graphics.DrawText(10, 10, "Hello Meadow!!! ");

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