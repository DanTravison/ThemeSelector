using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ThemeSelector
{
    /// <summary>
    /// Provides an abstract base class for classes supporting INotifyPropertyChanged
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        protected ObservableObject()
        {
        }

        #region INotifyPropertyChanged

        /// <summary>
        /// Occurs when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event with a cached <see cref="PropertyChangedEventArgs"/>.
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> for the event.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            ArgumentNullException.ThrowIfNull(e);
            PropertyChanged?.Invoke(this, e);
        }

        #endregion INotifyPropertyChanged

        #region SetProperty

        protected bool SetProperty(ref object field, object newValue, PropertyChangedEventArgs e)
        {
            if (!object.ReferenceEquals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(e);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Provides a <see cref="INotifyPropertyChanged"/> event with a cached <see cref="PropertyChangedEventArgs"/>.
        /// </summary>
        /// <typeparam name="T">The type of the property that changed.</typeparam>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> identifying the event.</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T field, T newValue, PropertyChangedEventArgs e)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }
            field = newValue;
            OnPropertyChanged(e);
            return true;
        }

        #endregion SetProperty
    }
}
