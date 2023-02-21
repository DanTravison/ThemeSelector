using Android.Content.Res;
using Application = Android.App.Application;

namespace ThemeSelector.Platforms.Android
{
    static public class ThemeInfo
    {
        static readonly AppThemeChangedEventArgs DarkThemeEventArgs = new(AppTheme.Dark);
        static readonly AppThemeChangedEventArgs LightThemeEventArgs = new(AppTheme.Light);

        static AppTheme? _currentTheme;

        /// <summary>
        /// Gets the current system theme.
        /// </summary>
        /// <remarks>
        /// Inefficient workaround to https://github.com/dotnet/maui/issues/8236
        /// RequestedThemeChanged not raised on Android.
        /// </remarks>
        static public AppTheme Theme
        {
            get
            {
                UiMode currentMode = Application.Context.Resources.Configuration.UiMode & UiMode.NightMask;
                return currentMode switch
                {
                    UiMode.NightYes => AppTheme.Dark,
                    UiMode.NightNo => AppTheme.Light,
                    _ => AppTheme.Unspecified,
                };
            }
        }

        /// <summary>
        /// Checks the system theme for changes and raises the <see cref="RequestedThemeChanged"/> event.
        /// </summary>
        static public void CheckTheme()
        {
            // Due to https://github.com/dotnet/maui/issues/8236,
            // RequestedThemeChanged is not raised.
            // Check for system theme changes when the window gains focus.

            AppTheme currentTheme = Theme;
            if (_currentTheme == null || _currentTheme != currentTheme)
            {
                _currentTheme = currentTheme;
                RequestedThemeChanged?.Invoke(null, currentTheme == AppTheme.Light ? LightThemeEventArgs : DarkThemeEventArgs);
            }
        }

        /// <summary>
        /// Occurs when the system theme changes.
        /// </summary>
        static public event EventHandler<AppThemeChangedEventArgs> RequestedThemeChanged;
    }
}
