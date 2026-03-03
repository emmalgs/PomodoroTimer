namespace PomodoroTimer.Core.Timing;

public class FakeClock : IClock
{
  public event Action<TimeSpan>? Tick;

  public void Start() { }
  public void Pause() { }
  public void TriggerTick(TimeSpan delta)
  {
    Tick?.Invoke(delta);
  }
}