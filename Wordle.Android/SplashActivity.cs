﻿using Android.App;
using Android.Content;
using Android.OS;
using Application = Android.App.Application;

using Avalonia;

namespace Wordle.Android
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (Avalonia.Application.Current == null)
            {
                AppBuilder.Configure<App>()
                    .UseAndroid()
                    .SetupWithoutStarting();
            }

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
