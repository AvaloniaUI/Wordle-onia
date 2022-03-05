namespace Wordle.Services;

using System;

public interface IThemeService
{
    public enum Theme
    {
        Dark,
        DarkHighContrast,
        Light,
        LightHighContrast
    }

    event EventHandler<(Theme? OldTheme, Theme NewTheme)>? ThemeChanged;

    Theme GetCurrentTheme();

    void SetThemeSettings(bool dark, bool highContrast);
}