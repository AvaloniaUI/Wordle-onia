namespace Wordle.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using Services;
using System;

public class SettingsViewModel : ObservableObject
{
    private readonly IThemeService _themeService;
    private bool _darkTheme;
    private bool _highContrast;

    public SettingsViewModel(IThemeService themeService)
    {
        ArgumentNullException.ThrowIfNull(themeService);

        _themeService = themeService;
        var currentTheme = themeService.GetCurrentTheme();

        switch (currentTheme)
        {
            case IThemeService.Theme.Dark:
                _darkTheme = true;
                _highContrast = false;
                break;
            case IThemeService.Theme.DarkHighContrast:
                _darkTheme = true;
                _highContrast = true;
                break;
            case IThemeService.Theme.Light:
                _darkTheme = false;
                _highContrast = false;
                break;
            case IThemeService.Theme.LightHighContrast:
                _darkTheme = false;
                _highContrast = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public bool DarkTheme
    {
        get => _darkTheme;
        set
        {
            _darkTheme = value;
            OnPropertyChanged();
            _themeService.SetThemeSettings(value, HighContrast);
        }
    }

    public bool HighContrast
    {
        get => _highContrast;
        set
        {
            _highContrast = value;
            OnPropertyChanged();
            _themeService.SetThemeSettings(DarkTheme, value);
        }
    }
}