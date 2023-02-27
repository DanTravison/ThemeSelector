using System.ComponentModel;

namespace ThemeSelector
{
    public sealed class ThemeItem : ObservableObject
    {
        bool? _isChecked;
        readonly Action<ThemeItem> _action;

        public ThemeItem(string text, Action<ThemeItem> action, AppTheme value)
        {
            Theme = value;
            Text = text;
            _action = action;
        }

        #region Properties

        public AppTheme Theme
        {
            get;
        }

        public string Text
        {
            get;
        }

        public bool IsChecked
        {
            get => _isChecked == true;
            set
            {
                if (SetProperty(ref _isChecked, value, IsSelectedChangedEventArgs))
                {
                    _action.Invoke(this);
                }
            }
        }

        #endregion Properties
        
        static readonly PropertyChangedEventArgs IsSelectedChangedEventArgs = new(nameof(IsChecked));
    }
}
