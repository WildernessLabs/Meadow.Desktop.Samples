using ReactiveUI;

namespace WaterTank.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private int _fillPercent;

    public int FillPercentage
    {
        get => _fillPercent;
        set
        {
            this.RaiseAndSetIfChanged(ref _fillPercent, value);
            this.RaisePropertyChanged(nameof(FillHeight));
            this.RaisePropertyChanged(nameof(WaterTop));
        }
    }

    public int TankHeight { get; set; } = 600;

    public int FillHeight
    {
        get => TankHeight * FillPercentage / 100;
    }

    public int WaterTop
    {
        get => TankHeight - FillHeight;
    }
}
