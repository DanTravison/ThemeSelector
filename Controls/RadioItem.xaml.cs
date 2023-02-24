using System.Windows.Input;

namespace ThemeSelector.Controls;

public partial class RadioItem : ContentView
{
	public RadioItem()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Gets or sets the command to execute when the control is tapped.
    /// </summary>
    public ICommand Command
    {
        get => GetValue(CommandProperty) as ICommand;
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BindableProperty"/> for the <see cref="IsChecked"/>.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create
    (
        nameof(Command),
        typeof(ICommand),
        typeof(RadioItem),
        null,
        BindingMode.TwoWay
     );

    /// <summary>
    /// Gets or sets the selected state of the control.
    /// </summary>
    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BindableProperty"/> for the <see cref="IsChecked"/>.
    /// </summary>
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create
    (
        nameof(IsChecked),
        typeof(bool),
        typeof(RadioItem),
        (bool)false,
        BindingMode.TwoWay,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            ((RadioItem)bindableObject).OnCheckedChanged();
        }
     );

    protected virtual void OnCheckedChanged()
    {
        App.Trace(this, nameof(IsChecked), IsChecked);
    }

    /// <summary>
    /// Gets or sets the color to use to fill the drawn area.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BindableProperty"/> for the <see cref="Text"/>.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create
    (
        nameof(Text),
        typeof(string),
        typeof(RadioItem),
        string.Empty,
        BindingMode.OneWay
    );


    /// <summary>
    /// Gets or sets the color to use to fill the drawn area.
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
        typeof(RadioItem),
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
        typeof(RadioItem),
        Colors.Gray,
        BindingMode.OneWay
    );

    private void OnTapped(object sender, TappedEventArgs e)
    {
        if (IsEnabled)
        {
            IsChecked = true;
            Command?.Execute(this);
        }
    }
}