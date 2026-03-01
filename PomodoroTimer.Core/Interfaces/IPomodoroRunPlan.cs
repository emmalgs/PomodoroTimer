public interface IPomodoroRunPlan
{
    IReadOnlyList<PomodoroBlock> Blocks { get; }
}