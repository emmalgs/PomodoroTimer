using Xunit;

namespace PomodoroTimer.Tests;

public class RunPlanBuilderTests
{
  [Fact]
  public void BuildOneSession_HasNoLongBreak()
  {
    // Arrange
    var builder = new PomodoroRunPlanBuilder();
    var focus = TimeSpan.FromMinutes(25);
    var shortBreak = TimeSpan.FromMinutes(5);
    var longBreak = TimeSpan.FromMinutes(20);
    // Act
    var plan = builder.Build(
      1,
      false,
      focus,
      shortBreak,
      longBreak
    );
    // Assert
    Assert.Equal(7, plan.Blocks.Count);
    Assert.Equal(PomodoroState.Focus, plan.Blocks[0].State);
    Assert.Equal(PomodoroState.ShortBreak, plan.Blocks[1].State);
    Assert.Equal(PomodoroState.Focus, plan.Blocks[2].State);
  }

  [Fact]
  public void BuildTwoSessions_HasOneLongBreak()
  {
        // Arrange
    var builder = new PomodoroRunPlanBuilder();
    var sessions = 2;
    var hasHalfSession = false;
    var focus = TimeSpan.FromMinutes(25);
    var shortBreak = TimeSpan.FromMinutes(5);
    var longBreak = TimeSpan.FromMinutes(20);
    // Act
    var plan = builder.Build(
      sessions,
      hasHalfSession,
      focus,
      shortBreak,
      longBreak
    );

    var focusCount = plan.Blocks.Count(b => b.State == PomodoroState.Focus);
    var states = plan.Blocks.Select(b => b.State).ToList();
    // Assert
    Assert.Equal(15, plan.Blocks.Count);
    Assert.Equal(8, focusCount);
    Assert.Equal(
    new[]
    {
        PomodoroState.Focus,
        PomodoroState.ShortBreak,
        PomodoroState.Focus
    },
    states.Take(3)
);
  }
  // public void BuildHalfSession_HasNoLongBreak()
  // {

  // }
  // public void BuildOneAndHalfSessions_HasOneLongBreak()
  // {

  // }
}
