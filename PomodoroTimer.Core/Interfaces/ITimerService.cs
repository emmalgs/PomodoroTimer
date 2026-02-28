namespace PomodoroTimer.Core.Interfaces;

public interface ITimerService
{
  event Action<int> OnTick;          // seconds remaining
  event Action OnTimerFinished;

  void Start();
  void Pause();
  void Reset(int durationInMinutes);
  bool IsRunning { get; }
}
