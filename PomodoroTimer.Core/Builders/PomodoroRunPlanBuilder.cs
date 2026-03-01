public sealed class PomodoroRunPlanBuilder : IPomodoroRunPlanBuilder
{
    public IPomodoroRunPlan Build(
        int fullSessions,
        bool includeHalfSession,
        TimeSpan focusDuration,
        TimeSpan shortBreakDuration,
        TimeSpan longBreakDuration)
    {
        if (fullSessions < 0)
            throw new ArgumentOutOfRangeException(nameof(fullSessions));

        var blocks = new List<PomodoroBlock>();

        for (int sessionIndex = 0; sessionIndex < fullSessions; sessionIndex++)
        {
            AddFullSession(blocks, focusDuration, shortBreakDuration);

            if (sessionIndex < fullSessions - 1)
            {
                blocks.Add(new PomodoroBlock(
                    PomodoroState.LongBreak,
                    longBreakDuration));
            }
        }

        if (includeHalfSession)
        {
            AddHalfSession(blocks, focusDuration, shortBreakDuration);
        }

        return new PomodoroRunPlan(blocks);
    }

    private static void AddFullSession(
        List<PomodoroBlock> blocks,
        TimeSpan focusDuration,
        TimeSpan shortBreakDuration)
    {
        for (int i = 0; i < 4; i++)
        {
            blocks.Add(new PomodoroBlock(
                PomodoroState.Focus,
                focusDuration));

            if (i < 3)
            {
                blocks.Add(new PomodoroBlock(
                    PomodoroState.ShortBreak,
                    shortBreakDuration));
            }
        }
    }

    private static void AddHalfSession(
        List<PomodoroBlock> blocks,
        TimeSpan focusDuration,
        TimeSpan shortBreakDuration)
    {
        blocks.Add(new PomodoroBlock(
            PomodoroState.Focus,
            focusDuration));

        blocks.Add(new PomodoroBlock(
            PomodoroState.ShortBreak,
            shortBreakDuration));

        blocks.Add(new PomodoroBlock(
            PomodoroState.Focus,
            focusDuration));
    }
}