public sealed class PomodoroRunPlan : IPomodoroRunPlan
{
    public IReadOnlyList<PomodoroBlock> Blocks { get; }

    public PomodoroRunPlan(IReadOnlyList<PomodoroBlock> blocks)
    {
        Blocks = blocks;
    }
}