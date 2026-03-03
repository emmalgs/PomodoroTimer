using PomodoroTimer.Core.Runner;
using PomodoroTimer.Core.Timing;

namespace PomodoroTimer.Core.Engine;

public class PomodoroEngine : IPomodoroEngine
{
  private PomodoroRunner? _runner;
  private SystemClock? _clock;
  // private int _remainingSessions;

  public event Action<PomodoroBlock>? BlockStarted;
  public event Action<PomodoroBlock>? BlockCompleted;
  public event Action? SessionCompleted;
  public event Action<TimeSpan>? Tick;

  public void StartSession(
      int sessions,
      TimeSpan focus,
      TimeSpan shortBreak,
      TimeSpan longBreak)
  {
    var builder = new PomodoroRunPlanBuilder();
    var plan = builder.Build(sessions, false, focus, shortBreak, longBreak);

    _clock = new SystemClock();
    _runner = new PomodoroRunner(plan, _clock);

    _runner.BlockStarted += block => BlockStarted?.Invoke(block);
    _runner.Tick += remaining => Tick?.Invoke(remaining);

    _runner.Start();
  }

  public void Start() => _runner?.Start();
  public void Pause() => _runner?.Pause();
  public void Reset() => _runner?.Reset();

  // private void OnBlockCompleted(PomodoroBlock block)
  // {
  //   BlockCompleted?.Invoke(block);
  // }

  // private void OnRunCompleted()
  // {
  //   _remainingSessions--;
  //   if (_remainingSessions > 0)
  //   {
  //     _runner?.Reset();
  //     _runner?.Start();
  //   }
  //   else
  //   {
  //     SessionCompleted?.Invoke();
  //   }
  // }
}