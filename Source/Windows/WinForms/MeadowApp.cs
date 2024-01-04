using Meadow;
using Meadow.Foundation.Displays;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsMeadow.Views;

namespace WinFormsMeadow
{
    public class MeadowApp : App<Windows>
    {
        WinFormsDisplay _display;

        public override Task Initialize()
        {
            // Default WinForms screen (800,600)
            //_display = new WinFormsDisplay();
            //var views = new RotatingCube(_display);

            // Screen size of a ILI9488 display
            //_display = new WinFormsDisplay(320, 480);
            //var views = new WiFiWeather(_display);

            // Screen size of a EPD4IN2bV2 e-paper display
            //_display = new WinFormsDisplay(300, 400);
            //var views = new HomeWidget(_display);

            // Screen size of a ILI9341 display
            _display = new WinFormsDisplay(320, 240);
            var views = new CultivarView(_display);
            //var views = new ProjectLabDemoView(_display);
            //var views = new AtmosphericHMI(_display);
            //var views = new WifiWeatherV2(_display);

            _ = Task.Run(() =>
            {
                Thread.Sleep(2000);
                views.Run();
            });

            return Task.CompletedTask;
        }

        public override Task Run()
        {
            Application.Run(_display);

            return Task.CompletedTask;
        }
    }
}