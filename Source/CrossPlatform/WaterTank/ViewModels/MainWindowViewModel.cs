using Meadow.Peripherals.Sensors.Distance;
using Meadow.Units;
using ReactiveUI;
using System;
using WaterTank.Models;

namespace WaterTank.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private IRangeFinder _rangeFinder;

    // TODO: set this to match hardware
    private Length _maxSensorLength = new Length(10, Length.UnitType.Centimeters);

    public MainWindowViewModel()
    {
        UseSimulator = true;

        if (UseSimulator)
        {
            _rangeFinder = new SimulatedDistanceSensor(_maxSensorLength);
        }
        else
        {
            // create a physical connection to hardware

            throw new NotImplementedException();
        }

        _rangeFinder.Updated += DistanceUpdated;
    }

    private void DistanceUpdated(object? sender, Meadow.IChangeResult<Length> e)
    {
        FillPercentage = (int)(e.New.Centimeters * 100 / _maxSensorLength.Centimeters);
    }

    private int _fillPercent;

    public bool UseSimulator { get; }

    public int FillPercentage
    {
        get => _fillPercent;
        set
        {
            // this is only called from the sim slider
            this.RaiseAndSetIfChanged(ref _fillPercent, value);
            this.RaisePropertyChanged(nameof(FillHeight));
            this.RaisePropertyChanged(nameof(WaterTop));
        }
    }

    public int SliderPercentage
    {
        get => _fillPercent;
        set
        {
            // this is only called from the sim slider
            if (UseSimulator && _rangeFinder is SimulatedDistanceSensor ds)
            {
                ds.SetDistance(value / 100d);
            }
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
