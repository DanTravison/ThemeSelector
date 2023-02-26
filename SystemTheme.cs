namespace ThemeSelector
{
    /// <summary>
    /// Provides an encapsulation of Application.Current.RequestedTheme
    /// and Application.Current.RequestedThemeChanged to address
    /// platform-specific issues.
    /// </summary>
    public static class SystemTheme
    {
        static object _lock = new object();

        /// <summary>
        /// Gets the system's <see cref="AppTheme"/>.
        /// </summary>
        /// <remarks>
        /// This property is an alternative to Application.Current.RequestedTheme.
        /// </remarks>
        public static AppTheme RequestedTheme
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

        /// <summary>
        /// Occurs when the system <see cref="RequestedTheme"/> changes.
        /// </summary>
        /// <remarks>
        /// This event is an alternative to Application.Current.RequestedThemeChanged.
        /// </remarks>
        public static event EventHandler<AppThemeChangedEventArgs> RequestedThemeChanged
        {
            // NOTE: the lock is used out of an abundance of caution but may
            // not be necessary.
            add
            {
                // Add the event delegate to the appropriate event
                lock (_lock)
                {
#if (ANDROID)
                    ThemeSelector.Platforms.Android.ThemeInfo.RequestedThemeChanged += value;
#else
                    Application.Current.RequestedThemeChanged += value;
#endif
                }
            }
            remove
            {
                // Remove the event delegate from appropriate event
                lock (_lock)
                { 
#if (ANDROID)
                    ThemeSelector.Platforms.Android.ThemeInfo.RequestedThemeChanged -= value;
#else
                    Application.Current.RequestedThemeChanged -= value;
#endif                
                }
            }
        }
    }
}
