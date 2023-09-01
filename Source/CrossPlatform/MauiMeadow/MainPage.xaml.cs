using MauiMeadow.ViewModels;

namespace MauiMeadow
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new BaseViewModel();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            (BindingContext as BaseViewModel).UpdateCounter();
        }
    }
}