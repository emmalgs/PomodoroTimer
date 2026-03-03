using PomodoroTimer.Core.Timing;

namespace PomodoroTimer.Core.Runner;

public class PomodoroRunner : IPomodoroRunner
{
  private readonly IClock _clock;
  private readonly IPomodoroRunPlan _plan;
  private int _currentBlockIndex;

  public event Action<PomodoroBlock>? BlockStarted;
  public event Action<PomodoroBlock>? BlockCompleted;
  public event Action? RunCompleted;
  public event Action<TimeSpan>? Tick;

  public PomodoroBlock? CurrentBlock { get; private set; }
  public TimeSpan Remaining { get; private set; }
  public bool IsRunning { get; private set; }

  public PomodoroRunner(IPomodoroRunPlan plan, IClock clock)
  {
    _plan = plan ?? throw new ArgumentNullException(nameof(plan));
    _clock = clock ?? throw new ArgumentNullException(nameof(clock));

    _currentBlockIndex = 0;
    if (plan.Blocks.Count > 0)
    {
      CurrentBlock = plan.Blocks[_currentBlockIndex];
      Remaining = CurrentBlock.Duration;
    }

    _clock.Tick += OnClockTick;
  }

  private void OnClockTick(TimeSpan delta)
  {
    if (!IsRunning || CurrentBlock == null)
      return;

    Remaining -= delta;
    Tick?.Invoke(Remaining);
    
    if (Remaining <= TimeSpan.Zero)
    {
      BlockCompleted?.Invoke(CurrentBlock);
      MoveToNextBlock();
    }
  }

  private void MoveToNextBlock()
  {
    _currentBlockIndex++;
    if (_currentBlockIndex >= _plan.Blocks.Count)
    {
      CurrentBlock = null;
      IsRunning = false;
      RunCompleted?.Invoke();
    }
    else
    {
      CurrentBlock = _plan.Blocks[_currentBlockIndex];
      Remaining = CurrentBlock.Duration;
      BlockStarted?.Invoke(CurrentBlock);
    }
  }

  public void Start()
  {
    if (CurrentBlock == null) return;

    IsRunning = true;
    _clock.Start();
    BlockStarted?.Invoke(CurrentBlock);
  }
  public void Pause()
  {
    IsRunning = false;
    _clock.Pause();
  }
  public void Reset()
  {
    if (CurrentBlock == null) return;

    Remaining = CurrentBlock.Duration;
    IsRunning = false;

    BlockStarted?.Invoke(CurrentBlock);
  }
}