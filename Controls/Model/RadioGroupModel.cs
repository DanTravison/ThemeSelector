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

        RadioItemModel _selectedItem;
        IEnumerable _itemsSource;
        readonly ObservableCollection<RadioItemModel> _items = new();

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
                    RadioItemModel item = _items[index];
                    item.IsChecked = true;
                    UpdateSelectedItem(item);
                }
            }
        }

        /// <summary>
        /// Gets the ReadOnlyObservableCollection{RadioItem} collection of items.
        /// </summary>
        public ReadOnlyObservableCollection<RadioItemModel> Items
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
                    _items.Add(new(value));
                }
                App.Trace(this, nameof(PopulateItems), _items.Count);
            }
        }

        /// <summary>
        /// Updates the <see cref="RadioItemModel.IsChecked"/> property
        /// when <see cref="SelectedValue"/> changes.
        /// </summary>
        /// <param name="target"></param>
        void UpdateSelectedItem(RadioItemModel target)
        {
            App.Trace(this, nameof(UpdateSelectedItem), target?.Value);
            _selectedItem = target;
            foreach (RadioItemModel item in Items)
            {
                if (!object.ReferenceEquals(item, target))
                {
                    item.IsChecked = false;
                }
            }
            OnPropertyChanged(SelectedValueChangedEventArgs);
        }

        #endregion Private Methods

        #region Event Handlers

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ReferenceEquals(e, RadioItemModel.IsCheckedChangedEventArgs))
            {
                RadioItemModel item = (RadioItemModel)sender;
                if (item.IsChecked)
                {
                    UpdateSelectedItem(item);
                }
            }
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Added((RadioItemModel)e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Removed((RadioItemModel)e.OldItems[0]);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (RadioItemModel item in Items)
                    {
                        Removed(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Added((RadioItemModel)e.NewItems[0]);
                    Removed((RadioItemModel)e.OldItems[0]);
                    break;
            }
        }

        void Added(RadioItemModel item)
        {
            item.PropertyChanged += OnItemPropertyChanged;
        }

        void Removed(RadioItemModel item)
        {
            item.PropertyChanged -= OnItemPropertyChanged;
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

        #endregion Cached PropertyChangedEventArgs
    }
}
