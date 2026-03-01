namespace PomodoroTimer.Core.Timing;

public interface IClock
{
  event Action<TimeSpan> Tick;          // seconds remaining

  void Start();
  void Pause();
}
