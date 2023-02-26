using System.Collections;
using System.ComponentModel;
using ThemeSelector.Controls.Model;

namespace ThemeSelector.Controls;

public partial class RadioGroup : ContentView
{
    /// <summary>
    /// Defines the resource key value of the default <see cref="RadioItem.ItemTemplate"/>.
    /// </summary>
    public const string RadioItemContentTemplate = "RadioItemContentTemplate";

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

    protected override void OnChildAdded(Element child)
    {
        if (ItemTemplate == null)
        {
            this.Resources.TryGetValue(RadioItemContentTemplate, out object template);
            ItemTemplate = (DataTemplate)template;
        }
        base.OnChildAdded(child);
    }

    RadioGroupModel Model
    {
        get;
    }

    /// <summary>
    /// Gets or sets <see cref="DataTemplate"/> for <see cref="RadioItem"/> content.
    /// </summary>
    public DataTemplate ItemTemplate
    {
        get => GetValue(ItemTemplateProperty) as DataTemplate;
        set => SetValue(ItemTemplateProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BindableProperty"/> for the <see cref="ItemTemplate"/>.
    /// </summary>
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create
    (
        nameof(ItemTemplate),
        typeof(DataTemplate),
        typeof(RadioItem),
        null,
        BindingMode.OneWay
    );

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
        propertyChanging: (bindableObject, oldValue, newValue) =>
        {
            App.Trace(bindableObject, nameof(CheckedColor), ((Color)newValue).Name());
        }
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
        propertyChanging: (bindableObject, oldValue, newValue) =>
        {
            App.Trace(bindableObject, nameof(UncheckedColor), ((Color)newValue).Name());
        }
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
        propertyChanging: (bindableObject, oldValue, newValue) =>
        {
            App.Trace(bindableObject, nameof(TextColor), ((Color)newValue).Name());
        }
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
        propertyChanging: (bindableObject, oldValue, newValue) =>
        {
            App.Trace(bindableObject, nameof(DisabledTextColor), newValue);
        }
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
        propertyChanging: (bindableObject, oldValue, newValue) =>
        {
            App.Trace(bindableObject, nameof(SelectedValue), newValue);
        },
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (bindableObject is RadioGroup control)
            {
                control.Model.SelectedValue = newValue;
            }
        }
    );
}