using PomodoroTimer.Core.Interfaces;
using Timer = System.Timers.Timer;
using System.Timers;

namespace PomodoroTimer.Core.Services;

public class TimerService : ITimerService
{
  private Timer _timer;
  private int _secondsRemaining;

  public event Action<int>? OnTick;
  public event Action? OnTimerFinished;

  public bool IsRunning { get; private set; }

  public TimerService()
  {
    _timer = new Timer(1000);
    _timer.Elapsed += TimerElapsed;
  }

  public void Start()
  {
    if (IsRunning) return;

    IsRunning = true;
    _timer.Start();
  }

  public void Pause()
  {
    if (!IsRunning) return;

    IsRunning = false;
    _timer.Stop();
  }

  public void Reset(int durationInMinutes)
  {
    IsRunning = false;
    _timer.Stop();
    _secondsRemaining = durationInMinutes * 60;
    OnTick?.Invoke(_secondsRemaining);
  }

  private void TimerElapsed(object? sender, ElapsedEventArgs e)
  {
    if (!IsRunning) return;

    _secondsRemaining--;

    OnTick?.Invoke(_secondsRemaining);

    if (_secondsRemaining <= 0)
    {
      _timer.Stop();
      IsRunning = false;
      OnTimerFinished?.Invoke();
    }
  }
}