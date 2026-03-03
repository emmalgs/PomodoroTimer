public interface IPomodoroRunner
{
  event Action<PomodoroBlock> BlockStarted;
  event Action<PomodoroBlock> BlockCompleted;
  event Action RunCompleted;
  event Action<TimeSpan>? Tick;

  PomodoroBlock? CurrentBlock { get; }
  TimeSpan Remaining { get; }

  void Start();
  void Pause();
  void Reset();
}