using PomodoroTimer.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PomodoroTimer.App.ViewModels;

public partial class MainViewModel : ObservableObject
{
  private readonly TimerService _timerService;

  [ObservableProperty]
  private string _title = "Pomodoro Timer 🍅";

  [ObservableProperty]
  private bool _isRunning;

  [ObservableProperty]
  private string _startPauseText = "Start";

  [ObservableProperty]
  private string _timeRemaining = "25:00";

  public MainViewModel()
  {
    _timerService = new TimerService();
    _timerService.Reset(25);


    _timerService.OnTick += seconds =>
    {
      var minutes = seconds / 60;
      var secs = seconds % 60;
      TimeRemaining = $"{minutes:D2}:{secs:D2}";
    };
    _timerService.OnTimerFinished += () =>
    {
      Title = "Pomodoro Finished!";
    };
  }

  [RelayCommand]
  private void ToggleTimer()
  {
    if (_timerService.IsRunning)
    {
      _timerService.Pause();
      IsRunning = false;
      StartPauseText = "Start";
    }
    else
    {
      _timerService.Start();
      IsRunning = true;
      StartPauseText = "Pause";
    }
  }

  [RelayCommand]
  public void Reset()
  {
    _timerService.Reset(25);
    Title = "Pomodoro Timer 🍅";
    IsRunning = false;
    StartPauseText = "Start";
  }
}