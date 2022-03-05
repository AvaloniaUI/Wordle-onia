using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Wordle.Services;
using Wordle.Services.Impl;
using Wordle.ViewModels;
using Wordle.Views;

namespace Wordle
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            AvaloniaLocator.CurrentMutable.Bind<IMessenger>().ToConstant(WeakReferenceMessenger.Default);
            AvaloniaLocator.CurrentMutable.Bind<IGuessValidationService>().ToConstant(new GuessValidationServiceImpl());
            AvaloniaLocator.CurrentMutable.Bind<IWordProvider>().ToConstant(new WordProviderImpl());
            
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel()
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = new MainViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}