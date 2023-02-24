# Theme Selector

Provides a workaround to address the Maui RequestedThemeChanged problem on android.

See https://github.com/dotnet/maui/issues/8236: Application.Current.RequestedThemeChanged event only raises once

The workaround involves an android-specific class, ThemeInfo, that reads the underlying 
Configuration.UiMode theme value and a CheckTheme() method to periodically 
check to see if the theme has changed and raising an RequestedThemChanged event.
In the example, ThemeInfo.CheckTheme() is by MainActivity.cs.

Note MauiAppCompatActivity.OnConfigurationChanged is not getting called so I could not use that as polling model.

# Classes

* Platforms/Android/ThemeInfo.cs

    The Theme property reads the theme from the native Configuration.UiMode.
    CheckTheme() determines if the native theme has changed.
       If not, it raises it's RequestedThemeChanged event.

* Platforms/Android/MainActivity.cs

    Android SDK 30 or greater

    Overrides OnConfigurationChanged and calls ThemeInfo.CheckThem.

    Android SDK < 30

    Overrides OnWindowFocusChanged and calls ThemeInfo.CheckTheme() 
    when hasFocus = true. This is not ideal since the application 
    doesn't see the theme change until it next receives focus; however, 
    it avoids polling the system theme.

* App.cs

    Provides a mirror to the RequestedTheme property via SystemTheme and 
    the RequestedThemeChanged event via SystemThemeChanged. It uses 
    Platforms/Android/ThemeInfo. on Android, and Application.Current.RequestedTheme
    on all other platforms.

* MainViewModel.cs

    Illustrates using the mirror APIs to adjust the application's theme.

* MainPage.xaml

    Provides a set of radio buttons to test switching between Dark, Light,
    and System (AppTheme.Unspecified) themes.

# SDK Note:
At the time of this writing, the Maui application template targets Android 5/21.
This impacts the application in two ways:

1: OnWindowFocusChanged(true) is used to poll for theme changes. Using SDK 30
solves this.

2: RadioButton theme support does not render correctly when using DynamicResource
to set text and background colors.  SDK 30 definitely solves this but earlier targets
may as well.

# RadioButton and custom Themes:

I'm finding RadioButton to be problematic when used with themes as illustrated in
below issues. 

The MainPage displays two sets of RadioButtons. 

1: The first group uses the MAUI RadioButton defines a style to set the RadioButton's 
TextColor to my theme color.

2: The second group uses the MAUI RadioButton with a control template.

3: The third group uses a custom RadioGroup. This ties the radio ellipse color
to the TextColor.  IsChecked is indicated by drawing a thicker ellipse.

3: The fourth group uses a custom RadioGroup with an ItemTemplate.

When the application follows the system theme (AppTheme.Unspecialized), the RadioButton displays 
reasonable colors when the system switches between light and dark.  When an explicit them
is selected, parts of the Maui RadioButton do not change.

This suggests there are color settings that the RadioButton uses that are not defined in my 
custom themes under Resources\Themes.

See https://github.com/dotnet/maui/issues/11709
 - Previously shown RadioButton with VisualState Manager becomes erratic after AppTheme is changed by user

See https://github.com/dotnet/maui/issues/11747
 - iOS Radio Button displays with same black coloring in Dark Mode
