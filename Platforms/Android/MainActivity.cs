using Android.App;
using Android.Content.PM;
using Android.OS;
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
    public override void OnWindowFocusChanged(bool hasFocus)
    {
        base.OnWindowFocusChanged(hasFocus);
        // Due to https://github.com/dotnet/maui/issues/8236,
        // RequestedThemeChanged is not raised.
        // To avoid polling, check for system theme changes when
        // the window gains focus.
        // The caveat to this is if the app is visible when the theme changes,
        // such as when multiple screens are available, it will not update
        // until it receives focus.
        if (hasFocus)
        {
            ThemeInfo.CheckTheme();
        }
    }
}
