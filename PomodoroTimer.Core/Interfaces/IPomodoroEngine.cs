public interface IPomodoroEngine
{
    PomodoroState CurrentState { get; }
    TimeSpan TimeRemaining { get; }
    bool IsRunning { get; }

    void LoadPlan(IPomodoroRunPlan plan);
    void Start();
    void Pause();
    void Reset();

    event Action<PomodoroState>? StateChanged;
    event Action<TimeSpan>? Tick;
    event Action? RunCompleted;
}