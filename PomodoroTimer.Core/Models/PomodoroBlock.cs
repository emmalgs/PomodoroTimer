public sealed record PomodoroBlock(
    PomodoroState State,
    TimeSpan Duration
);