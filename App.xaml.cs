using System.Globalization;

namespace ThemeSelector;

public partial class App : Application
{
    public App()
	{
        InitializeComponent();
        BindingContext = Model = new MainViewModel();

        MainPage = new NavigationPage(new MainPage()
        {
            BindingContext = BindingContext
        });

        Model.SetTheme(Model.PreferredTheme);
        SystemTheme.RequestedThemeChanged += OnRequestedThemeChanged;
    }

#if WINDOWS
    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);
        window.Width = 800;
        window.Height = 1100;
        return window;
    }
#endif

    MainViewModel Model
    {
        get;
        set;
    }

    private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
    {
        App.Trace(this, nameof(OnRequestedThemeChanged), e.RequestedTheme);
        // If the user wants to use the system theme...
        if (Model.PreferredTheme == AppTheme.Unspecified)
        {
            // Update the application's them to the system theme.
            Model.SetTheme(e.RequestedTheme);
        }
        // otherwise, ignore this.
    }

    #region Tracing

    public static void Trace(object source, string memberName, object value)
    {
        Trace(source, memberName, "{0}", value);
    }

    public static void Trace(string sourceName, string memberName, object value)
    {
        Trace(sourceName, memberName, value is string message ? message : "{0}", value ?? "[NULL]");
    }

    public static void Trace(object source, string memberName, string format, params object[] args)
    {
        Trace(source.GetType().Name, memberName, format, args);
    }

    public static void Trace(string sourceName, string memberName, string format, params object[] args)
    {
        string message;
        if (format != null && args.Length > 0)
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
            message = format ?? string.Empty;
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
