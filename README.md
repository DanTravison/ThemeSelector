# Theme Selector

Provides a workaround to address the Maui RequestedThemeChanged problem on android.

See https://github.com/dotnet/maui/issues/8236: Application.Current.RequestedThemeChanged event only raises once

The workaround involves an android-specific class, ThemeInfo, that reads the underlying 
Configuration.UiMode theme value and a CheckTheme() method to periodically 
check to see if the theme has changed and raising an RequestedThemChanged event.
In the example, CheckTheme is called when the application receives focus versus
using a recurring polling model.  

Note MauiAppCompatActivity.OnConfigurationChanged is not getting called so I could not use that as polling model.

# Classes

* Platforms/Android/ThemeInfo.cs

    The Theme property reads the theme from the native Configuration.UiMode.
    CheckTheme() determines if the native theme matches the cached theme value.
       If not, it raises it's RequestedThemeChanged event.

* Platforms/Android/MainActivity.cs

    Overrides OnWindowFocusChanged and calls ThemeInfo.CheckTheme() when hasFocus = true.
    This logic is not ideal since the application doesn't see the theme
    change until it next receives focus; however, it avoids polling the system theme.

* App.cs

    Provides a mirror to the RequestedTheme property via SystemTheme and 
    the RequestedThemeChanged event via SystemThemeChanged. It uses 
    Platforms/Android/ThemeInfo on Android, and Application.Current.RequestedTheme
    on all other platforms.

* MainViewModel.cs

    Illustrates using the mirror APIs to adjust the application's theme.

* MainPage.xaml

    Provides a set of radio buttons to test switching between Dark, Light,
    and System (AppTheme.Unspecified).

# SDK Note: 
The application was originally written using the Maui default application template
which defaults Android builds to Android 5/21. However, with that SDK, RadioButton
does not render as expected when using DynamicResource for text and background colors.
Switching to Android 11/API 30 solves this issue; however, I could not determine how
to adjust the color of the button itself.

# Future:

I'm finding RadioButton to be problematic when used with themes as illustrated in
below issues. I'm working on a pur Maui alternative, that while not as rich in 
capabilities, does provide consistent visuals across themes. 

See https://github.com/dotnet/maui/issues/11709
 - Previously shown RadioButton with VisualState Manager becomes erratic after AppTheme is changed by user

See https://github.com/dotnet/maui/issues/11747
 - iOS Radio Button displays with same black coloring in Dark Mode

