using System.Windows.Input;

namespace ThemeSelector.Controls;

public partial class RadioItem : TemplatedView
{
    RadioCheck _icon;
	
    public RadioItem()
	{
		InitializeComponent();
        _icon = GetTemplateChild("Icon") as RadioCheck;
	}

    RadioCheck Check
    {
        get
        {
            _icon ??= GetTemplateChild("Icon") as RadioCheck;
            return _icon;
        }
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }

    /// <summary>
    /// Gets or sets the command to execute when the control is tapped.
    /// </summary>
    public DataTemplate ItemTemplate
    {
        get => GetValue(ItemTemplateProperty) as DataTemplate;
        set => SetValue(ItemTemplateProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BindableProperty"/> for the <see cref="IsChecked"/>.
    /// </summary>
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create
    (
        nameof(ItemTemplate),
        typeof(DataTemplate),
        typeof(RadioItem),
        null,
        BindingMode.TwoWay,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            ((RadioItem)bindableObject).OnItemTemplateChanged();
        }
     );

    void OnItemTemplateChanged()
    {
        if (ItemTemplate != null)
        {
            object value = ItemTemplate.CreateContent();
            if (value is View view)
            {
                ContentPresenter presenter = GetTemplateChild("Presenter") as ContentPresenter;
                presenter.Content = view;
                view.BindingContext = this;
            }
            else
            {
                App.Trace
                (
                    this, 
                    nameof(ItemTemplate), 
                    "ItemTemplate.CreateContent returned a {0} instead of a View for {1}", 
                    value.GetType().FullName,
                    Text
                );
            }
        }
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
        App.Trace(this, nameof(IsChecked), "{0} {1}", Value, IsChecked);
        UpdateCheckColor();
    }
 
    /// <summary>
    /// Gets or sets the color to use to fill the drawn area.
    /// </summary>
    public object Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BindableProperty"/> for the <see cref="Value"/>.
    /// </summary>
    public static readonly BindableProperty ValueProperty = BindableProperty.Create
    (
        nameof(Value),
        typeof(object),
        typeof(RadioItem),
        null,
        BindingMode.OneWay
    );


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
        typeof(RadioItem),
        Colors.White,
        BindingMode.OneWay,
        propertyChanging: (bindableObject, oldValue, newValue) =>
        {
            RadioItem item = (RadioItem)bindableObject;
            App.Trace(item, nameof(CheckedColor), "{0} {1}", item.Value, ((Color)newValue).Name());
        },
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (bindableObject is RadioItem item)
            {
                App.Trace(item, nameof(CheckedColor), "{0} {1}", item.Value, ((Color)newValue).Name());
                item.UpdateCheckColor();
            }
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
        typeof(RadioItem),
        Colors.White,
        BindingMode.OneWay,
        propertyChanging: (bindableObject, oldValue, newValue) =>
        {
            RadioItem item = (RadioItem)bindableObject;
            App.Trace(item, nameof(UncheckedColor), "{0} {1}", item.Value, ((Color)newValue).Name());
        },
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (bindableObject is RadioItem item)
            {
                App.Trace(item, nameof(CheckedColor), "{0} {1}", item.Value, ((Color)newValue).Name());
                item.UpdateCheckColor();
            }
        }
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
        BindingMode.OneWay,
        propertyChanging: (bindableObject, oldValue, newValue) =>
        {
            RadioItem item = (RadioItem)bindableObject;
            App.Trace(item, nameof(TextColor), "{0} {1}", item.Value, ((Color)newValue).Name());
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
        typeof(RadioItem),
        Colors.Gray,
        BindingMode.OneWay,
        propertyChanging: (bindableObject, oldValue, newValue) =>
        {
            RadioItem item = (RadioItem)bindableObject;
            App.Trace(item, nameof(DisabledTextColor), "{0} {1}", item.Value, ((Color)newValue).Name());
        }
    );

    #region Methods

    private void UpdateCheckColor()
    {
        if (Check != null)
        {
            if (IsChecked)
            {
                Check.StrokeColor = CheckedColor;
            }
            else
            {
                Check.StrokeColor = UncheckedColor;
            }
        }
    }

    private void OnTapped(object sender, TappedEventArgs e)
    {
        if (IsEnabled)
        {
            IsChecked = true;
            Command?.Execute(this);
            object child = GetTemplateChild("Icon");
            if (child is RadioCheck circle)
            {
                circle.Invalidate();
            }
        }
    }

    #endregion Methods
}