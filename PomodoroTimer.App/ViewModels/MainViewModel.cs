using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PomodoroTimer.Core.Engine;

namespace PomodoroTimer.App.ViewModels;

public partial class MainViewModel : ObservableObject
{
  private readonly PomodoroEngine _engine;

  private readonly TimeSpan _focus = TimeSpan.FromMinutes(25);
  private readonly TimeSpan _shortBreak = TimeSpan.FromMinutes(5);
  private readonly TimeSpan _longBreak = TimeSpan.FromMinutes(15);
  public MainViewModel()
  {
    _engine = new PomodoroEngine();
    _engine.BlockStarted += HandleBlockStarted;
    _engine.Tick += HandleTick;
  }
  // =========================
  // User Input
  // =========================

  [ObservableProperty]
  private int _sessionCount = 1;

  [ObservableProperty]
  private bool _isRunning;

  public bool CanEditSessions => !IsRunning;

  partial void OnIsRunningChanged(bool value)
  {
    OnPropertyChanged(nameof(CanEditSessions));
  }
  // =========================
  // Display Properties
  // =========================
  [ObservableProperty]
  private string _title = "LE TIMER";

  [ObservableProperty]
  private string _currentBlockName = "Ready";

  [ObservableProperty]
  private TimeSpan _remaining;

  public string DisplayTime => $"{Remaining:mm\\:ss}";

  partial void OnRemainingChanged(TimeSpan value)
  {
    OnPropertyChanged(nameof(DisplayTime));
  }

  // =========================
  // Commands
  // =========================
  [RelayCommand]
  private void Begin()
  {
    _engine.StartSession(
        SessionCount,
        _focus,
        _shortBreak,
        _longBreak
    );

    IsRunning = true;
  }

  [RelayCommand]
  private void Start()
  {
    _engine.Start();
    IsRunning = true;
  }

  [RelayCommand]
  private void Pause()
  {
    _engine.Pause();
    IsRunning = false;
  }

  [RelayCommand]
  private void Reset()
  {
    _engine.Reset();
    IsRunning = false;
  }

  private void HandleBlockStarted(PomodoroBlock block)
  {
    CurrentBlockName = block.State.ToString();
    Remaining = block.Duration;
  }

  private void HandleTick(TimeSpan remaining)
  {
    Remaining = remaining;
  }
}