using System.Collections.ObjectModel;
using System.ComponentModel;
using ThemeSelector.Controls;

namespace ThemeSelector
{
    public sealed class MainViewModel : ObservableObject
    {
        #region Fields

        static readonly ResourceDictionary DarkTheme = new Resources.Themes.Dark();
        static readonly ResourceDictionary LightTheme = new Resources.Themes.Light();

        AppTheme _preferredTheme = AppTheme.Unspecified;
        AppTheme _activeTheme = AppTheme.Unspecified;

        readonly ObservableCollection<ThemeItem> _themeItems = new ();
        readonly ObservableCollection<AppTheme> _themes = new();
        #endregion Fields

        public MainViewModel()
        {
            _themeItems.Add(new ("System", OnThemeSelected, AppTheme.Unspecified));
            _themeItems.Add(new (nameof(AppTheme.Light), OnThemeSelected, AppTheme.Light));
            _themeItems.Add(new (nameof(AppTheme.Dark), OnThemeSelected, AppTheme.Dark));
            ThemeItems = new (_themeItems);

            foreach (AppTheme theme in Enum.GetValues<AppTheme>())
            {
                _themes.Add(theme);
            }
            Themes = new(_themes);

            if (Application.Current is App app)
            {
                app.SystemThemeChanged += OnSystemThemeChanged;
            }
        }

        #region Properties

        /// <summary>
        /// Provides a <see cref="ThemeItem"/> collection for testing radio buttons
        /// </summary>
        public ReadOnlyObservableCollection<ThemeItem> ThemeItems
        {
            get;
        }

        /// <summary>
        /// Provides an <see cref="AppTheme"/> collection for testing <see cref="RadioGroup"/>.
        /// </summary>
        public ReadOnlyObservableCollection<AppTheme> Themes
        {
            get;
        }

        public AppTheme PreferredTheme
        {
            get => _preferredTheme;
            set
            {
                if (SetProperty(ref _preferredTheme, value, PreferredThemeChangedEventArgs))
                {
                    SetTheme(value);
                    foreach (ThemeItem theme in _themeItems)
                    {
                        theme.IsSelected = theme.Theme == _preferredTheme;
                    }
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Updates the application's active theme.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        /// <remarks>
        /// This method is called when <see cref="PreferredTheme"/> is updated
        /// or when the system's them changes.
        /// </remarks>
        public void SetTheme(AppTheme theme)
        {
            App.Trace(nameof(MainViewModel), nameof(SetTheme), theme);

            if (theme == AppTheme.Unspecified)
            {
                theme = PreferredTheme;
                // if the preferred theme is 'use system'
                if (theme == AppTheme.Unspecified)
                {
                    theme = App.SystemTheme;
                }
            }

            if (theme != _activeTheme)
            {
                _activeTheme = theme;
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add
                (   
                    theme == AppTheme.Light ? LightTheme : DarkTheme
                );
            }
        }

        private void OnSystemThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            // If the user wants to use the system theme...
            if (PreferredTheme == AppTheme.Unspecified)
            {
                // Update the application's them to the system theme.
                SetTheme(App.SystemTheme);
            }
            // otherwise, ignore this.
        }

        void OnThemeSelected(ThemeItem item)
        {
            if (item.IsSelected)
            {
                PreferredTheme = item.Theme;
            }
        }

        #endregion Methods

        static readonly PropertyChangedEventArgs PreferredThemeChangedEventArgs = new(nameof(PreferredTheme));
    }
}
