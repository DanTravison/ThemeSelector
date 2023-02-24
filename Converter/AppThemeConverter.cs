using System.Globalization;

namespace ThemeSelector.Converter
{
    public sealed class AppThemeConverter : IValueConverter
    {
        public static readonly AppThemeConverter Converter = new AppThemeConverter();

        public AppThemeConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string) && value is AppTheme theme)
            {
                return Enum.GetName<AppTheme>(theme);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AppTheme theme = AppTheme.Unspecified;
            if (value is string stringValue)
            {
                if (!Enum.TryParse(stringValue, out theme))
                {
                    theme = AppTheme.Unspecified;
                }
            }
            return theme;
        }
    }
}
