using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ThemeSelector.Controls.Model
{
    /// <summary>
    /// Provides a view model for <see cref="RadioGroup"/>.
    /// </summary>
    public sealed class RadioGroupModel : ObservableObject
    {
        #region Fields

        RadioItem _selectedItem;
        IEnumerable _itemsSource;
        TypeConverter _converter;
        readonly ObservableCollection<RadioItem> _items = new();

        #endregion Fields

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public RadioGroupModel()
        {
            Items = new(_items);
            _items.CollectionChanged += OnItemsCollectionChanged;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the item source.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get => _itemsSource;
            set
            {
                App.Trace(this, nameof(ItemsSource), value == null ? "[NULL]" : value.GetType().Name);
                if (!ReferenceEquals(_itemsSource, value))
                {
                    _itemsSource = value;
                    PopulateItems();
                    OnPropertyChanged(ItemsSourceChangedEventArgs);
                }
            }
        }

        /// <summary>
        /// Gets the selected value.
        /// </summary>
        /// <value>
        /// The selected value; otherwise, a null reference if no item is selected.
        /// </value>
        public object SelectedValue
        {
            get => _selectedItem?.Value;
            set
            {
                App.Trace(this, nameof(SelectedValue), value);
                int index = IndexOf(value);
                if (index != -1)
                {
                    RadioItem item = _items[index];
                    item.IsSelected = true;
                    UpdateSelected(item);
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="TypeConverter"/> to use to convert ItemsSource objects to text.
        /// </summary>
        public TypeConverter Converter
        {
            get => _converter;
            set
            {
                if (!ReferenceEquals(_converter, value))
                {
                    App.Trace(this, nameof(Converter), value == null ? "[NULL]" : value.GetType().Name);
                    _converter = value;
                    foreach (RadioItem item in _items)
                    {
                        item.Converter = _converter;
                    }
                    OnPropertyChanged(ConverterChangedEventArgs);
                }
            }
        }

        /// <summary>
        /// Gets the ReadOnlyObservableCollection{RadioItem} collection of items.
        /// </summary>
        public ReadOnlyObservableCollection<RadioItem> Items
        {
            get;
        }

        #endregion Properties

        /// <summary>
        /// Gets the index of the specified value.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <returns>The index of the specified <paramref name="value"/>; otherwise, 
        /// -1 if the value is not present.</returns>
        /// <remarks>
        /// if the value is present multiple times, the index of the first value is returned.
        /// </remarks>
        public int IndexOf(object value)
        {
            int result = -1;
            for (int x = 0; x < _items.Count; x++)
            {
                if (_items[x].Value.Equals(value))
                {
                    result = x;
                    break;
                }
            }

            return result;
        }

        #region Private Methods

        /// <summary>
        /// Populates the <see cref="Items"/> collection when <see cref="ItemsSource"/> changes.
        /// </summary>
        void PopulateItems()
        {
            _items.Clear();
            if (_itemsSource != null)
            {
                foreach (object value in _itemsSource)
                {
                    _items.Add(new(value, Converter));
                }
                App.Trace(this, nameof(PopulateItems), _items.Count);
            }
        }

        /// <summary>
        /// Updates the <see cref="RadioItem.IsSelected"/> property
        /// when <see cref="SelectedValue"/> changes.
        /// </summary>
        /// <param name="target"></param>
        void UpdateSelected(RadioItem target)
        {
            App.Trace(this, nameof(UpdateSelected), target?.Value);
            _selectedItem = target;
            foreach (RadioItem item in Items)
            {
                if (!object.ReferenceEquals(item, target))
                {
                    item.IsSelected = false;
                }
            }
            OnPropertyChanged(SelectedValueChangedEventArgs);
        }

        #endregion Private Methods

        #region Event Handlers

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ReferenceEquals(e, RadioItem.IsSelectedChangedEventArgs))
            {
                RadioItem item = (RadioItem)sender;
                if (item.IsSelected)
                {
                    UpdateSelected(item);
                }
            }
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Added((RadioItem)e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Removed((RadioItem)e.OldItems[0]);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (RadioItem item in Items)
                    {
                        Removed(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Added((RadioItem)e.NewItems[0]);
                    Removed((RadioItem)e.OldItems[0]);
                    break;
            }
        }

        void Added(RadioItem item)
        {
            item.PropertyChanged += OnItemPropertyChanged;
            item.Parent = this;
        }

        void Removed(RadioItem item)
        {
            item.PropertyChanged -= OnItemPropertyChanged;
            item.Parent = null;
            if (object.ReferenceEquals(_selectedItem, item))
            {
                _selectedItem = null;
                OnPropertyChanged(SelectedValueChangedEventArgs);
            }
        }

        #endregion Event Handlers

        #region Cached PropertyChangedEventArgs

        /// <summary>
        /// The <see cref="PropertyChangedEventArgs"/> passed to <see cref="INotifyPropertyChanged.PropertyChanged"/> when <see cref="SelectedValue"/> changes.
        /// </summary>
        public static readonly PropertyChangedEventArgs SelectedValueChangedEventArgs = new(nameof(SelectedValue));

        /// <summary>
        /// The <see cref="PropertyChangedEventArgs"/> passed to <see cref="INotifyPropertyChanged.PropertyChanged"/> when <see cref="ItemsSource"/> changes.
        /// </summary>
        public static readonly PropertyChangedEventArgs ItemsSourceChangedEventArgs = new(nameof(ItemsSource));

        /// <summary>
        /// The <see cref="PropertyChangedEventArgs"/> passed to <see cref="INotifyPropertyChanged.PropertyChanged"/> when <see cref="Converter"/> changes.
        /// </summary>
        public static readonly PropertyChangedEventArgs ConverterChangedEventArgs = new(nameof(Converter));

        #endregion Cached PropertyChangedEventArgs
    }
}
