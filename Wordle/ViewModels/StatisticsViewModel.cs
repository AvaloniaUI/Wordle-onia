namespace Wordle.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

public partial class StatisticsViewModel : ObservableObject
{
    [ObservableProperty]
    private int _currentStreak;

    [ObservableProperty]
    private int _maxStreak;

    [ObservableProperty]
    private int _played;

    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(WinPercentage))]
    private int _won;

    public int WinPercentage => Won / Played * 100;
}