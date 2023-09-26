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

        //RotatingCube views;
        WiFiWeather views;

        public override Task Initialize()
        {
            //_display = new WinFormsDisplay();
            //views = new RotatingCube(_display);

            // Screen size of a ILI9488 display
            _display = new WinFormsDisplay(320, 480);
            views = new WiFiWeather(_display);

            return Task.CompletedTask;
        }

        public override Task Run()
        {
            _ = Task.Run(() =>
            {
                Thread.Sleep(2000);
                views.Run();
            });

            Application.Run(_display);

            return Task.CompletedTask;
        }
    }
}