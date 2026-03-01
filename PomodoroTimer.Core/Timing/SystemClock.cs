using System.Timers;
using Timer = System.Timers.Timer;

namespace PomodoroTimer.Core.Timing;

public sealed class SystemClock : IClock
{
  public event Action<TimeSpan>? Tick;
  private Timer _timer;
  public SystemClock()
  {
    _timer = new Timer(1000);
    _timer.Elapsed += OnElapsed;
  }
  public void Start() => _timer.Start();
  public void Pause() => _timer.Stop();

  private void OnElapsed(object? sender, ElapsedEventArgs e)
  {
    Tick?.Invoke(TimeSpan.FromSeconds(1));
  }
}