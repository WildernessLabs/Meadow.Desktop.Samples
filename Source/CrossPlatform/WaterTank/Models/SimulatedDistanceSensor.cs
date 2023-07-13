using Meadow;
using Meadow.Peripherals.Sensors;
using Meadow.Units;
using System;
using System.Threading.Tasks;

namespace WaterTank.Models;

internal class SimulatedDistanceSensor : IRangeFinder
{
    private Length? _distance;

    public event EventHandler<IChangeResult<Length>> DistanceUpdated;

    public Length MaxDistance { get; }
    public TimeSpan UpdateInterval => new TimeSpan(0);
    public bool IsSampling => true;

    public SimulatedDistanceSensor(Length maxDistance)
    {
        MaxDistance = maxDistance;
        Distance = new Length(0);
    }

    public Length? Distance
    {
        get => _distance;
        private set
        {
            var old = _distance;
            _distance = value;
            DistanceUpdated?.Invoke(this, new ChangeResult<Length>(_distance.Value, old));
        }
    }

    public void SetDistance(double percentage)
    {
        Distance = MaxDistance * percentage;
    }

    public void SetDistance(Length distance)
    {
        Distance = distance;
    }

    public void MeasureDistance()
    {
        // NOP
    }

    public Task<Length> Read()
    {
        return Task.FromResult(Distance.Value);
    }

    public void StartUpdating(TimeSpan? updateInterval = null)
    {
        // NOP
    }

    public void StopUpdating()
    {
        // NOP
    }
}
