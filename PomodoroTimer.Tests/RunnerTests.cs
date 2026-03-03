using PomodoroTimer.Core.Runner;
using PomodoroTimer.Core.Timing;
using Xunit;

namespace PomodoroTimer.Tests;

public class RunnerTests
{
  [Fact]
  public void RunnerWith7Blocks()
  {
    // Arrange
    var builder = new PomodoroRunPlanBuilder();

    var focus = TimeSpan.FromMinutes(25);
    var shortBreak = TimeSpan.FromMinutes(5);
    var longBreak = TimeSpan.FromMinutes(20);

    var clock = new FakeClock();
    // Act
    var plan = builder.Build(
      1,
      false,
      focus,
      shortBreak,
      longBreak
    );

    var runner = new PomodoroRunner(plan, clock);
    // Assert
    Assert.Equal(plan.Blocks[0], runner.CurrentBlock);
    Assert.Equal(focus, runner.Remaining);
    Assert.False(runner.IsRunning);
  }

  [Fact]
  public void Runner_Starts_And_Fires_BlockStarted()
  {
    // Arrange
    var builder = new PomodoroRunPlanBuilder();

    var focus = TimeSpan.FromMinutes(25);
    var shortBreak = TimeSpan.FromMinutes(5);
    var longBreak = TimeSpan.FromMinutes(20);

    var clock = new FakeClock();
    // Act
    var plan = builder.Build(
      1,
      false,
      focus,
      shortBreak,
      longBreak
    );

    var runner = new PomodoroRunner(plan, clock);
    bool blockStartedFired = false;
    PomodoroBlock? startedBlock = null;

    runner.BlockStarted += block =>
    {
      blockStartedFired = true;
      startedBlock = block;
    };
    // Act
    runner.Start();
    // Assert
    Assert.True(runner.IsRunning);
    Assert.True(blockStartedFired);
    Assert.Equal(plan.Blocks[0], startedBlock);
  }

  [Fact]
  public void Runner_Pauses()
  {
    // Arrange
    var builder = new PomodoroRunPlanBuilder();

    var focus = TimeSpan.FromMinutes(25);
    var shortBreak = TimeSpan.FromMinutes(5);
    var longBreak = TimeSpan.FromMinutes(20);

    var clock = new FakeClock();
    // Act
    var plan = builder.Build(
      1,
      false,
      focus,
      shortBreak,
      longBreak
    );

    var runner = new PomodoroRunner(plan, clock);
    // Act
    runner.Start();
    Assert.True(runner.IsRunning);

    runner.Pause();
    // Assert
    Assert.False(runner.IsRunning);
  }

  [Fact]
  public void Runner_Resets_CurrentBlock()
  {
    // Arrange
    var block = new PomodoroBlock(PomodoroState.Focus, TimeSpan.FromSeconds(5));
    var plan = new PomodoroRunPlan(new List<PomodoroBlock> { block });
    var clock = new FakeClock();
    var runner = new PomodoroRunner(plan, clock);
    // Act
    runner.Start();
    clock.TriggerTick(TimeSpan.FromSeconds(3));
    Assert.Equal(TimeSpan.FromSeconds(2), runner.Remaining);
    Assert.True(runner.IsRunning);

    runner.Reset();
    Assert.Equal(TimeSpan.FromSeconds(5), runner.Remaining);
    Assert.Equal(block, runner.CurrentBlock);
  }

  [Fact]
  public void Runner_Single_Block_Completion()
  {
    // Arrange
    var block = new PomodoroBlock(PomodoroState.Focus, TimeSpan.FromSeconds(5));
    var plan = new PomodoroRunPlan(new List<PomodoroBlock> { block });
    var clock = new FakeClock();
    var runner = new PomodoroRunner(plan, clock);

    bool blockStartedFired = false;
    PomodoroBlock? startedBlock = null;
    bool blockCompletedFired = false;
    PomodoroBlock? completedBlock = null;
    bool runCompletedFired = false;

    runner.BlockStarted += block =>
    {
      blockStartedFired = true;
      startedBlock = block;
    };

    runner.BlockCompleted += block =>
    {
      blockCompletedFired = true;
      completedBlock = block;
    };

    runner.RunCompleted += () =>
    {
      runCompletedFired = true;
    };

    // Act
    runner.Start();
    Assert.True(blockStartedFired);
    Assert.Equal(block, startedBlock);

    clock.TriggerTick(TimeSpan.FromSeconds(5));

    Assert.True(blockCompletedFired);
    Assert.Equal(block, completedBlock);

    Assert.True(runCompletedFired);
  }
}