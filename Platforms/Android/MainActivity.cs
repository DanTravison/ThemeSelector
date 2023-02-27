using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using ThemeSelector.Platforms.Android;

namespace ThemeSelector;

[Activity(Theme = "@style/Maui.SplashTheme", 
    MainLauncher = true, 
    ConfigurationChanges = 
    ConfigChanges.ScreenSize | 
    ConfigChanges.Orientation | 
    ConfigChanges.UiMode | 
    ConfigChanges.ScreenLayout | 
    ConfigChanges.SmallestScreenSize | 
    ConfigChanges.Density
)]
public class MainActivity : MauiAppCompatActivity
{

// Address https://github.com/dotnet/maui/issues/8236,
// RequestedThemeChanged is not raised.
// On Android 30 or greater, use OnConfigurationChanged().
// On earlier versions, OnConfigurationChanged is not called.
// Use OnWindowFocusChanged(true) to avoid polling.
#if ANDROID30_0_OR_GREATER

    public override void OnConfigurationChanged(Configuration newConfig)
    {
        base.OnConfigurationChanged(newConfig);
        Platforms.Android.ThemeInfo.CheckTheme();
    }

#else

    public override void OnWindowFocusChanged(bool hasFocus)
    {
        base.OnWindowFocusChanged(hasFocus);
 
        if (hasFocus)
        {
            ThemeInfo.CheckTheme();
        }
    }
#endif

}
