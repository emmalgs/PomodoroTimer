using PomodoroTimer.Core.Runner;

public interface IPomodoroEngine
{
  event Action<PomodoroBlock> BlockStarted;
  event Action<PomodoroBlock> BlockCompleted;
  event Action SessionCompleted;
  event Action<TimeSpan>? Tick;
}