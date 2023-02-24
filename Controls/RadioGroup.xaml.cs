using System.Collections;
using System.ComponentModel;
using ThemeSelector.Controls.Model;

namespace ThemeSelector.Controls;

public partial class RadioGroup : ContentView
{
    public RadioGroup()
    {
        Model = new RadioGroupModel();
        Model.PropertyChanged += OnModelPropertyChanged;
        InitializeComponent();
        Items.BindingContext = Model;
    }

    private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (object.ReferenceEquals(e, RadioGroupModel.SelectedValueChangedEventArgs))
        {
            SelectedValue = Model.SelectedValue;
        }
    }

    RadioGroupModel Model
    {
        get;
    }

    /// <summary>
    /// Gets or sets the color to use to draw the checked image.
    /// </summary>
    public Color CheckedColor
    {
        get => (Color)GetValue(CheckedColorProperty);
        set => SetValue(CheckedColorProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BindableProperty"/> for the <see cref="CheckedColor"/>.
    /// </summary>
    public static readonly BindableProperty CheckedColorProperty = BindableProperty.Create
    (
        nameof(CheckedColor),
        typeof(Color),
        typeof(RadioGroup),
        Colors.White,
        BindingMode.OneWay
    );

    /// <summary>
    /// Gets or sets the color to use to draw the unchecked image.
    /// </summary>
    public Color UncheckedColor
    {
        get => (Color)GetValue(UncheckedColorProperty);
        set => SetValue(UncheckedColorProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BindableProperty"/> for the <see cref="UncheckedColor"/>.
    /// </summary>
    public static readonly BindableProperty UncheckedColorProperty = BindableProperty.Create
    (
        nameof(UncheckedColor),
        typeof(Color),
        typeof(RadioGroup),
        Colors.White,
        BindingMode.OneWay
    );

    /// <summary>
    /// Gets or sets the color to draw text.
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BindableProperty"/> for the <see cref="TextColor"/>.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create
    (
        nameof(TextColor),
        typeof(Color),
        typeof(RadioGroup),
        Colors.White,
        BindingMode.OneWay
    );

    /// <summary>
    /// Gets or sets the color to draw disabled text.
    /// </summary>
    public Color DisabledTextColor
    {
        get => (Color)GetValue(DisabledTextColorProperty);
        set => SetValue(DisabledTextColorProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BindableProperty"/> for the <see cref="DisabledTextColor"/>.
    /// </summary>
    public static readonly BindableProperty DisabledTextColorProperty = BindableProperty.Create
    (
        nameof(DisabledTextColor),
        typeof(Color),
        typeof(RadioGroup),
        Colors.Gray,
        BindingMode.OneWay
    );

    /// <summary>
    /// Gets or sets the <see cref="IEnumerable"/> to use to populate the control.
    /// </summary>
    public IEnumerable ItemsSource
    {
        get => GetValue(ItemsSourceProperty) as IEnumerable;
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Provides the backing store for the <see cref="ItemsSource"/> property.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create
    (
        nameof(ItemsSource),
        typeof(IEnumerable),
        typeof(RadioGroup),
        null,
        BindingMode.OneWay,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (bindableObject is RadioGroup control)
            {
                control.Model.ItemsSource = newValue as IEnumerable;
            }
        }
    );

    /// <summary>
    /// Provides a <see cref="TypeConverter"/> for converting values in the 
    /// <see cref="ItemsSource"/> to text.
    /// </summary>
    public TypeConverter TextConverter
    {
        get => GetValue(TextConverterProperty) as TypeConverter;
        set => SetValue(TextConverterProperty, value);
    }

    /// <summary>
    /// Provides the backing store for the <see cref="TextConverter"/> property.
    /// </summary>
    public static readonly BindableProperty TextConverterProperty = BindableProperty.Create
    (
        nameof(TextConverter),
        typeof(TypeConverter),
        typeof(RadioGroup),
        null,
        BindingMode.OneWay,
        validateValue: (bindableObject, value) =>
        {
            if (value is TypeConverter converter)
            {
                return converter.CanConvertTo(typeof(string));
            }
            return true;
        },
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (bindableObject is RadioGroup control)
            {
                control.Model.Converter = newValue as TypeConverter;
            }
        }
    );

    /// <summary>
    /// Gets or sets the <see cref="RadioItemModel"/> to use to populate the control.
    /// </summary>
    public object SelectedValue
    {
        get => GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }

    /// <summary>
    /// Provides the backing store for the <see cref="SelectedValue"/> property.
    /// </summary>
    public static readonly BindableProperty SelectedValueProperty = BindableProperty.Create
    (
        nameof(SelectedValue),
        typeof(object),
        typeof(RadioGroup),
        null,
        BindingMode.TwoWay,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (bindableObject is RadioGroup control)
            {
                control.Model.SelectedValue = newValue;
            }
        }
    );
}