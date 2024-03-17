namespace Wordle.Services.Impl;

using System;

public class ThemeServiceImpl : IThemeService
{
    private IThemeService.Theme _currentTheme = IThemeService.Theme.Light;

    public event EventHandler<(IThemeService.Theme? OldTheme, IThemeService.Theme NewTheme)>? ThemeChanged;

    public IThemeService.Theme GetCurrentTheme()
    {
        return _currentTheme;
    }

    public void SetThemeSettings(bool dark, bool highContrast)
    {
        var oldTheme = _currentTheme;
        IThemeService.Theme newTheme;
        if (dark)
        {
            newTheme = highContrast
                ? IThemeService.Theme.DarkHighContrast
                : IThemeService.Theme.Dark;
        }
        else
        {
            newTheme = highContrast
                ? IThemeService.Theme.LightHighContrast
                : IThemeService.Theme.Light;
        }

        _currentTheme = newTheme;
        ThemeChanged?.Invoke(this, (oldTheme, newTheme));
    }
}