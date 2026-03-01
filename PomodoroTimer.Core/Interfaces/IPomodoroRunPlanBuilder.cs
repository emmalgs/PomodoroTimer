public interface IPomodoroRunPlanBuilder
{
    IPomodoroRunPlan Build(
        int fullSessions,
        bool includeHalfSession,
        TimeSpan focusDuration,
        TimeSpan shortBreakDuration,
        TimeSpan longBreakDuration
    );
}