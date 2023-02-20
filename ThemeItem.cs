using System.ComponentModel;

namespace ThemeSelector
{
    public sealed class ThemeItem : ObservableObject
    {
        bool _isSelected;
        readonly Action<ThemeItem> _action;

        public ThemeItem(string text, Action<ThemeItem> action, AppTheme value, bool isSelected = false)
        {
            Theme = value;
            Text = text;
            _action = action;
            _isSelected = isSelected;
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

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (SetProperty(ref _isSelected, value, IsSelectedChangedEventArgs))
                {
                    _action.Invoke(this);
                }
            }
        }

        #endregion Properties
        
        static readonly PropertyChangedEventArgs IsSelectedChangedEventArgs = new(nameof(IsSelected));
    }
}
