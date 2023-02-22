using ThemeSelector.Controls.Model;

namespace ThemeSelector.Controls;

public partial class RadioItemView : ContentView
{
	public RadioItemView()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Gets or sets the color to use to draw the items Selected image.
    /// </summary>
    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BindableProperty"/> for the <see cref="SelectedColor"/>.
    /// </summary>
    public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create
    (
        nameof(SelectedColor),
        typeof(Color),
        typeof(RadioItemView),
        Colors.White,
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
        typeof(RadioItemView),
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

    private void OnTapped(object sender, TappedEventArgs e)
    {
        if (IsEnabled)
        {
            if (BindingContext is RadioItem item)
            {
                item.IsSelected = true;
            }
        }
    }
}