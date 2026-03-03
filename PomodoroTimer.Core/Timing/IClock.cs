namespace PomodoroTimer.Core.Timing;

public interface IClock
{
  event Action<TimeSpan> Tick;

  void Start();
  void Pause();
}
