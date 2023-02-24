using System.ComponentModel;

namespace ThemeSelector.Controls.Model
{
    public sealed class RadioItemModel : ObservableObject
    {
        readonly object _value;
        string _text;
        bool _isChecked;
        bool _isEnabled;
        TypeConverter _converter;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="value">The <see cref="Value"/> value.</param>
        /// <param name="isEnabled">The <see cref="IsEnabled"/> value.</param>
        /// <param name="isChecked">The <see cref="IsChecked"/> value.</param>
        internal RadioItemModel(object value, bool isEnabled = true, bool isChecked = false)
        {
            _value = value;
            _isChecked = isChecked;
            _isEnabled = isEnabled;
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="value">The <see cref="Value"/> value.</param>
        /// <param name="isEnabled">The <see cref="IsEnabled"/> value.</param>
        /// <param name="isChecked">The <see cref="IsChecked"/> value.</param>
        /// <param name="converter">The <see cref="Converter"/> value.</param>
        internal RadioItemModel(object value, TypeConverter converter, bool isEnabled = true, bool isChecked = false)
        {
            _value = value;
            _isChecked = isChecked;
            _isEnabled = isEnabled;
            _converter = converter;
        }

        internal object Parent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the value for this instance.
        /// </summary>
        public object Value
        {
            get => _value;
        }

        /// <summary>
        /// Gets the value indicating if the theme is selected.
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value, IsCheckedChangedEventArgs);
        }

        /// <summary>
        /// Gets the value indicating if the theme is selected.
        /// </summary>
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                SetProperty(ref _isEnabled, value, IsEnabledChangedEventArgs);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="TypeConverter"/> to use to convert <see cref="Value"/> a string.
        /// </summary>
        public TypeConverter Converter
        {
            get => _converter;
            internal set
            {
                if (SetProperty(ref _converter, value, ConverterChangedEventArgs))
                {
                    OnPropertyChanged(TextChangedEventArgs);
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="Value"/> as a string.
        /// </summary>
        public string Text
        {
            get
            {
                if (_text == null)
                {
                    if (_value == null)
                    {
                        _text = string.Empty;
                    }
                    else if (_converter != null)
                    {
                        _text = (string)_converter.ConvertTo(_value, typeof(string));
                    }
                    else
                    {
                        _text = _value.ToString();
                    }
                }
                return _text;
            }
        }

        /// <summary>
        /// The <see cref="PropertyChangedEventArgs"/> passed to <see cref="INotifyPropertyChanged.PropertyChanged"/> when <see cref="IsChecked"/> changes.
        /// </summary>
        public static readonly PropertyChangedEventArgs IsCheckedChangedEventArgs = new(nameof(IsChecked));

        /// <summary>
        /// The <see cref="PropertyChangedEventArgs"/> passed to <see cref="INotifyPropertyChanged.PropertyChanged"/> when <see cref="IsEnabled"/> changes.
        /// </summary>
        public static readonly PropertyChangedEventArgs IsEnabledChangedEventArgs = new(nameof(IsEnabled));

        /// <summary>
        /// The <see cref="PropertyChangedEventArgs"/> passed to <see cref="INotifyPropertyChanged.PropertyChanged"/> when <see cref="Converter"/> changes.
        /// </summary>
        public static readonly PropertyChangedEventArgs ConverterChangedEventArgs = new(nameof(Converter));

        /// <summary>
        /// The <see cref="PropertyChangedEventArgs"/> passed to <see cref="INotifyPropertyChanged.PropertyChanged"/> when <see cref="Text"/> changes.
        /// </summary>
        public static readonly PropertyChangedEventArgs TextChangedEventArgs = new(nameof(Text));
    }
}
