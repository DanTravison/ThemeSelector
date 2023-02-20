using System.Globalization;

namespace ThemeSelector;

public partial class App : Application
{
    public App()
	{
#if ANDROID
        // Due to https://github.com/dotnet/maui/issues/8236,
        // RequestedThemeChanged is not raised consistently on android. Use a custom workaround.
        ThemeSelector.Platforms.Android.ThemeInfo.RequestedThemeChanged += OnRequestedThemeChanged;
#else
        // Use standard RequestedThemChanged events.
        Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
#endif
        InitializeComponent();
		MainPage = new AppShell();
	}

    /// <summary>
    /// Provides an alternative to Application.Current.RequestedTheme to workaround
    /// https://github.com/dotnet/maui/issues/8236.
    /// </summary>
    /// <remarks>
    /// This property replaces the standard Application.Current.RequestedTheme
    /// with a wrapper that uses a work around on Android.
    /// </remarks>
    public static AppTheme SystemTheme
    {
        get
        {
#if (ANDROID)
            // See https://github.com/dotnet/maui/issues/8236
            return ThemeSelector.Platforms.Android.ThemeInfo.Theme;
#else
            return Application.Current.RequestedTheme;
#endif
        }
    }

    void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
    {
        Trace(nameof(App), nameof(e.RequestedTheme), e.RequestedTheme);
        SystemThemeChanged?.Invoke
        (
            Application.Current, 
            new AppThemeChangedEventArgs(e.RequestedTheme)
        );
    }

    /// <summary>
    /// Occurs when the system theme changes.
    /// </summary>
    public event EventHandler<AppThemeChangedEventArgs> SystemThemeChanged;

    #region Tracing

    public static void Trace(string sourceName, string memberName, object value)
    {
        Trace(sourceName, memberName, value is string message ? message : "{0}", value);
    }
    
    public static void Trace(string sourceName, string memberName, string format, params object[] args)
    {
        string message;
        if (args.Length > 0)
        {
            message = string.Format
            (
                CultureInfo.InvariantCulture,
                format,
                args
            );
        }
        else
        {
            message = format;
        }
        System.Diagnostics.Trace.WriteLine
        (
            string.Format
            (
                CultureInfo.InvariantCulture,
                "{0}.{1}: {2}",
                sourceName, memberName, message
            )
        );
    }
    #endregion Tracing
}
