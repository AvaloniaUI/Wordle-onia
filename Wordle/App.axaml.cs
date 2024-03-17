using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Wordle.Services;
using Wordle.Services.Impl;
using Wordle.ViewModels;
using Wordle.Views;

namespace Wordle;

public partial class App : Application
{
    public static ServiceProvider? ServiceProvider { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ServiceProvider = BuildServices();

        var viewModel = ServiceProvider.GetRequiredService<MainViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = viewModel
            };
        }
    
        base.OnFrameworkInitializationCompleted();
    }

    private static ServiceProvider BuildServices()
    {
        var sc = new ServiceCollection();
        sc.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
        sc.AddTransient<IGuessValidationService, GuessValidationServiceImpl>();
        sc.AddTransient<IWordProvider, WordProviderImpl>();
        sc.AddTransient<MainViewModel>();
        return sc.BuildServiceProvider();
    }
}