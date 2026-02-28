using Avalonia.Controls;
using PomodoroTimer.App.ViewModels;

namespace PomodoroTimer.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
         DataContext = new MainViewModel();
    }
}