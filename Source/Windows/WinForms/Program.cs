using Meadow;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsMeadow
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();

            await MeadowOS.Start(args);
        }
    }
}